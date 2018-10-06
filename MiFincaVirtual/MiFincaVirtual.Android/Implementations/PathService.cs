[assembly: Xamarin.Forms.Dependency(typeof(MiFincaVirtual.Droid.Implementations.PathService))]
namespace MiFincaVirtual.Droid.Implementations
{
    using MiFincaVirtual.Interfaces;
    using System;
    using System.IO;

    public class PathService : IPathService
    {
        public string GetDatabasePath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, "MiFincaVirtual.db3");
        }
    }

}