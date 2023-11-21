namespace LibraryManagement.DTOs.ResponseDTOs
{
    public class UserLoginResponseDto
    {
        private Guid id;

        private string username = string.Empty;

        private string email = string.Empty;

        private string token = string.Empty;

        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

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

        public string Token {
            get { return token; }
            set { token = value; }
        }
    }
}
