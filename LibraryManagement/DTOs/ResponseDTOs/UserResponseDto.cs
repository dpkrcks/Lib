using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.DTOs.ResponseDTOs
{
    public class UserResponseDto
    {
        private Guid id;

        private string username = string.Empty;

        private string email = string.Empty;

        private bool isActive;

        private bool isDeleted;

        private DateTime createdOn;

        private DateTime modifiedOn;

        private string createdBy = string.Empty;

        private string modifiedBy = string.Empty;

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

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public bool IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }

        public DateTime CreatedOn
        {
            get { return createdOn; }
            set { createdOn = value; }
        }

        public DateTime ModifiedOn
        {
            get { return modifiedOn; }
            set { modifiedOn = value; }
        }

        public string CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }

        public string ModifiedBy
        {
            get { return modifiedBy; }
            set { modifiedBy = value; }
        }


    }
}
