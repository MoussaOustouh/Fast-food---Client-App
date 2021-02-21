using System;
using System.Collections.Generic;
using System.Text;

namespace Fast_food
{
    public class Constants
    {

        public static readonly string ip = "192.168.1.236";
        /*public static readonly string ApiURL = "http://" + ip + ":49863/";*/

        public static readonly string ApiURL = "http://" + ip + "/WebFastfood/";

        public static readonly string ClientCheckEmail = ApiURL + "api/Accounts/ClientCheckEmail";
        public static readonly string ApiSignUp = ApiURL + "api/Accounts/ClientSignUp";
        public static readonly string ApiSignIn = ApiURL + "api/Accounts/ClientSignIn";


        // Account
        public static readonly string ApiGetDeliveryManById = ApiURL + "api/Accounts/GetDeliveryManById";
        public static readonly string ApiGetClientById = ApiURL + "api/Accounts/GetClientById";  // HttpGet    ?id_client=6

        //
        public static readonly string ApiPutChangeClientInfo = ApiURL + "api/Accounts/PutChangeClientInfo";
        public static readonly string ApiPutChangeClientPassword = ApiURL + "api/Accounts/PutChangeClientPassword";
        public static readonly string ApiPutChangeDeliveryManInfo = ApiURL + "api/Accounts/PutChangeDeliveryManInfo";
        public static readonly string ApiPutChangeDeliveryManPassword = ApiURL + "api/Accounts/PutChangeDeliveryManPassword";
        public static readonly string ApiPutChangeDeliveryManTransport = ApiURL + "api/Accounts/PutChangeDeliveryManTransport";



        // Product
        public static readonly string ApiGetProductsByCategory = ApiURL + "api/Products/GetProductsByCategory";
        public static readonly string ApiGetProductById = ApiURL + "api/Products/GetProductById";  // HttpGet  +?id_product=36


        // Orders
        public static readonly string ApiGetClientOrdersByState = ApiURL + "api/Orders/GetClientOrdersByState";
        public static readonly string ApiGetOrderById = ApiURL + "api/Orders/GetOrderById";

        public static readonly string ApiGetClientOrdersFullInfoByState = ApiURL + "api/Orders/GetClientOrdersFullInfoByState"; //HttpGet
        public static readonly string ApiGetClientOrderFullInfoById = ApiURL + "api/Orders/GetClientOrderFullInfoById"; //HttpGet  +?id_order=36

        public static readonly string ApiNewOrder = ApiURL + "api/Orders/NewOrder"; // HttpPost   request body : {"id_client": 6 }
        public static readonly string ApiDeleteOrder = ApiURL + "api/Orders/DeleteOrder"; // HttpDelete  |  +?id_order=36  |  return bool;

        public static readonly string ApiGetOrderContentsByIdOrder = ApiURL + "api/Orders/GetOrderContentsByIdOrder"; //HttpGet  +?id_order=36
        public static readonly string ApiGetOrderContent = ApiURL + "api/Orders/GetOrderContent"; //HttpGet  +?id_order=36&id_product=63
        public static readonly string ApiPostOrderContent = ApiURL + "api/Orders/PostOrderContent"; //HttpPost    request boby : {"quantity": 10, "price": 30, "id_order": 45, "id_product": 13}
        public static readonly string ApiPutOrderContent = ApiURL + "api/Orders/PutOrderContent"; //HttpPut    request boby : {"quantity": 10, "price": 30, "id_order": 45, "id_product": 13}
        public static readonly string ApiDeleteOrderContent = ApiURL + "api/Orders/DeleteOrderContent"; //HttpDelete  +?id_order=36&id_product=63

        public static readonly string ApiConfirmOrder = ApiURL + "api/Orders/ConfirmOrder"; //HttpPost    request boby : {"latitude": xxxxxx, "longitude": yyyyyyy}

       








        // Picturs
        public static readonly string picturesFolder = ApiURL + "pictures/";
        public static readonly string appPicturesFolder = picturesFolder + "app/";
        public static readonly string adminsPicturesFolder = picturesFolder + "admins/";
        public static readonly string productsPicturesFolder = picturesFolder + "products/";
        public static readonly string clientsPicturesFolder = picturesFolder + "clients/";
        public static readonly string delivery_menPicturesFolder = picturesFolder + "delivery_men/";





        // Colors
	    public static readonly string colorPrimary = "#f58634";
	    public static readonly string colorPrimaryDark = "#ffa45b";
	    public static readonly string colorAccent = "#ffcc29";
        public static readonly string color1 = "#16c79a";

    }
}
