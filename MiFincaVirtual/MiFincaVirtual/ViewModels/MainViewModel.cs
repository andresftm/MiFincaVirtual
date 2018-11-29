namespace MiFincaVirtual.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using MiFincaVirtual.Common.Models;
    using MiFincaVirtual.Helpers;
    using MiFincaVirtual.Services;
    using MiFincaVirtual.Views;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class MainViewModel
    {
        public ApiService apiService { get; set; }

        #region Properties
        public LoginViewModel Login { get; set; }

        public OrdeViewModel OrdenosM { get; set; }

        public FincasViewModel FincasM { get; set; }

        public FincasAddViewModel FincaAddM { get; set; }

        public FincasEditViewModel FincaEditM { get; set; }

        public OrdenosAddViewModel OrdenoAddM { get; set; }

        public OrdenosEditViewModel OrdenoEditM { get; set; }

        public ObservableCollection<MenuItemViewModel> Menu { get; set; }

        public List<Ordenos> myBovinosGestantes { get; set; }
        
        public RegisterViewModel Register { get; set; }

        public MyUserASP UserASP { get; set; }

        public string UserFullName
        {
            get
            {
                if (this.UserASP != null && this.UserASP.Claims != null && this.UserASP.Claims.Count > 1)
                {
                    return $"{this.UserASP.Claims[0].ClaimValue} {this.UserASP.Claims[1].ClaimValue}";
                }

                return null;
            }
        }

        public string UserImageFullPath
        {
            get
            {
                foreach (var claim in this.UserASP.Claims)
                {
                    if (claim.ClaimType == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uri")
                    {
                        if (claim.ClaimValue.StartsWith("~"))
                        {
                            return $"http://mifincavirtual-001-site2.dtempurl.com{claim.ClaimValue.Substring(1)}";
                        }

                        return claim.ClaimValue;
                    }
                }

                return null;
            }
        }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
            //this.FincasM = new FincasViewModel();
            this.LoadMenu();
        }
        #endregion

        #region Comands
        public ICommand AddFincaCommand
        {
            get
            {
                return new RelayCommand(GoToAddFarm);
            }
        }

        public ICommand AddOrdenoCommand
        {
            get
            {
                return new RelayCommand(GoToAddOrdeno);
            }
        }

        #endregion

        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion

        #region Metods
        private void LoadMenu()
        {
            this.Menu = new ObservableCollection<MenuItemViewModel>();

            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_info",
                PageName = "AboutPage",
                Title = Languages.About,
            });

            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_phonelink_setup",
                PageName = "SetupPage",
                Title = Languages.Setup,
            });

            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_exit_to_app",
                PageName = "AnimalesPage",
                Title = Languages.Animals,
            });

            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_exit_to_app",
                PageName = "LoginPage",
                Title = Languages.Exit,
            });

        }

        private async void GoToAddFarm()
        {
            this.FincaAddM = new FincasAddViewModel();
            //await App.Navigator.PushAsync(new FincasAddPage());
            //await App.Navigator.PushAsync(new FincasAddPage());
        }

        private async void GoToAddOrdeno()
        {
            await this.GetHembrasGestanes();
            this.OrdenoAddM = new OrdenosAddViewModel(this.myBovinosGestantes);
            await App.Navigator.PushAsync(new OrdenosAddPage());
        }

        private async Task<bool> GetHembrasGestanes()
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlOrdenosController"].ToString();
            var response = await this.apiService.GetList<Ordenos>(url, prefix, 0, controller, Settings.TokenType, Settings.AccessToken);
            if (!response.IsSuccess)
            {
                return false;
            }
            this.myBovinosGestantes = (List<Ordenos>)response.Result;
            return true;
        }

        #endregion
    }
}
