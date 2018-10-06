namespace MiFincaVirtual.Api.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using MiFincaVirtual.Api.Helpers;
    using MiFincaVirtual.Common.Models;
    using MiFincaVirtual.Domain.Models;

    [Authorize]
    public class FincasController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Fincas
        public IQueryable<Fincas> GetFincas()
        {
            return db.Fincas;
        }

        // GET: api/Fincas/5
        [ResponseType(typeof(Fincas))]
        public async Task<IHttpActionResult> GetFincas(int id)
        {
            Fincas fincas = await db.Fincas.FindAsync(id);
            if (fincas == null)
            {
                return NotFound();
            }

            return Ok(fincas);
        }

        // PUT: api/Fincas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFincas(int id, Fincas fincas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fincas.FincaId)
            {
                return BadRequest();
            }

            if (fincas.ImageArray != null && fincas.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(fincas.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "~/Content/Fincas";
                var fullPath = $"{folder}/{file}";
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    fincas.ImagePath = fullPath;
                }
            }

            db.Entry(fincas).State = EntityState.Modified;

            try
            {


                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FincasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(fincas);
        }

        // POST: api/Fincas
        [ResponseType(typeof(Fincas))]
        public async Task<IHttpActionResult> PostFincas(Fincas fincas)
        {
            //fincas.IngresoFinca = DateTime.Now.ToUniversalTime();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            fincas.ImagePath = "la cargo";

            //if (fincas.ImageArray != null && fincas.ImageArray.Length > 0)
            //{
            //    var stream = new MemoryStream(fincas.ImageArray);
            //    var guid = Guid.NewGuid().ToString();
            //    var file = $"{guid}.jpg";
            //    var folder = "~/Content/Fincas";
            //    var fullPath = $"{folder}/{file}";
            //    var response = FilesHelper.UploadPhoto(stream, folder, file);

            //    if (response)
            //    {
            //        fincas.ImagePath = fullPath;
            //    }
            //}

            db.Fincas.Add(fincas);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = fincas.FincaId }, fincas);
        }

        // DELETE: api/Fincas/5
        [ResponseType(typeof(Fincas))]
        public async Task<IHttpActionResult> DeleteFincas(int id)
        {
            Fincas fincas = await db.Fincas.FindAsync(id);
            if (fincas == null)
            {
                return NotFound();
            }

            db.Fincas.Remove(fincas);
            await db.SaveChangesAsync();

            return Ok(fincas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FincasExists(int id)
        {
            return db.Fincas.Count(e => e.FincaId == id) > 0;
        }
    }
}