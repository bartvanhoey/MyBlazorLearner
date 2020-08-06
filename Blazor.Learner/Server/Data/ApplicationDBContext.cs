using Blazor.Learner.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Blazor.Learner.Shared;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Blazor.Learner.Server.Models;

namespace Server.Data
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }

        public DbSet<Developer> Developers { get; set; }
    }
}