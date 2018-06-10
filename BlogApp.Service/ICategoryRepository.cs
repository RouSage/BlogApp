using BlogApp.Data;
using System;
using System.Collections.Generic;

namespace BlogApp.Service
{
    public interface ICategoryRepository : IRepository<Category>, IDisposable
    {
        /// <summary>
        /// Returns all Category entities
        /// </summary>
        /// <returns></returns>
        IEnumerable<Category> GetCategories();

        /// <summary>
        /// Returns a single Category entity based on url slug
        /// </summary>
        /// <param name="categorySlug">Category's url slug</param>
        /// <returns></returns>
        Category GetCategoryByUrlSlug(string categorySlug);

        /// <summary>
        /// Returns a single Category entity based on ID
        /// </summary>
        /// <param name="categoryID">Category's ID</param>
        /// <returns></returns>
        Category GetCategoryByID(int categoryID);
    }
}
