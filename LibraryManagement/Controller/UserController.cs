using Contracts;
using LibraryManagement.DTOs.RequestDTOs;
using LibraryManagement.DTOs.ResponseDTOs;
using LibraryManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace LibraryManagement.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;
        private readonly ILoggerManager _logger;

        public UserController(UserService service, ILoggerManager logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        [Route("/register-user")]
        public async Task<ActionResult<UserResponseDto>> RegisterUser([FromBody] UserDTO request) {
          
               var user = await _service.RegisterUser(request);
                _logger.logInfo($"Time:{DateTime.Now} , user is registered ,{this.GetType().Name}");
                return user;
           
        }

        [HttpPost]
        [Route("/login-user")]
        public async Task<ActionResult<UserLoginResponseDto>> LoginUser([FromBody] UserLogInDto request)
        {
        
                var user = await _service.LoginUser(request);
                _logger.logInfo($"Time:{DateTime.Now} , user with email: {request.Email} logged in ,{this.GetType().Name}");
                return user;
        
        }

        [HttpGet]
        [Route("/get-users")]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAllUsers() { 
             var userResponse = await _service.GetAllUsers();
            _logger.logInfo($"Time:{DateTime.Now} , All users are found. , {this.GetType().Name}");
            return userResponse.ToList();
        }


        [HttpGet]
        [Route("/get-user/{userId}")]
        public async Task<ActionResult<UserResponseDto>> GetUserById([FromRoute] Guid userId) { 
           var user = await _service.GetUserById(userId);
            _logger.logInfo($"Time:{DateTime.Now} , user with Id :{userId} is found , {this.GetType().Name}");
            return user;
        }


        [HttpPost]
        [Route("/update-user/{userId}")]
        public async Task<ActionResult<UserResponseDto>> UpdateUser([FromRoute] Guid userId, [FromBody] UserDTO request) {
          var user =  await _service.UpdateUser(userId, request);
            _logger.logInfo($"Time:{DateTime.Now} , user with id:{userId} is updated.");
            return user;
        }


        [HttpDelete]
        [Route("/delete-user/{userId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid userId) {
            await _service.DeleteUser(userId);
            _logger.logInfo($"Time:{DateTime.Now} , user with id: {userId} is deleted successfully. , {this.GetType().Name}");
            return Ok();
        }

    }
}
