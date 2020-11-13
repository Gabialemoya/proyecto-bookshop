using System.ComponentModel.DataAnnotations;

namespace BookShop.Models
{
    public class Libro
    {
        [Key]
        public string ISBN { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string DescripcionLibro { get; set; }
        [Required]
        public string Portada { get; set; }
        [Required]
        public Autor Creador { get; set; }
        [Required]
        public Genero Clasificacion { get; set; }
    }
}