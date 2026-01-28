using Microsoft.EntityFrameworkCore;
using AcademicInfoHub.Api.Models;

namespace AcademicInfoHub.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<InfoPost> InfoPosts { get; set; }
        public DbSet<InfoImage> InfoImages { get; set; }
    }
}
