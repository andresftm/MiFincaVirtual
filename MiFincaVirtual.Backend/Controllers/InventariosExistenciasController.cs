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
    public class InventariosExistenciasController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: InventariosExistencias
        public async Task<ActionResult> Index()
        {
            var inventariosExistencias = db.InventariosExistencias.Include(i => i.Opciones);
            return View(await inventariosExistencias.ToListAsync());
        }

        // GET: InventariosExistencias/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventariosExistencias inventariosExistencias = await db.InventariosExistencias.FindAsync(id);
            if (inventariosExistencias == null)
            {
                return HttpNotFound();
            }
            return View(inventariosExistencias);
        }

        // GET: InventariosExistencias/Create
        public ActionResult Create()
        {
            ViewBag.OpcionId = new SelectList(db.Opciones, "OpcionId", "Codigopcion");
            return View();
        }

        // POST: InventariosExistencias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "InventarioExistenciaId,OpcionId,GramosConsumoDiaLote,ValorUnitarioInventario")] InventariosExistencias inventariosExistencias)
        {
            if (ModelState.IsValid)
            {
                db.InventariosExistencias.Add(inventariosExistencias);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.OpcionId = new SelectList(db.Opciones, "OpcionId", "Codigopcion", inventariosExistencias.OpcionId);
            return View(inventariosExistencias);
        }

        // GET: InventariosExistencias/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventariosExistencias inventariosExistencias = await db.InventariosExistencias.FindAsync(id);
            if (inventariosExistencias == null)
            {
                return HttpNotFound();
            }
            ViewBag.OpcionId = new SelectList(db.Opciones, "OpcionId", "Codigopcion", inventariosExistencias.OpcionId);
            return View(inventariosExistencias);
        }

        // POST: InventariosExistencias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "InventarioExistenciaId,OpcionId,GramosConsumoDiaLote,ValorUnitarioInventario")] InventariosExistencias inventariosExistencias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventariosExistencias).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.OpcionId = new SelectList(db.Opciones, "OpcionId", "Codigopcion", inventariosExistencias.OpcionId);
            return View(inventariosExistencias);
        }

        // GET: InventariosExistencias/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventariosExistencias inventariosExistencias = await db.InventariosExistencias.FindAsync(id);
            if (inventariosExistencias == null)
            {
                return HttpNotFound();
            }
            return View(inventariosExistencias);
        }

        // POST: InventariosExistencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            InventariosExistencias inventariosExistencias = await db.InventariosExistencias.FindAsync(id);
            db.InventariosExistencias.Remove(inventariosExistencias);
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
