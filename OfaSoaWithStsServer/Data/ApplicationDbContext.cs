using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OfaSoaWithStsServer.Models;

namespace OfaSoaWithStsServer.Data
{
    public sealed class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private static bool _migrated;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            if (_migrated) return;
            Database.Migrate();
            _migrated = true;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<UserClaim>(b =>
            //{
            //    // Primary key
            //    b.HasKey(uc => uc.Id);

            //    // Maps to the AspNetUserClaims table
            //    b.ToTable("AspNetUserClaims");
            //});

            base.OnModelCreating(builder);
        }
    }
}
