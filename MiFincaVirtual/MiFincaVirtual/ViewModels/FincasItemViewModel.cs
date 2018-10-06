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

    public class FincasItemViewModel: Fincas
    {
        #region Attributs
        private ApiService apiService;
        #endregion

        #region Contructors
        public FincasItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

        #region Commands
        public ICommand EditFincaCommand
        {
            get
            {
                return new RelayCommand(EditFinca);
            }
        }

        public ICommand DeleteFincaCommand
        {
            get
            {
                return new RelayCommand(DeleteFinca);
            }
        }
        #endregion

        #region Metods
        private async void DeleteFinca()
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
            var controller = Application.Current.Resources["UrlFincasController"].ToString();
            var response = await this.apiService.Delete(url, prefix, controller, this.FincaId, Settings.TokenType, Settings.AccessToken); //, Settings.TokenType, Settings.AccessToken);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            var fincaViewModel = FincasViewModel.GetInstance();
            var deleteProducto = fincaViewModel.myFincas.Where(o => o.FincaId == this.FincaId).FirstOrDefault();

            if (deleteProducto != null)
            {
                fincaViewModel.myFincas.Remove(deleteProducto);
            }

            fincaViewModel.RefreshList();


        }

        private async void EditFinca()
        {
            MainViewModel.GetInstance().FincaEditM = new FincasEditViewModel(this);

            //await App.Navigator.PushAsync(new FincasEditPage());
;
        }
        #endregion
    }
}
