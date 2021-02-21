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
    public partial class ProductRequestPage : ContentPage
    {
        ProductRequestViewModel _productRequestViewModel;

        public ProductRequestPage()
        {
            InitializeComponent();

            BindingContext = _productRequestViewModel = new ProductRequestViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _productRequestViewModel.OnAppearing();
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            string quantity = QuantityEntry.Text.Trim();
            if (quantity.Contains('-') || quantity.Contains('+'))
            {
                quantity = quantity.Replace("-", "");
                quantity = quantity.Replace("+", "");
                int q = Convert.ToInt32(quantity);
                QuantityEntry.Text = Math.Abs(q).ToString();
            }


            TotalePriceLabel.Text=string.Format("{0:0.00}", _productRequestViewModel.orderContent.price*_productRequestViewModel.orderContent.quantity);
        }
    }
}