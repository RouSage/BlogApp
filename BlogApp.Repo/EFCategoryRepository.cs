﻿using BlogApp.Data;
using BlogApp.Service;
using System.Collections.Generic;
using System.Linq;

namespace BlogApp.Repo
{
    public class EFCategoryRepository : Repository<Category>, ICategoryRepository
    {
        public EFCategoryRepository(EFBlogAppDbContext context)
            : base(context)
        {
        }

        public EFBlogAppDbContext DbContext
        {
            get { return context as EFBlogAppDbContext; }
        }

        public IEnumerable<Category> GetCategories()
        {
            return DbContext.Categories.OrderBy(c => c.Name).ToList();
        }

        public Category GetCategoryByID(int categoryID)
        {
            return DbContext.Categories.FirstOrDefault(c => c.ID == categoryID);
        }

        public Category GetCategoryByUrlSlug(string categorySlug)
        {
            return DbContext.Categories.FirstOrDefault(c => c.UrlSlug.Equals(categorySlug));
        }
    }
}
