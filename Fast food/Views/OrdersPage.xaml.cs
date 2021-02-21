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
    public partial class OrdersPage : ContentPage
    {
        OrdersViewModel _ordersViewModel;

        public OrdersPage()
        {
            InitializeComponent();

            BindingContext = _ordersViewModel = new OrdersViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ordersViewModel.OnAppearing();
        }

        private void StatePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ordersViewModel.LoadDataCommand.Execute(_ordersViewModel.ExecuteLoadDataCommand());
        }
    }
}