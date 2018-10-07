
namespace MiFincaVirtual.Helpers
{
    using MiFincaVirtual.Interfaces;
    using MiFincaVirtual.Resources;
    using Xamarin.Forms;

    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Error
        {
            get { return Resource.Error; }
        }

        public static string Milking
        {
            get { return Resource.Milking; }
        }

        public static string Touroninternet
        {
            get { return Resource.Touroninternet; }
        }

        public static string NoInternet
        {
            get { return Resource.NoInternet; }
        }

        public static string Accept
        {
            get { return Resource.Accept; }
        }

        public static string Farm
        {
            get { return Resource.Farm; }
        }

        public static string FarmAdd
        {
            get { return Resource.FarmAdd; }
        }

        public static string Name
        {
            get { return Resource.Name; }
        }

        public static string Country
        {
            get { return Resource.Country; }
        }

        public static string State
        {
            get { return Resource.State; }
        }

        public static string City
        {
            get { return Resource.City; }
        }

        public static string FarmDateIn
        {
            get { return Resource.FarmDateIn; }
        }

        public static string Enabled
        {
            get { return Resource.Enabled; }
        }

        public static string NameError
        {
            get { return Resource.NameError; }
        }

        public static string CoutryError
        {
            get { return Resource.CoutryError; }
        }

        public static string StateError
        {
            get { return Resource.StateError; }
        }

        public static string CityError
        {
            get { return Resource.CityError; }
        }

        public static string CodeAnimalError
        {
            get { return Resource.CodeAnimalError; }
        }

        public static string MilkingError
        {
            get { return Resource.MilkingError; }
        }

        public static string MilkingValueError
        {
            get { return Resource.MilkingValueError; }
        }

        public static string MilkingLitersError
        {
            get { return Resource.MilkingLitersError; }
        }

        public static string MilkingWeightError
        {
            get { return Resource.MilkingWeightError; }
        }

        public static string ImageSource
        {
            get { return Resource.ImageSource; }
        }

        public static string Cancel
        {
            get { return Resource.Cancel; }
        }

        public static string FromGallery
        {
            get { return Resource.FromGallery; }
        }

        public static string NewPicture
        {
            get { return Resource.NewPicture; }
        }

        public static string Edit
        {
            get { return Resource.Edit; }
        }

        public static string Delete
        {
            get { return Resource.Delete; }
        }

        public static string DeleteConfirmation
        {
            get { return Resource.DeleteConfirmation; }
        }

        public static string Yes
        {
            get { return Resource.Yes; }
        }

        public static string No
        {
            get { return Resource.No; }
        }

        public static string FarmEdit
        {
            get { return Resource.FarmEdit; }
        }

        public static string Search
        {
            get { return Resource.Search; }
        }

        public static string Login
        {
            get { return Resource.Login; }
        }

        public static string EMail
        {
            get { return Resource.EMail; }
        }

        public static string EmailPlaceHolder
        {
            get { return Resource.EmailPlaceHolder; }
        }

        public static string Password
        {
            get { return Resource.Password; }
        }

        public static string PasswordPlaceHolder
        {
            get { return Resource.PasswordPlaceHolder; }
        }

        public static string Rememberme
        {
            get { return Resource.Rememberme; }
        }

        public static string Forgot
        {
            get { return Resource.Forgot; }
        }

        public static string Register
        {
            get { return Resource.Register; }
        }

        public static string EmailValidation
        {
            get { return Resource.EmailValidation; }
        }

        public static string PasswordValidation
        {
            get { return Resource.PasswordValidation; }
        }

        public static string SomethingWrong
        {
            get { return Resource.SomethingWrong; }
        }

        public static string Menu
        {
            get { return Resource.Menu; }
        }

        public static string Setup
        {
            get { return Resource.Setup; }
        }

        public static string About
        {
            get { return Resource.About; }
        }

        public static string Exit
        {
            get { return Resource.Exit; }
        }

        public static string NoMilkingsMessage
        {
            get { return Resource.NoMilkingsMessage; }
        }

        public static string FirstName
        {
            get { return Resource.FirstName; }
        }

        public static string FirstNamePlaceholder
        {
            get { return Resource.FirstNamePlaceholder; }
        }

        public static string LastName
        {
            get { return Resource.LastName; }
        }

        public static string LastNamePlaceholder
        {
            get { return Resource.LastNamePlaceholder; }
        }

        public static string Phone
        {
            get { return Resource.Phone; }
        }

        public static string PhonePlaceHolder
        {
            get { return Resource.PhonePlaceHolder; }
        }

        public static string PasswordConfirm
        {
            get { return Resource.PasswordConfirm; }
        }

        public static string PasswordConfirmPlaceHolder
        {
            get { return Resource.PasswordConfirmPlaceHolder; }
        }

        public static string Address
        {
            get { return Resource.Address; }
        }

        public static string AddressPlaceHolder
        {
            get { return Resource.AddressPlaceHolder; }
        }

        public static string FirstNameError
        {
            get { return Resource.FirstNameError; }
        }

        public static string LastNameError
        {
            get { return Resource.LastNameError; }
        }

        public static string EMailError
        {
            get { return Resource.EMailError; }
        }

        public static string PhoneError
        {
            get { return Resource.PhoneError; }
        }

        public static string PasswordError
        {
            get { return Resource.PasswordError; }
        }

        public static string PasswordConfirmError
        {
            get { return Resource.PasswordConfirmError; }
        }

        public static string PasswordsNoMatch
        {
            get { return Resource.PasswordsNoMatch; }
        }

        public static string RegisterConfirmation
        {
            get { return Resource.RegisterConfirmation; }
        }

        public static string Confirm
        {
            get { return Resource.Confirm; }
        }

        public static string Grams
        {
            get { return Resource.Grams; }
        }

        public static string GramsMilking
        {
            get { return Resource.GramsMilking; }
        }

    }
}
