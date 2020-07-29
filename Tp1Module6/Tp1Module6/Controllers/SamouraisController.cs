using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BO;
using Tp1Module6.Data;
using Tp1Module6.Models;

namespace Tp1Module6.Controllers
{
    public class SamouraisController : Controller
    {
        private Tp1Module6Context db = new Tp1Module6Context();

        // GET: Samourais
        public ActionResult Index()
        {
            return View(db.Samourais.ToList());
        }

        // GET: Samourais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SamouraiCreateEditVM vm = new SamouraiCreateEditVM();
            vm.Samourai = db.Samourais.Find(id);
            if (vm.Samourai == null)
            {
                return HttpNotFound();
            }
            return View(vm);
        }

        // GET: Samourais/Create
        public ActionResult Create()
        {
            SamouraiCreateEditVM vm = new SamouraiCreateEditVM();
            vm.Armes = db.Armes.ToList();
            List<int> ArmeIds = db.Samourais.Where(x => x.Arme != null).Select(x => x.Arme.Id).ToList();
            vm.Armes = db.Armes.Where(x => !ArmeIds.Contains(x.Id)).ToList();
            vm.ArtMartiaux = db.ArtMartials.ToList();
            return View(vm);
        }

        // POST: Samourais/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SamouraiCreateEditVM vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (vm.IdSelectedArme == null)
                    {
                        ModelState.AddModelError("", "Veuillez selectionner une pate !");
                        return View(vm);
                    }
                    if(vm.ArtMartiauxId.Count == 0)
                    {
                        ModelState.AddModelError("", "Veuillez selectionner un art martial !");
                        return View(vm);
                    }
                    vm.Samourai.Arme = db.Armes.FirstOrDefault(x => x.Id == vm.IdSelectedArme);
                    vm.Samourai.ArtMartiaux = db.ArtMartials.Where(x => vm.ArtMartiauxId.Contains(x.Id)).ToList();
                    db.Samourais.Add(vm.Samourai);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(vm);
            }
            catch
            {
                return View(vm);
            }
        }

        // GET: Samourais/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SamouraiCreateEditVM vm = new SamouraiCreateEditVM();
            List<int> ArmeIds = db.Samourais.Where(x => x.Arme != null).Select(x => x.Arme.Id).ToList();
            vm.Armes = db.Armes.Where(x => !ArmeIds.Contains(x.Id)).ToList();
            vm.Samourai = db.Samourais.Find(id);
            if (vm.Samourai == null)
            {
                return HttpNotFound();
            }
            return View(vm);
        }

        // POST: Samourais/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SamouraiCreateEditVM vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var samouraiDb = db.Samourais.Find(vm.Samourai.Id);
                    samouraiDb.Force = vm.Samourai.Force;
                    samouraiDb.Nom = vm.Samourai.Nom;
                    samouraiDb.Arme = db.Armes.FirstOrDefault(x => x.Id == vm.IdSelectedArme);
                    samouraiDb.ArtMartiaux = db.ArtMartials.Where(x => vm.ArtMartiauxId.Contains(x.Id)).ToList();
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(vm);
            }
            catch
            {
                return View(vm);
            }
        }

        // GET: Samourais/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }
            return View(samourai);
        }

        // POST: Samourais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Samourai samourai = db.Samourais.Find(id);
            db.Samourais.Remove(samourai);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
