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
    public partial class MapPage : ContentPage
    {
        MapViewModel _mapViewModel;

        public MapPage()
        {
            InitializeComponent();

            BindingContext = _mapViewModel = new MapViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}