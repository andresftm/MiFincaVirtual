namespace MiFincaVirtual.Helpers
{
    using System;

    public class PlatformCulture
    {

        public PlatformCulture(string platformCultureString)
        {
            if (string.IsNullOrEmpty(platformCultureString))
            {
                throw new ArgumentException("Expected Culture Identifiquer", "platformCultureString");
            }

            PlatformString = platformCultureString.Replace("_", "-");
            var dashIndex = PlatformString.IndexOf("-", StringComparison.Ordinal);

            if (dashIndex > 0)
            {
                var Parts = PlatformString.Split('-');
                LanguageCode = Parts[0];
                LocaleCode = Parts[1];
            }
            else
            {
                LanguageCode = PlatformString;
                LocaleCode = "";
            }
        }

        public string PlatformString { get; private set; }

        public string LanguageCode { get; private set; }

        public string LocaleCode { get; private set; }

        public override string ToString()
        {
            return PlatformString;
        }
    }
}
