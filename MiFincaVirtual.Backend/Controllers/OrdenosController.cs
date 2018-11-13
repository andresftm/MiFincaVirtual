namespace MiFincaVirtual.Backend.Controllers
{
    using MiFincaVirtual.Backend.Models;
    using MiFincaVirtual.Backend.Tools;
    using MiFincaVirtual.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Data.Entity;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.Routing;

    [Authorize]
    public class OrdenosController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        public ActionResult Index(string animal, int pagina = 1)
        {
            var cantidadRegistrosPorPagina = 10; // parámetro

            using (var db = new LocalDataContext())
            {
                var ordenos = db.Ordenos.Where(o => o.Animales.CodigoAnimal == animal).OrderByDescending(o => o.FechaOrdeno)
                    .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                    .Take(cantidadRegistrosPorPagina).ToList();

                if (String.IsNullOrEmpty(animal))
                {
                    ordenos = db.Ordenos.OrderByDescending(o => o.FechaOrdeno)
                                        .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                                        .Take(cantidadRegistrosPorPagina).ToList();
                }

                var totalDeRegistros = db.Ordenos.Count();

                var modelo = new ordenosPaginados();
                modelo.PaginaActual = pagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                modelo.ValoresQueryString = new RouteValueDictionary();
                modelo.ValoresQueryString["animal"] = animal;

                var lstOrdenos = ordenos.Select(o => new Ordenos()
                {
                    FechaOrdeno = o.FechaOrdeno,
                    LitrosOrdeno = o.LitrosOrdeno,
                    NumeroOrdeno = o.NumeroOrdeno,
                    PesoOrdeno = o.PesoOrdeno,
                    OrdenoId = o.OrdenoId,
                    GramosCuidoOrdeno = o.GramosCuidoOrdeno,
                    Animal = o.Animales.CodigoAnimal,
                });
                modelo.OrdenosO = new ObservableCollection<Ordenos>(lstOrdenos);

                return View(modelo);
            }
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordenos ordenos = await db.Ordenos.FindAsync(id);
            if (ordenos == null)
            {
                return HttpNotFound();
            }
            return View(ordenos);
        }

        public ActionResult Create()
        {
            List<Animales> lstAnimales = new List<Animales>();
            Animales objAnimal = new Animales();
            objAnimal.AnimalId = -1;
            objAnimal.CodigoAnimal = "-- Seleccione --";
            lstAnimales.Add(objAnimal);
            lstAnimales.AddRange(db.Animales.Where(O => O.Opciones.Codigopcion == "Bovino" && O.EshembraAnimal == true && O.EshembraGestanteAnimal == true).ToList());
            ViewBag.AnimalId = new SelectList(lstAnimales, "AnimalId", "CodigoAnimal");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Ordenos ordenos)
        {
            List<Animales> lstAnimales = new List<Animales>();
            Animales objAnimal = new Animales();
            if (ModelState.IsValid)
            {
                if(ordenos.AnimalId == -1)
                {
                    objAnimal.AnimalId = -1;
                    objAnimal.CodigoAnimal = "-- Seleccione --";
                    lstAnimales.Add(objAnimal);
                    lstAnimales.AddRange(db.Animales.Where(O => O.Opciones.Codigopcion == "Bovino" && O.EshembraAnimal == true && O.EshembraGestanteAnimal == true).ToList());
                    ViewBag.AnimalId = new SelectList(lstAnimales, "AnimalId", "CodigoAnimal");

                    return View(ordenos);
                }

                db.Ordenos.Add(ordenos);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            objAnimal.AnimalId = -1;
            objAnimal.CodigoAnimal = "-- Seleccione --";
            lstAnimales.Add(objAnimal);
            lstAnimales.AddRange(db.Animales.Where(O => O.Opciones.Codigopcion == "Bovino" && O.EshembraAnimal == true && O.EshembraGestanteAnimal == true).ToList());
            ViewBag.AnimalId = new SelectList(lstAnimales, "AnimalId", "CodigoAnimal");

            return View(ordenos);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordenos ordenos = await db.Ordenos.FindAsync(id);
            if (ordenos == null)
            {
                return HttpNotFound();
            }
            return View(ordenos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Ordenos ordenos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordenos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(ordenos);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordenos ordenos = await db.Ordenos.FindAsync(id);
            if (ordenos == null)
            {
                return HttpNotFound();
            }
            return View(ordenos);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Ordenos ordenos = await db.Ordenos.FindAsync(id);
            db.Ordenos.Remove(ordenos);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> ConsultarOrdenos()
        {
            return View();
        }

        [HttpPost, ActionName("ConsultarOrdenosLista")]
        public async Task<ActionResult> ConsultarOrdenosLista(OrdenosxFechaConsulta objOrdenosxFechaConsulta)
        {
            List<OrdenosxFechaConsulta> Respuesta = new List<OrdenosxFechaConsulta>();
            using (LocalDataContext db = new LocalDataContext())
            {
                SqlParameter prFechaIni = new SqlParameter("FechaIni", objOrdenosxFechaConsulta.FechaInicial.ToString("yyyy-MM-dd"));
                SqlParameter prFechaFin = new SqlParameter("FechaFin", objOrdenosxFechaConsulta.FechaFinal.ToString("yyyy-MM-dd"));

                Respuesta = db.Database.SqlQuery<OrdenosxFechaConsulta>(Sp.uspOrdenosEntreFechasConsultar + " @FechaIni, @FechaFin", prFechaIni, prFechaFin).ToList();
            }
            return View(Respuesta);
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
