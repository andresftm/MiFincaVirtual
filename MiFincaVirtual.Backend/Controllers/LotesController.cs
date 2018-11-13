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

    [Authorize]
    public class LotesController : Controller
    {
        #region Metods
        private String fechaString(DateTime fecha)
        {
            String Año = fecha.Year.ToString();

            String Mes = fecha.Month.ToString();
            if(Mes.Length ==1)
            {
                Mes = "0" + Mes;
            }

            String Dia = fecha.Day.ToString();
            if (Dia.Length == 1)
            {
                Dia = "0" + Dia;
            }

            return Año + "-" + Mes + "-" + Dia;
        }
        #endregion

        private LocalDataContext db = new LocalDataContext();

        // GET: Lotes
        public async Task<ActionResult> Index()
        {
            return View(await db.Lotes.Where(L => L.CerradoLote == false).ToListAsync());
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

            List<Opciones> lstOpcionesCuido = new List<Opciones>();
            Opciones objOpcionCuido = new Opciones();
            objOpcionCuido.OpcionId = -1;
            objOpcionCuido.Codigopcion = "-- Seleccione --";
            lstOpcionesCuido.Add(objOpcionCuido);
            lstOpcionesCuido.AddRange(db.Opciones.Where(O => O.TipoOpcion == "CuidoCerdos").ToList());
            ViewBag.CuidoId = new SelectList(lstOpcionesCuido, "OpcionId", "Codigopcion");

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
            List<Opciones> lstOpcionesCuido = new List<Opciones>();
            Opciones objOpcionCuido = new Opciones();

            if (ModelState.IsValid)
            {
                if (lotes.OpcionId == -1 || lotes.CuidoId == -1)
                {
                    objOpcion.OpcionId = -1;
                    objOpcion.Codigopcion = "-- Seleccione --";
                    lstOpciones.Add(objOpcion);
                    lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "TiposAnimales").ToList());
                    ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion", lotes.OpcionId);

                    objOpcionCuido.OpcionId = -1;
                    objOpcionCuido.Codigopcion = "-- Seleccione --";
                    lstOpcionesCuido.Add(objOpcionCuido);
                    lstOpcionesCuido.AddRange(db.Opciones.Where(O => O.TipoOpcion == "CuidoCerdos").ToList());
                    ViewBag.CuidoId = new SelectList(lstOpcionesCuido, "OpcionId", "Codigopcion", lotes.CuidoId);

                    return View(lotes);
                }

                lotes.CerradoLote = false;
                lotes.FechaFinalLote = Convert.ToDateTime("1900-01-01");
                //lotes.FechaFinal = this.fechaString(lotes.FechaFinalLote);
                //lotes.FechaApertura = this.fechaString(lotes.FechaAperturaLote);

                //MemoryStream stream1 = new MemoryStream();
                //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Lotes));
                //ser.WriteObject(stream1, lotes);
                //stream1.Position = 0;
                //StreamReader sr = new StreamReader(stream1);
                //String Resultado = sr.ReadToEnd();

                db.Lotes.Add(lotes);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            objOpcion.OpcionId = -1;
            objOpcion.Codigopcion = "-- Seleccione --";
            lstOpciones.Add(objOpcion);
            lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "TiposAnimales").ToList());
            ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion", lotes.OpcionId);

            objOpcionCuido.OpcionId = -1;
            objOpcionCuido.Codigopcion = "-- Seleccione --";
            lstOpcionesCuido.Add(objOpcionCuido);
            lstOpcionesCuido.AddRange(db.Opciones.Where(O => O.TipoOpcion == "CuidoCerdos").ToList());
            ViewBag.CuidoId = new SelectList(lstOpcionesCuido, "OpcionId", "Codigopcion", lotes.CuidoId);

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

            List<Opciones> lstOpcionesCuido = new List<Opciones>();
            Opciones objOpcionCuido = new Opciones();
            objOpcionCuido.OpcionId = -1;
            objOpcionCuido.Codigopcion = "-- Seleccione --";
            lstOpcionesCuido.Add(objOpcionCuido);
            lstOpcionesCuido.AddRange(db.Opciones.Where(O => O.TipoOpcion == "CuidoCerdos").ToList());
            ViewBag.CuidoId = new SelectList(lstOpcionesCuido, "OpcionId", "Codigopcion", lotes.CuidoId);

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
            List<Opciones> lstOpcionesCuido = new List<Opciones>();
            Opciones objOpcionCuido = new Opciones();

            if (ModelState.IsValid)
            {
                if (lotes.OpcionId == -1 || lotes.CuidoId == -1)
                {
                    objOpcion.OpcionId = -1;
                    objOpcion.Codigopcion = "-- Seleccione --";
                    lstOpciones.Add(objOpcion);
                    lstOpciones.AddRange(db.Opciones.Where(O => O.TipoOpcion == "TiposAnimales").ToList());
                    ViewBag.OpcionId = new SelectList(lstOpciones, "OpcionId", "Codigopcion", lotes.OpcionId);

                    objOpcionCuido.OpcionId = -1;
                    objOpcionCuido.Codigopcion = "-- Seleccione --";
                    lstOpcionesCuido.Add(objOpcionCuido);
                    lstOpcionesCuido.AddRange(db.Opciones.Where(O => O.TipoOpcion == "CuidoCerdos").ToList());
                    ViewBag.CuidoId = new SelectList(lstOpcionesCuido, "OpcionId", "Codigopcion", lotes.CuidoId);

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

            objOpcionCuido.OpcionId = -1;
            objOpcionCuido.Codigopcion = "-- Seleccione --";
            lstOpcionesCuido.Add(objOpcionCuido);
            lstOpcionesCuido.AddRange(db.Opciones.Where(O => O.TipoOpcion == "CuidoCerdos").ToList());
            ViewBag.CuidoId = new SelectList(lstOpcionesCuido, "OpcionId", "Codigopcion", lotes.CuidoId);

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
