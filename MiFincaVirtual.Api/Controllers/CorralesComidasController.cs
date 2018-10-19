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
    public class CorralesComidasController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/CorralesComidas
        public IQueryable<CorralesComida> GetCorralesComidas()
        {
            return db.CorralesComidas;
        }

        // GET: api/CorralesComidas/5
        [ResponseType(typeof(CorralesComida))]
        public async Task<IHttpActionResult> GetCorralesComida(int id)
        {
            CorralesComida corralesComida = await db.CorralesComidas.FindAsync(id);
            if (corralesComida == null)
            {
                return NotFound();
            }

            return Ok(corralesComida);
        }

        // PUT: api/CorralesComidas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCorralesComida(int id, CorralesComida corralesComida)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != corralesComida.CorralComidaId)
            {
                return BadRequest();
            }

            db.Entry(corralesComida).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CorralesComidaExists(id))
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

        // POST: api/CorralesComidas
        [ResponseType(typeof(CorralesComida))]
        public async Task<IHttpActionResult> PostCorralesComida(CorralesComida corralesComida)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CorralesComidas.Add(corralesComida);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = corralesComida.CorralComidaId }, corralesComida);
        }

        // DELETE: api/CorralesComidas/5
        [ResponseType(typeof(CorralesComida))]
        public async Task<IHttpActionResult> DeleteCorralesComida(int id)
        {
            CorralesComida corralesComida = await db.CorralesComidas.FindAsync(id);
            if (corralesComida == null)
            {
                return NotFound();
            }

            db.CorralesComidas.Remove(corralesComida);
            await db.SaveChangesAsync();

            return Ok(corralesComida);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CorralesComidaExists(int id)
        {
            return db.CorralesComidas.Count(e => e.CorralComidaId == id) > 0;
        }
    }
}