using Fast_food.Models.MyModels;
using Fast_food.MyHelpers;
using Fast_food.Views;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Fast_food.ViewModels
{
    [QueryProperty(nameof(OrderId), nameof(OrderId))]
    public class MenuViewModel : BaseViewModel
    {
        private string _orderId;

        public string OrderId
        {
            get
            {
                return _orderId;
            }
            set
            {
                _orderId = value;
            }
        }



        private MyHttpClient myHttpClient = new MyHttpClient();

        public ProductCategory productCategory;

        public List<Product> listProducts { get; set; }

        public ObservableCollection<Product> listProductsBinding { get; set; }


        public Command LoadDataCommand { get; }
        public Command<Product> ProductTapped { get; }

        private Product _selectedProduct;

        public List<string> pickerCategoryList { get; }
        public int pickedIndex { get; set; }


        public MenuViewModel()
        {
            Title = "Menu";
            pickerCategoryList = new List<string> { ProductCategory.Beverages, ProductCategory.Burgers, ProductCategory.FullMeal, ProductCategory.Pizza, ProductCategory.Salads };
            pickedIndex = 0;

            listProducts = new List<Product>();
            listProductsBinding = new ObservableCollection<Product>();

            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());
            ProductTapped = new Command<Product>(OnProductSelected);

        }

        public async Task ExecuteLoadDataCommand()
        {
            IsBusy = true;
            try
            {
                listProducts = await myHttpClient.sendHttpGetAsyncList<Product>(Constants.ApiGetProductsByCategory + "?categoryP=" + pickerCategoryList[pickedIndex]);
                listProducts.ForEach(p => p.picture = Constants.productsPicturesFolder + p.picture);
                listProductsBinding.Clear();
                foreach (var p in listProducts)
                {
                    listProductsBinding.Add(p);
                }
            }
            catch (Exception e)
            {
                IsBusy = true;
            }
            finally
            {
                IsBusy = false;
            }
        }


        public void OnAppearing()
        {
            IsBusy = true;
            SelectedProduct = null;

        }


        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                SetProperty(ref _selectedProduct, value);
                OnProductSelected(value);
            }
        }


        async void OnProductSelected(Product product)
        {
            if (product == null)
            {
                return;
            }
            else
            {
                await Shell.Current.GoToAsync($"{nameof(ProductRequestPage)}?{nameof(ProductRequestViewModel.ProductId)}={product.id_product}&{nameof(ProductRequestViewModel.OrderId)}={_orderId}&{nameof(ProductRequestViewModel.OperationType)}=add");
            }
        }



    }
}
