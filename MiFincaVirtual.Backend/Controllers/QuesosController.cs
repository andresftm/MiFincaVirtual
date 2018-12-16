namespace MiFincaVirtual.Backend.Controllers
{
    using MiFincaVirtual.Backend.Models;
    using MiFincaVirtual.Common.Models;
    using System.Data.Entity;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    [Authorize]
    public class QuesosController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Quesos
        public async Task<ActionResult> Index()
        {
            return View(await db.Quesos.ToListAsync());
        }

        // GET: Quesos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quesos quesos = await db.Quesos.FindAsync(id);
            if (quesos == null)
            {
                return HttpNotFound();
            }
            return View(quesos);
        }

        // GET: Quesos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Quesos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( Quesos quesos)
        {
            if (ModelState.IsValid)
            {
                db.Quesos.Add(quesos);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(quesos);
        }

        // GET: Quesos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quesos quesos = await db.Quesos.FindAsync(id);
            if (quesos == null)
            {
                return HttpNotFound();
            }
            return View(quesos);
        }

        // POST: Quesos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( Quesos quesos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quesos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(quesos);
        }

        // GET: Quesos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quesos quesos = await db.Quesos.FindAsync(id);
            if (quesos == null)
            {
                return HttpNotFound();
            }
            return View(quesos);
        }

        // POST: Quesos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Quesos quesos = await db.Quesos.FindAsync(id);
            db.Quesos.Remove(quesos);
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
