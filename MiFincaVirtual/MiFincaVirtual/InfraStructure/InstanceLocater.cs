

namespace MiFincaVirtual.InfraStructure
{
    using MiFincaVirtual.ViewModels;

    public class InstanceLocater
    {
        #region Properties
        public MainViewModel Main { get; set; }
        #endregion

        #region Contructor
        public InstanceLocater()
        {
            this.Main = new MainViewModel();
        }
        #endregion

    }
}
