namespace MiFincaVirtual.ViewModels
{
    using System;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using MiFincaVirtual.Common.Models;
    using MiFincaVirtual.Helpers;
    using MiFincaVirtual.Services;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Xamarin.Forms;
    public class FincasAddViewModel : BaseViewModel
    {
        #region Attributes

        private MediaFile file;

        private ApiService apiService;

        private Boolean isRunning;

        private Boolean isEnabled;

        private ImageSource imageSource;
        #endregion

        #region Properties
        public String NombreFinca { get; set; }

        public String PaisFinca { get; set; }

        public String EstadoFinca { get; set; }

        public String CiudadFinca { get; set; }

        public DateTime IngresoFinca { get; set; }

        public Boolean HabilitadaFinca { get; set; }

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

        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { this.SetValue(ref this.imageSource, value); }
        }
        #endregion

        #region Contructors
        public FincasAddViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.IngresoFinca = DateTime.Now;
            this.imageSource = "farm";
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

        public ICommand ChangeImageCommand
        {
            get
            {
                return new RelayCommand(ChangeImage);
            }
        }
        #endregion

        #region Metods
        private async void Save()
        {
            if (String.IsNullOrEmpty(this.NombreFinca))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error
                    , Languages.NameError
                    , Languages.Accept);
                return;
            }

            if (String.IsNullOrEmpty(this.PaisFinca))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error
                    , Languages.CoutryError
                    , Languages.Accept);
                return;
            }

            if (String.IsNullOrEmpty(this.EstadoFinca))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error
                    , Languages.StateError
                    , Languages.Accept);
                return;
            }

            if (String.IsNullOrEmpty(this.CiudadFinca))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error
                    , Languages.CityError
                    , Languages.Accept);
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

            byte[] imageArray = null;
            if (this.file != null)
            {
                imageArray = FilesHelper.ReadFully(this.file.GetStream());
            }

            var finca = new Fincas
            {
                CiudadFinca = this.CiudadFinca,
                EstadoFinca = this.EstadoFinca,
                HabilitadaFinca = this.HabilitadaFinca,
                NombreFinca = this.NombreFinca,
                PaisFinca = this.PaisFinca,
                IngresoFinca = this.IngresoFinca,
                ImageArray = imageArray,
            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlFincasController"].ToString();
            var response = await this.apiService.Post(url, prefix, controller, finca, Settings.TokenType, Settings.AccessToken);

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

            var newFinca = (Fincas)response.Result;
            var fincasViewModel = FincasViewModel.GetInstance();
            fincasViewModel.myFincas.Add(newFinca);
            fincasViewModel.RefreshList();

            this.IsRunning = false;
            this.IsEnabled = true;
            await App.Navigator.PopAsync();
        }

        private async void ChangeImage()
        {
            await CrossMedia.Current.Initialize();

            var source = await Application.Current.MainPage.DisplayActionSheet(
                Languages.ImageSource,
                Languages.Cancel,
                null,
                Languages.FromGallery,
                Languages.NewPicture);

            if (source == Languages.Cancel)
            {
                this.file = null;
                return;
            }

            if (source == Languages.NewPicture)
            {
                this.file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    }
                );
            }
            else
            {
                this.file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (this.file != null)
            {
                this.ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }

        }

        #endregion
    }
}
