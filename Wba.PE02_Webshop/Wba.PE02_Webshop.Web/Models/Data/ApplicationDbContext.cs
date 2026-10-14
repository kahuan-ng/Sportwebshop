using Microsoft.EntityFrameworkCore;
using Wba.PE02_Webshop.Core.Entities;

namespace Wba.PE02_Webshop.Web.Models.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<SportProduct> SportProducts { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Feature> Features { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent api's
            // Regels voor sportproduct
            modelBuilder.Entity<SportProduct>().HasKey(sp => sp.Id);
            modelBuilder.Entity<SportProduct>()
                .Property(sp => sp.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<SportProduct>()
                .Property(sp => sp.Price)
                .IsRequired()
                .HasPrecision(14, 2);

            modelBuilder.Entity<SportProduct>()
                .Property(sp => sp.ProductImage)
                .IsRequired();

            // Set NULL delete
            modelBuilder.Entity<SportProduct>()
                .HasOne(sp => sp.Brand)
                .WithMany(b => b.SportProducts)
                .HasForeignKey(sp => sp.BrandId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<SportProduct>()
                .HasOne(sp => sp.Type)
                .WithMany(c => c.SportProducts)
                .HasForeignKey(sp => sp.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            //Regels voor Brand
            modelBuilder.Entity<Brand>()
                .HasKey(b => b.Id);
            modelBuilder.Entity<Brand>()
                .Property(b => b.BrandLogo)
                .IsRequired();

            modelBuilder.Entity<Brand>()
                .Property(b => b.BrandName)
                .IsRequired()
                .HasMaxLength(50);

            // Category
            modelBuilder.Entity<Category>().HasKey(c => c.Id);
            modelBuilder.Entity<Category>()
                .Property(c => c.Type)
                .IsRequired()
                .HasMaxLength(30);

            // Attribute
            modelBuilder.Entity<Feature>().HasKey(a => a.Id);
            modelBuilder.Entity<Feature>()
                .Property(a => a.Attribute)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<Feature>()
                .Property(a => a.AttributeCategory)
                .IsRequired()
                .HasMaxLength(30);

            DataSeeder.Seed(modelBuilder);
        }
    }
}