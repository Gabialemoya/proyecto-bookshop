using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BookShop.Models
{
    public class Carrito
    {
        [Key]
        public string ClienteID { get; set; }
        public List<Libro> Libros { get; set; }
    }

}