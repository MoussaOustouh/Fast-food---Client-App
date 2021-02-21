using Fast_food.Models.MyModels;
using Fast_food.MyHelpers;
using Fast_food.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fast_food.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        HttpClient httpClient;
        MyHttpClient myHttpClient;

        AuthenticationViewModel _authenticationViewModel;

        public SignInPage()
        {
            InitializeComponent();

            httpClient = new HttpClient();
            myHttpClient = new MyHttpClient();

            BindingContext = _authenticationViewModel = new AuthenticationViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }



    }
}