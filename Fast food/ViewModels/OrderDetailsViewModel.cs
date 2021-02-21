using Fast_food.Models.MyModels;
using Fast_food.MyHelpers;
using Fast_food.Views;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Fast_food.ViewModels
{
    [QueryProperty(nameof(OrderId), nameof(OrderId))]
    class OrderDetailsViewModel : BaseViewModel
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
                SetProperty(ref _orderId, value);

                LoadOrderFullInfoCommand.Execute(LoadOrderFullInfo(Convert.ToInt32(_orderId)));
            }
        }

        private MyHttpClient myHttpClient = new MyHttpClient();

        public OrderFullInfo _orderFullInfo;
        public OrderFullInfo orderFullInfo 
        {
            get => _orderFullInfo;
            set => SetProperty(ref _orderFullInfo, value);
        }


        private Order_content _selectedItem;

        public Command LoadOrderFullInfoCommand;
        public Command LoadDataCommand { get; }
        public Command DeleteOrderCommand { get; }
        public Command AddProductCommand { get; }
        public Command<Order_content> ItemTapped { get; }
        public Command ConfirmOrderCommand { get; } 


        public OrderDetailsViewModel()
        {
            Title = "New order";

            LoadOrderFullInfoCommand = new Command(async () => await LoadOrderFullInfo(Convert.ToInt32(_orderId)));
            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());
            DeleteOrderCommand = new Command(async () => await OnDeleteOrder());
            AddProductCommand = new Command(async () => await OnAddProduct());
            ItemTapped = new Command<Order_content>(OnItemSelected);
            ConfirmOrderCommand = new Command(OnSaveOrder);


        }


        public void OnAppearing()
        {

        }

        public async Task LoadOrderFullInfo(int id_order)
        {
            orderFullInfo = (OrderFullInfo)await myHttpClient.sendHttpGetAsyncObject<OrderFullInfo>(Constants.ApiGetClientOrderFullInfoById+"?id_order="+id_order);

            if (orderFullInfo == null)
            {
                await Shell.Current.GoToAsync("..");
            }
        }

        public async Task ExecuteLoadDataCommand()
        {
            IsBusy = true;
            try
            {
                await LoadOrderFullInfo(Convert.ToInt32(OrderId));

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

        private async Task OnDeleteOrder()
        {
            var delete=await App.Current.MainPage.DisplayAlert("Delete order", "Do you really want to delete it ?", "Yes", "No");
            if (delete)
            {
                await myHttpClient.sendHttpDeleteAsyncObject<bool>(Constants.ApiDeleteOrder + "?id_order=" + orderFullInfo.id_order);

                await Shell.Current.GoToAsync("..");
            }
        }

        private async Task OnAddProduct()
        {
            await Shell.Current.GoToAsync($"{nameof(MenuRequestsPage)}?{nameof(MenuViewModel.OrderId)}={orderFullInfo.id_order}");
        }



        public Order_content SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        async void OnItemSelected(Order_content oc)
        {
            if (oc == null)
            {
                return;
            }
            else
            {
                await Shell.Current.GoToAsync($"{nameof(ProductRequestPage)}?{nameof(ProductRequestViewModel.ProductId)}={oc.id_product}&{nameof(ProductRequestViewModel.OrderId)}={_orderId}&{nameof(ProductRequestViewModel.OperationType)}=edit");
            }
        }


        async void OnSaveOrder()
        {
            if(orderFullInfo.order_contents.Count==0)
            {
                await App.Current.MainPage.DisplayAlert("The order is empty", "You can not confirm an empty order.", "Ok");
            }
            else
            {
                var rep = await App.Current.MainPage.DisplayAlert("Confirming", "Are you sure you want to confirm this order (You can not cancel it after the confirmation) ?", "Yes", "No");

                if (rep)
                {
                    await App.Current.MainPage.DisplayAlert("Notification", "The application will use the GPS to get your location.", "Ok");

                    JObject locationInfo = await MyHelper.GetLocation();

                    if ((bool)locationInfo["Worked"])
                    {
                        orderFullInfo.latitude = Convert.ToDouble(locationInfo["Latitude"]);
                        orderFullInfo.longitude = Convert.ToDouble(locationInfo["Longitude"]);

                        

                        await myHttpClient.sendHttpPostAsyncJson(Constants.ApiConfirmOrder, orderFullInfo);

                        await Shell.Current.GoToAsync($"..");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Can not get your location", (string)locationInfo["Message"], "Ok");
                    }

                }
            }
        }

    }
}
