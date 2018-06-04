﻿using BlogApp.Data;
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

        public IEnumerable<Post> GetPublishedPosts(int page, int pageSize)
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

        public IEnumerable<Post> GetAllPosts(int page, int pageSize, string sortColumn, bool sortByAscending)
        {
            IEnumerable<Post> posts;

            switch (sortColumn)
            {
                case "Title":
                    if (sortByAscending)
                    {
                        posts = DbContext.Posts
                            .OrderBy(p => p.Title)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .Include(c => c.Category)
                            .Include(t => t.Tags)
                            .ToList();
                    }
                    else
                    {
                        posts = DbContext.Posts
                            .OrderByDescending(p => p.Title)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .Include(c => c.Category)
                            .Include(t => t.Tags)
                            .ToList();
                    }
                    break;
                case "Published":
                    if (sortByAscending)
                    {
                        posts = DbContext.Posts
                            .OrderBy(p => p.Published)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .Include(c => c.Category)
                            .Include(t => t.Tags)
                            .ToList();
                    }
                    else
                    {
                        posts = DbContext.Posts
                            .OrderByDescending(p => p.Published)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .Include(c => c.Category)
                            .Include(t => t.Tags)
                            .ToList();
                    }
                    break;
                case "PostedOn":
                    if (sortByAscending)
                    {
                        posts = DbContext.Posts
                            .OrderBy(p => p.PostedOn)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .Include(c => c.Category)
                            .Include(t => t.Tags)
                            .ToList();
                    }
                    else
                    {
                        posts = DbContext.Posts
                            .OrderByDescending(p => p.PostedOn)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .Include(c => c.Category)
                            .Include(t => t.Tags)
                            .ToList();
                    }
                    break;
                case "Modified":
                    if (sortByAscending)
                    {
                        posts = DbContext.Posts
                            .OrderBy(p => p.Modified)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .Include(c => c.Category)
                            .Include(t => t.Tags)
                            .ToList();
                    }
                    else
                    {
                        posts = DbContext.Posts
                            .OrderByDescending(p => p.Modified)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .Include(c => c.Category)
                            .Include(t => t.Tags)
                            .ToList();
                    }
                    break;
                case "Category":
                    if (sortByAscending)
                    {
                        posts = DbContext.Posts
                            .OrderBy(p => p.Category.Name)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .Include(c => c.Category)
                            .Include(t => t.Tags)
                            .ToList();
                    }
                    else
                    {
                        posts = DbContext.Posts
                            .OrderByDescending(p => p.Category.Name)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .Include(c => c.Category)
                            .Include(t => t.Tags)
                            .ToList();
                    }
                    break;
                default:
                    posts = DbContext.Posts
                            .OrderBy(p => p.PostedOn)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .Include(c => c.Category)
                            .Include(t => t.Tags)
                            .ToList();
                    break;
            }

            return posts;
        }

        public IEnumerable<Post> GetPublishedPostsByCategory(string categorySlug, int page, int pageSize)
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

        public IEnumerable<Post> GetPublishedPostsByTag(string tagSlug, int page, int pageSize)
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

        public int TotalPosts(bool isPublished = true)
        {
            return DbContext.Posts
                .Where(p => !isPublished || p.Published)
                .Count();
        }

        public int TotalPublishedPostsForCategory(string categorySlug)
        {
            return DbContext.Posts
                .Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug))
                .Count();
        }

        public int TotalPublishedPostsForTag(string tagSlug)
        {
            return DbContext.Posts
                .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
                .Count();
        }

        public int AddPost(Post post)
        {
            DbContext.Posts.Add(post);

            return post.ID;
        }
    }
}
