using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Api.Migrations
{
    /// <inheritdoc />
    public partial class changedpermissions2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Permissions_UserClaimEnum",
                table: "Permissions");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Id_UserClaimEnum",
                table: "Permissions",
                columns: new[] { "Id", "UserClaimEnum" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Permissions_Id_UserClaimEnum",
                table: "Permissions");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_UserClaimEnum",
                table: "Permissions",
                column: "UserClaimEnum",
                unique: true);
        }
    }
}
