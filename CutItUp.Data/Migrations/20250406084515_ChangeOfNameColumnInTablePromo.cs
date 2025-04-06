using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CutItUp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOfNameColumnInTablePromo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Promo",
                newName: "ImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Promo",
                newName: "Url");
        }
    }
}
