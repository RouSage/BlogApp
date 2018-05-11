using BlogApp.Data;
using BlogApp.Service;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System;

namespace BlogApp.Repo
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private readonly EFBlogAppDbContext _dbContext;

        public EFCategoryRepository(EFBlogAppDbContext context)
        {
            _dbContext = context;
        }

        public int AddCategory(Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();

            return category.ID;
        }

        public void DeleteCategory(int categoryID)
        {
            Category dbEntry = _dbContext.Categories.Find(categoryID);

            if(dbEntry != null)
            {
                _dbContext.Categories.Remove(dbEntry);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<Category> GetCategories()
        {
            return _dbContext.Categories.OrderBy(c => c.Name);
        }

        public Category GetCategoryByID(int categoryID)
        {
            return _dbContext.Categories.FirstOrDefault(c => c.ID == categoryID);
        }

        public Category GetCategoryByUrlSlug(string categorySlug)
        {
            return _dbContext.Categories.FirstOrDefault(c => c.UrlSlug.Equals(categorySlug));
        }

        public void UpdateCategory(Category category)
        {
            _dbContext.Entry(category).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
