namespace MiFincaVirtual.Services
{
    using Interfaces;
    using MiFincaVirtual.Common.Models;
    using SQLite;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class DataService
    {
        private SQLiteAsyncConnection connection;

        public DataService()
        {
            this.OpenOrCreateDB();
        }

        private async Task OpenOrCreateDB()
        {
            var databasePath = DependencyService.Get<IPathService>().GetDatabasePath();
            this.connection = new SQLiteAsyncConnection(databasePath);
            await connection.CreateTableAsync<Ordenos>().ConfigureAwait(false);
        }

        public async Task Insert<T>(T model)
        {
            await this.connection.InsertAsync(model);
        }

        public async Task Insert<T>(List<T> models)
        {
            await this.connection.InsertAllAsync(models);
        }

        public async Task Update<T>(T model)
        {
            await this.connection.UpdateAsync(model);
        }

        public async Task Update<T>(List<T> models)
        {
            await this.connection.UpdateAllAsync(models);
        }

        public async Task Delete<T>(T model)
        {
            await this.connection.DeleteAsync(model);
        }

        public async Task<List<Ordenos>> GetAllOrdenos()
        {
            var query = await this.connection.QueryAsync<Ordenos>("select * from [Ordenos]");
            var array = query.ToArray();
            var list = array.Select(p => new Ordenos
            {
                AnimalId = p.AnimalId,
                FechaOrdeno = p.FechaOrdeno,
                LitrosOrdeno = p.LitrosOrdeno,
                NumeroOrdeno = p.NumeroOrdeno,
                OrdenoId = p.OrdenoId,
                PesoOrdeno = p.PesoOrdeno,
            }).ToList();
            return list;
        }

        public async Task DeleteAllOrdenos()
        {
            var query = await this.connection.QueryAsync<Ordenos>("delete from [ordenos]");
        }

    }
}
