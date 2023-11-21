using AutoMapper;
using LibraryManagement.Database;
using LibraryManagement.DTOs.RequestDTOs;
using LibraryManagement.DTOs.ResponseDTOs;
using LibraryManagement.EmailUtilities.Helper;
using LibraryManagement.EmailUtilities.Service;
using LibraryManagement.Models;
using LibraryManagement.Templates;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Xunit.Sdk;

namespace LibraryManagement.Services.ServiceImpl
{
    public class UserServiceImpl : UserService
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IEmailSender _email;

        public UserServiceImpl(LibraryDbContext dbContext,IMapper mapper ,IConfiguration config,IEmailSender email)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _config = config;
            _email = email;
        }

        public async Task DeleteUser(Guid userId)
        {
            var user = await _dbContext.Users
                                     .Where(b => b.Id == userId && b.IsDeleted == false)
                                     .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new Exception($"user with Id: {userId} does not exist.");
            }

            user.IsDeleted = true;
            user.ModifiedOn = DateTime.Now;

            await _dbContext.SaveChangesAsync();
        }



        public async Task<IEnumerable<UserResponseDto>> GetAllUsers()
        {
            var users = await _dbContext.Users
                      .Where(b => b.IsDeleted == false)
                      .ToListAsync();

            if (!(users.Any())) {
                throw new Exception("No user exists.");
            }
                          
            var usersResponse = _mapper.Map<IList<UserResponseDto>>(users);
            return usersResponse;
        }



        public async Task<UserResponseDto> GetUserById(Guid userId)
        {
            var user = await _dbContext.Users
                                       .Where(b => b.Id == userId && b.IsDeleted == false)
                                       .FirstOrDefaultAsync();

            if (user == null) {
                throw new Exception($"user with Id: {userId} does not exist.");
            }

            var userResponse = _mapper.Map<UserResponseDto>(user);
            return userResponse;
        }



        public async Task<UserLoginResponseDto> LoginUser(UserLogInDto request)
        {
            if(request == null)
            {
                throw new Exception("Something went wromg!!");
            }

           var user = await _dbContext.Users.Where(b => b.Email.ToLower().Equals(request.Email.ToLower())).FirstOrDefaultAsync();

            if (user == null) {
                throw new Exception("No user found!!");
            }

            if (BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash) == false) {
                throw new Exception("Wrong Password!!");
            }

            var claims = new List<Claim>() {
               new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
               new Claim(ClaimTypes.Name, user.Username),
               new Claim(ClaimTypes.Email,user.Email),
               new Claim(ClaimTypes.Role,user.Role)
            };
            var signingKey = _config.GetSection("jwt").GetSection("SigningKey").Value!;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                       issuer : _config.GetSection("jwt : Issuer").Value,
                       audience : _config.GetSection("jwt : Audience").Value,
                       claims : claims.ToArray(),
                       expires : DateTime.Now.AddMinutes(60),
                       signingCredentials : creds
                );

            var loginResponse = _mapper.Map<UserLoginResponseDto>(user);
            loginResponse.Token = new JwtSecurityTokenHandler().WriteToken(token);

            return loginResponse;
            
        }

       
        
        
        public async Task<UserResponseDto> RegisterUser(UserDTO request)
        {
            if (request == null) {
                throw new Exception("Invalid Request.");
            }

            var user = new User(){
             Username = request.Username,
             Email = request.Email,
             Role = "User",
             PasswordHash= BCrypt.Net.BCrypt.HashPassword(request.Password),
             CreatedOn = DateTime.Now,
             ModifiedOn = DateTime.Now,
             CreatedBy = "Admin",
             ModifiedBy = "Admin",
        };
           var result = await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            if (!(result.State != EntityState.Added))
            {
                throw new Exception("Something went wrong");
            }

            var userResponse = _mapper.Map<UserResponseDto>(user);

            LoginEmailTemplate temp = new LoginEmailTemplate();
            string loginContent = temp.LoginHtmlTemplate(request.Username);

            Message message = new Message(new string[] { request.Email }, "Library Management", loginContent);
            _email.SendEmail(message);

            return userResponse;
                     
        }



        public async Task<UserResponseDto> UpdateUser(Guid userId , UserDTO request )
        {
           var user = await  _dbContext.Users
                      .Where(b => b.Id == userId && b.IsDeleted == false)
                      .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new Exception($"user with id :{userId} does not exist.");
            }

            user.Email = request.Email;
            user.Username = request.Username;
            user.ModifiedOn = DateTime.Now;
            user.ModifiedBy = "Admin";
             
            await _dbContext.SaveChangesAsync();
            var userResponse = _mapper.Map<UserResponseDto>(user);

            return userResponse;
        }

    }
}
