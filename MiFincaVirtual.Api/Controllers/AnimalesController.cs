using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MiFincaVirtual.Common.Models;
using MiFincaVirtual.Domain.Models;

namespace MiFincaVirtual.Api.Controllers
{
    public class AnimalesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Animales
        public IQueryable<Animales> GetAnimales()
        {
            return db.Animales;
        }

        // GET: api/Animales/5
        [ResponseType(typeof(Animales))]
        public async Task<IHttpActionResult> GetAnimales(int id)
        {
            Animales animales = await db.Animales.FindAsync(id);
            if (animales == null)
            {
                return NotFound();
            }

            return Ok(animales);
        }

        // PUT: api/Animales/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAnimales(int id, Animales animales)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != animales.AnimalId)
            {
                return BadRequest();
            }

            db.Entry(animales).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Animales
        [ResponseType(typeof(Animales))]
        public async Task<IHttpActionResult> PostAnimales(Animales animales)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Animales.Add(animales);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = animales.AnimalId }, animales);
        }

        // DELETE: api/Animales/5
        [ResponseType(typeof(Animales))]
        public async Task<IHttpActionResult> DeleteAnimales(int id)
        {
            Animales animales = await db.Animales.FindAsync(id);
            if (animales == null)
            {
                return NotFound();
            }

            db.Animales.Remove(animales);
            await db.SaveChangesAsync();

            return Ok(animales);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnimalesExists(int id)
        {
            return db.Animales.Count(e => e.AnimalId == id) > 0;
        }
    }
}