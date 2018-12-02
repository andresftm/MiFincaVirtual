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
using System.IO;
using System.Runtime.Serialization.Json;
using MiFincaVirtual.Backend.Tools;
using System.Data.SqlClient;

namespace MiFincaVirtual.Backend.Controllers
{
    public class VacasCargadasController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: VacasCargadas
        public async Task<ActionResult> Index()
        {
            var vacasCargadas = db.VacasCargadas.Include(v => v.Animales);
            return View(await vacasCargadas.ToListAsync());
        }

        // GET: VacasCargadas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VacasCargadas vacasCargadas = await db.VacasCargadas.FindAsync(id);
            if (vacasCargadas == null)
            {
                return HttpNotFound();
            }
            return View(vacasCargadas);
        }

        // GET: VacasCargadas/Create
        public ActionResult Create()
        {
            List<Animales> lstAnimales = new List<Animales>();
            Animales objAnimal = new Animales();
            objAnimal.AnimalId = -1;
            objAnimal.CodigoAnimal = "-- Seleccione --";
            lstAnimales.Add(objAnimal);
            lstAnimales.AddRange(db.Animales.Where(O => O.Opciones.Codigopcion == "Bovino" && O.EshembraAnimal == true && O.EshembraGestanteAnimal == false).ToList());
            ViewBag.AnimalId = new SelectList(lstAnimales, "AnimalId", "CodigoAnimal");

            return View();
        }

        // POST: VacasCargadas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VacasCargadas vacasCargadas)
        {
            List<Animales> lstAnimales = new List<Animales>();
            Animales objAnimal = new Animales();

            if (ModelState.IsValid)
            {
                if (vacasCargadas.AnimalId == -1)
                {
                    objAnimal.AnimalId = -1;
                    objAnimal.CodigoAnimal = "-- Seleccione --";
                    lstAnimales.Add(objAnimal);
                    lstAnimales.AddRange(db.Animales.Where(O => O.Opciones.Codigopcion == "Bovino" && O.EshembraAnimal == true && O.EshembraGestanteAnimal == false).ToList());
                    ViewBag.AnimalId = new SelectList(lstAnimales, "AnimalId", "CodigoAnimal");

                    return View(vacasCargadas);
                }

                vacasCargadas.ActivoVacaCargada = true;
                vacasCargadas.FechaDesteteVacaCargada = Convert.ToDateTime("0001-01-01");
                vacasCargadas.FechaPosiblePartoVacaCargada = vacasCargadas.FechaMontaVacaCargada.AddDays(282);
                vacasCargadas.FechaRealPartoVacaCargada = Convert.ToDateTime("0001-01-01");
                vacasCargadas.FechaRecordacionVacaCargada = vacasCargadas.FechaMontaVacaCargada.AddDays(270);
                vacasCargadas.NacidosVacaCargada = 0;
                vacasCargadas.SexoCriaVacaCargada = false;

                vacasCargadas.FechaMontaVacaCargadaS = vacasCargadas.FechaMontaVacaCargada.ToString("yyyy-MM-dd");
                vacasCargadas.FechaDesteteVacaCargadaS = vacasCargadas.FechaDesteteVacaCargada.ToString("yyyy-MM-dd");
                vacasCargadas.FechaPosiblePartoVacaCargadaS = vacasCargadas.FechaPosiblePartoVacaCargada.ToString("yyyy-MM-dd");
                vacasCargadas.FechaRealPartoVacaCargadaS = vacasCargadas.FechaRealPartoVacaCargada.ToString("yyyy-MM-dd");
                vacasCargadas.FechaRecordacionVacaCargadaS = vacasCargadas.FechaRecordacionVacaCargada.ToString("yyyy-MM-dd");

                MemoryStream stream1 = new MemoryStream();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(VacasCargadas));
                ser.WriteObject(stream1, vacasCargadas);
                stream1.Position = 0;
                StreamReader sr = new StreamReader(stream1);
                String Resultado = sr.ReadToEnd();

                using (LocalDataContext db = new LocalDataContext())
                {
                    var Respuesta = db.Database.SqlQuery<Respuesta>(Sp.uspVacaGestanteInsertar + " @json", new SqlParameter("json", Resultado)).ToList();

                    if (Respuesta[0].Codigo == 1)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(vacasCargadas);
                    }
                }
            }

            objAnimal.AnimalId = -1;
            objAnimal.CodigoAnimal = "-- Seleccione --";
            lstAnimales.Add(objAnimal);
            lstAnimales.AddRange(db.Animales.Where(O => O.Opciones.Codigopcion == "Bovino" && O.EshembraAnimal == true && O.EshembraGestanteAnimal == false).ToList());
            ViewBag.AnimalId = new SelectList(lstAnimales, "AnimalId", "CodigoAnimal");

            return View(vacasCargadas);
        }

        // GET: VacasCargadas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VacasCargadas vacasCargadas = await db.VacasCargadas.FindAsync(id);
            if (vacasCargadas == null)
            {
                return HttpNotFound();
            }

            List<Animales> lstAnimales = new List<Animales>();
            Animales objAnimal = new Animales();
            objAnimal.AnimalId = -1;
            objAnimal.CodigoAnimal = "-- Seleccione --";
            lstAnimales.Add(objAnimal);
            lstAnimales.AddRange(db.Animales.Where(O => O.Opciones.Codigopcion == "Bovino" && O.EshembraAnimal == true && O.EshembraGestanteAnimal == true).ToList());
            ViewBag.AnimalId = new SelectList(lstAnimales, "AnimalId", "CodigoAnimal", vacasCargadas.AnimalId);
            @TempData["VacaCargadaId"] = vacasCargadas.VacaCargadaId;
            return View(vacasCargadas);
        }

        // POST: VacasCargadas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(VacasCargadas vacasCargadas)
        {
            if (ModelState.IsValid)
            {
                vacasCargadas.VacaCargadaId = Convert.ToInt32(@TempData["VacaCargadaId"]);

                if (vacasCargadas.FechaRealPartoVacaCargada.Year != 1)
                {
                    vacasCargadas.FechaDesteteVacaCargada = vacasCargadas.FechaRealPartoVacaCargada.AddDays(240);
                }

                vacasCargadas.FechaRealPartoVacaCargadaS = vacasCargadas.FechaRealPartoVacaCargada.ToString("yyyy-MM-dd");
                vacasCargadas.FechaDesteteVacaCargadaS = vacasCargadas.FechaDesteteVacaCargada.ToString("yyyy-MM-dd");

                MemoryStream stream1 = new MemoryStream();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(VacasCargadas));
                ser.WriteObject(stream1, vacasCargadas);
                stream1.Position = 0;
                StreamReader sr = new StreamReader(stream1);
                String Resultado = sr.ReadToEnd();

                using (LocalDataContext db = new LocalDataContext())
                {
                    var Respuesta = db.Database.SqlQuery<Respuesta>(Sp.uspVacaGestanteActualizar + " @json", new SqlParameter("json", Resultado)).ToList();

                    if (Respuesta[0].Codigo == 1)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(vacasCargadas);
                    }
                }
            }
            return View(vacasCargadas);
        }

        // GET: VacasCargadas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VacasCargadas vacasCargadas = await db.VacasCargadas.FindAsync(id);
            if (vacasCargadas == null)
            {
                return HttpNotFound();
            }
            return View(vacasCargadas);
        }

        // POST: VacasCargadas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (LocalDataContext db = new LocalDataContext())
            {
                var Respuesta = db.Database.SqlQuery<Respuesta>(Sp.uspVacaGestanteEliminar + " @VacaCargadaId", new SqlParameter("VacaCargadaId", id)).ToList();

                if (Respuesta[0].Codigo == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["msnVacasLactanciaEliminar"] = Mensajes.Mensaje0009;

                    VacasCargadas vacasCargadas = await db.VacasCargadas.FindAsync(id);
                    if (vacasCargadas == null)
                    {
                        return HttpNotFound();
                    }

                    return View(vacasCargadas);
                }

            }
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
