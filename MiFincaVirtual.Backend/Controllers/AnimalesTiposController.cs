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
    public class AnimalesTiposController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: AnimalesTipos
        public async Task<ActionResult> Index()
        {
            return View(await db.AnimalesTipos.ToListAsync());
        }

        // GET: AnimalesTipos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnimalesTipos animalesTipos = await db.AnimalesTipos.FindAsync(id);
            if (animalesTipos == null)
            {
                return HttpNotFound();
            }
            return View(animalesTipos);
        }

        // GET: AnimalesTipos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AnimalesTipos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AnimalTipoId,TipoAnimalTipo")] AnimalesTipos animalesTipos)
        {
            if (ModelState.IsValid)
            {
                db.AnimalesTipos.Add(animalesTipos);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(animalesTipos);
        }

        // GET: AnimalesTipos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnimalesTipos animalesTipos = await db.AnimalesTipos.FindAsync(id);
            if (animalesTipos == null)
            {
                return HttpNotFound();
            }
            return View(animalesTipos);
        }

        // POST: AnimalesTipos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AnimalTipoId,TipoAnimalTipo")] AnimalesTipos animalesTipos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(animalesTipos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(animalesTipos);
        }

        // GET: AnimalesTipos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnimalesTipos animalesTipos = await db.AnimalesTipos.FindAsync(id);
            if (animalesTipos == null)
            {
                return HttpNotFound();
            }
            return View(animalesTipos);
        }

        // POST: AnimalesTipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AnimalesTipos animalesTipos = await db.AnimalesTipos.FindAsync(id);
            db.AnimalesTipos.Remove(animalesTipos);
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
