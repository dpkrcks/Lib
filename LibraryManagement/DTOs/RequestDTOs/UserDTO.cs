using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.DTOs.RequestDTOs
{
    public class UserDTO
    {
        [Required]
        private string username = string.Empty;
        [Required]
        private string email = string.Empty;
        [Required]
        private string password = string.Empty;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

    }
}
