namespace MiFincaVirtual.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using MiFincaVirtual.Common.Models;
    using MiFincaVirtual.Helpers;
    using MiFincaVirtual.Services;
    using System;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class OrdenosEditViewModel: BaseViewModel
    {
        #region Attributes
        private Ordenos ordeno;

        private ApiService apiService;

        private Boolean isRunning;

        private Boolean isEnabled;
        #endregion

        #region Properties
        public Ordenos Ordeno
        {
            get { return this.ordeno; }
            set { this.SetValue(ref this.ordeno, value); }
        }

        public Boolean IsRunning
        {
            get { return this.isRunning; }
            set { this.SetValue(ref this.isRunning, value); }
        }

        public Boolean IsEnabled
        {
            get { return this.isEnabled; }
            set { this.SetValue(ref this.isEnabled, value); }
        }
        #endregion

        #region Constructors
        public OrdenosEditViewModel(Ordenos ordenoP)
        {
            this.ordeno = ordenoP;
            this.apiService = new ApiService();
            this.IsEnabled = true;
        }
        #endregion

        #region Commands
        public ICommand EditCommand
        {
            get
            {
                return new RelayCommand(Edit);
            }
        }
        #endregion

        #region Metods
        private async void Edit()
        {
            if (String.IsNullOrEmpty(this.ordeno.Animales.CodigoAnimal))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error
                    , Languages.CodeAnimalError
                    , Languages.Accept);
                return;
            }

            if (this.ordeno.NumeroOrdeno <= 0)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error
                    , Languages.MilkingError
                    , Languages.Accept);
                return;
            }

            if (this.ordeno.NumeroOrdeno > 3 || this.ordeno.NumeroOrdeno < 1)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.MilkingValueError,
                    Languages.Accept);
                return;
            }

            if (this.ordeno.LitrosOrdeno <= 0)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error
                    , Languages.MilkingLitersError
                    , Languages.Accept);
                return;
            }

            if (this.ordeno.LitrosOrdeno < 1)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.MilkingLitersError,
                    Languages.Accept);
                return;
            }

            if (this.ordeno.PesoOrdeno < 0)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error
                    , Languages.MilkingWeightError
                    , Languages.Accept);
                return;
            }

            if (this.ordeno.GramosCuidoOrdeno < 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.GramsMilking,
                    Languages.Accept);
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlOrdenosController"].ToString();
            var response = await this.apiService.Put(url, prefix, controller, this.Ordeno, this.Ordeno.OrdenoId, Settings.TokenType, Settings.AccessToken);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }

            var newOrdeno = (Ordenos)response.Result;
            var ordenosViewModel = OrdeViewModel.GetInstance();
            var oldOrdeno = ordenosViewModel.myOrdenos.Where(p => p.OrdenoId == this.Ordeno.OrdenoId).FirstOrDefault();
            if (oldOrdeno != null)
            {
                ordenosViewModel.myOrdenos.Remove(oldOrdeno);
            }

            ordenosViewModel.myOrdenos.Add(this.Ordeno);
            ordenosViewModel.RefreshList();

            this.IsRunning = false;
            this.IsEnabled = true;
            await App.Navigator.PopAsync();
            //await App.Navigator.PopAsync();
        }

        #endregion
    }
}
