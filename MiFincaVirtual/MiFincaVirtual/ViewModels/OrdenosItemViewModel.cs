namespace MiFincaVirtual.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using MiFincaVirtual.Common.Models;
    using MiFincaVirtual.Helpers;
    using MiFincaVirtual.Services;
    using MiFincaVirtual.Views;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class OrdenosItemViewModel: Ordenos
    {
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
            MainViewModel.GetInstance().OrdenoEditM = new OrdenosEditViewModel(this);

            await App.Navigator.PushAsync(new OrdenosEditPage());
        }
        #endregion
    }
}
