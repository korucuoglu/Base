using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Base.Api.Persistence.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notes_categories_CategoryId",
                table: "notes");

            migrationBuilder.AddForeignKey(
                name: "FK_notes_categories_CategoryId",
                table: "notes",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notes_categories_CategoryId",
                table: "notes");

            migrationBuilder.AddForeignKey(
                name: "FK_notes_categories_CategoryId",
                table: "notes",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
