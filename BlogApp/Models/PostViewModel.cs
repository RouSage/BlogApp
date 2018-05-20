using BlogApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApp.Models
{
    public class PostViewModel
    {
        public IEnumerable<Post> Posts { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}