using Microsoft.EntityFrameworkCore;

namespace BookShop.Models
{
    public class LibrosContext : DbContext
    {
        public LibrosContext(DbContextOptions<LibrosContext> options)
            :base(options)
        {

        }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Cliente> Clientes {get; set; }
        public DbSet<Carrito> Carritos { get; set;}
    }
}