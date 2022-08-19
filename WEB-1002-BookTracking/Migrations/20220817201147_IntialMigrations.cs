using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEB_1002_BookTracking.Migrations
{
    public partial class IntialMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryTypeModels",
                columns: table => new
                {
                    CategoryTypeModelId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTypeModels", x => x.CategoryTypeModelId);
                });

            migrationBuilder.CreateTable(
                name: "CategoryModels",
                columns: table => new
                {
                    CategoryModelId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryTypeModelId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryModels", x => x.CategoryModelId);
                    table.ForeignKey(
                        name: "FK_CategoryModels_CategoryTypeModels_CategoryTypeModelId",
                        column: x => x.CategoryTypeModelId,
                        principalTable: "CategoryTypeModels",
                        principalColumn: "CategoryTypeModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookModels",
                columns: table => new
                {
                    BookModelId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    CategoryModelId = table.Column<int>(type: "INTEGER", nullable: false),
                    Author = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookModels", x => x.BookModelId);
                    table.ForeignKey(
                        name: "FK_BookModels_CategoryModels_CategoryModelId",
                        column: x => x.CategoryModelId,
                        principalTable: "CategoryModels",
                        principalColumn: "CategoryModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookModels_CategoryModelId",
                table: "BookModels",
                column: "CategoryModelId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryModels_CategoryTypeModelId",
                table: "CategoryModels",
                column: "CategoryTypeModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookModels");

            migrationBuilder.DropTable(
                name: "CategoryModels");

            migrationBuilder.DropTable(
                name: "CategoryTypeModels");
        }
    }
}
