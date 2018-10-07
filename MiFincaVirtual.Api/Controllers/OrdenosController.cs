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
    [Authorize]
    public class OrdenosController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Ordenos
        public IQueryable<Ordenos> GetOrdenos()
        {
            return db.Ordenos.OrderByDescending(o => o.OrdenoId).Take(16);
        }

        // GET: api/Ordenos/5
        [ResponseType(typeof(Ordenos))]
        public async Task<IHttpActionResult> GetOrdenos(int id)
        {
            Ordenos ordenos = await db.Ordenos.FindAsync(id);
            if (ordenos == null)
            {
                return NotFound();
            }

            return Ok(ordenos);
        }

        // PUT: api/Ordenos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOrdenos(int id, Ordenos ordenos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ordenos.OrdenoId)
            {
                return BadRequest();
            }

            db.Entry(ordenos).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdenosExists(id))
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

        // POST: api/Ordenos
        [ResponseType(typeof(Ordenos))]
        public async Task<IHttpActionResult> PostOrdenos(Ordenos ordenos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ordenos.FechaOrdeno = ordenos.FechaOrdeno.ToUniversalTime();
            db.Ordenos.Add(ordenos);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = ordenos.OrdenoId }, ordenos);
        }

        // DELETE: api/Ordenos/5
        [ResponseType(typeof(Ordenos))]
        public async Task<IHttpActionResult> DeleteOrdenos(int id)
        {
            Ordenos ordenos = await db.Ordenos.FindAsync(id);
            if (ordenos == null)
            {
                return NotFound();
            }

            db.Ordenos.Remove(ordenos);
            await db.SaveChangesAsync();

            return Ok(ordenos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrdenosExists(int id)
        {
            return db.Ordenos.Count(e => e.OrdenoId == id) > 0;
        }
    }
}