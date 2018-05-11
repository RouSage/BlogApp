using BlogApp.Service;

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
        }

        public IPostRepository Posts { get; }

        public ICategoryRepository Categories { get; }

        public ITagRepository Tags { get; }

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
