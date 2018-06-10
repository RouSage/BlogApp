using BlogApp.Data;
using System;
using System.Collections.Generic;

namespace BlogApp.Service
{
    public interface IPostRepository : IRepository<Post>, IDisposable
    {
        /// <summary>
        /// Adds post to the database BUT doesn't saving it
        /// </summary>
        /// <param name="post"></param>
        /// <returns>Id of the new record</returns>
        void AddPost(Post post);

        void Edit(Post post);

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
        IEnumerable<Post> GetPublishedPosts(int page, int pageSize);

        /// <summary>
        /// Returns all posts with pagination and sorting support
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortByAscending"></param>
        /// <returns></returns>
        IEnumerable<Post> GetAllPosts(int page, int pageSize, string sortColumn, bool sortByAscending);

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
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IEnumerable<Post> GetPublishedPostsByCategory(string categorySlug, int page, int pageSize);

        /// <summary>
        /// Returns all Post entities based on tag's slug
        /// </summary>
        /// <param name="tagSlug">Tag's slug</param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IEnumerable<Post> GetPublishedPostsByTag(string tagSlug, int page, int pageSize);

        /// <summary>
        /// Returns total number of published/not published posts
        /// </summary>
        /// <param name="isPublished">True to return only published posts</param>
        /// <returns></returns>
        int TotalPosts(bool isPublished = true);

        /// <summary>
        /// Returns total number of published posts based on category
        /// </summary>
        /// <param name="categorySlug">Category's slug</param>
        /// <returns></returns>
        int TotalPublishedPostsForCategory(string categorySlug);

        /// <summary>
        /// Return total number of published posts based on category
        /// </summary>
        /// <param name="tagSlug">Tag's slug</param>
        /// <returns></returns>
        int TotalPublishedPostsForTag(string tagSlug);

        /// <summary>
        /// Returns a single Post entity based on ID
        /// </summary>
        /// <param name="postID">Post's ID</param>
        /// <returns></returns>
        Post GetPostByID(int postID);
    }
}
