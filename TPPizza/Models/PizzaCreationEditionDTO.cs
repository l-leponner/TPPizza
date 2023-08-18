using BO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TPPizza.Controllers;

namespace TPPizza.Models
{
    public class PizzaCreationEditionDTO

    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Vous devez donner un nom à votre pizza !", AllowEmptyStrings = false)]
        [StringLength(10)]
        public string Nom { get; set; }
        [Required(ErrorMessage = "Vous devez sélectionner une pate pour votre pizza !", AllowEmptyStrings = false)]
        public Pate Pate { get; set; }
        public List<Pate> Pates { get; set; }
        public SelectList PatesSelect { get; set; }
        public MultiSelectList IngredientsSelect { get; set; }
        [Required(ErrorMessage = "Vous devez sélectionner au moins un ingrédient pour votre pizza !", AllowEmptyStrings = false)]
        public List<Ingredient> Ingredients { get; set; }
        public List<int> IngredientsIds { get; set; }

    }
}
