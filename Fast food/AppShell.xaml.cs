using Fast_food.ViewModels;
using Fast_food.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Fast_food
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //My Routes
            Routing.RegisterRoute(nameof(OrderDetailsPage), typeof(OrderDetailsPage));
            Routing.RegisterRoute(nameof(OrdersPage), typeof(OrdersPage));
            Routing.RegisterRoute(nameof(MenuRequestsPage), typeof(MenuRequestsPage));
            Routing.RegisterRoute(nameof(ProductRequestPage), typeof(ProductRequestPage));
            Routing.RegisterRoute(nameof(MapPage), typeof(MapPage));


        }

    }
}
