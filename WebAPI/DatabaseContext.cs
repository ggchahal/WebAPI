using System.Data.Entity;
using WebAPI.Models;

namespace WebAPI
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("DefaultConnection")
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //Configure primary key
            //modelBuilder.Entity<Category>().HasKey(s => s.CategoryId);
            //modelBuilder.Entity<Product>().HasKey(p => p.ProductId);
            //modelBuilder.Entity<Product>().Property(p => p.ProductId)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //modelBuilder.Entity<Category>()
            //        .Property(c => c.CategoryName)
            //        .IsRequired()
            //        .HasMaxLength(50);

            //modelBuilder.Entity<Product>()
            //        .Property(p => p.ProductName)
            //        .IsRequired()
            //        .HasMaxLength(50);

            //modelBuilder.Entity<Product>()
            //        .Property(p => p.ProductPrice)
            //        .IsRequired();

            //modelBuilder.Entity<Product>().HasRequired(p => p.Category)
            //    .WithMany(b => b.Product).HasForeignKey(b => b.CategoryId);

            //modelBuilder.Entity<Product>()
            //    .HasRequired(x => x.Category);
        }
    }
}