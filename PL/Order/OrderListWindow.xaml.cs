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
            OrderListView.DataContext = bl.Order.GetOrderList(); // that in the beginning it will be initialized
            
        }
        private void GetOrdersButton(object sender, RoutedEventArgs e)
        {
        }
        private void GetDataOfOrdersButton(object sender, RoutedEventArgs e) {
            new InputIdForGetOrderWindow().ShowDialog();
            OrderListView.DataContext = (bl ?? BlApi.Factory.Get()).Order.GetOrderList(); // print the new list on the board
        }
        private void UpdateOrderSendButton(object sender, RoutedEventArgs e) {
            new UpdateOrderSendingWindow().ShowDialog();
            OrderListView.DataContext = (bl ?? BlApi.Factory.Get()).Order.GetOrderList(); // print the new list on the boards
        }
        private void UpdateOrderDelieverdButton(object sender, RoutedEventArgs e) {
            new UpdateOrderDelieveringWindow().ShowDialog();
            OrderListView.DataContext = (bl ?? BlApi.Factory.Get()).Order.GetOrderList(); // print the new list on the boards
        }
        private void OrderTrackingButton(object sender, RoutedEventArgs e) {
            new OrderTrackingWindow().ShowDialog();
            OrderListView.DataContext = (bl ?? BlApi.Factory.Get()).Order.GetOrderList(); // print the new list on the boards                                                                                  
        }
            private void UpdateOrderButton(object sender, RoutedEventArgs e) {
            new OrderUpdateManagerWindow().ShowDialog();
            OrderListView.DataContext = (bl ?? BlApi.Factory.Get()).Order.GetOrderList(); // print the new list on the boards                                                                                  

        }
        private void UpdateOrderButton(object sender, MouseButtonEventArgs e)
        {
            BO.OrderForList order = (BO.OrderForList)OrderListView.SelectedItem; // the order we want to update
            if (order== null)
                return;
            new OrderWindow("UPDATE_CUSTOMER", order.ID).ShowDialog(); // can't do anything else until it closed
            OrderListView.DataContext = (bl ?? BlApi.Factory.Get()).Order.GetOrderList(); // print the new list on the board
        }
        
    }
}
