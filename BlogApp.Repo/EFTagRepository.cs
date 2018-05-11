using BlogApp.Data;
using BlogApp.Service;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System;

namespace BlogApp.Repo
{
    public class EFTagRepository : ITagRepository
    {
        private readonly EFBlogAppDbContext _dbContext;

        public EFTagRepository(EFBlogAppDbContext context)
        {
            _dbContext = context;
        }

        public int AddTag(Tag tag)
        {
            _dbContext.Tags.Add(tag);
            _dbContext.SaveChanges();

            return tag.ID;
        }

        public void DeleteTag(int tagID)
        {
            Tag dbEntry = _dbContext.Tags.Find(tagID);

            if(dbEntry != null)
            {
                _dbContext.Tags.Remove(dbEntry);
                _dbContext.SaveChanges();
            }
        }

        public Tag GetTagByID(int tagID)
        {
            return _dbContext.Tags.FirstOrDefault(t => t.ID == tagID);
        }

        public Tag GetTagByUrlSlug(string tagSlug)
        {
            return _dbContext.Tags.FirstOrDefault(t => t.UrlSlug.Equals(tagSlug));
        }

        public IEnumerable<Tag> GetTags()
        {
            return _dbContext.Tags.OrderBy(t => t.Name);
        }

        public void UpdateTag(Tag tag)
        {
            _dbContext.Entry(tag).State = EntityState.Modified;
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
