using Microsoft.EntityFrameworkCore.Migrations;

namespace lab10.Migrations
{
    public partial class FixeArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "filePath",
                table: "Article",
                newName: "FilePath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Article",
                newName: "filePath");
        }
    }
}
