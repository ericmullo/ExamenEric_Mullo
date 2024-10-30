using System.ComponentModel.DataAnnotations;

namespace ExamenEric_Mullo.Models
{
    public class Celular
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        public string Modelo { get; set; }
        [Range(2000, 2024)]
        public anio
    }
}
