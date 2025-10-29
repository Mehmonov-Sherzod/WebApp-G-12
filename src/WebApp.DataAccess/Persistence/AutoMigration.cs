using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp.DataAccess.Persistence
{
    public static class AutoMigration
    {
        public static void Migrate(IServiceProvider serviceProvider)
        {
            //var context = serviceProvider.GetRequiredService<AppDbContext>();

            //if (context.Database.IsNpgsql())
            //{
            //    context.Database.Migrate();
            //}
        }
    }
}
