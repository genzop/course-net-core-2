using System.ComponentModel.DataAnnotations;

namespace OdeToFood.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
                
        // Obligatorio
        [Required]
        // Puede contener hasta 80 caracteres
        [MaxLength(80)]
        // Texto a mostrar
        [Display(Name = "Restaurant Name")]
        public string Name { get; set; }

        // Texto a mostrar
        [Display(Name = "Cuisine")]
        public CuisineType Cuisine { get; set; }
    }
}
