using Microsoft.EntityFrameworkCore.Migrations;

namespace BookShop.Migrations
{
    public partial class modificacionLibros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Precio",
                table: "Libros",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Libros",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Libros");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Libros");
        }
    }
}
