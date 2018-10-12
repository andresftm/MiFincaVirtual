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
    public class OpcionesController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Opciones
        public async Task<ActionResult> Index()
        {
            return View(await db.Opciones.ToListAsync());
        }

        // GET: Opciones/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opciones opciones = await db.Opciones.FindAsync(id);
            if (opciones == null)
            {
                return HttpNotFound();
            }
            return View(opciones);
        }

        // GET: Opciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Opciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "OpcionId,Codigopcion,DescripcionOpcion,TipoOpcion")] Opciones opciones)
        {
            if (ModelState.IsValid)
            {
                db.Opciones.Add(opciones);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(opciones);
        }

        // GET: Opciones/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opciones opciones = await db.Opciones.FindAsync(id);
            if (opciones == null)
            {
                return HttpNotFound();
            }
            return View(opciones);
        }

        // POST: Opciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OpcionId,Codigopcion,DescripcionOpcion,TipoOpcion")] Opciones opciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(opciones).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(opciones);
        }

        // GET: Opciones/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opciones opciones = await db.Opciones.FindAsync(id);
            if (opciones == null)
            {
                return HttpNotFound();
            }
            return View(opciones);
        }

        // POST: Opciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Opciones opciones = await db.Opciones.FindAsync(id);
            db.Opciones.Remove(opciones);
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
