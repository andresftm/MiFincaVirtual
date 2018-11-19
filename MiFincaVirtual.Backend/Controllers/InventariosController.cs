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
using System.IO;
using System.Runtime.Serialization.Json;
using MiFincaVirtual.Backend.Tools;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;

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
                var folder = "~/Images";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var Inventario = this.ToInventario(view, pic);

                Inventario.ValorTotalInventario = Inventario.PrecioInventario + Inventario.FleteInventario;
                Inventario.ValorUnitarioInventario = Inventario.ValorTotalInventario / Inventario.CantidadInventario;

                MemoryStream stream1 = new MemoryStream();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Inventarios));
                ser.WriteObject(stream1, Inventario);
                stream1.Position = 0;
                StreamReader sr = new StreamReader(stream1);
                String Resultado = sr.ReadToEnd();

                using (LocalDataContext db = new LocalDataContext())
                {
                    var Respuesta = db.Database.SqlQuery<Respuesta>(Sp.uspInventarioInsertar + " @json", new SqlParameter("json", Resultado)).ToList();
                }

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
                ValorTotalInventario = view.ValorTotalInventario,
                ValorUnitarioInventario = view.ValorUnitarioInventario,
                FechaIngresoS = view.FechaIngreso.ToString("yyyy-MM-dd"),
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
            if( view.FechaIngreso.Year != 1 && view.ImageFile != null)
            {
                var pic = view.ImagePath;
                var folder = "~/Images";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var Inventario = this.ToInventario(view, pic);

                MemoryStream stream1 = new MemoryStream();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Inventarios));
                ser.WriteObject(stream1, Inventario);
                stream1.Position = 0;
                StreamReader sr = new StreamReader(stream1);
                String Resultado = sr.ReadToEnd();

                using (LocalDataContext db = new LocalDataContext())
                {
                    var Respuesta = db.Database.SqlQuery<Respuesta>(Sp.uspInventarioModificar + " @json", new SqlParameter("json", Resultado)).ToList();
                }

                return RedirectToAction("Index");
            }

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
            List<Respuesta> Respuesta = new List<Respuesta>();
            using (LocalDataContext db = new LocalDataContext())
            {
                Respuesta = db.Database.SqlQuery<Respuesta>(Sp.uspInventarioEliminar + " @InventarioId", new SqlParameter("InventarioId", id)).ToList();
            }

            if (Respuesta[0].Codigo == 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Inventarios inventarios = db.Inventarios.Find(id);
                if (inventarios == null)
                {
                    return HttpNotFound();
                }
                return View(inventarios);
            }
        }

        public async Task<ActionResult> Disponibilidad()
        {
            List<disponibilidadCuido> Respuesta = new List<disponibilidadCuido>();
            using (LocalDataContext db = new LocalDataContext())
            {
                 Respuesta = db.Database.SqlQuery<disponibilidadCuido>(Sp.uspInventarioDisponibleConsultar).ToList();
            }

            return View(Respuesta);
        }

        public ActionResult Reports(String reportType)
        {
            List<disponibilidadCuido> Respuesta = new List<disponibilidadCuido>();
            using (LocalDataContext db = new LocalDataContext())
            {
                Respuesta = db.Database.SqlQuery<disponibilidadCuido>(Sp.uspInventarioDisponibleConsultar).ToList();
            }

            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reports/Inventarios/rptDisponibilidad.rdlc");
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "dsInventarioDisponibleConsultar";
            reportDataSource.Value = Respuesta;
            localReport.DataSources.Add(reportDataSource);

            String nimeType = String.Empty;
            String encoding = String.Empty;
            String fileNameExtencion = String.Empty;

            switch (reportType)
            {
                case "Excel":
                    fileNameExtencion = "xls";
                    break;
                case "Pdf":
                    fileNameExtencion = "pdf";
                    break;
            }

            String[] stream;
            Warning[] warning;
            byte[] renderedByte;
            renderedByte = localReport.Render(reportType, "", out nimeType, out encoding, out fileNameExtencion, out stream, out warning);
            //Response.AddHeader("content-disposition", "attachment:filename= ordenos_report." + fileNameExtencion);
            if (reportType == "Excel")
            {
                return File(renderedByte, "application/Excel", "report_DisponibilidadCuidos." + fileNameExtencion);
            }
            else
            {
                return File(renderedByte, "application/Pdf", "report_DisponibilidadCuidos." + fileNameExtencion);
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
