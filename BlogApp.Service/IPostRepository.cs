using BlogApp.Data;
using System;
using System.Collections.Generic;

namespace BlogApp.Service
{
    public interface IPostRepository : IDisposable
    {
        /// <summary>
        /// Returns all Post entities
        /// </summary>
        /// <returns></returns>
        IEnumerable<Post> GetPosts();

        /// <summary>
        /// Return all Post entities based on category's slug
        /// </summary>
        /// <param name="categorySlug">Category's slug</param>
        /// <returns></returns>
        IEnumerable<Post> GetPostsByCategory(string categorySlug);

        /// <summary>
        /// Returns all Post entities based on tag's slug
        /// </summary>
        /// <param name="tagSlug">Tag's slug</param>
        /// <returns></returns>
        IEnumerable<Post> GetPostsByTag(string tagSlug);

        /// <summary>
        /// Returns a single Post entity based on ID
        /// </summary>
        /// <param name="postID">Post's ID</param>
        /// <returns></returns>
        Post GetPostByID(int postID);

        /// <summary>
        /// Add a new Post and returns its ID
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        int AddPost(Post post);

        /// <summary>
        /// Deletes the Post from database
        /// </summary>
        /// <param name="postID"></param>
        void DeletePost(int postID);

        /// <summary>
        /// Updates the Post
        /// </summary>
        /// <param name="post"></param>
        void UpdatePost(Post post);
    }
}
