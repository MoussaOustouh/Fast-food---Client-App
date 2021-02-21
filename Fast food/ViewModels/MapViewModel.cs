using Fast_food.MyHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Fast_food.ViewModels
{
    [QueryProperty(nameof(PhoneNum), nameof(PhoneNum))]
    [QueryProperty(nameof(DeliveryManId), nameof(DeliveryManId))]
    [QueryProperty(nameof(OrderId), nameof(OrderId))]
    [QueryProperty(nameof(OrderCode), nameof(OrderCode))]
    class MapViewModel : BaseViewModel
    {
        private string _phoneNum;
        public string PhoneNum
        {
            get
            {
                return _phoneNum;
            }
            set
            {
                _phoneNum = value;
            }
        }

        private string _deliveryManId;
        public string DeliveryManId
        {
            get
            {
                return _deliveryManId;
            }
            set
            {
                _deliveryManId = value;
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

        private string _orderCode;
        public string OrderCode
        {
            get
            {
                return _orderCode;
            }
            set
            {
                _orderCode = value;
                Title = "Map - Odrer code : " + _orderCode;
            }
        }

        private MyHttpClient myHttpClient = new MyHttpClient();
        public Command PhoneCallCommand { get; }


        public MapViewModel()
        {
            

            PhoneCallCommand = new Command(OnPhoneCallTapped);
        }



        private async void OnPhoneCallTapped()
        {
            await Browser.OpenAsync("tel:"+_phoneNum);
        }


    }
}
