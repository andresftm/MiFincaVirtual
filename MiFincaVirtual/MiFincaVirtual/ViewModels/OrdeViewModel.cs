namespace MiFincaVirtual.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using MiFincaVirtual.Common.Models;
    using MiFincaVirtual.Helpers;
    using MiFincaVirtual.Services;
    using Xamarin.Forms;
    using System.Linq;
    using System.Threading.Tasks;

    public class OrdeViewModel : BaseViewModel
    {
        #region Attributes
        public ApiService apiService { get; set; }

        public DataService dataService { get; set; }

        private bool isRefreshing;

        private ObservableCollection<OrdenosItemViewModel> ordenosOVM;

        #endregion

        #region Properties
        public ObservableCollection<OrdenosItemViewModel> OrdenosOVM
        {
            get { return this.ordenosOVM; }
            set { this.SetValue(ref this.ordenosOVM, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public List<Ordenos> myOrdenos { get; set; }
        #endregion

        #region Constructor
        public OrdeViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
            this.dataService = new DataService();
            this.loadOrdenos();
        }
        #endregion

        #region Singleton
        private static OrdeViewModel instance;

        public static OrdeViewModel GetInstance()
        {
            if (instance == null)
            {
                return new OrdeViewModel();
            }

            return instance;
        }
        #endregion

        #region Metods
        public void RefreshList()
        {
            var myListOrdenosItemViewModel = this.myOrdenos.Select(o => new OrdenosItemViewModel()
            {
                AnimalId = o.AnimalId,
                FechaOrdeno = o.FechaOrdeno,
                LitrosOrdeno = o.LitrosOrdeno,
                NumeroOrdeno = o.NumeroOrdeno,
                PesoOrdeno = o.PesoOrdeno,
                OrdenoId = o.OrdenoId,
                GramosCuidoOrdeno = o.GramosCuidoOrdeno,
            });

            this.OrdenosOVM = new ObservableCollection<OrdenosItemViewModel>(myListOrdenosItemViewModel.OrderByDescending(f => f.FechaOrdeno));
        }

        private async void loadOrdenos()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();
            if (connection.IsSuccess)
            {
                var answer = await this.LoadOrdenosFromAPI();
                if (answer)
                {
                    this.SaveOrdenosToDB();
                }
            }
            else
            {
                await this.LoadOrdenosFromDB();
            }

            if (this.myOrdenos == null || this.myOrdenos.Count == 0)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.NoMilkingsMessage, Languages.Accept);
                return;
            }

            this.RefreshList();
            this.IsRefreshing = false;
        }

        private async Task LoadOrdenosFromDB()
        {
            this.myOrdenos = await this.dataService.GetAllOrdenos();
        }

        private async Task<bool> LoadOrdenosFromAPI()
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlOrdenosController"].ToString();
            var response = await this.apiService.GetList<Ordenos>(url, prefix, controller, Settings.TokenType, Settings.AccessToken);
            if (!response.IsSuccess)
            {
                return false;
            }

            this.myOrdenos = (List<Ordenos>)response.Result;
            return true;
        }

        private async Task SaveOrdenosToDB()
        {
            await this.dataService.DeleteAllOrdenos();
            this.dataService.Insert(this.myOrdenos);
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(loadOrdenos);
            }
        }
        #endregion
    }
}
