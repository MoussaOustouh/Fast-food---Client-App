using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Fast_food.MyHelpers
{
    class MyHelper
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static bool IsConnectedToInternet()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                return true;
            }
            else{
                return false;
            }
        }



        public static async Task SaveValueForAppAsync(string key, string value)
        {
            await SecureStorage.SetAsync(key, value);
        }

        public static async Task<string> GetSavedValueForAppAsync(string key)
        {
            return await SecureStorage.GetAsync(key);
        }

        public static void RemoveSavedValueForApp(string key)
        {
            SecureStorage.Remove(key);
        }

        public static void RemoveAllSavedValuesForApp()
        {
            SecureStorage.RemoveAll();
        }



        public static async Task<JObject> GetLocation()
        {
            JObject geol = new JObject();

            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(5));
                var cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {
                    geol.Add(new JProperty("Worked", true));
                    geol.Add(new JProperty("Latitude", location.Latitude));
                    geol.Add(new JProperty("Longitude", location.Longitude));
                }
                else
                {
                    geol.Add(new JProperty("Worked", false));
                    geol.Add(new JProperty("Message", "GPS not enabled on your device."));
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                geol.Add(new JProperty("Worked", false));
                geol.Add(new JProperty("Message", "GPS not supported on your device."));
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                geol.Add(new JProperty("Worked", false));
                geol.Add(new JProperty("Message", "GPS not enabled on your device."));
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                geol.Add(new JProperty("Worked", false));
                geol.Add(new JProperty("Message", "The application does not have the permission to get your location."));
            }
            catch (Exception ex)
            {
                // Unable to get location
                geol.Add(new JProperty("Worked", false));
                geol.Add(new JProperty("Message", "Unable to get location"));
            }

            return geol;
        }


    }
}
