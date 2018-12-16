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
using MiFincaVirtual.Backend.Tools;
using System.Data.SqlClient;

namespace MiFincaVirtual.Backend.Controllers
{
    [Authorize]
    public class AnimalesController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Animales
        public async Task<ActionResult> Index()
        {
            var animales = db.Animales.Include(a => a.Opciones).Include(a => a.Razas).Where(A => A.ActivoAnimal);
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
        public async Task<ActionResult> Create(Animales animales)
        {
            List<Opciones> lstOpciones = new List<Opciones>();
            Opciones objOpcion = new Opciones();
            List<Razas> lstRazas = new List<Razas>();
            Razas objRaza = new Razas();

            if (ModelState.IsValid)
            {
                if(animales.OpcionId == -1 || animales.RazaId == -1 )
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

                if(animales.EshembraGestanteAnimal && !animales.EshembraAnimal)
                {
                    TempData["testmsg"] = Mensajes.GestanteMacho;

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
        public async Task<ActionResult> Edit(Animales animales)
        {
            List<Opciones> lstOpciones = new List<Opciones>();
            Opciones objOpcion = new Opciones();
            List<Razas> lstRazas = new List<Razas>();
            Razas objRaza = new Razas();

            if (ModelState.IsValid)
            {
                if (animales.OpcionId == -1 || animales.RazaId == -1 )
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

                if (animales.EshembraGestanteAnimal && !animales.EshembraAnimal)
                {
                    TempData["testmsg"] = Mensajes.GestanteMacho;

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
            List<Respuesta> Respuesta = new List<Respuesta>();

            using (LocalDataContext db = new LocalDataContext())
            {
                Respuesta = db.Database.SqlQuery<Respuesta>(Sp.uspAnimalEliminar + " @AnimalId", new SqlParameter("AnimalId", id)).ToList();
            }

            if (Respuesta[0].Codigo == 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                if (Respuesta[0].Descripcion == "0001")
                {
                    TempData["msnAnimalesEliminar"] = Mensajes.Mensaje0001;
                }
                else if (Respuesta[0].Descripcion == "0002")
                {
                    TempData["msnAnimalesEliminar"] = Mensajes.Mensaje0002;
                }

                Animales animales = await db.Animales.FindAsync(id);
                if (animales == null)
                {
                    return HttpNotFound();
                }
                return View(animales);
            }
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
