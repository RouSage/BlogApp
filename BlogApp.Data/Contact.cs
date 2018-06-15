using System.ComponentModel.DataAnnotations;

namespace BlogApp.Data
{
    public class Contact
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required, EmailAddress]
        public string Email { get; set; }

        [Url]
        public string Website { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }
    }
}
