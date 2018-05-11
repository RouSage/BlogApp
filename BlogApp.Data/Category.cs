using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Data
{
    public class Category
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(500, ErrorMessage = "Length should not exceed 500 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "UrlSlug is required")]
        [StringLength(500, ErrorMessage = "Length should not exceed 500 characters")]
        public string UrlSlug { get; set; }

        //public int Frequence { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
