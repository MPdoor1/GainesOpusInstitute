using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gaines_Opus_Institute_Current.Data
{
    public class GOSContext : IdentityDbContext<IdentityUser>
    {
        public GOSContext(DbContextOptions<GOSContext> options)
            : base(options)
        {
        }

        public DbSet<Gaines_Opus_Institute_Current.Models.User> user { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
