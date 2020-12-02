using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Models
{
    public class Tienda
    {
        [Required]
        public List<Libro> Lista_libros { get; set; }

        [Required]
        public List<Genero> Lista_generos { get; set; }

        
    }

}