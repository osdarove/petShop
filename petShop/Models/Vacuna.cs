using petShop.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace petShop.Models
{
    public class Vacuna
    {
        [Key]
        public int IDVacuna { get; set; }

        [Required]
        public int IDFicha { get; set; }  // Clave foránea

        [ForeignKey("IDFicha")]
        public FichaMedica FichaMedica { get; set; }

        [Required]
        public int Numero { get; set; }  // Número de dosis

        [Required]
        public string Tipo { get; set; } // Tipo de vacuna

        [Required]
        public DateTime Fecha { get; set; }
    }
}