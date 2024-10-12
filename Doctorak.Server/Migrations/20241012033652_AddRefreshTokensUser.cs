using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doctorak.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokensUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordResetCode",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "PasswordResetCodeExipration",
                table: "Users",
                newName: "RefreshTokenExpiry");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "RefreshTokenExpiry",
                table: "Users",
                newName: "PasswordResetCodeExipration");

            migrationBuilder.AddColumn<string>(
                name: "PasswordResetCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
