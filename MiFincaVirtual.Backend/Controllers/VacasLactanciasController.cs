using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MiFincaVirtual.Common.Models;
using MiFincaVirtual.Domain.Models;

namespace MiFincaVirtual.Backend.Controllers
{
    public class VacasLactanciasController : Controller
    {
        private DataContext db = new DataContext();

        // GET: VacasLactancias
        public ActionResult Index()
        {
            var vacasLactancias = db.VacasLactancias.Include(v => v.Animales).Include(v => v.VacasCargadas).Where(A => A.ActivoVacasLactancias);
            return View(vacasLactancias.ToList());
        }

        // GET: VacasLactancias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VacasLactancias vacasLactancias = db.VacasLactancias.Find(id);
            if (vacasLactancias == null)
            {
                return HttpNotFound();
            }
            return View(vacasLactancias);
        }

        // GET: VacasLactancias/Create
        public ActionResult Create()
        {
            ViewBag.AnimalId = new SelectList(db.Animales, "AnimalId", "CodigoAnimal");
            ViewBag.VacaCargadaId = new SelectList(db.VacasCargadas, "VacaCargadaId", "VacaCargadaId");
            return View();
        }

        // POST: VacasLactancias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VacasLactancias vacasLactancias)
        {
            if (ModelState.IsValid)
            {
                db.VacasLactancias.Add(vacasLactancias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AnimalId = new SelectList(db.Animales, "AnimalId", "CodigoAnimal", vacasLactancias.AnimalId);
            ViewBag.VacaCargadaId = new SelectList(db.VacasCargadas, "VacaCargadaId", "VacaCargadaId", vacasLactancias.VacaCargadaId);
            return View(vacasLactancias);
        }

        // GET: VacasLactancias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VacasLactancias vacasLactancias = db.VacasLactancias.Find(id);
            if (vacasLactancias == null)
            {
                return HttpNotFound();
            }
            ViewBag.AnimalId = new SelectList(db.Animales, "AnimalId", "CodigoAnimal", vacasLactancias.AnimalId);
            ViewBag.VacaCargadaId = new SelectList(db.VacasCargadas, "VacaCargadaId", "VacaCargadaId", vacasLactancias.VacaCargadaId);
            return View(vacasLactancias);
        }

        // POST: VacasLactancias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VacasLactancias vacasLactancias)
        {
            if (ModelState.IsValid)
            {
                vacasLactancias.ActivoVacasLactancias = false;
                db.Entry(vacasLactancias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AnimalId = new SelectList(db.Animales, "AnimalId", "CodigoAnimal", vacasLactancias.AnimalId);
            ViewBag.VacaCargadaId = new SelectList(db.VacasCargadas, "VacaCargadaId", "VacaCargadaId", vacasLactancias.VacaCargadaId);
            return View(vacasLactancias);
        }

        // GET: VacasLactancias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VacasLactancias vacasLactancias = db.VacasLactancias.Find(id);
            if (vacasLactancias == null)
            {
                return HttpNotFound();
            }
            return View(vacasLactancias);
        }

        // POST: VacasLactancias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VacasLactancias vacasLactancias = db.VacasLactancias.Find(id);
            db.VacasLactancias.Remove(vacasLactancias);
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
