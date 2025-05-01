using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CutItUp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingFieldsLoginAndPasswordHashToUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Login",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "User");
        }
    }
}
