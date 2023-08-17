using BO;
using TPPizza.Controllers;

namespace TPPizza.Models
{
    public class PizzaCreationEditionDTO

    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public Pate Pate { get; set; }
        public List<Pate> Pates { get; set; } = PizzaController.PatesDisponibles;

        public List<Ingredient> Ingredients { get; set; } = PizzaController.IngredientsDisponibles;
    }
}
