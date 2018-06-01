using BlogApp.Data;
using System.Collections.Generic;

namespace BlogApp.Models
{
    public class WidgetViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}