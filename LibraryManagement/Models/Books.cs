using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace LibraryManagement.Models
{
    public class Books
    {
        public Books() { }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        private Guid id;
       
        private string title = String.Empty;

        private string author = String.Empty;

        private string edition = String.Empty;

        private string category = String.Empty;

        private string description = String.Empty;

        private bool isAvailable = true;

        private int bookCount;

        public Guid Id { get => id; set => id = value; }

        public string Title { get => title; set => title = value; }

        public string Author { get => author; set=> author = value; }

        public string Edition { get => edition; set => edition = value; }

        public string Category { get => category; set => category = value; }

        public string Description { get => description; set => description = value; }

        public bool IsAvailable { get => isAvailable; set => isAvailable = value; }

        public int BookCount { get => bookCount; set => bookCount = value; }

    }
}
