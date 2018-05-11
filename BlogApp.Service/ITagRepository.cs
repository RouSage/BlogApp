using BlogApp.Data;
using System;
using System.Collections.Generic;

namespace BlogApp.Service
{
    public interface ITagRepository : IDisposable
    {
        /// <summary>
        /// Returns all Tag entities
        /// </summary>
        /// <returns></returns>
        IEnumerable<Tag> GetTags();

        /// <summary>
        /// Returns a single Tag entity based on url slug
        /// </summary>
        /// <param name="tagSlug">Tag's url slug</param>
        /// <returns></returns>
        Tag GetTagByUrlSlug(string tagSlug);

        /// <summary>
        /// Returns a single Tag entity based on ID
        /// </summary>
        /// <param name="tagID">Tag's ID</param>
        /// <returns></returns>
        Tag GetTagByID(int tagID);

        /// <summary>
        /// Adds a new Tag and returns its ID
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        int AddTag(Tag tag);

        /// <summary>
        /// Deletes the Tag from database
        /// </summary>
        /// <param name="tagID">Tag's ID</param>
        void DeleteTag(int tagID);

        /// <summary>
        /// Updates the Tag
        /// </summary>
        /// <param name="tag"></param>
        void UpdateTag(Tag tag);
    }
}
