using LibraryManagement.DTOs.RequestDTOs;
using LibraryManagement.DTOs.ResponseDTOs;

namespace LibraryManagement.Services
{
    public interface UserService
    {
        public Task<UserResponseDto> RegisterUser(UserDTO request);

        public Task<IEnumerable<UserResponseDto>> GetAllUsers();

        public Task<UserResponseDto> GetUserById(Guid userId);

        public Task<UserResponseDto> UpdateUser(Guid userId , UserDTO requst);

        public Task DeleteUser(Guid userId);

        public Task<UserLoginResponseDto> LoginUser(UserLogInDto request);
    }

}
