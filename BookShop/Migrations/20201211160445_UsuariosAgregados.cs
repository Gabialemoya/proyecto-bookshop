using Microsoft.EntityFrameworkCore.Migrations;

namespace BookShop.Migrations
{
    public partial class UsuariosAgregados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClienteMail",
                table: "Libros",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Mail = table.Column<string>(type: "TEXT", nullable: false),
                    Clave = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Mail);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Libros_ClienteMail",
                table: "Libros",
                column: "ClienteMail");

            migrationBuilder.AddForeignKey(
                name: "FK_Libros_Clientes_ClienteMail",
                table: "Libros",
                column: "ClienteMail",
                principalTable: "Clientes",
                principalColumn: "Mail",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libros_Clientes_ClienteMail",
                table: "Libros");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Libros_ClienteMail",
                table: "Libros");

            migrationBuilder.DropColumn(
                name: "ClienteMail",
                table: "Libros");
        }
    }
}
