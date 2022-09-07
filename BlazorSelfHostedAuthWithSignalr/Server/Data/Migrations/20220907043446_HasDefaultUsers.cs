using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorSelfHostedAuthWithSignalr.Server.Data.Migrations
{
    public partial class HasDefaultUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "478017fd-1678-4eca-ba96-574e341b2d38", 0, "155ba676-e489-45c0-8dc8-b0db135fcc13", "bob@bob.com", true, true, null, "BOB@BOB.COM", "BOB@BOB.COM", "AQAAAAEAACcQAAAAEAl0taTXilNsxcoe3efVn4nJfuOv+gbJ6vvzgtYo0lw0IrxpFHEqd5SBjGLGestTnw==", null, false, "6FUED63EUWWELGPF4REX77O2KURKKPQ7", false, "bob@bob.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4c8b3ed2-1d07-4c4c-bead-414ea6826c5c", 0, "a87d30c1-7cdb-4d9e-b67b-42ace993259c", "sally@sally.com", true, true, null, "SALLY@SALLY.COM", "SALLY@SALLY.COM", "AQAAAAEAACcQAAAAEM0NaKvVhbFLp+6qyimUFARaeua3aiKjHDwkuvFrfZx2Rrh+IJP4XAovsMz9qP9USw==", null, false, "76QH7NHMKZJL65OX2KXFOWNWD2VWFMST", false, "sally@sally.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "478017fd-1678-4eca-ba96-574e341b2d38");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4c8b3ed2-1d07-4c4c-bead-414ea6826c5c");
        }
    }
}
