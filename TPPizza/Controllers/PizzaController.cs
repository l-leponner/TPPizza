using BO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TPPizza.Models;

namespace TPPizza.Controllers
{
    public class PizzaController : Controller
    {
        private static List<Pizza> pizzas = new List<Pizza> {
            new Pizza{Id = 1, Nom = "Calzone", Pate = Pizza.PatesDisponibles[1], Ingredients = new List<Ingredient>{ Pizza.IngredientsDisponibles[0], Pizza.IngredientsDisponibles[1]} },
            new Pizza{Id = 2, Nom = "Reine", Pate = Pizza.PatesDisponibles[0], Ingredients = new List<Ingredient>{ Pizza.IngredientsDisponibles[1], Pizza.IngredientsDisponibles[3], Pizza.IngredientsDisponibles[4]} },
        };

        // GET: PizzaController
        public ActionResult Index()
        {
            return View(pizzas);
        }

        // GET: PizzaController/Details/5
        public ActionResult Details(int id)
        {
            Pizza? pizza = pizzas.Find(p => p.Id == id);
            if (pizza == null)
            {
                return NotFound();
            }
            return View(pizza);
        }

        // GET: PizzaController/Create
        public ActionResult Create()
        {
            return View(new PizzaVM());
        }

        // POST: PizzaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PizzaVM pizzaVM)
        {
            try
            {
                if (Valider(pizzaVM))
                {
                    pizzas.Add(new Pizza
                    {
                        Id = pizzas.Any() ? pizzas.Max(p => p.Id) + 1 : 1,
                        Nom = pizzaVM.Nom,
                        Pate = Pizza.PatesDisponibles.First(p => p.Id == pizzaVM.IdPate),
                        Ingredients = Pizza.IngredientsDisponibles.Where(i => pizzaVM.IdsIngredients.Contains(i.Id)).ToList()
                    });
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(pizzaVM);
                }
            }
            catch
            {
                return View(pizzaVM);
            }
        }

        // GET: PizzaController/Edit/5
        public ActionResult Edit(int id)
        {
            Pizza? pizza = pizzas.Find(p => p.Id == id);
            if (pizza == null)
            {
                return NotFound();
            }
            return View(new PizzaVM
            {
                Id = pizza.Id,
                Nom = pizza.Nom,
                IdPate = pizza.Pate.Id,
                IdsIngredients = pizza.Ingredients.Select(i => i.Id).ToList()
            });
        }

        // POST: PizzaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PizzaVM pizzaVM)
        {
            try
            {
                if (Valider(pizzaVM))
                {
                    Pizza? pizza = pizzas.Find(p => p.Id == pizzaVM.Id);
                    if (pizza == null)
                    {
                        return NotFound();
                    }
                    pizza.Nom = pizzaVM.Nom;
                    pizza.Pate = Pizza.PatesDisponibles.First(p => p.Id == pizzaVM.IdPate);
                    pizza.Ingredients = Pizza.IngredientsDisponibles.Where(i => pizzaVM.IdsIngredients.Contains(i.Id)).ToList();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(pizzaVM);
                }
            }
            catch
            {
                return View(pizzaVM);
            }
        }

        // GET: PizzaController/Delete/5
        public ActionResult Delete(int id)
        {
            Pizza? pizza = pizzas.Find(p => p.Id == id);
            if (pizza == null)
            {
                return NotFound();
            }
            return View(pizza);
        }

        // POST: PizzaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Pizza? pizza = pizzas.Find(p => p.Id == id);
                if (pizza == null)
                {
                    return NotFound();
                }
                pizzas.Remove(pizza);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [AcceptVerbs("GET", "POST")]
        public ActionResult VerifNomPizzaUnique(string nom, int id)
        {
            if (NomPizzaExistant(nom, id))
                return Json("Une autre pizza porte déjà ce nom");
            else
                return Json(true);
        }

        [AcceptVerbs("GET", "POST")]
        public ActionResult VerifIngredientsOriginaux(string idsIngredients, int id)
        {
            List<int> ids = new List<int>();
            foreach (string idI in idsIngredients.Split(','))
            {
                ids.Add(int.Parse(idI));
            }
            if (MemesIngredients(ids, id))
                return Json("Une autre pizza utilise exactement ces mêmes ingrédients");
            else
                return Json(true);
        }

        private bool Valider(PizzaVM pizzaVM)
        {
            if (!ModelState.IsValid)
                return false;

            if (NomPizzaExistant(pizzaVM.Nom, pizzaVM.Id))
            {
                ModelState.AddModelError("Nom", "Une autre pizza porte déjà ce nom");
                return false;
            }

            if (MemesIngredients(pizzaVM.IdsIngredients, pizzaVM.Id))
            {
                ModelState.AddModelError("IdsIngredients", "Une autre pizza utilise exactement ces mêmes ingrédients");
                return false;
            }

            return true;
        }

        private bool NomPizzaExistant(string nomPizza, int id)
        {
            nomPizza = nomPizza.ToLower();
            return pizzas.Any(p => p.Nom.ToLower() == nomPizza && p.Id != id);
        }

        private bool MemesIngredients(List<int> idsIngredients, int id)
        {
            return pizzas.Where(p => p.Id != id && p.Ingredients.Count() == idsIngredients.Count()).Any(p => p.Ingredients.All(i => idsIngredients.Contains(i.Id)));
        }
    }
}
