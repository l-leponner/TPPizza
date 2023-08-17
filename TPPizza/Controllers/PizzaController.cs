using BO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        public static List<Pizza> ListePizzas => new List<Pizza>
        {
            new Pizza { Id = 1, Nom= "Grasgras", 
                Pate=PatesDisponibles.Where(p => p.Id == 3).Single()}
        };
        // GET: PizzaController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PizzaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PizzaController/Create
        public ActionResult Create()
        {
            return View();
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
            return View();
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
            return View();
        }

        // POST: PizzaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
