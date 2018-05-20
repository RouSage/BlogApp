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
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .OrderByDescending(p => p.PostedOn)
                .Include(c => c.Category)
                .Include(t => t.Tags)
                .ToList();
        }

        public IEnumerable<Post> GetPostsByCategory(string categorySlug)
        {
            return DbContext.Posts
                .Where(p => p.Published && p.Category.Equals(categorySlug))
                .ToList();
        }

        public IEnumerable<Post> GetPostsByTag(string tagSlug)
        {
            return DbContext.Posts
                .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
                .ToList();
        }

        public int TotalPosts()
        {
            return DbContext.Posts
                .Where(p => p.Published)
                .Count();
        }
    }
}
