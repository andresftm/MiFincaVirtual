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
    public class CorralesController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Corrales
        public async Task<ActionResult> Index()
        {
            var corrales = db.Corrales.Include(c => c.Opciones);
            return View(await corrales.ToListAsync());
        }

        // GET: Corrales/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Corrales corrales = await db.Corrales.FindAsync(id);
            if (corrales == null)
            {
                return HttpNotFound();
            }
            return View(corrales);
        }

        // GET: Corrales/Create
        public ActionResult Create()
        {
            List<Opciones> lstOpciones = new List<Opciones>();
            Opciones objOpcion = new Opciones();
            objOpcion.OpcionId = -1;
            objOpcion.Codigopcion = "-- Seleccione --";
            lstOpciones.Add(objOpcion);
            lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "TiposAnimales").ToList());
            ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion");
            return View();
        }

        // POST: Corrales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CorralId,CodigoCorral,CantidadAnimalesCorral,MedidasCorral,ActivoCorral,OpcionId")] Corrales corrales)
        {
            List<Opciones> lstOpciones = new List<Opciones>();
            Opciones objOpcion = new Opciones();

            if (ModelState.IsValid)
            {
                if (corrales.OpcionId == -1)
                {
                    objOpcion.OpcionId = -1;
                    objOpcion.Codigopcion = "-- Seleccione --";
                    lstOpciones.Add(objOpcion);
                    lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "TiposAnimales").ToList());
                    ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion", corrales.OpcionId);

                    return View(corrales);
                }

                db.Corrales.Add(corrales);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            objOpcion.OpcionId = -1;
            objOpcion.Codigopcion = "-- Seleccione --";
            lstOpciones.Add(objOpcion);
            lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "TiposAnimales").ToList());
            ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion", corrales.OpcionId);

            return View(corrales);
        }

        // GET: Corrales/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Corrales corrales = await db.Corrales.FindAsync(id);
            if (corrales == null)
            {
                return HttpNotFound();
            }

            List<Opciones> lstOpciones = new List<Opciones>();
            Opciones objOpcion = new Opciones();
            objOpcion.OpcionId = -1;
            objOpcion.Codigopcion = "-- Seleccione --";
            lstOpciones.Add(objOpcion);
            lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "TiposAnimales").ToList());
            ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion", corrales.OpcionId);

            return View(corrales);
        }

        // POST: Corrales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CorralId,CodigoCorral,CantidadAnimalesCorral,MedidasCorral,ActivoCorral,OpcionId")] Corrales corrales)
        {
            List<Opciones> lstOpciones = new List<Opciones>();
            Opciones objOpcion = new Opciones();

            if (ModelState.IsValid)
            {
                if (corrales.OpcionId == -1)
                {
                    objOpcion.OpcionId = -1;
                    objOpcion.Codigopcion = "-- Seleccione --";
                    lstOpciones.Add(objOpcion);
                    lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "TiposAnimales").ToList());
                    ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion", corrales.OpcionId);

                    return View(corrales);
                }

                db.Entry(corrales).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            objOpcion.OpcionId = -1;
            objOpcion.Codigopcion = "-- Seleccione --";
            lstOpciones.Add(objOpcion);
            lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "TiposAnimales").ToList());
            ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion", corrales.OpcionId);

            return View(corrales);
        }

        // GET: Corrales/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Corrales corrales = await db.Corrales.FindAsync(id);
            if (corrales == null)
            {
                return HttpNotFound();
            }
            return View(corrales);
        }

        // POST: Corrales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Corrales corrales = await db.Corrales.FindAsync(id);
            db.Corrales.Remove(corrales);
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
