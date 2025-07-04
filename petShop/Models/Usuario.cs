using System;
using System.ComponentModel.DataAnnotations;

namespace petShop.Models
{
    public class Usuario
    {
        [Key]
        public int IDUsuario { get; set; }


        [Display(Name = "Nombre completo")]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Contraseña")]
        public string PasswordHash { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public bool Estado { get; set; } = true;
    }
}