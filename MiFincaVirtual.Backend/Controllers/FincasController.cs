using MiFincaVirtual.Backend.Helpers;
using MiFincaVirtual.Backend.Models;
using MiFincaVirtual.Common.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MiFincaVirtual.Backend.Controllers
{
    [Authorize]
    public class FincasController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        public async Task<ActionResult> Index()
        {
            return View(await db.Fincas.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fincas fincas = await db.Fincas.FindAsync(id);
            if (fincas == null)
            {
                return HttpNotFound();
            }
            return View(fincas);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FincasView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Fincas";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var Finca = this.ToFinca(view, pic);

                db.Fincas.Add(Finca);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(view);
        }

        private Fincas ToFinca(FincasView view, string pic)
        {
            return new Fincas
            {
                CiudadFinca = view.CiudadFinca,
                EstadoFinca = view.EstadoFinca,
                HabilitadaFinca = view.HabilitadaFinca,
                ImagePath = pic,
                IngresoFinca = view.IngresoFinca,
                NombreFinca = view.NombreFinca,
                FincaId = view.FincaId,
                PaisFinca = view.PaisFinca,
            };
        }
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fincas fincas = await db.Fincas.FindAsync(id);
            if (fincas == null)
            {
                return HttpNotFound();
            }
            var fincaview = this.ToView(fincas);
            return View(fincaview);
        }
        private FincasView ToView(Fincas finca)
        {
            return new FincasView
            {
                CiudadFinca = finca.CiudadFinca,
                EstadoFinca = finca.EstadoFinca,
                HabilitadaFinca = finca.HabilitadaFinca,
                ImagePath = finca.ImagePath,
                IngresoFinca = finca.IngresoFinca,
                NombreFinca = finca.NombreFinca,
                FincaId = finca.FincaId,
                PaisFinca = finca.PaisFinca,
                
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(FincasView view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.ImagePath;
                var folder = "~/Content/Fincas";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var Finca = this.ToFinca(view, pic);
                db.Entry(Finca).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(view);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fincas fincas = await db.Fincas.FindAsync(id);
            if (fincas == null)
            {
                return HttpNotFound();
            }
            return View(fincas);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Fincas fincas = await db.Fincas.FindAsync(id);
            db.Fincas.Remove(fincas);
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
