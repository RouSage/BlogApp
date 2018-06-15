using BlogApp.Data;
using BlogApp.Service;
using System;

namespace BlogApp.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFBlogAppDbContext _dbContext;

        public UnitOfWork(EFBlogAppDbContext context)
        {
            _dbContext = context;
            Posts = new EFPostRepository(_dbContext);
            Categories = new EFCategoryRepository(_dbContext);
            Tags = new EFTagRepository(_dbContext);
            ContactRepository = new Repository<Contact>(_dbContext);
        }

        public IPostRepository Posts { get; private set; }

        public ICategoryRepository Categories { get; private set; }

        public ITagRepository Tags { get; private set; }

        public IRepository<Contact> ContactRepository { get; private set; }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
