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
    public class CorralesComidasController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: CorralesComidas
        public async Task<ActionResult> Index()
        {
            var corralesComidas = db.CorralesComidas.Include(c => c.Corrales).Include(c => c.Opciones);
            return View(await corralesComidas.ToListAsync());
        }

        // GET: CorralesComidas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CorralesComida corralesComida = await db.CorralesComidas.FindAsync(id);
            if (corralesComida == null)
            {
                return HttpNotFound();
            }
            return View(corralesComida);
        }

        // GET: CorralesComidas/Create
        public ActionResult Create()
        {
            ViewBag.CorralId = new SelectList(db.Corrales, "CorralId", "CodigoCorral");
            ViewBag.OpcionId = new SelectList(db.Opciones, "OpcionId", "Codigopcion");
            return View();
        }

        // POST: CorralesComidas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CorralComidaId,CantidadCorralComida,FechaCorralComida,CorralId,OpcionId")] CorralesComida corralesComida)
        {
            if (ModelState.IsValid)
            {
                db.CorralesComidas.Add(corralesComida);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CorralId = new SelectList(db.Corrales, "CorralId", "CodigoCorral", corralesComida.CorralId);
            ViewBag.OpcionId = new SelectList(db.Opciones, "OpcionId", "Codigopcion", corralesComida.OpcionId);
            return View(corralesComida);
        }

        // GET: CorralesComidas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CorralesComida corralesComida = await db.CorralesComidas.FindAsync(id);
            if (corralesComida == null)
            {
                return HttpNotFound();
            }
            ViewBag.CorralId = new SelectList(db.Corrales, "CorralId", "CodigoCorral", corralesComida.CorralId);
            ViewBag.OpcionId = new SelectList(db.Opciones, "OpcionId", "Codigopcion", corralesComida.OpcionId);
            return View(corralesComida);
        }

        // POST: CorralesComidas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CorralComidaId,CantidadCorralComida,FechaCorralComida,CorralId,OpcionId")] CorralesComida corralesComida)
        {
            if (ModelState.IsValid)
            {
                db.Entry(corralesComida).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CorralId = new SelectList(db.Corrales, "CorralId", "CodigoCorral", corralesComida.CorralId);
            ViewBag.OpcionId = new SelectList(db.Opciones, "OpcionId", "Codigopcion", corralesComida.OpcionId);
            return View(corralesComida);
        }

        // GET: CorralesComidas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CorralesComida corralesComida = await db.CorralesComidas.FindAsync(id);
            if (corralesComida == null)
            {
                return HttpNotFound();
            }
            return View(corralesComida);
        }

        // POST: CorralesComidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CorralesComida corralesComida = await db.CorralesComidas.FindAsync(id);
            db.CorralesComidas.Remove(corralesComida);
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
