using BlogApp.Data;
using System.Collections.Generic;

namespace BlogApp.Service
{
    public interface IPostRepository : IRepository<Post>
    {
        /// <summary>
        /// Returns all Post entities
        /// </summary>
        /// <returns></returns>
        IEnumerable<Post> GetPosts();

        /// <summary>
        /// Returns all posts with pagination support
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IEnumerable<Post> GetPosts(int page, int pageSize);

        /// <summary>
        /// Return post based on the published year, month and title slug
        /// </summary>
        /// <param name="year">Published year</param>
        /// <param name="month">Published month</param>
        /// <param name="titleSlug">Post's url slug</param>
        /// <returns></returns>
        Post GetPost(int year, int month, string titleSlug);

        /// <summary>
        /// Return all Post entities based on category's slug
        /// </summary>
        /// <param name="categorySlug">Category's slug</param>
        /// <returns></returns>
        IEnumerable<Post> GetPostsByCategory(string categorySlug, int page, int pageSize);

        /// <summary>
        /// Returns all Post entities based on tag's slug
        /// </summary>
        /// <param name="tagSlug">Tag's slug</param>
        /// <returns></returns>
        IEnumerable<Post> GetPostsByTag(string tagSlug);

        /// <summary>
        /// Returns total number of published posts
        /// </summary>
        /// <returns></returns>
        int TotalPosts();

        /// <summary>
        /// Returns total number of published posts based on category
        /// </summary>
        /// <param name="categorySlug">Category's slug</param>
        /// <returns></returns>
        int TotalPostsForCategory(string categorySlug);

        /// <summary>
        /// Returns a single Post entity based on ID
        /// </summary>
        /// <param name="postID">Post's ID</param>
        /// <returns></returns>
        Post GetPostByID(int postID);
    }
}
