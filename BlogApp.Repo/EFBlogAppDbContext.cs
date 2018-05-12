using BlogApp.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogApp.Repo
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here

            return userIdentity;
        }
    }

    public class EFBlogAppDbContext : IdentityDbContext<ApplicationUser>
    {
        public EFBlogAppDbContext()
            : base("EFBlogAppDbContext", throwIfV1Schema: false)
        {
        }

        public static EFBlogAppDbContext Create()
        {
            return new EFBlogAppDbContext();
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

            base.OnModelCreating(modelBuilder);
        }
    }
}
