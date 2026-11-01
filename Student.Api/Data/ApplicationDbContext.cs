using Microsoft.EntityFrameworkCore;
using Student.Api.Models;

namespace Student.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<global::Student.Api.Models.Student> Students { get; set; } = null!;
    }
}