namespace MiFincaVirtual.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using MiFincaVirtual.Common.Models;
    using MiFincaVirtual.Helpers;
    using MiFincaVirtual.Services;
    using MiFincaVirtual.Views;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class OrdenosItemViewModel : Ordenos
    {
        public List<Ordenos> myBovinosGestantes { get; set; }

        #region Attributs
        private ApiService apiService;
        #endregion

        #region Contructors
        public OrdenosItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

        #region Commands
        public ICommand EditOrdenoCommand
        {
            get
            {
                return new RelayCommand(EditOrdeno);
            }
        }

        public ICommand DeleteOrdenoCommand
        {
            get
            {
                return new RelayCommand(DeleteOrdeno);
            }
        }
        #endregion

        #region Metods
        private async void DeleteOrdeno()
        {
            var answer = await Application.Current.MainPage.DisplayAlert(Languages.Delete, Languages.DeleteConfirmation, Languages.Yes, Languages.No);

            if (!answer)
            {
                return;
            }

            var Conecction = await this.apiService.CheckConnection();
            if (!Conecction.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Conecction.Message, Languages.Accept);
                return;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlOrdenosController"].ToString();
            var response = await this.apiService.Delete(url, prefix, controller, this.OrdenoId, Settings.TokenType, Settings.AccessToken); //, Settings.TokenType, Settings.AccessToken);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            var ordeViewModel = OrdeViewModel.GetInstance();
            var deleteProducto = ordeViewModel.OrdenosOVM.Where(o => o.OrdenoId == this.OrdenoId).FirstOrDefault();

            if (deleteProducto != null)
            {
                ordeViewModel.OrdenosOVM.Remove(deleteProducto);
            }

            //ordeViewModel.RefreshList();
        }

        private async void EditOrdeno()
        {
            await GetHembrasGestanes();

            MainViewModel.GetInstance().OrdenoEditM = new OrdenosEditViewModel(this, myBovinosGestantes);

            await App.Navigator.PushAsync(new OrdenosEditPage());
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
