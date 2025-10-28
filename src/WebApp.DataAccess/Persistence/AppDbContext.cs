using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebApp.Domain.Entities;

namespace WebApp.DataAccess.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        { }

            public DbSet<Subject> Subjects { get; set; }
            public DbSet<Section> Sections { get; set; }
            public DbSet<Question> Questions { get; set; }
            public DbSet<Answer> Answers { get; set; }
            public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
