using Fast_food.Models.MyModels;
using Fast_food.MyHelpers;
using Fast_food.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Fast_food.ViewModels
{
    [QueryProperty(nameof(OrderId), nameof(OrderId))]
    [QueryProperty(nameof(ProductId), nameof(ProductId))]
    [QueryProperty(nameof(OperationType), nameof(OperationType))]
    class ProductRequestViewModel : BaseViewModel
    {
        private string _operationType;

        public string OperationType
        {
            get
            {
                return _operationType;
            }
            set
            {
                _operationType = value;
            }
        }


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

        private string _productId;

        public string ProductId
        {
            get
            {
                return _productId;
            }
            set
            {
                _productId = value;

                LoadProductCommand.Execute(LoadProduct(Convert.ToInt32(_productId)));
            }
        }

        private int _quantity;
        public int quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, Math.Abs(value));
        }

        private Order_content oc { set; get; }
        private Order_content _orderContent;
        public Order_content orderContent 
        {
            get => _orderContent;
            set => SetProperty(ref _orderContent, value);
        }

        public bool showDeleteButton { set; get; }

        private MyHttpClient myHttpClient = new MyHttpClient();

        public Product _product;
        public Product product
        {
            get => _product;
            set => SetProperty(ref _product, value);
        }

        public string productsPicturesFolder { set; get; }

        public Command LoadProductCommand;
        public Command LoadDataCommand { get; }
        public Command SaveProductCommand { get; }
        public Command DeleteProductCommand { get; }



        public ProductRequestViewModel()
        {
            LoadProductCommand = new Command(async () => await LoadProduct(Convert.ToInt32(_productId)));
            SaveProductCommand = new Command(OnSaveProduct);
            DeleteProductCommand = new Command(OnDeleteProduct);

            productsPicturesFolder =Constants.productsPicturesFolder;

            orderContent = new Order_content();
        }


        public void OnAppearing()
        {

        }

        public async Task LoadProduct(int id_product)
        {
            product = (Product)await myHttpClient.sendHttpGetAsyncObject<Product>(Constants.ApiGetProductById + "?id_product=" + id_product);
            
            oc = (Order_content)await myHttpClient.sendHttpGetAsyncObject<Order_content>(Constants.ApiGetOrderContent + "?id_order=" + _orderId + "&id_product=" + _productId);

            if (oc == null)
            {
                showDeleteButton = false;
            }
            else
            {
                orderContent = oc;
                showDeleteButton = true;
            }


            if (product == null)
            {
                await Shell.Current.GoToAsync($"../{nameof(MenuRequestsPage)}?{nameof(MenuViewModel.OrderId)}={_orderId}");
            }
            else
            {
                Title = product.title;

                orderContent.price = product.price;
            }

        }


        private async void OnSaveProduct()
        {
            if (orderContent.quantity > 0)
            {
                oc = (Order_content)await myHttpClient.sendHttpGetAsyncObject<Order_content>(Constants.ApiGetOrderContent + "?id_order=" + _orderId + "&id_product=" + _productId);

                if (oc == null)
                {
                    oc = new Order_content();
                    oc.id_order = Convert.ToInt32(_orderId);
                    oc.id_product = Convert.ToInt32(_productId);
                    oc.price = product.price;
                    oc.quantity = orderContent.quantity;

                    await myHttpClient.sendHttpPostAsyncJson(Constants.ApiPostOrderContent, oc);
                }
                else
                {
                    oc.id_order = Convert.ToInt32(_orderId);
                    oc.id_product = Convert.ToInt32(_productId);
                    oc.price = product.price;
                    oc.quantity = orderContent.quantity;

                    await myHttpClient.sendHttpPutAsyncJson(Constants.ApiPutOrderContent, oc);
                }

                if (_operationType.ToLower() == "add")
                {
                    await Shell.Current.GoToAsync($"../../../{nameof(OrderDetailsPage)}?{nameof(OrderDetailsViewModel.OrderId)}={_orderId}");
                }
                else if (_operationType.ToLower() == "edit")
                {
                    await Shell.Current.GoToAsync($"../../{nameof(OrderDetailsPage)}?{nameof(OrderDetailsViewModel.OrderId)}={_orderId}");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Invalid quantity", "The quantity is 0", "Ok");
            }
        }


        private async void OnDeleteProduct()
        {
            await myHttpClient.sendHttpDeleteAsyncJson(Constants.ApiDeleteOrderContent + "?id_order=" + _orderId + "&id_product=" + _productId);

            if (_operationType.ToLower() == "add")
            {
                await Shell.Current.GoToAsync($"../../../{nameof(OrderDetailsPage)}?{nameof(OrderDetailsViewModel.OrderId)}={_orderId}");
            }
            else if (_operationType.ToLower() == "edit")
            {
                await Shell.Current.GoToAsync($"../../{nameof(OrderDetailsPage)}?{nameof(OrderDetailsViewModel.OrderId)}={_orderId}");
            }
        }






    }
}
