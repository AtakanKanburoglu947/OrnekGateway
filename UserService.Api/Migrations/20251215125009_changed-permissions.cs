using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Api.Migrations
{
    /// <inheritdoc />
    public partial class changedpermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Permissions_UserClaimEnum",
                table: "Permissions",
                column: "UserClaimEnum",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Permissions_UserClaimEnum",
                table: "Permissions");
        }
    }
}
