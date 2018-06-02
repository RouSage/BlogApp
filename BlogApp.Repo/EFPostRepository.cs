using BlogApp.Data;
using BlogApp.Service;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace BlogApp.Repo
{
    public class EFPostRepository : Repository<Post>, IPostRepository
    {
        public EFPostRepository(EFBlogAppDbContext context)
            : base(context)
        {
        }

        public EFBlogAppDbContext DbContext
        {
            get { return context as EFBlogAppDbContext; }
        }

        public Post GetPost(int year, int month, string titleSlug)
        {
            return DbContext.Posts
                .Where(p => p.PostedOn.Year == year && p.PostedOn.Month == month && p.UrlSlug.Equals(titleSlug))
                .Include(c => c.Category)
                .Include(t => t.Tags)
                .Single();
        }

        public Post GetPostByID(int postID)
        {
            return DbContext.Posts.Find(postID);
        }

        public IEnumerable<Post> GetPosts()
        {
            return DbContext.Posts
                .Where(p => p.Published)
                .OrderByDescending(p => p.PostedOn)
                .Include(c => c.Category)
                .Include(t => t.Tags)
                .ToList();
        }

        public IEnumerable<Post> GetPosts(int page, int pageSize)
        {
            return DbContext.Posts
                .Where(p => p.Published)
                .OrderByDescending(p => p.PostedOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(c => c.Category)
                .Include(t => t.Tags)
                .ToList();
        }

        public IEnumerable<Post> GetPostsByCategory(string categorySlug, int page, int pageSize)
        {
            return DbContext.Posts
                .Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug))
                .OrderByDescending(p=>p.PostedOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(c=>c.Category)
                .Include(t=>t.Tags)
                .ToList();
        }

        public IEnumerable<Post> GetPostsByTag(string tagSlug, int page, int pageSize)
        {
            return DbContext.Posts
                .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
                .OrderByDescending(p=>p.PostedOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(c=>c.Category)
                .Include(t=>t.Tags)
                .ToList();
        }

        public int TotalPosts()
        {
            return DbContext.Posts
                .Where(p => p.Published)
                .Count();
        }

        public int TotalPostsForCategory(string categorySlug)
        {
            return DbContext.Posts
                .Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug))
                .Count();
        }

        public int TotalPostsForTag(string tagSlug)
        {
            return DbContext.Posts
                .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
                .Count();
        }
    }
}
