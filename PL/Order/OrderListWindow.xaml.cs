using BlApi;

using System;

using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get()!; // as it was asked...

        public OrderListWindow()
        {
            InitializeComponent();
            OrderListView.ItemsSource = bl.Order.GetOrderList(); // that in the beginning it will be initialized
            
        }
        private void GetOrdersButton(object sender, RoutedEventArgs e)
        {
        }
        private void GetDataOfOrdersButton(object sender, RoutedEventArgs e) { }
        private void UpdateOrderSendButton(object sender, RoutedEventArgs e) { }
        private void UpdateOrderDelieverdButton(object sender, RoutedEventArgs e) { }
        private void OrderTrackingButton(object sender, RoutedEventArgs e) { }
        private void UpdateOrderButton(object sender, RoutedEventArgs e) { }
        private void UpdateProductButton(object sender, MouseButtonEventArgs e)
        {
            BO.OrderForList order = (BO.OrderForList)OrderListView.SelectedItem; // the product we want to update
            if (order== null)
                return;
            new OrderWindow("UPDATE", order.ID).ShowDialog(); // can't do anything else until it closed
            OrderListView.ItemsSource = (bl ?? BlApi.Factory.Get()).Order.GetOrderList(); // print the new list on the board
        }
        /*
        private void AddProductButton(object sender, RoutedEventArgs e)
        {
            new ProductWindow((bl ?? BlApi.Factory.Get()), "ADD", 0).ShowDialog(); // can't do anything else until it closed
            ProductListView.ItemsSource = (bl ?? BlApi.Factory.Get()).Product.GetList(); // print the new list on the board
        }

        private void UpdateProductButton(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList prdct = (BO.ProductForList)ProductListView.SelectedItem; // the product we want to update
            if (prdct == null)
                return;
            new ProductWindow((bl ?? BlApi.Factory.Get()), "UPDATE", prdct.ID).ShowDialog(); // can't do anything else until it closed
            ProductListView.ItemsSource = (bl ?? BlApi.Factory.Get()).Product.GetList(); // print the new list on the board
        }
        */
    }
}
