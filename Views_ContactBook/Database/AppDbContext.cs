using ContactBook.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactBook.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User
            modelBuilder.Entity<User>().HasKey(u => u.Id);

            //Contact
            modelBuilder.Entity<Contact>().HasKey(c => c.Id);

            //Table Relationships
            modelBuilder.Entity<User>().HasMany(c => c.Contacts).WithOne(h => h.User);

        }
    }
}
