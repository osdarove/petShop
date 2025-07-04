using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace petShop.Models
{
    public class FichaMedica
    {
        [Key]
        public int IDFicha { get; set; }

        [Required]
        public int IDUsuario { get; set; } 

        [ForeignKey("IDUsuario")]
        [ValidateNever]
        public Usuario Usuario { get; set; }

        [Required]
        [Display(Name = "Nombre de la mascota")]
        public string NombreMascota { get; set; }

        [Required]
        [Display(Name = "Tipo de mascota")]
        public string TipoMascota { get; set; }  // Gato, Perro, etc.

        [Required]
        [Display(Name = "Tamaño")]
        public string Tamano { get; set; }       // Pequeño, Mediano, Grande

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
    }
}