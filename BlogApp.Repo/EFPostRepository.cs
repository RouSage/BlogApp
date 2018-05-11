using BlogApp.Data;
using BlogApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace BlogApp.Repo
{
    public class EFPostRepository : IPostRepository
    {
        private readonly EFBlogAppDbContext _dbContext;

        public EFPostRepository(EFBlogAppDbContext context)
        {
            _dbContext = context;
        }

        public int AddPost(Post post)
        {
            _dbContext.Posts.Add(post);
            _dbContext.SaveChanges();

            return post.ID;
        }

        public void DeletePost(int postID)
        {
            Post dbEntry = _dbContext.Posts.Find(postID);

            if(dbEntry != null)
            {
                _dbContext.Posts.Remove(dbEntry);
                _dbContext.SaveChanges();
            }
        }

        public Post GetPostByID(int postID)
        {
            return _dbContext.Posts.Find(postID);
        }

        public IEnumerable<Post> GetPosts()
        {
            return _dbContext.Posts
                .Where(p => p.Published)
                .OrderByDescending(p => p.PostedOn)
                .Include(c => c.Category)
                .Include(t => t.Tags);
        }

        public IEnumerable<Post> GetPostsByCategory(string categorySlug)
        {
            return _dbContext.Posts
                .Where(p => p.Published && p.Category.Equals(categorySlug));
        }

        public IEnumerable<Post> GetPostsByTag(string tagSlug)
        {
            return _dbContext.Posts
                .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)));
        }

        public void UpdatePost(Post post)
        {
            _dbContext.Entry(post).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        private bool disposedValue = false;

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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
