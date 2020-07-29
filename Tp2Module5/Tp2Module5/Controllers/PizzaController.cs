using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using Tp2Module5.Models;
using TPModule5_2_BO;

namespace Tp2Module5.Controllers
{
    public class PizzaController : Controller
    {
        private static List<Ingredient> ingredients = Pizza.IngredientsDisponibles;
        private static List<Pate> pates = Pizza.PatesDisponibles;
        private static List<Pizza> pizzas = new List<Pizza>();

        // GET: Pizza
        public ActionResult Index()
        {
            return View(pizzas);
        }

        // GET: Pizza/Details/5
        public ActionResult Details(int id)
        {
            PizzaCreateEditVM vm = new PizzaCreateEditVM();
            vm.Pizza = pizzas.FirstOrDefault(x => x.Id == id);
            return View(vm);
        }

        // GET: Pizza/Create
        public ActionResult Create()
        {
            PizzaCreateEditVM vm = new PizzaCreateEditVM();
            vm.Ingredients = PizzaController.ingredients;
            vm.Pates = PizzaController.pates;
            return View(vm);
        }

        // POST: Pizza/Create
        [HttpPost]
        public ActionResult Create(PizzaCreateEditVM vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Une pizza doit toujours avoir une pâte
                    if(vm.IdSelectedPate == null)
                    {
                        ModelState.AddModelError("", "Veuillez selectionner une pate !");
                        return View(vm);
                    }

                    // Pas de pizzas avec le même nom
                    if (pizzas.Any(p => p.Nom.ToUpper() == vm.Pizza.Nom.ToUpper()))
                    {
                        ModelState.AddModelError("", "Cette pizza existe déjà !");
                        return View(vm);
                    }
                    // Une pizza doit avoir entre 2 et 5 ingrédients
                    if (vm.IdSelectedIngredients.Count < 2 || vm.IdSelectedIngredients.Count > 5)
                    {
                        ModelState.AddModelError("", "Une pizza doit avoir entre 2 et 5 ingrédients !");
                        return View(vm);
                    }

                    // Deux pizzas, ou plus, ne peuvent pas avoir la même liste d’ingrédients
                    foreach (Pizza pizza in pizzas)
                    {
                        List<int> ingredients = new List<int>();
                        foreach (Ingredient ingredient in pizza.Ingredients)
                        {
                            ingredients.Add(ingredient.Id);
                        }

                        if(Enumerable.SequenceEqual(ingredients, vm.IdSelectedIngredients))
                        {
                            ModelState.AddModelError("", "Deux pizzas, ou plus, ne peuvent pas avoir la même liste d’ingrédients !");
                            return View(vm);
                        }

                    }

                        vm.Pizza.Pate = pates.FirstOrDefault(x => x.Id == vm.IdSelectedPate);
                    foreach (int ingredientId in vm.IdSelectedIngredients)
                    {
                        Ingredient ingredient = PizzaController.ingredients.FirstOrDefault(x => x.Id == ingredientId);
                        vm.Pizza.Ingredients.Add(ingredient);
                    }
                    vm.Pizza.Id = PizzaController.pizzas.Count == 0 ? 1 : PizzaController.pizzas.Max(x => x.Id) + 1;
                    PizzaController.pizzas.Add(vm.Pizza);
                    return RedirectToAction("Index");
                }
                return View(vm);
            }
            catch
            {
                return View(vm);
            }
        }

        // GET: Pizza/Edit/5
        public ActionResult Edit(int id)
        {
            PizzaCreateEditVM vm = new PizzaCreateEditVM();
            vm.Pizza = PizzaController.pizzas.FirstOrDefault(x => x.Id == id);
            vm.Ingredients = PizzaController.ingredients;
            vm.Pates = PizzaController.pates;
            return View(vm);
        }

        // POST: Pizza/Edit/5
        [HttpPost]
        public ActionResult Edit(PizzaCreateEditVM vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (pizzas.Any(p => p.Nom.ToUpper() == vm.Pizza.Nom.ToUpper()))
                    {
                        ModelState.AddModelError("", "Cette pizza existe déjà !");
                        return View(vm);
                    }
                    List<Ingredient> lesIngredients = new List<Ingredient>();
                    Pizza pizza = pizzas.FirstOrDefault(x => x.Id == vm.Pizza.Id);
                    pizza.Pate = pates.FirstOrDefault(x => x.Id == vm.IdSelectedPate);
                    pizza.Nom = vm.Pizza.Nom;
                    foreach (int ingredientId in vm.IdSelectedIngredients)
                    {
                        Ingredient ingredient = PizzaController.ingredients.FirstOrDefault(x => x.Id == ingredientId);
                        lesIngredients.Add(ingredient);
                    }
                    pizza.Ingredients = lesIngredients;
                    return RedirectToAction("Index");
                }
                return View(vm);
            }
            catch
            {
                return View(vm);
            }
        }

        // GET: Pizza/Delete/5
        public ActionResult Delete(int id)
        {
            PizzaCreateEditVM vm = new PizzaCreateEditVM();
            vm.Pizza = pizzas.FirstOrDefault(x => x.Id == id);
            return View(vm);

        }

        // POST: Pizza/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Pizza laPizza = PizzaController.pizzas.FirstOrDefault(x => x.Id == id);
                pizzas.Remove(laPizza);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
