using Fast_food.Models.MyModels;
using Fast_food.MyHelpers;
using Fast_food.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Fast_food.ViewModels
{
    public class OrdersViewModel : BaseViewModel
    {

        private MyHttpClient myHttpClient = new MyHttpClient();

        private OrderFullInfo _selectedOrder;

        public List<string> pickerStatesList { get; }
        public int pickedIndex { get; set; }
        public string ordersState { get; set; }

        public bool newOrders { get; set; }
        public bool showOrdersDateTime { get; set; }
        public bool showDeliveryMen { get; set; }
        public bool ordersDelivred { get; set; }

        private OrderVisibility orderVisibility = new OrderVisibility();


        public ProductCategory productCategory;

        public List<OrderFullInfo> listOrders { get; set; }
        public ObservableCollection<OrderFullInfo> listOrdersBinding { get; set; }


        public Command LoadDataCommand { get; }
        public Command<OrderFullInfo> OrderTapped { get; }
        public Command AddOrderCommand { get; }


        public OrdersViewModel()
        {
            Title = "Orders";

            pickerStatesList = OrderStates.app_states_list;
            pickedIndex = 0;

            listOrders = new List<OrderFullInfo>();
            listOrdersBinding = new ObservableCollection<OrderFullInfo>();

            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());
            OrderTapped = new Command<OrderFullInfo>(OnOrderSelected);
            AddOrderCommand = new Command(async () => await OnAddOrder());
        }


        public async Task ExecuteLoadDataCommand()
        {
            IsBusy = true;
            try
            {
                ordersState = OrderStates.server_states_list[pickedIndex];

                if (ordersState == OrderStates.choosing_food)
                {
                    orderVisibility.newOrders = true;
                    orderVisibility.showOrdersDateTime = false;
                    orderVisibility.showDeliveryMen = false;
                    orderVisibility.ordersDelivred = false;
                }
                else if (ordersState == OrderStates.order_to_deliver)
                {
                    orderVisibility.newOrders = false;
                    orderVisibility.showOrdersDateTime = true;
                    orderVisibility.showDeliveryMen = false;
                    orderVisibility.ordersDelivred = false;
                }
                else if (ordersState == OrderStates.order_on_the_way)
                {
                    orderVisibility.newOrders = false;
                    orderVisibility.showOrdersDateTime = true;
                    orderVisibility.showDeliveryMen = true;
                    orderVisibility.ordersDelivred = false;
                }
                else if (ordersState == OrderStates.order_has_been_delivered)
                {
                    orderVisibility.newOrders = false;
                    orderVisibility.showOrdersDateTime = true;
                    orderVisibility.showDeliveryMen = false;
                    orderVisibility.ordersDelivred = true;
                }



                string link = string.Format("{0}?id_client={1}&state={2}", Constants.ApiGetClientOrdersFullInfoByState, App.client.id_client, ordersState);

                listOrders = await myHttpClient.sendHttpGetAsyncList<OrderFullInfo>(link);
                listOrdersBinding.Clear();
                foreach (var p in listOrders)
                {
                    p.orderVisibility = orderVisibility;

                    p.delivery_man.photo = Constants.delivery_menPicturesFolder + p.delivery_man.photo;
                    p.delivery_man.transport = Constants.appPicturesFolder + p.delivery_man.transport;
                    p.client.photo = Constants.clientsPicturesFolder + p.client.photo;

                    listOrdersBinding.Add(p);
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
            SelectedOrder = null;
        }



        public OrderFullInfo SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                SetProperty(ref _selectedOrder, value);
                OnOrderSelected(value);
            }
        }


        private async Task OnAddOrder()
        {
            OrderFullInfo order = new OrderFullInfo();
            order.id_client = App.client.id_client;

            order = (OrderFullInfo)await myHttpClient.sendHttpPostAsyncObject<OrderFullInfo>(Constants.ApiNewOrder, order);

            OnOrderSelected(order);
        }

        async void OnOrderSelected(OrderFullInfo order)
        {
            if (order == null)
            {
                return;
            }
            else
            {
                if (ordersState == OrderStates.choosing_food)
                {
                    await Shell.Current.GoToAsync($"{nameof(OrderDetailsPage)}?{nameof(OrderDetailsViewModel.OrderId)}={order.id_order}");
                }
                else if (ordersState == OrderStates.order_to_deliver)
                {

                }
                else if (ordersState == OrderStates.order_on_the_way)
                {
                    await Shell.Current.GoToAsync($"{nameof(MapPage)}?{nameof(MapViewModel.OrderId)}={order.id_order}&{nameof(MapViewModel.OrderCode)}={order.order_code}&{nameof(MapViewModel.DeliveryManId)}={order.id_delivery_man}&{nameof(MapViewModel.PhoneNum)}={order.delivery_man.phone}");
                }
                else if (ordersState == OrderStates.order_has_been_delivered)
                {

                }
            }
        }



    }
}
