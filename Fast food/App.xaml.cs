using Fast_food.Models.MyModels;
using Fast_food.MyHelpers;
using Fast_food.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace Fast_food
{
    public partial class App : Xamarin.Forms.Application
    {
        public static Client client = new Client();

        public App()
        {
            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            InitializeComponent();


        }


        protected override async void OnStart()
        {

            NavigationPage np;

            // bach nms7 les JWT o LoggedIN
            // MyHelper.RemoveAllSavedValuesForApp();
            string loggedIn = await MyHelper.GetSavedValueForAppAsync("LoggedIn");

            if (loggedIn != null && loggedIn.Equals("true"))
            {
                string user_info = await MyHelper.GetSavedValueForAppAsync("user_info");
                client = JsonConvert.DeserializeObject<Client>(user_info);

                MainPage = new AppShell();
            }
            else
            {
                np = new NavigationPage(new SignInPage());
                MainPage = np;
            }

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
