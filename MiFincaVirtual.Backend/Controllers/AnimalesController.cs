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
    public class AnimalesController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Animales
        public async Task<ActionResult> Index()
        {
            var animales = db.Animales.Include(a => a.Opciones).Include(a => a.Razas);
            return View(await animales.ToListAsync());
        }

        // GET: Animales/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animales animales = await db.Animales.FindAsync(id);
            if (animales == null)
            {
                return HttpNotFound();
            }
            return View(animales);
        }

        // GET: Animales/Create
        public ActionResult Create()
        {
            List<Opciones> lstOpciones = new List<Opciones>();
            Opciones objOpcion = new Opciones();
            objOpcion.OpcionId = -1;
            objOpcion.Codigopcion = "-- Seleccione --";
            lstOpciones.Add(objOpcion);
            lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "TiposAnimales").ToList());
            ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion");

            List<Razas> lstRazas = new List<Razas>();
            Razas objRaza = new Razas();
            objRaza.RazaId = -1;
            objRaza.NombreRaza = "-- Seleccione --";
            lstRazas.Add(objRaza);
            lstRazas.AddRange(db.Razas);
            ViewBag.RazaId = new SelectList(lstRazas, "RazaId", "NombreRaza");
            return View();
        }

        // POST: Animales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AnimalId,CodigoAnimal,DescripcionAnimal,FechaIngresoAnimal,FechaNacimientoAnimal,ActivoAnimal,PerteneceAnimal,EshembraAnimal,PadreAnimal,MadreAnimal,RazaId,OpcionId")] Animales animales)
        {
            List<Opciones> lstOpciones = new List<Opciones>();
            Opciones objOpcion = new Opciones();
            List<Razas> lstRazas = new List<Razas>();
            Razas objRaza = new Razas();

            if (ModelState.IsValid)
            {
                if(animales.OpcionId == -1)
                {
                    objOpcion.OpcionId = -1;
                    objOpcion.Codigopcion = "-- Seleccione --";
                    lstOpciones.Add(objOpcion);
                    lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "TiposAnimales").ToList());
                    ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion", animales.OpcionId);

                    objRaza.RazaId = -1;
                    objRaza.NombreRaza = "-- Seleccione --";
                    lstRazas.Add(objRaza);
                    lstRazas.AddRange(db.Razas);
                    ViewBag.RazaId = new SelectList(lstRazas, "RazaId", "NombreRaza", animales.RazaId);

                    return View(animales);
                }

                if (animales.RazaId == -1)
                {
                    objOpcion.OpcionId = -1;
                    objOpcion.Codigopcion = "-- Seleccione --";
                    lstOpciones.Add(objOpcion);
                    lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "TiposAnimales").ToList());
                    ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion", animales.OpcionId);

                    objRaza.RazaId = -1;
                    objRaza.NombreRaza = "-- Seleccione --";
                    lstRazas.Add(objRaza);
                    lstRazas.AddRange(db.Razas);
                    ViewBag.RazaId = new SelectList(lstRazas, "RazaId", "NombreRaza", animales.RazaId);

                    return View(animales);
                }

                db.Animales.Add(animales);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            objOpcion.OpcionId = -1;
            objOpcion.Codigopcion = "-- Seleccione --";
            lstOpciones.Add(objOpcion);
            lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "TiposAnimales").ToList());
            ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion", animales.OpcionId);

            objRaza.RazaId = -1;
            objRaza.NombreRaza = "-- Seleccione --";
            lstRazas.Add(objRaza);
            lstRazas.AddRange(db.Razas);
            ViewBag.RazaId = new SelectList(lstRazas, "RazaId", "NombreRaza", animales.RazaId);

            return View(animales);
        }

        // GET: Animales/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animales animales = await db.Animales.FindAsync(id);
            if (animales == null)
            {
                return HttpNotFound();
            }

            List<Opciones> lstOpciones = new List<Opciones>();
            Opciones objOpcion = new Opciones();
            objOpcion.OpcionId = -1;
            objOpcion.Codigopcion = "-- Seleccione --";
            lstOpciones.Add(objOpcion);
            lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "TiposAnimales").ToList());
            ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion", animales.OpcionId);

            List<Razas> lstRazas = new List<Razas>();
            Razas objRaza = new Razas();
            objRaza.RazaId = -1;
            objRaza.NombreRaza = "-- Seleccione --";
            lstRazas.Add(objRaza);
            lstRazas.AddRange(db.Razas);
            ViewBag.RazaId = new SelectList(lstRazas, "RazaId", "NombreRaza", animales.RazaId);

            return View(animales);
        }

        // POST: Animales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AnimalId,CodigoAnimal,DescripcionAnimal,FechaIngresoAnimal,FechaNacimientoAnimal,ActivoAnimal,PerteneceAnimal,EshembraAnimal,PadreAnimal,MadreAnimal,RazaId,OpcionId")] Animales animales)
        {
            List<Opciones> lstOpciones = new List<Opciones>();
            Opciones objOpcion = new Opciones();
            List<Razas> lstRazas = new List<Razas>();
            Razas objRaza = new Razas();

            if (ModelState.IsValid)
            {
                if (animales.OpcionId == -1)
                {
                    objOpcion.OpcionId = -1;
                    objOpcion.Codigopcion = "-- Seleccione --";
                    lstOpciones.Add(objOpcion);
                    lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "TiposAnimales").ToList());
                    ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion", animales.OpcionId);

                    objRaza.RazaId = -1;
                    objRaza.NombreRaza = "-- Seleccione --";
                    lstRazas.Add(objRaza);
                    lstRazas.AddRange(db.Razas);
                    ViewBag.RazaId = new SelectList(lstRazas, "RazaId", "NombreRaza", animales.RazaId);

                    return View(animales);
                }

                if (animales.RazaId == -1)
                {
                    objOpcion.OpcionId = -1;
                    objOpcion.Codigopcion = "-- Seleccione --";
                    lstOpciones.Add(objOpcion);
                    lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "TiposAnimales").ToList());
                    ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion", animales.OpcionId);

                    objRaza.RazaId = -1;
                    objRaza.NombreRaza = "-- Seleccione --";
                    lstRazas.Add(objRaza);
                    lstRazas.AddRange(db.Razas);
                    ViewBag.RazaId = new SelectList(lstRazas, "RazaId", "NombreRaza", animales.RazaId);

                    return View(animales);
                }

                db.Entry(animales).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            objOpcion.OpcionId = -1;
            objOpcion.Codigopcion = "-- Seleccione --";
            lstOpciones.Add(objOpcion);
            lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "TiposAnimales").ToList());
            ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion", animales.OpcionId);

            objRaza.RazaId = -1;
            objRaza.NombreRaza = "-- Seleccione --";
            lstRazas.Add(objRaza);
            lstRazas.AddRange(db.Razas);
            ViewBag.RazaId = new SelectList(lstRazas, "RazaId", "NombreRaza", animales.RazaId);
            return View(animales);
        }

        // GET: Animales/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animales animales = await db.Animales.FindAsync(id);
            if (animales == null)
            {
                return HttpNotFound();
            }
            return View(animales);
        }

        // POST: Animales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Animales animales = await db.Animales.FindAsync(id);
            db.Animales.Remove(animales);
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
