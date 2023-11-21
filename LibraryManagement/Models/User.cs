using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Models
{
    public class User
    {

        public User() { }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity),Key]
        private Guid id;
     
        private string username= string.Empty;
             
        private string email = string.Empty;

        private string passwordHash = string.Empty;

        private string role = string.Empty;

        private bool isActive = true;

        private bool isDeleted = false;

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
            set { username= value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string PasswordHash {
            get { return passwordHash;}
            set { passwordHash = value;}
        }

        public string Role {
            get {return role;}
            set {role = value;}
        }

        public bool IsActive {
            get { return isActive;}
            set { isActive = value;}
        }

        public bool IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }

        public DateTime CreatedOn {
            get { return createdOn;}
            set { createdOn = value; }
        }

        public DateTime ModifiedOn {
            get { return modifiedOn; }
            set { modifiedOn = value; }
        }

        public string CreatedBy {
            get { return createdBy;}
            set { createdBy = value;}
        }

        public string ModifiedBy {
            get { return modifiedBy;}
            set { modifiedBy = value;}
        }

    }
}
