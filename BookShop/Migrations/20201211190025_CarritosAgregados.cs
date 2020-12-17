using Microsoft.EntityFrameworkCore.Migrations;

namespace BookShop.Migrations
{
    public partial class CarritosAgregados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CarritoClienteID",
                table: "Libros",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Carritos",
                columns: table => new
                {
                    ClienteID = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carritos", x => x.ClienteID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Libros_CarritoClienteID",
                table: "Libros",
                column: "CarritoClienteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Libros_Carritos_CarritoClienteID",
                table: "Libros",
                column: "CarritoClienteID",
                principalTable: "Carritos",
                principalColumn: "ClienteID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libros_Carritos_CarritoClienteID",
                table: "Libros");

            migrationBuilder.DropTable(
                name: "Carritos");

            migrationBuilder.DropIndex(
                name: "IX_Libros_CarritoClienteID",
                table: "Libros");

            migrationBuilder.DropColumn(
                name: "CarritoClienteID",
                table: "Libros");
        }
    }
}
