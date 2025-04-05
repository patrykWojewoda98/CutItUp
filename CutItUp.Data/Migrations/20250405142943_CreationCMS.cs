using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CutItUp.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreationCMS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drill");

            migrationBuilder.DropTable(
                name: "Mill");

            migrationBuilder.DropTable(
                name: "SpecialTools");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tap",
                table: "Tap");

            migrationBuilder.RenameTable(
                name: "Tap",
                newName: "Tool");

            migrationBuilder.AlterColumn<bool>(
                name: "PassThrough",
                table: "Tool",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<float>(
                name: "Angle",
                table: "Tool",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Tool",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NoCuttingEddges",
                table: "Tool",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Radius",
                table: "Tool",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SpecialTool_Angle",
                table: "Tool",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpecialTool_NoCuttingEddges",
                table: "Tool",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tool",
                table: "Tool",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "CartTool",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    ToolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartTool", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartTool_Cart_CartId",
                        column: x => x.CartId,
                        principalTable: "Cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartTool_Tool_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tool",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Promo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebsiteId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Promo_MainWebsite_WebsiteId",
                        column: x => x.WebsiteId,
                        principalTable: "MainWebsite",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WhyUsSection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reasons = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebsiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhyUsSection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WhyUsSection_MainWebsite_WebsiteId",
                        column: x => x.WebsiteId,
                        principalTable: "MainWebsite",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartTool_CartId",
                table: "CartTool",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartTool_ToolId",
                table: "CartTool",
                column: "ToolId");

            migrationBuilder.CreateIndex(
                name: "IX_Promo_WebsiteId",
                table: "Promo",
                column: "WebsiteId");

            migrationBuilder.CreateIndex(
                name: "IX_WhyUsSection_WebsiteId",
                table: "WhyUsSection",
                column: "WebsiteId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartTool");

            migrationBuilder.DropTable(
                name: "Promo");

            migrationBuilder.DropTable(
                name: "WhyUsSection");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "MainWebsite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tool",
                table: "Tool");

            migrationBuilder.DropColumn(
                name: "Angle",
                table: "Tool");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Tool");

            migrationBuilder.DropColumn(
                name: "NoCuttingEddges",
                table: "Tool");

            migrationBuilder.DropColumn(
                name: "Radius",
                table: "Tool");

            migrationBuilder.DropColumn(
                name: "SpecialTool_Angle",
                table: "Tool");

            migrationBuilder.DropColumn(
                name: "SpecialTool_NoCuttingEddges",
                table: "Tool");

            migrationBuilder.RenameTable(
                name: "Tool",
                newName: "Tap");

            migrationBuilder.AlterColumn<bool>(
                name: "PassThrough",
                table: "Tap",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tap",
                table: "Tap",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Drill",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Angle = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dimension = table.Column<float>(type: "real", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Length = table.Column<float>(type: "real", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoOfSaled = table.Column<int>(type: "int", nullable: false),
                    NoOfToolsInMagazine = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drill", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mill",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dimension = table.Column<float>(type: "real", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Length = table.Column<float>(type: "real", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoCuttingEddges = table.Column<int>(type: "int", nullable: false),
                    NoOfSaled = table.Column<int>(type: "int", nullable: false),
                    NoOfToolsInMagazine = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mill", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpecialTools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Angle = table.Column<float>(type: "real", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dimension = table.Column<float>(type: "real", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Length = table.Column<float>(type: "real", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoCuttingEddges = table.Column<int>(type: "int", nullable: false),
                    NoOfSaled = table.Column<int>(type: "int", nullable: false),
                    NoOfToolsInMagazine = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Radius = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialTools", x => x.Id);
                });
        }
    }
}
