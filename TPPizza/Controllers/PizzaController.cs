using BO;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
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
            
            return View(PatesDisponibles, IngredientsDisponibles);
        }

        // POST: PizzaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                
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
            }
            return View(pizza);
        }

        // POST: PizzaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
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
