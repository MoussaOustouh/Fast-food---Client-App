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
    public partial class OrderDetailsPage : ContentPage
    {
        OrderDetailsViewModel _orderDetailsViewModel;

        public OrderDetailsPage()
        {
            InitializeComponent();

            BindingContext = _orderDetailsViewModel = new OrderDetailsViewModel();
            OrderContentsListView.BindingContext = _orderDetailsViewModel.orderFullInfo;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _orderDetailsViewModel.OnAppearing();
        }
    }
}