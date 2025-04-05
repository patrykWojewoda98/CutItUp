using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CutItUp.Data.Migrations
{
    /// <inheritdoc />
    public partial class CMS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drill",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Angle = table.Column<float>(type: "real", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Dimension = table.Column<float>(type: "real", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Length = table.Column<float>(type: "real", nullable: false),
                    NoOfToolsInMagazine = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfSaled = table.Column<int>(type: "int", nullable: false)
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
                    NoCuttingEddges = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Dimension = table.Column<float>(type: "real", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Length = table.Column<float>(type: "real", nullable: false),
                    NoOfToolsInMagazine = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfSaled = table.Column<int>(type: "int", nullable: false)
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
                    NoCuttingEddges = table.Column<int>(type: "int", nullable: false),
                    Angle = table.Column<float>(type: "real", nullable: true),
                    Radius = table.Column<float>(type: "real", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Dimension = table.Column<float>(type: "real", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Length = table.Column<float>(type: "real", nullable: false),
                    NoOfToolsInMagazine = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfSaled = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialTools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tap",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PassThrough = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Dimension = table.Column<float>(type: "real", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Length = table.Column<float>(type: "real", nullable: false),
                    NoOfToolsInMagazine = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfSaled = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tap", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drill");

            migrationBuilder.DropTable(
                name: "Mill");

            migrationBuilder.DropTable(
                name: "SpecialTools");

            migrationBuilder.DropTable(
                name: "Tap");
        }
    }
}
