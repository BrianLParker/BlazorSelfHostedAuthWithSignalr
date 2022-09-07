using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorSelfHostedAuthWithSignalr.Server.Data.Migrations
{
    public partial class HasDefaultRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "41be5bfa-ca55-4d0b-8964-729fbaa300e2", "f8795cf5-a57d-4011-8ab4-4e7a122f40fa", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "96e6b283-2c38-48a0-8742-80bdf6862cdd", "94415246-a51d-47e5-8e5f-f17816a3dcdc", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "96e6b283-2c38-48a0-8742-80bdf6862cdd", "478017fd-1678-4eca-ba96-574e341b2d38" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "41be5bfa-ca55-4d0b-8964-729fbaa300e2", "4c8b3ed2-1d07-4c4c-bead-414ea6826c5c" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "96e6b283-2c38-48a0-8742-80bdf6862cdd", "4c8b3ed2-1d07-4c4c-bead-414ea6826c5c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "96e6b283-2c38-48a0-8742-80bdf6862cdd", "478017fd-1678-4eca-ba96-574e341b2d38" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "41be5bfa-ca55-4d0b-8964-729fbaa300e2", "4c8b3ed2-1d07-4c4c-bead-414ea6826c5c" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "96e6b283-2c38-48a0-8742-80bdf6862cdd", "4c8b3ed2-1d07-4c4c-bead-414ea6826c5c" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41be5bfa-ca55-4d0b-8964-729fbaa300e2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "96e6b283-2c38-48a0-8742-80bdf6862cdd");
        }
    }
}
