using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Uyg04WorkProject.API.Models
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {

        public DbSet<Work> Works { get; set; }
        public DbSet<WorkStep> WorkSteps { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
