using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CutItUp.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovingUnnecessaryColumnsAndTableFromTheDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Promo_MainWebsite_WebsiteId",
                table: "Promo");

            migrationBuilder.DropForeignKey(
                name: "FK_WhyUsSection_MainWebsite_WebsiteId",
                table: "WhyUsSection");

            migrationBuilder.DropTable(
                name: "MainWebsite");

            migrationBuilder.DropIndex(
                name: "IX_WhyUsSection_WebsiteId",
                table: "WhyUsSection");

            migrationBuilder.DropIndex(
                name: "IX_Promo_WebsiteId",
                table: "Promo");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "WhyUsSection");

            migrationBuilder.DropColumn(
                name: "WebsiteId",
                table: "WhyUsSection");

            migrationBuilder.DropColumn(
                name: "WebsiteId",
                table: "Promo");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Promo",
                newName: "PromoFileURL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PromoFileURL",
                table: "Promo",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "WhyUsSection",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "WebsiteId",
                table: "WhyUsSection",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WebsiteId",
                table: "Promo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MainWebsite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainWebsite", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WhyUsSection_WebsiteId",
                table: "WhyUsSection",
                column: "WebsiteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Promo_WebsiteId",
                table: "Promo",
                column: "WebsiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Promo_MainWebsite_WebsiteId",
                table: "Promo",
                column: "WebsiteId",
                principalTable: "MainWebsite",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WhyUsSection_MainWebsite_WebsiteId",
                table: "WhyUsSection",
                column: "WebsiteId",
                principalTable: "MainWebsite",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
