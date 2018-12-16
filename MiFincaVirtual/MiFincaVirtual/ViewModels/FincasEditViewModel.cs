namespace MiFincaVirtual.ViewModels
{
    using Common.Models;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Services;
    using System;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class FincasEditViewModel : BaseViewModel
    {
        #region Attributes
        private Fincas finca;

        private MediaFile file;

        private ApiService apiService;

        private Boolean isRunning;

        private Boolean isEnabled;

        private ImageSource imageSource;
        #endregion

        #region Properties
        public Fincas Finca
        {
            get { return this.finca; }
            set
            {
                this.SetValue(ref this.finca, value);
            }
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

        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { this.SetValue(ref this.imageSource, value); }
        }
        #endregion

        #region Constructors
        public FincasEditViewModel(Fincas fincaP)
        {
            this.finca = fincaP;
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.finca.IngresoFinca = finca.IngresoFinca;
            this.imageSource = finca.ImageFullPath;
        }
        #endregion

        #region Comands
        public ICommand EditCommand
        {
            get
            {
                return new RelayCommand(Edit);
            }
        }

        public ICommand ChangeImageCommand
        {
            get
            {
                return new RelayCommand(ChangeImage);
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(Delete);
            }
        }

        #endregion


        #region Metods

        private async void Delete()
        {
            var answer = await Application.Current.MainPage.DisplayAlert(Languages.Delete, Languages.DeleteConfirmation, Languages.Yes, Languages.No);

            if (!answer)
            {
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var Conecction = await this.apiService.CheckConnection();
            if (!Conecction.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert(Languages.Error, Conecction.Message, Languages.Accept);
                return;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlFincasController"].ToString();
            var response = await this.apiService.Delete(url, prefix, controller, this.Finca.FincaId, Settings.TokenType, Settings.AccessToken); //, Settings.TokenType, Settings.AccessToken);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            var fincaViewModel = FincasViewModel.GetInstance();
            var deleteProducto = fincaViewModel.myFincas.Where(o => o.FincaId == this.Finca.FincaId).FirstOrDefault();

            if (deleteProducto != null)
            {
                fincaViewModel.myFincas.Remove(deleteProducto);
            }

            this.IsRunning = false;
            this.IsEnabled = true;

            fincaViewModel.RefreshList();
            await App.Navigator.PopAsync();

        }

        private async void Edit()
        {
            if (String.IsNullOrEmpty(this.Finca.NombreFinca))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error
                    , Languages.NameError
                    , Languages.Accept);
                return;
            }

            if (String.IsNullOrEmpty(this.Finca.PaisFinca))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error
                    , Languages.CoutryError
                    , Languages.Accept);
                return;
            }

            if (String.IsNullOrEmpty(this.Finca.EstadoFinca))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error
                    , Languages.StateError
                    , Languages.Accept);
                return;
            }

            if (String.IsNullOrEmpty(this.Finca.CiudadFinca))
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
                this.Finca.ImageArray = imageArray;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlFincasController"].ToString();
            var response = await this.apiService.Put(url, prefix, controller, this.Finca, this.Finca.FincaId, Settings.TokenType, Settings.AccessToken);

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
            var oldFinca = fincasViewModel.myFincas.Where(p => p.FincaId == this.Finca.FincaId).FirstOrDefault();
            if (oldFinca != null)
            {
                fincasViewModel.myFincas.Remove(oldFinca);
            }

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
