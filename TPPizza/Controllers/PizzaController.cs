using BO;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Collections.Generic;
using TPPizza.Models;

namespace TPPizza.Controllers
{
    public class PizzaController : Controller
    {

        public static List<Ingredient> IngredientsDisponibles => new List<Ingredient> {
                    new Ingredient{Id=1,Nom="Mozzarella"},
                    new Ingredient{Id=2,Nom="Jambon"},
                    new Ingredient{Id=3,Nom="Tomate"},
                    new Ingredient{Id=4,Nom="Oignon"},
                    new Ingredient{Id=5,Nom="Cheddar"},
                    new Ingredient{Id=6,Nom="Saumon"},
                    new Ingredient{Id=7,Nom="Champignon"},
                    new Ingredient{Id=8,Nom="Poulet"}
                };

        public static List<Pate> PatesDisponibles => new List<Pate> {
                    new Pate{ Id=1,Nom="Pate fine, base crême"},
                    new Pate{ Id=2,Nom="Pate fine, base tomate"},
                    new Pate{ Id=3,Nom="Pate épaisse, base crême"},
                    new Pate{ Id=4,Nom="Pate épaisse, base tomate"}
                };

        public static List<Pizza> ListePizzas = new List<Pizza>
        {
            new Pizza { Id = 1, Nom= "Grasgras", 
                Pate=PatesDisponibles.Where(p => p.Id == 3).Single(),
            Ingredients=IngredientsDisponibles},
            new Pizza { Id = 2, Nom= "Nope",
                Pate=PatesDisponibles.Where(p => p.Id == 2).Single(),
            Ingredients=IngredientsDisponibles.Where(i => i.Id > 5).Select(i => i).ToList()}
        };

        //public static List<SelectListItem> GetPatesSelect()
        //{
        //    List < SelectListItem > PatesSelect = new List <SelectListItem> ();
        //    foreach (var pate in PatesDisponibles)
        //    {
        //        PatesSelect.Add();
        //    }
        //    return;
        //}
        // GET: PizzaController
        public ActionResult Index()
        {
            return View(ListePizzas);
        }

        // GET: PizzaController/Details/5
        public ActionResult Details(int id)
        {
            Pizza? pizza = GetPizza(id);
            if(pizza is null)
            {
                return View(nameof(Index));
            }
            return View(pizza);
        }

        // GET: PizzaController/Create
        public ActionResult Create()
        {

            var vm = new PizzaCreationEditionDTO();
            vm.PatesSelect = new SelectList(
                    PatesDisponibles, 
                    nameof(PizzaCreationEditionDTO.Pate.Id), 
                    nameof(PizzaCreationEditionDTO.Pate.Nom));
            vm.IngredientsSelect = new MultiSelectList(
                IngredientsDisponibles,
                nameof(Ingredient.Id),
                nameof(Ingredient.Nom)
                );
            return View(vm);
        }

        // POST: PizzaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] IFormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(collection);
                } else
                {
                    Pizza createdPizza = new Pizza();
                    int id = ListePizzas.Any() ? ListePizzas.Last().Id + 1 : 1;
                    string nom = collection["Nom"];
                    Pate pate = PatesDisponibles.Single(p => p.Id == int.Parse(collection["Pate"]));
                    List<Ingredient> ingredients = new List<Ingredient>();
                    foreach (string ingredientId in collection["Ingredients"])
                    {
                        ingredients.Add(IngredientsDisponibles.Single(i => i.Id == int.Parse(ingredientId)));
                    }
                    createdPizza.Id = id;
                    createdPizza.Nom = nom;
                    createdPizza.Pate = pate;
                    createdPizza.Ingredients = ingredients;

                    ListePizzas.Add(createdPizza);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PizzaController/Edit/5
        public ActionResult Edit(int id)
        {
            Pizza? pizza = GetPizza(id);
            if (pizza is null)
            {
                return View(nameof(Index));
            } else
            {
                var vm = new PizzaCreationEditionDTO();
                vm.Id = pizza.Id;
                vm.Nom = pizza.Nom;
                vm.PatesSelect = new SelectList(
                        PatesDisponibles,
                        nameof(PizzaCreationEditionDTO.Pate.Id),
                        nameof(PizzaCreationEditionDTO.Pate.Nom),
                        pizza.Pate.Id);
                List<int> ingredientsId = new List<int>();
                foreach (Ingredient ingredient in pizza.Ingredients)
                {
                    ingredientsId.Add(ingredient.Id);
                }
                vm.IngredientsSelect = new MultiSelectList(
                    IngredientsDisponibles,
                    nameof(Ingredient.Id),
                    nameof(Ingredient.Nom),
                    ingredientsId
                    );
                return View(vm);
            }
        }

        // POST: PizzaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(collection);
                }
                else
                {
                    Pizza? editedPizza = GetPizza(id);

                    if (editedPizza is null)
                    {
                        return View(nameof(Index));
                    }
                    else
                    {
                        string nom = collection["Nom"];
                        Pate pate = PatesDisponibles.Single(p => p.Id == int.Parse(collection["Pate"]));
                        List<Ingredient> ingredients = new List<Ingredient>();
                        foreach (string ingredientId in collection["Ingredients"])
                        {
                            ingredients.Add(IngredientsDisponibles.Single(i => i.Id == int.Parse(ingredientId)));
                        }
                        
                        editedPizza.Nom = nom;
                        editedPizza.Pate = pate;
                        editedPizza.Ingredients = ingredients;

                        int index = ListePizzas.FindIndex(p => p.Id == editedPizza.Id);
                        ListePizzas[index] = editedPizza;
                    }

                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PizzaController/Delete/5
        public ActionResult Delete(int id)
        {
            Pizza? pizza = GetPizza(id);
            if (pizza is null)
            {
                return View(nameof(Index));
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
                Pizza? pizza = GetPizza(id);
                if (pizza is null)
                {
                    throw new Exception("Pizza introuvable ! :(");
                }
                ListePizzas.Remove(pizza);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(nameof(Index), ListePizzas);
            }
        }

        private static Pizza? GetPizza(int id)
        {
            return ListePizzas.SingleOrDefault(p => p.Id == id);
        }
    }
}
