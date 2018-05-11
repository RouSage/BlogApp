using BlogApp.Data;
using System.Data.Entity;

namespace BlogApp.Repo
{
    public class EFBlogAppDbContext : DbContext
    {
        public EFBlogAppDbContext() : base("EFBlogAppDbContext")
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasMany<Tag>(p => p.Tags)
                .WithMany(t => t.Posts)
                .Map(tp =>
                {
                    tp.MapLeftKey("PostRefID");
                    tp.MapRightKey("TagRefID");
                    tp.ToTable("PostTag");
                });
        }
    }
}
