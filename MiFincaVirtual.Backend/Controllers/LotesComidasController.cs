namespace MiFincaVirtual.Backend.Controllers
{
    using MiFincaVirtual.Backend.Models;
    using MiFincaVirtual.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class LotesComidasController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        public JsonResult GetDisponibilidadCuidos(String  cuido)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var disponibilidadCuido = db.Inventarios.Where(i => i.SaldoInventario > 0 && i.Opciones.Codigopcion == cuido);
            return Json(disponibilidadCuido);
        }

        // GET: LotesComidas
        public async Task<ActionResult> Index()
        {
            var LotesComidas = db.LotesComidas.Include(a => a.Lotes).Include(a => a.Opciones);
            return View(await LotesComidas.ToListAsync());
        }

        // GET: LotesComidas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LotesComida lotesComida = await db.LotesComidas.FindAsync(id);
            if (lotesComida == null)
            {
                return HttpNotFound();
            }
            return View(lotesComida);
        }

        // GET: LotesComidas/Create
        public ActionResult Create()
        {
            List<Opciones> lstOpciones = new List<Opciones>();
            Opciones objOpcion = new Opciones();
            objOpcion.OpcionId = -1;
            objOpcion.Codigopcion = "-- Seleccione --";
            lstOpciones.Add(objOpcion);
            lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "CuidoCerdos").ToList());
            ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion");

            List<Lotes> lstLotes = new List<Lotes>();
            Lotes objLote = new Lotes();
            objLote.LoteId = -1;
            objLote.NombreLote = "-- Seleccione --";
            lstLotes.Add(objLote);
            lstLotes.AddRange(db.Lotes);
            ViewBag.LoteId = new SelectList(lstLotes, "LoteId", "NombreLote");

            return View();
        }

        // POST: LotesComidas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LotesComida lotesComida)
        {
            List<Opciones> lstOpciones = new List<Opciones>();
            Opciones objOpcion = new Opciones();
            List<Lotes> lstLotes = new List<Lotes>();
            Lotes objLote = new Lotes();

            if (ModelState.IsValid)
            {
                if(lotesComida.LoteId == -1 || lotesComida.OpcionId == 1)
                {
                    objOpcion.OpcionId = -1;
                    objOpcion.Codigopcion = "-- Seleccione --";
                    lstOpciones.Add(objOpcion);
                    lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "CuidoCerdos").ToList());
                    ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion");

                    objLote.LoteId = -1;
                    objLote.NombreLote = "-- Seleccione --";
                    lstLotes.Add(objLote);
                    ViewBag.LoteId = new SelectList(lstLotes, "LoteId", "NombreLote");

                    return View(lotesComida);
                }

                db.LotesComidas.Add(lotesComida);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            objOpcion.OpcionId = -1;
            objOpcion.Codigopcion = "-- Seleccione --";
            lstOpciones.Add(objOpcion);
            lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "CuidoCerdos").ToList());
            ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion", lotesComida.OpcionId);

            objLote.LoteId = -1;
            objLote.NombreLote = "-- Seleccione --";
            lstLotes.Add(objLote);
            ViewBag.LoteId = new SelectList(lstLotes, "LoteId", "NombreLote", lotesComida.LoteId);

            return View(lotesComida);
        }

        // GET: LotesComidas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LotesComida lotesComida = await db.LotesComidas.FindAsync(id);
            if (lotesComida == null)
            {
                return HttpNotFound();
            }

            List<Opciones> lstOpciones = new List<Opciones>();
            Opciones objOpcion = new Opciones();
            objOpcion.OpcionId = -1;
            objOpcion.Codigopcion = "-- Seleccione --";
            lstOpciones.Add(objOpcion);
            lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "CuidoCerdos").ToList());
            ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion", lotesComida.OpcionId);

            List<Lotes> lstLotes = new List<Lotes>();
            Lotes objLote = new Lotes();
            objLote.LoteId = -1;
            objLote.NombreLote = "-- Seleccione --";
            lstLotes.Add(objLote);
            lstLotes.AddRange(db.Lotes);
            ViewBag.LoteId = new SelectList(lstLotes, "LoteId", "NombreLote", lotesComida.LoteId);

            return View(lotesComida);
        }

        // POST: LotesComidas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LotesComida lotesComida)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lotesComida).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(lotesComida);
        }

        // GET: LotesComidas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LotesComida lotesComida = await db.LotesComidas.FindAsync(id);
            if (lotesComida == null)
            {
                return HttpNotFound();
            }
            return View(lotesComida);
        }

        // POST: LotesComidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LotesComida lotesComida = await db.LotesComidas.FindAsync(id);
            db.LotesComidas.Remove(lotesComida);
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
