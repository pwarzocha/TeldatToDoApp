using Microsoft.EntityFrameworkCore;
using TeldatTaskApp.Entities;

namespace TeldatTaskApp.Connection
{
    public class ToDoAppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserTask> Tasks { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            dbContextOptionsBuilder.UseSqlServer(configuration.GetConnectionString("TeldatTaskConnection"));
        }
    }
}
