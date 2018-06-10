using BlogApp.Data;
using BlogApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogApp.Repo
{
    public class EFTagRepository : Repository<Tag>, ITagRepository
    {
        public EFTagRepository(EFBlogAppDbContext context)
            : base(context)
        {
        }

        public EFBlogAppDbContext DbContext
        {
            get { return dbContext as EFBlogAppDbContext; }
        }

        public Tag GetTagByID(int tagID)
        {
            return DbContext.Tags.FirstOrDefault(t => t.ID == tagID);
        }

        public Tag GetTagByUrlSlug(string tagSlug)
        {
            return DbContext.Tags.FirstOrDefault(t => t.UrlSlug.Equals(tagSlug));
        }

        public IEnumerable<Tag> GetTags()
        {
            return DbContext.Tags.OrderBy(t => t.Name).ToList();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
