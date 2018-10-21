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
using MiFincaVirtual.Backend.Helpers;

namespace MiFincaVirtual.Backend.Controllers
{
    [Authorize]
    public class InventariosController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Inventarios
        public async Task<ActionResult> Index()
        {
            var inventarios = db.Inventarios.Include(i => i.Opciones);
            return View(await inventarios.OrderByDescending(i => i.FechaIngreso).ToListAsync());
        }

        // GET: Inventarios/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventarios inventarios = await db.Inventarios.FindAsync(id);
            if (inventarios == null)
            {
                return HttpNotFound();
            }
            return View(inventarios);
        }

        // GET: Inventarios/Create
        public ActionResult Create()
        {
            List<Opciones> lstOpciones = new List<Opciones>();
            Opciones objOpcion = new Opciones();
            objOpcion.OpcionId = -1;
            objOpcion.Codigopcion = "-- Seleccione --";
            lstOpciones.Add(objOpcion);
            lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "CuidoCerdos").ToList());
            ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion");

            return View();
        }

        // POST: Inventarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(InventariosView view)
        {
            List<Opciones> lstOpciones = new List<Opciones>();
            Opciones objOpcion = new Opciones();

            if (ModelState.IsValid)
            {
                if (view.OpcionId == -1)
                {
                    objOpcion.OpcionId = -1;
                    objOpcion.Codigopcion = "-- Seleccione --";
                    lstOpciones.Add(objOpcion);
                    lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "CuidoCerdos").ToList());
                    ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion");

                    return View(view);
                }

                var pic = string.Empty;
                var folder = "~/Content/Inventarios";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var Inventario = this.ToInventario(view, pic);

                Inventario.RepartidoInventario = 0;
                Inventario.ValorTotalInventario = Inventario.PrecioInventario + Inventario.FleteInventario;
                Inventario.ValorUnitarioInventario = Inventario.ValorTotalInventario / Inventario.CantidadInventario;

                db.Inventarios.Add(Inventario);

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            objOpcion.OpcionId = -1;
            objOpcion.Codigopcion = "-- Seleccione --";
            lstOpciones.Add(objOpcion);
            lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "CuidoCerdos").ToList());
            ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion", view.OpcionId);

            return View(view);
        }

        private Inventarios ToInventario(InventariosView view, string pic)
        {
            return new Inventarios
            {
                CantidadInventario = view.CantidadInventario,
                FechaIngreso = view.FechaIngreso,
                FleteInventario = view.FleteInventario,
                ImagePath = pic,
                InventarioId = view.InventarioId,
                OpcionId = view.OpcionId,
                Opciones = view.Opciones,
                PrecioInventario = view.PrecioInventario,
                RepartidoInventario = view.RepartidoInventario,
                ValorTotalInventario = view.ValorTotalInventario,
                ValorUnitarioInventario = view.ValorUnitarioInventario,
            };
        }

        // GET: Inventarios/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventarios inventarios = await db.Inventarios.FindAsync(id);
            if (inventarios == null)
            {
                return HttpNotFound();
            }

            List<Opciones> lstOpciones = new List<Opciones>();
            Opciones objOpcion = new Opciones();
            objOpcion.OpcionId = -1;
            objOpcion.Codigopcion = "-- Seleccione --";
            lstOpciones.Add(objOpcion);
            lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "CuidoCerdos").ToList());
            ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion", inventarios.OpcionId);
            var inventariosView = this.ToView(inventarios);

            return View(inventariosView);
        }

        private InventariosView ToView(Inventarios inventario)
        {
            return new InventariosView
            {
                CantidadInventario = inventario.CantidadInventario,
                FechaIngreso = inventario.FechaIngreso,
                FleteInventario = inventario.FleteInventario,
                ImagePath = inventario.ImagePath,
                PrecioInventario = inventario.PrecioInventario,
                RepartidoInventario = inventario.RepartidoInventario,
                ValorTotalInventario = inventario.ValorTotalInventario,
                ValorUnitarioInventario = inventario.ValorUnitarioInventario,
                InventarioId = inventario.InventarioId,
                OpcionId = inventario.OpcionId,
            };
        }
        // POST: Inventarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit (InventariosView view)
        {
            List<Opciones> lstOpciones = new List<Opciones>();
            Opciones objOpcion = new Opciones();

            if (ModelState.IsValid)
            {
                if (view.OpcionId == -1)
                {
                    objOpcion.OpcionId = -1;
                    objOpcion.Codigopcion = "-- Seleccione --";
                    lstOpciones.Add(objOpcion);
                    lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "CuidoCerdos").ToList());
                    ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion", view.OpcionId);
                }

                var pic = view.ImagePath;
                var folder = "~/Content/Inventarios";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var Inventario = this.ToInventario(view, pic);


                db.Entry(Inventario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            objOpcion.OpcionId = -1;
            objOpcion.Codigopcion = "-- Seleccione --";
            lstOpciones.Add(objOpcion);
            lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "CuidoCerdos").ToList());
            ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion", view.OpcionId);

            return View(view);
        }

        // GET: Inventarios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventarios inventarios = await db.Inventarios.FindAsync(id);
            if (inventarios == null)
            {
                return HttpNotFound();
            }
            return View(inventarios);
        }

        // POST: Inventarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Inventarios inventarios = await db.Inventarios.FindAsync(id);
            db.Inventarios.Remove(inventarios);
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
