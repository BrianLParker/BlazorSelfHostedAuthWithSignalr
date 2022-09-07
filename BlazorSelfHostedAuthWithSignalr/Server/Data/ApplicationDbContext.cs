using BlazorSelfHostedAuthWithSignalr.Server.Models;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BlazorSelfHostedAuthWithSignalr.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            SeedDebugUsers(builder);
        }

        private void SeedDebugUsers(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser[]
                {
                    new ()
                    {
                        Id = "478017fd-1678-4eca-ba96-574e341b2d38",
                        UserName="bob@bob.com",
                        NormalizedUserName = "BOB@BOB.COM",
                        Email = "bob@bob.com",
                        NormalizedEmail="BOB@BOB.COM",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAEAl0taTXilNsxcoe3efVn4nJfuOv+gbJ6vvzgtYo0lw0IrxpFHEqd5SBjGLGestTnw==",
                        SecurityStamp = "6FUED63EUWWELGPF4REX77O2KURKKPQ7",
                        ConcurrencyStamp = "155ba676-e489-45c0-8dc8-b0db135fcc13",
                        LockoutEnabled = true,
                        AccessFailedCount = 0
                    },
                    new ()
                    {
                        Id = "4c8b3ed2-1d07-4c4c-bead-414ea6826c5c",
                        UserName="sally@sally.com",
                        NormalizedUserName = "SALLY@SALLY.COM",
                        Email = "sally@sally.com",
                        NormalizedEmail="SALLY@SALLY.COM",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAEM0NaKvVhbFLp+6qyimUFARaeua3aiKjHDwkuvFrfZx2Rrh+IJP4XAovsMz9qP9USw==",
                        SecurityStamp = "76QH7NHMKZJL65OX2KXFOWNWD2VWFMST",
                        ConcurrencyStamp = "a87d30c1-7cdb-4d9e-b67b-42ace993259c",
                        LockoutEnabled = true,
                        AccessFailedCount = 0
                    },
                });

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole[]
                {
                    new ()
                    {
                        Id = "41be5bfa-ca55-4d0b-8964-729fbaa300e2",
                        Name = "Administrator",
                        NormalizedName = "ADMINISTRATOR",
                        ConcurrencyStamp = "f8795cf5-a57d-4011-8ab4-4e7a122f40fa"
                    },
                    new ()
                    {
                        Id = "96e6b283-2c38-48a0-8742-80bdf6862cdd",
                        Name = "Manager",
                        NormalizedName = "MANAGER",
                        ConcurrencyStamp = "94415246-a51d-47e5-8e5f-f17816a3dcdc"
                    }
                });

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>[]
                {
                    new() { RoleId = "41be5bfa-ca55-4d0b-8964-729fbaa300e2", UserId = "4c8b3ed2-1d07-4c4c-bead-414ea6826c5c" },
                    new() { RoleId = "96e6b283-2c38-48a0-8742-80bdf6862cdd", UserId = "4c8b3ed2-1d07-4c4c-bead-414ea6826c5c" },
                    new() { RoleId = "96e6b283-2c38-48a0-8742-80bdf6862cdd", UserId = "478017fd-1678-4eca-ba96-574e341b2d38" }
                });
        }
    }
}