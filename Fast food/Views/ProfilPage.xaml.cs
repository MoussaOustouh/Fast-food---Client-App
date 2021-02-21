using Fast_food.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fast_food.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilPage : ContentPage
    {
        ProfilViewModel _profilViewModel;

        public ProfilPage()
        {
            InitializeComponent();

            BindingContext = _profilViewModel = new ProfilViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}