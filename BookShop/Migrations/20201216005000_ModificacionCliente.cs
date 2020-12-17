using Microsoft.EntityFrameworkCore.Migrations;

namespace BookShop.Migrations
{
    public partial class ModificacionCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libros_Clientes_ClienteMail",
                table: "Libros");

            migrationBuilder.DropIndex(
                name: "IX_Libros_ClienteMail",
                table: "Libros");

            migrationBuilder.DropColumn(
                name: "ClienteMail",
                table: "Libros");

            migrationBuilder.AddColumn<string>(
                name: "CarritoClienteID",
                table: "Clientes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_CarritoClienteID",
                table: "Clientes",
                column: "CarritoClienteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Carritos_CarritoClienteID",
                table: "Clientes",
                column: "CarritoClienteID",
                principalTable: "Carritos",
                principalColumn: "ClienteID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Carritos_CarritoClienteID",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_CarritoClienteID",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "CarritoClienteID",
                table: "Clientes");

            migrationBuilder.AddColumn<string>(
                name: "ClienteMail",
                table: "Libros",
                type: "TEXT",
                nullable: true);

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
    }
}
