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
using MiFincaVirtual.Api.Models;
using MiFincaVirtual.Api.Tools;
using MiFincaVirtual.Common.Models;
using MiFincaVirtual.Domain.Models;

namespace MiFincaVirtual.Api.Controllers
{
    [Authorize]
    public class OrdenosController : ApiController
    {
        private DataContext db = new DataContext();


        /// <summary> Consulta los ordeños registrados.</summary>
        /// <returns> Listado de los ultimos 20 ordeños registrados.</returns>
        // GET: api/Ordenos
        public IEnumerable<Ordenos> GetOrdenos()
        {
            List<ConsultaOrdeno> lstConsultaOrdenos = new List<ConsultaOrdeno>();
            using (LocalDataContext db = new LocalDataContext())
            {
                lstConsultaOrdenos = db.Database.SqlQuery<ConsultaOrdeno>(Sp.uspOrdenosConsultar).ToList();
            }

            var lstOrdenos = lstConsultaOrdenos.Select(o => new Ordenos()
            {
                Animal = o.Animal,
                AnimalId = o.AnimalId,
                FechaOrdeno = o.FechaOrdeno,
                LitrosOrdeno = o.LitrosOrdeno,
                NumeroOrdeno = o.NumeroOrdeno,
                PesoOrdeno = o.PesoOrdeno,
                OrdenoId = o.OrdenoId,
                GramosCuidoOrdeno = o.GramosCuidoOrdeno,
            });

            return lstOrdenos;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"> 0 - Bovinos en lactancia. </param>
        /// <returns> Listado de bovinos consultados. </returns>
        // GET: api/Ordenos/5
        [ResponseType(typeof(Ordenos))]
        public IEnumerable<Ordenos> GetOrdenos(int id)
        {
            List<ConsultaOrdeno> lstConsultaOrdenos = new List<ConsultaOrdeno>();

            switch (id)
            {
                case 0:
                    using (LocalDataContext db = new LocalDataContext())
                    {
                        lstConsultaOrdenos = db.Database.SqlQuery<ConsultaOrdeno>(Sp.uspBovinosGestantesConsultar).ToList();
                    }

                    var lstOrdenos = lstConsultaOrdenos.Select(o => new Ordenos()
                    {
                        Animal = o.Animal,
                        AnimalId = o.AnimalId,
                    });
                    return lstOrdenos.Take(20);
                    break;
                default:
                    using (LocalDataContext db = new LocalDataContext())
                    {
                        lstConsultaOrdenos = db.Database.SqlQuery<ConsultaOrdeno>(Sp.uspBovinosGestantesConsultar).ToList();
                    }

                    var lstOrdenos1 = lstConsultaOrdenos.Select(o => new Ordenos()
                    {
                        Animal = o.Animal,
                        AnimalId = o.AnimalId,
                    });
                    return lstOrdenos1.Take(20); break;
            }
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