using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Website.Migrations
{
    /// <inheritdoc />
    public partial class Cart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Tap",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoOfToolsInMagazine",
                table: "Tap",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "SpecialTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Length",
                table: "SpecialTools",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "NoOfToolsInMagazine",
                table: "SpecialTools",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Mill",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Length",
                table: "Mill",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "NoOfToolsInMagazine",
                table: "Mill",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Drill",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoOfToolsInMagazine",
                table: "Drill",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Tap");

            migrationBuilder.DropColumn(
                name: "NoOfToolsInMagazine",
                table: "Tap");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "SpecialTools");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "SpecialTools");

            migrationBuilder.DropColumn(
                name: "NoOfToolsInMagazine",
                table: "SpecialTools");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Mill");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "Mill");

            migrationBuilder.DropColumn(
                name: "NoOfToolsInMagazine",
                table: "Mill");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Drill");

            migrationBuilder.DropColumn(
                name: "NoOfToolsInMagazine",
                table: "Drill");
        }
    }
}
