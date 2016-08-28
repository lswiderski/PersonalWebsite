using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalWebsite.Data.Entities;
using PersonalWebsite.IdentityModel;

namespace PersonalWebsite.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public virtual DbSet<Setting> Settings { get; set; }

        public virtual DbSet<Post> Posts { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          

            modelBuilder.Entity<PostTag>()
                            .HasKey(t => new { t.PostId, t.TagId });

            modelBuilder.Entity<PostTag>()
                            .HasOne(pt => pt.Post)
                            .WithMany(p => p.PostTags)
                            .HasForeignKey(pt => pt.PostId);

            modelBuilder.Entity<PostTag>()
                            .HasOne(pt => pt.Tag)
                            .WithMany(t => t.PostTags)
                            .HasForeignKey(pt => pt.TagId);

            modelBuilder.Entity<PostCategory>()
                            .HasKey(t => new { t.PostId, t.CategoryId });

            modelBuilder.Entity<PostCategory>()
                            .HasOne(pt => pt.Post)
                            .WithMany(p => p.PostCategories)
                            .HasForeignKey(pt => pt.PostId);

            modelBuilder.Entity<PostCategory>()
                            .HasOne(pt => pt.Category)
                            .WithMany(t => t.PostCategories)
                            .HasForeignKey(pt => pt.CategoryId);
        }
    }
}