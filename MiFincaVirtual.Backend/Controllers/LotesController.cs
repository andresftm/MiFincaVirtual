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
    public class LotesController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Lotes
        public async Task<ActionResult> Index()
        {
            var lotes = db.Lotes.Include(l => l.Opciones);
            return View(await lotes.ToListAsync());
        }

        // GET: Lotes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lotes lotes = await db.Lotes.FindAsync(id);
            if (lotes == null)
            {
                return HttpNotFound();
            }
            return View(lotes);
        }

        // GET: Lotes/Create
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

        // POST: Lotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Lotes lotes)
        {
            List<Opciones> lstOpciones = new List<Opciones>();
            Opciones objOpcion = new Opciones();

            if (ModelState.IsValid)
            {
                if (lotes.OpcionId == -1)
                {
                    objOpcion.OpcionId = -1;
                    objOpcion.Codigopcion = "-- Seleccione --";
                    lstOpciones.Add(objOpcion);
                    lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "TiposAnimales").ToList());
                    ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion", lotes.OpcionId);

                    return View(lotes);
                }

                lotes.CerradoLote = false;
                lotes.FechaFinalLote = Convert.ToDateTime("1900-01-01");

                db.Lotes.Add(lotes);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            objOpcion.OpcionId = -1;
            objOpcion.Codigopcion = "-- Seleccione --";
            lstOpciones.Add(objOpcion);
            lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "TiposAnimales").ToList());
            ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion", lotes.OpcionId);

            return View(lotes);
        }

        // GET: Lotes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lotes lotes = await db.Lotes.FindAsync(id);
            if (lotes == null)
            {
                return HttpNotFound();
            }

            List<Opciones> lstOpciones = new List<Opciones>();
            Opciones objOpcion = new Opciones();
            objOpcion.OpcionId = -1;
            objOpcion.Codigopcion = "-- Seleccione --";
            lstOpciones.Add(objOpcion);
            lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "TiposAnimales").ToList());
            ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion", lotes.OpcionId);

            return View(lotes);
        }

        // POST: Lotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Lotes lotes)
        {
            List<Opciones> lstOpciones = new List<Opciones>();
            Opciones objOpcion = new Opciones();

            if (ModelState.IsValid)
            {
                if (lotes.OpcionId == -1)
                {
                    objOpcion.OpcionId = -1;
                    objOpcion.Codigopcion = "-- Seleccione --";
                    lstOpciones.Add(objOpcion);
                    lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "TiposAnimales").ToList());
                    ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion", lotes.OpcionId);

                    return View(lotes);
                }


                db.Entry(lotes).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            objOpcion.OpcionId = -1;
            objOpcion.Codigopcion = "-- Seleccione --";
            lstOpciones.Add(objOpcion);
            lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "TiposAnimales").ToList());
            ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion", lotes.OpcionId);

            return View(lotes);
        }

        // GET: Lotes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lotes lotes = await db.Lotes.FindAsync(id);
            if (lotes == null)
            {
                return HttpNotFound();
            }
            return View(lotes);
        }

        // POST: Lotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Lotes lotes = await db.Lotes.FindAsync(id);
            db.Lotes.Remove(lotes);
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
