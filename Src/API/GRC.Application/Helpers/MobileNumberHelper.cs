using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GRC.Application.Helpers;

/// <summary>
/// Helper class for validating and normalizing mobile numbers in E.164 format
/// </summary>
public static class MobileNumberHelper
{
    // Country code to country name mapping
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

        // Remove spaces
        mobileNumber = mobileNumber.Trim();
        countryCode = countryCode.Trim();

        // Validate country code format (digits only, 1-3 chars)
        if (!Regex.IsMatch(countryCode, @"^\d{1,3}$"))
            return (false, "Invalid country code format.");

        // Validate mobile number contains only digits
        if (!Regex.IsMatch(mobileNumber, @"^\d+$"))
            return (false, "Mobile number must contain digits only.");

        // Check length: country code + number should be 7-15 digits total
        int totalLength = countryCode.Length + mobileNumber.Length;
        if (totalLength < 7 || totalLength > 15)
            return (false, "Mobile number length is invalid for the selected country.");

        // Validate country code exists
        if (!CountryCodeMap.ContainsKey(countryCode))
            return (false, "Invalid or unsupported country code.");

        return (true, string.Empty);
    }

    /// <summary>
    /// Normalizes mobile number to E.164 format: +<countrycode><number>
    /// </summary>
    public static string NormalizeMobileNumber(string mobileNumber, string countryCode)
    {
        if (string.IsNullOrWhiteSpace(mobileNumber) || string.IsNullOrWhiteSpace(countryCode))
            return string.Empty;

        // Remove all non-digit characters
        string cleanNumber = Regex.Replace(mobileNumber.Trim(), @"\D", string.Empty);
        string cleanCountryCode = Regex.Replace(countryCode.Trim(), @"\D", string.Empty);

        // Return in E.164 format
        return $"+{cleanCountryCode}{cleanNumber}";
    }

    /// <summary>
    /// Extracts country code from E.164 formatted number
    /// </summary>
    public static (string CountryCode, string Number) ExtractCountryCodeAndNumber(string e164Number)
    {
        if (string.IsNullOrWhiteSpace(e164Number) || !e164Number.StartsWith("+"))
            return (string.Empty, string.Empty);

        string cleanNumber = e164Number.Substring(1); // Remove '+'

        // Try to match country code (1-3 digits)
        foreach (var countryCode in CountryCodeMap.Keys.OrderByDescending(x => x.Length))
        {
            if (cleanNumber.StartsWith(countryCode))
            {
                return (countryCode, cleanNumber.Substring(countryCode.Length));
            }
        }

        return (string.Empty, string.Empty);
    }

    /// <summary>
    /// Gets all supported countries with their codes
    /// </summary>
    public static Dictionary<string, string> GetSupportedCountries()
    {
        return new Dictionary<string, string>(CountryCodeMap);
    }

    /// <summary>
    /// Gets country name by country code
    /// </summary>
    public static string GetCountryName(string countryCode)
    {
        return CountryCodeMap.TryGetValue(countryCode, out var country) ? country : "Unknown";
    }
}