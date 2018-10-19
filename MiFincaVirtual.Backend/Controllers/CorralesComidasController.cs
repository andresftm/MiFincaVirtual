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
            List<Opciones> lstOpciones = new List<Opciones>();
            Opciones objOpcion = new Opciones();
            objOpcion.OpcionId = -1;
            objOpcion.Codigopcion = "-- Seleccione --";
            lstOpciones.Add(objOpcion);
            lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "CuidoCerdos").ToList());
            ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion");

            List<Corrales> lstCorrales = new List<Corrales>();
            Corrales objCorral = new Corrales();
            objCorral.CorralId = -1;
            objCorral.CodigoCorral = "-- Seleccione --";
            lstCorrales.Add(objCorral);
            lstCorrales.AddRange(db.Corrales.ToList());
            ViewBag.CorralId = new SelectList(lstCorrales, "CorralId", "CodigoCorral");

            return View();
        }

        // POST: CorralesComidas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CorralComidaId,CantidadCorralComida,FechaCorralComida,CorralId,OpcionId")] CorralesComida corralesComida)
        {
            List<Opciones> lstOpciones = new List<Opciones>();
            Opciones objOpcion = new Opciones();
            List<Corrales> lstCorrales = new List<Corrales>();
            Corrales objCorral = new Corrales();

            if (ModelState.IsValid)
            {
                if (corralesComida.OpcionId == -1)
                {
                    objOpcion.OpcionId = -1;
                    objOpcion.Codigopcion = "-- Seleccione --";
                    lstOpciones.Add(objOpcion);
                    lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "CuidoCerdos").ToList());
                    ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion");
                }

                if (corralesComida.CorralId == -1)
                {
                    objCorral.CorralId = -1;
                    objCorral.CodigoCorral = "-- Seleccione --";
                    lstCorrales.Add(objCorral);
                    lstCorrales.AddRange(db.Corrales.ToList());
                    ViewBag.CorralId = new SelectList(lstCorrales, "CorralId", "CodigoCorral");
                }

                db.CorralesComidas.Add(corralesComida);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            objCorral.CorralId = -1;
            objCorral.CodigoCorral = "-- Seleccione --";
            lstCorrales.Add(objCorral);
            lstCorrales.AddRange(db.Corrales.ToList());
            ViewBag.CorralId = new SelectList(lstCorrales, "CorralId", "CodigoCorral", corralesComida.CorralId);

            objOpcion.OpcionId = -1;
            objOpcion.Codigopcion = "-- Seleccione --";
            lstOpciones.Add(objOpcion);
            lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "CuidoCerdos").ToList());
            ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion", corralesComida.OpcionId);

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

            List<Corrales> lstCorrales = new List<Corrales>();
            Corrales objCorral = new Corrales();
            objCorral.CorralId = -1;
            objCorral.CodigoCorral = "-- Seleccione --";
            lstCorrales.Add(objCorral);
            lstCorrales.AddRange(db.Corrales.ToList());
            ViewBag.CorralId = new SelectList(lstCorrales, "CorralId", "CodigoCorral", corralesComida.CorralId);

            List<Opciones> lstOpciones = new List<Opciones>();
            Opciones objOpcion = new Opciones();
            objOpcion.OpcionId = -1;
            objOpcion.Codigopcion = "-- Seleccione --";
            lstOpciones.Add(objOpcion);
            lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "CuidoCerdos").ToList());
            ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion", corralesComida.OpcionId);
            return View(corralesComida);
        }

        // POST: CorralesComidas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CorralComidaId,CantidadCorralComida,FechaCorralComida,CorralId,OpcionId")] CorralesComida corralesComida)
        {
            List<Opciones> lstOpciones = new List<Opciones>();
            Opciones objOpcion = new Opciones();
            List<Corrales> lstCorrales = new List<Corrales>();
            Corrales objCorral = new Corrales();

            if (ModelState.IsValid)
            {
                if (corralesComida.OpcionId == -1)
                {
                    objOpcion.OpcionId = -1;
                    objOpcion.Codigopcion = "-- Seleccione --";
                    lstOpciones.Add(objOpcion);
                    lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "CuidoCerdos").ToList());
                    ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion", corralesComida.OpcionId);
                }

                if (corralesComida.CorralId == -1)
                {
                    objCorral.CorralId = -1;
                    objCorral.CodigoCorral = "-- Seleccione --";
                    lstCorrales.Add(objCorral);
                    lstCorrales.AddRange(db.Corrales.ToList());
                    ViewBag.CorralId = new SelectList(lstCorrales, "CorralId", "CodigoCorral", corralesComida.CorralId);
                }

                db.Entry(corralesComida).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            objCorral.CorralId = -1;
            objCorral.CodigoCorral = "-- Seleccione --";
            lstCorrales.Add(objCorral);
            lstCorrales.AddRange(db.Corrales.ToList());
            ViewBag.CorralId = new SelectList(lstCorrales, "CorralId", "CodigoCorral", corralesComida.CorralId);

            objOpcion.OpcionId = -1;
            objOpcion.Codigopcion = "-- Seleccione --";
            lstOpciones.Add(objOpcion);
            lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "CuidoCerdos").ToList());
            ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion", corralesComida.OpcionId);

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
