using BlogApp.Data;
using System;
using System.Collections.Generic;

namespace BlogApp.Service
{
    public interface ICategoryRepository : IDisposable
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

        /// <summary>
        /// Adds a new Category and returns its ID
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        int AddCategory(Category category);

        /// <summary>
        /// Deletes the Category from database
        /// </summary>
        /// <param name="categoryID"></param>
        void DeleteCategory(int categoryID);

        /// <summary>
        /// Updates the Category
        /// </summary>
        /// <param name="category"></param>
        void UpdateCategory(Category category);

        /// <summary>
        /// Saves all changes to the database
        /// </summary>
        void Save();
    }
}
