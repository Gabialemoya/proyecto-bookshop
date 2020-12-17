using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BookShop.Models
{
    public class Cliente
    {
        [Key]
        public string Mail { get; set; }
        [Required]
        public string Clave { get; set; }
        public Carrito Carrito { get; set; }
    }

}