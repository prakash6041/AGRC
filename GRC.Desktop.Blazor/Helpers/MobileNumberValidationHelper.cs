using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GRC.Desktop.Blazor.Helpers
{
    public static class MobileNumberValidationHelper
    {
        private static readonly Dictionary<string, string> CountryCodeMap = new()
        {
            { "1", "USA/Canada" },
            { "44", "United Kingdom" },
            { "91", "India" },
            { "61", "Australia" },
            { "33", "France" },
            { "49", "Germany" },
            { "81", "Japan" },
            { "86", "China" },
            { "55", "Brazil" },
            { "39", "Italy" },
            { "34", "Spain" },
            { "31", "Netherlands" },
            { "46", "Sweden" },
            { "47", "Norway" },
            { "41", "Switzerland" },
            { "43", "Austria" },
            { "32", "Belgium" },
            { "30", "Greece" },
            { "48", "Poland" },
            { "7", "Russia" },
            { "82", "South Korea" },
            { "60", "Malaysia" },
            { "65", "Singapore" },
            { "66", "Thailand" },
            { "62", "Indonesia" },
            { "63", "Philippines" },
            { "64", "New Zealand" },
            { "27", "South Africa" },
            { "20", "Egypt" },
            { "234", "Nigeria" },
            { "254", "Kenya" }
        };

        /// <summary>
        /// Validates mobile number format and length
        /// </summary>
        public static (bool IsValid, string ErrorMessage) ValidateMobileNumber(string mobileNumber, string countryCode)
        {
            if (string.IsNullOrWhiteSpace(mobileNumber))
                return (false, "Mobile number is required.");

            if (string.IsNullOrWhiteSpace(countryCode))
                return (false, "Country code is required.");

            mobileNumber = mobileNumber.Trim();
            countryCode = countryCode.Trim();

            if (!Regex.IsMatch(countryCode, @"^\d{1,3}$"))
                return (false, "Invalid country code format.");

            if (!Regex.IsMatch(mobileNumber, @"^\d+$"))
                return (false, "Mobile number must contain digits only.");

            int totalLength = countryCode.Length + mobileNumber.Length;
            if (totalLength < 7 || totalLength > 15)
                return (false, "Mobile number length is invalid for the selected country.");

            if (!CountryCodeMap.ContainsKey(countryCode))
                return (false, "Invalid or unsupported country code.");

            return (true, string.Empty);
        }

        /// <summary>
        /// Gets formatted mobile number with country code
        /// </summary>
        public static string GetFormattedNumber(string mobileNumber, string countryCode)
        {
            if (string.IsNullOrWhiteSpace(mobileNumber) || string.IsNullOrWhiteSpace(countryCode))
                return string.Empty;

            return $"+{countryCode} {mobileNumber}";
        }

        /// <summary>
        /// Gets all supported countries with their codes
        /// </summary>
        public static Dictionary<string, string> GetSupportedCountries()
        {
            return new Dictionary<string, string>(CountryCodeMap);
        }
    }
}