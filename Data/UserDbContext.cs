using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using VkCrud2.Models;
namespace VkCrud2.Data
{
    public sealed class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroupClass> UserGroups { get; set; }
        public DbSet<UserStateClass> UserStates { get; set; }

        public UserDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Server=localhost;port=5432;Database=Crud;User Id=CrudTest;Password=CrudTestPass;");
        }

        
    }
}
