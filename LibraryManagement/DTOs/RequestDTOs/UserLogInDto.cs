namespace LibraryManagement.DTOs.RequestDTOs
{
    public class UserLogInDto
    {
        public required string Email { get; set; }

        public required  string Password { get; set; }
    }
}
