using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Fast_food.Models;
using Fast_food.MyHelpers;
using Xamarin.Essentials;
using Fast_food.ViewModels;

namespace Fast_food.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        private HttpClient httpClient;


        AuthenticationViewModel _authenticationViewModel;

        public SignUpPage()
        {
            InitializeComponent();
            httpClient = new HttpClient();

            BindingContext = _authenticationViewModel = new AuthenticationViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    
    
    
    
    }
}