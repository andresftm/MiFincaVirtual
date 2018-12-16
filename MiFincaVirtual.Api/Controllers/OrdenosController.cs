using MiFincaVirtual.Api.Models;
using MiFincaVirtual.Api.Tools;
using MiFincaVirtual.Common.Models;
using MiFincaVirtual.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
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
    private DataContext db;

    public IEnumerable<Ordenos> GetOrdenos()
    {
      List<ConsultaOrdeno> source = new List<ConsultaOrdeno>();
      using (LocalDataContext localDataContext = new LocalDataContext())
        source = ((IEnumerable<ConsultaOrdeno>) ((DbContext) localDataContext).get_Database().SqlQuery<ConsultaOrdeno>(Sp.uspOrdenosConsultar, (object[]) Array.Empty<object>())).ToList<ConsultaOrdeno>();
      return source.Select<ConsultaOrdeno, Ordenos>((Func<ConsultaOrdeno, Ordenos>) (o => new Ordenos()
      {
        Animal = o.Animal,
        AnimalId = new int?(o.AnimalId),
        FechaOrdeno = o.FechaOrdeno,
        LitrosOrdeno = o.LitrosOrdeno,
        NumeroOrdeno = o.NumeroOrdeno,
        PesoOrdeno = o.PesoOrdeno,
        OrdenoId = o.OrdenoId,
        GramosCuidoOrdeno = o.GramosCuidoOrdeno
      })).Take<Ordenos>(20);
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

    [ResponseType(typeof (void))]
    public async Task<IHttpActionResult> PutOrdenos(int id, Ordenos ordenos)
    {
      OrdenosController ordenosController = this;
      if (!ordenosController.get_ModelState().get_IsValid())
        return (IHttpActionResult) ordenosController.BadRequest(ordenosController.get_ModelState());
      if (id != ordenos.OrdenoId)
        return (IHttpActionResult) ordenosController.BadRequest();
      ((DbEntityEntry<Ordenos>) ((DbContext) ordenosController.db).Entry<Ordenos>((M0) ordenos)).set_State((EntityState) 16);
      try
      {
        int num = await ((DbContext) ordenosController.db).SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException ex)
      {
        if (!ordenosController.OrdenosExists(id))
          return (IHttpActionResult) ordenosController.NotFound();
        throw;
      }
      return (IHttpActionResult) ordenosController.StatusCode(HttpStatusCode.NoContent);
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
            ordenos.PesoOrdeno = 10;

            db.Ordenos.Add(ordenos);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = ordenos.OrdenoId }, ordenos);
        }

    [ResponseType(typeof (Ordenos))]
    public async Task<IHttpActionResult> DeleteOrdenos(int id)
    {
      OrdenosController ordenosController = this;
      Ordenos ordenos = await ordenosController.db.get_Ordenos().FindAsync(new object[1]
      {
        (object) id
      });
      if (ordenos == null)
        return (IHttpActionResult) ordenosController.NotFound();
      ordenosController.db.get_Ordenos().Remove(ordenos);
      int num = await ((DbContext) ordenosController.db).SaveChangesAsync();
      return (IHttpActionResult) ordenosController.Ok<Ordenos>((M0) ordenos);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
        ((DbContext) this.db).Dispose();
      base.Dispose(disposing);
    }

    private bool OrdenosExists(int id)
    {
      return ((IQueryable<Ordenos>) this.db.get_Ordenos()).Count<Ordenos>((Expression<Func<Ordenos, bool>>) (e => e.OrdenoId == id)) > 0;
    }

    public OrdenosController()
    {
      base.\u002Ector();
    }
  }
}
