using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MiFincaVirtual.Backend.Models;
using MiFincaVirtual.Common.Models;

namespace MiFincaVirtual.Backend.Controllers
{
    [Authorize]
    public class CerdasCargadasController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: CerdasCargadas
        public async Task<ActionResult> Index()
        {
            var cerdasCargadas = db.CerdasCargadas.Include(c => c.Animales);
            return View(await cerdasCargadas.ToListAsync());
        }

        // GET: CerdasCargadas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CerdasCargadas cerdasCargadas = await db.CerdasCargadas.FindAsync(id);
            if (cerdasCargadas == null)
            {
                return HttpNotFound();
            }
            return View(cerdasCargadas);
        }

        // GET: CerdasCargadas/Create
        public ActionResult Create()
        {
            List<Animales> lstAnimales = new List<Animales>();
            Animales objAnimal = new Animales();
            objAnimal.AnimalId = -1;
            objAnimal.CodigoAnimal = "-- Seleccione --";
            lstAnimales.Add(objAnimal);
            lstAnimales.AddRange(db.Animales.Where(O => O.Opciones.Codigopcion == "Porcino" && O.EshembraAnimal == true && O.EshembraGestanteAnimal == true).ToList());
            ViewBag.AnimalId = new SelectList(lstAnimales, "AnimalId", "CodigoAnimal");

            return View();
        }

        // POST: CerdasCargadas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CerdasCargadas cerdasCargadas)
        {
            List<Animales> lstAnimales = new List<Animales>();
            Animales objAnimal = new Animales();

            if (ModelState.IsValid)
            {
                if (cerdasCargadas.AnimalId == -1)
                {
                    objAnimal.AnimalId = -1;
                    objAnimal.CodigoAnimal = "-- Seleccione --";
                    lstAnimales.Add(objAnimal);
                    lstAnimales.AddRange(db.Animales.Where(O => O.Opciones.Codigopcion == "Porcino" && O.EshembraAnimal == true && O.EshembraGestanteAnimal == true).ToList());
                    ViewBag.AnimalId = new SelectList(lstAnimales, "AnimalId", "CodigoAnimal");

                    return View(cerdasCargadas);
                }

                cerdasCargadas.FechaRecordacionCerdaCargada = cerdasCargadas.FechaMontaCerdaCargada.AddDays(90);
                cerdasCargadas.FechaInyectarCerdaCargada = cerdasCargadas.FechaMontaCerdaCargada.AddDays(100);
                cerdasCargadas.FechaPosiblePartoCerdaCargada = cerdasCargadas.FechaMontaCerdaCargada.AddDays(113);
                cerdasCargadas.FechaDestetePartoCerdaCargada = Convert.ToDateTime("1900-01-01");
                cerdasCargadas.ActivoCerdaCargada = true;
                cerdasCargadas.NacidosCerdaCargada = 0;
                cerdasCargadas.NacidosVivosCerdaCargada = 0;
                cerdasCargadas.NacidosMuertosCerdaCargada = 0;
                cerdasCargadas.NacidosMomiasCerdaCargada = 0;
                cerdasCargadas.DestetosCerdaCargada = 0;
                db.CerdasCargadas.Add(cerdasCargadas);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            objAnimal.AnimalId = -1;
            objAnimal.CodigoAnimal = "-- Seleccione --";
            lstAnimales.Add(objAnimal);
            lstAnimales.AddRange(db.Animales.Where(O => O.Opciones.Codigopcion == "Porcino" && O.EshembraAnimal == true && O.EshembraGestanteAnimal == true).ToList());
            ViewBag.AnimalId = new SelectList(lstAnimales, "AnimalId", "CodigoAnimal", cerdasCargadas.AnimalId);

            return View(cerdasCargadas);
        }

        // GET: CerdasCargadas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CerdasCargadas cerdasCargadas = await db.CerdasCargadas.FindAsync(id);
            if (cerdasCargadas == null)
            {
                return HttpNotFound();
            }

            List<Animales> lstAnimales = new List<Animales>();
            Animales objAnimal = new Animales();
            objAnimal.AnimalId = -1;
            objAnimal.CodigoAnimal = "-- Seleccione --";
            lstAnimales.Add(objAnimal);
            lstAnimales.AddRange(db.Animales.Where(O => O.Opciones.Codigopcion == "Porcino" && O.EshembraAnimal == true && O.EshembraGestanteAnimal == true).ToList());
            ViewBag.AnimalId = new SelectList(lstAnimales, "AnimalId", "CodigoAnimal", cerdasCargadas.AnimalId);

            return View(cerdasCargadas);
        }

        // POST: CerdasCargadas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CerdasCargadas cerdasCargadas)
        {
            List<Animales> lstAnimales = new List<Animales>();
            Animales objAnimal = new Animales();

            if (ModelState.IsValid)
            {
                if (cerdasCargadas.AnimalId == -1)
                {
                    objAnimal.AnimalId = -1;
                    objAnimal.CodigoAnimal = "-- Seleccione --";
                    lstAnimales.Add(objAnimal);
                    lstAnimales.AddRange(db.Animales.Where(O => O.Opciones.Codigopcion == "Porcino" && O.EshembraAnimal == true && O.EshembraGestanteAnimal == true).ToList());
                    ViewBag.AnimalId = new SelectList(lstAnimales, "AnimalId", "CodigoAnimal", cerdasCargadas.AnimalId);

                    return View(cerdasCargadas);
                }

                if(cerdasCargadas.FechaRealPartoCerdaCargada.Year != 1)
                {
                    cerdasCargadas.FechaDestetePartoCerdaCargada = cerdasCargadas.FechaRealPartoCerdaCargada.AddDays(28);
                }

                db.Entry(cerdasCargadas).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            objAnimal.AnimalId = -1;
            objAnimal.CodigoAnimal = "-- Seleccione --";
            lstAnimales.Add(objAnimal);
            lstAnimales.AddRange(db.Animales.Where(O => O.Opciones.Codigopcion == "Porcino" && O.EshembraAnimal == true && O.EshembraGestanteAnimal == true).ToList());
            ViewBag.AnimalId = new SelectList(lstAnimales, "AnimalId", "CodigoAnimal", cerdasCargadas.AnimalId);

            return View(cerdasCargadas);
        }

        // GET: CerdasCargadas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CerdasCargadas cerdasCargadas = await db.CerdasCargadas.FindAsync(id);
            if (cerdasCargadas == null)
            {
                return HttpNotFound();
            }
            return View(cerdasCargadas);
        }

        // POST: CerdasCargadas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CerdasCargadas cerdasCargadas = await db.CerdasCargadas.FindAsync(id);
            db.CerdasCargadas.Remove(cerdasCargadas);
            await db.SaveChangesAsync();
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
