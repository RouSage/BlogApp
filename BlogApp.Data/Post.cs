using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Data
{
    public class Post
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(500, ErrorMessage = "Length should not exceed 500 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(5000, ErrorMessage = "Length should not exceed 5000 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }

        [Required(ErrorMessage = "UrlSlug is required")]
        [StringLength(50, ErrorMessage = "Length should not exceed 50 characters")]
        public string UrlSlug { get; set; }

        public bool Published { get; set; }

        [Display(Name = "Posted On")]
        [Required(ErrorMessage = "Posted On is required")]
        public DateTime PostedOn { get; set; }

        public DateTime? Modified { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
