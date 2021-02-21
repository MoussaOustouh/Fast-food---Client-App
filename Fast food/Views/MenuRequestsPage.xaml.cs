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
    public partial class MenuRequestsPage : ContentPage
    {
        MenuViewModel _menuViewModel;

        public MenuRequestsPage()
        {
            InitializeComponent();

            BindingContext = _menuViewModel = new MenuViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _menuViewModel.OnAppearing();
        }

        private void CategoryPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            _menuViewModel.LoadDataCommand.Execute(_menuViewModel.ExecuteLoadDataCommand());
        }

    }
}