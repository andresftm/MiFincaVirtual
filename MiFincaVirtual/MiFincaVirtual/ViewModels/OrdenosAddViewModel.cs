using GalaSoft.MvvmLight.Command;
using MiFincaVirtual.Common.Models;
using MiFincaVirtual.Helpers;
using MiFincaVirtual.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MiFincaVirtual.ViewModels
{
    public class OrdenosAddViewModel : BaseViewModel
    {
        #region Attributes

        private ApiService apiService;

        private Boolean isRunning;

        private Boolean isEnabled;

        #endregion

        #region Properties
        public String CodigoAnimal { get; set; }

        public String NumeroOrdeno { get; set; }

        public String LitrosOrdeno { get; set; }

        public String PesoOrdeno { get; set; }

        public String GramosCuidoOrdeno { get; set; }

        public DateTime FechaOrdeno { get; set; }

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

        #region Contructors
        public OrdenosAddViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.FechaOrdeno = DateTime.Now;
        }
        #endregion

        #region Comands
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }
        #endregion

        #region Metods
        private async void Save()
        {
            if (String.IsNullOrEmpty(this.CodigoAnimal))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error
                    , Languages.CodeAnimalError
                    , Languages.Accept);
                return;
            }

            if (String.IsNullOrEmpty(this.NumeroOrdeno))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error
                    , Languages.MilkingError
                    , Languages.Accept);
                return;
            }

            var numeroOrdeño = Int32.Parse(this.NumeroOrdeno);
            if (numeroOrdeño > 3 || numeroOrdeño < 1)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.MilkingValueError,
                    Languages.Accept);
                return;
            }

            if (String.IsNullOrEmpty(this.LitrosOrdeno))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error
                    , Languages.MilkingLitersError
                    , Languages.Accept);
                return;
            }

            Decimal litrosOrdeño = 0;
            Decimal.TryParse(this.LitrosOrdeno, out litrosOrdeño);

            if (litrosOrdeño < 1)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.MilkingLitersError,
                    Languages.Accept);
                return;
            }

            if (String.IsNullOrEmpty(this.PesoOrdeno))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error
                    , Languages.MilkingWeightError
                    , Languages.Accept);
                return;
            }

            var pesosOrdeño = Int32.Parse(this.PesoOrdeno);
            if (pesosOrdeño < 0)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error
                    , Languages.MilkingWeightError
                    , Languages.Accept);
                return;
            }

            var gramosCuidoOrdeno = Int32.Parse(this.GramosCuidoOrdeno);
            if (gramosCuidoOrdeno < 0)
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

            var ordeno = new Ordenos
            {
                FechaOrdeno = this.FechaOrdeno.ToUniversalTime(),
                LitrosOrdeno = litrosOrdeño,
                NumeroOrdeno = Convert.ToInt32(NumeroOrdeno),
                PesoOrdeno = pesosOrdeño,
                GramosCuidoOrdeno = gramosCuidoOrdeno,
                AnimalId = Convert.ToInt32(CodigoAnimal),
            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlOrdenosController"].ToString();
            var response = await this.apiService.Post(url, prefix, controller, ordeno, Settings.TokenType, Settings.AccessToken);

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
            ordenosViewModel.myOrdenos.Add(newOrdeno);
            ordenosViewModel.RefreshList();

            //ordenosViewModel.OrdenosOVM.Add(new OrdenosItemViewModel
            //{
            //    CodigoAnimal = newOrdeno.CodigoAnimal,
            //    FechaOrdeno = newOrdeno.FechaOrdeno,
            //    LitrosOrdeno = newOrdeno.LitrosOrdeno,
            //    NumeroOrdeno = newOrdeno.NumeroOrdeno,
            //    PesoOrdeno = newOrdeno.PesoOrdeno,
            //    OrdenoId = newOrdeno.OrdenoId,
            //});
            //fincasViewModel.RefreshList();

            this.IsRunning = false;
            this.IsEnabled = true;
            await App.Navigator.PopAsync();
            //await App.Navigator.PopAsync();
        }
        #endregion
    }
}
