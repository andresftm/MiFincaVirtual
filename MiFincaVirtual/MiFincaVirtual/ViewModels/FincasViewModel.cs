namespace MiFincaVirtual.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using MiFincaVirtual.Common.Models;
    using MiFincaVirtual.Helpers;
    using MiFincaVirtual.Services;
    using Xamarin.Forms;

    public class FincasViewModel : BaseViewModel
    {
        #region Attributes
        private string filter;

        public ApiService apiService { get; set; }

        private bool isRefreshing;

        private ObservableCollection<FincasItemViewModel> fincasFVM;

        #endregion

        #region Properties
        public string Filter
        {
            get { return this.filter; }
            set
            {
                this.filter = value;
                this.RefreshList();
            }
        }

        public ObservableCollection<FincasItemViewModel> FincasFVM
        {
            get { return this.fincasFVM; }
            set { this.SetValue(ref this.fincasFVM, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public List<Fincas> myFincas { get; set; }
        #endregion

        #region Constructor
        public FincasViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
            this.loadFincas();
        }
        #endregion

        #region Singleton
        private static FincasViewModel instance;

        public static FincasViewModel GetInstance()
        {
            if (instance == null)
            {
                return new FincasViewModel();
            }

            return instance;
        }
        #endregion

        #region Metods
        public void RefreshList()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                var myListFincasItemViewModel = this.myFincas.Select(o => new FincasItemViewModel()
                {
                    CiudadFinca = o.CiudadFinca,
                    EstadoFinca = o.EstadoFinca,
                    FincaId = o.FincaId,
                    HabilitadaFinca = o.HabilitadaFinca,
                    ImageArray = o.ImageArray,
                    ImagePath = o.ImagePath,
                    IngresoFinca = o.IngresoFinca,
                    NombreFinca = o.NombreFinca,
                    PaisFinca = o.PaisFinca
                });

                this.FincasFVM = new ObservableCollection<FincasItemViewModel>(myListFincasItemViewModel.OrderByDescending(f => f.NombreFinca));
            }
            else
            {
                var myListFincasItemViewModel = this.myFincas.Select(o => new FincasItemViewModel()
                {
                    CiudadFinca = o.CiudadFinca,
                    EstadoFinca = o.EstadoFinca,
                    FincaId = o.FincaId,
                    HabilitadaFinca = o.HabilitadaFinca,
                    ImageArray = o.ImageArray,
                    ImagePath = o.ImagePath,
                    IngresoFinca = o.IngresoFinca,
                    NombreFinca = o.NombreFinca,
                    PaisFinca = o.PaisFinca
                }).Where(f => f.NombreFinca.ToLower().Contains(this.Filter.ToLower())).ToList();

                this.FincasFVM = new ObservableCollection<FincasItemViewModel>(myListFincasItemViewModel.OrderByDescending(f => f.NombreFinca));

            }
        }

        private async void loadFincas()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.Touroninternet, Languages.Accept);
                return;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlFincasController"].ToString();

            var response = await this.apiService.GetList<Fincas>(url, prefix, controller, Settings.TokenType, Settings.AccessToken);

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.NoInternet, Languages.Accept);
                return;
            }

            this.myFincas = (List<Fincas>)response.Result;
            this.RefreshList();

            this.IsRefreshing = false;
        }
        #endregion

        #region Commands
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(RefreshList);
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(loadFincas);
            }
        }
        #endregion

    }
}
