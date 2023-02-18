using BlApi;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<BO.OrderForList> obsColOrderForList
        {
            set
            {
                SetValue(ProductsProperty, value);
            }

            get
            {
                return (ObservableCollection<BO.OrderForList?>)GetValue(ProductsProperty);
            }

        }
        public static readonly DependencyProperty ProductsProperty = DependencyProperty.Register("obsColOrderForList", typeof(ObservableCollection<BO.OrderForList>),
            typeof(Window), new PropertyMetadata(new ObservableCollection<BO.OrderForList>()));
        IEnumerable<BO.OrderForList> orders;

        public BO.OrderForList Order { set; get; }
        public OrderListWindow()
        {
            orders = bl.Order.GetOrderList();
            obsColOrderForList = new ObservableCollection<BO.OrderForList>(orders);
            InitializeComponent();
            //OrderListView.DataContext = bl.Order.GetOrderList(); // that in the beginning it will be initialized

        }
        
        private void GetDataOfOrdersButton(object sender, RoutedEventArgs e)
        {
            new InputIdForGetOrderWindow().Show();
            orders = bl.Order.GetOrderList();
            obsColOrderForList = new ObservableCollection<BO.OrderForList>(orders);
        }
        private void UpdateOrderSendButton(object sender, RoutedEventArgs e)
        {
            new UpdateOrderSendingWindow().ShowDialog();
            orders = bl.Order.GetOrderList();
            obsColOrderForList = new ObservableCollection<BO.OrderForList>(orders);
        }
        private void UpdateOrderDelieverdButton(object sender, RoutedEventArgs e)
        {
            new UpdateOrderDelieveringWindow().ShowDialog();
            orders = bl.Order.GetOrderList();
            obsColOrderForList = new ObservableCollection<BO.OrderForList>(orders);
        }
        private void OrderTrackingButton(object sender, RoutedEventArgs e)
        {
            new OrderTrackingWindow().ShowDialog();
            orders = bl.Order.GetOrderList();
            obsColOrderForList = new ObservableCollection<BO.OrderForList>(orders);
        }
        private void UpdateOrderButton(object sender, RoutedEventArgs e)
        {
            new OrderUpdateManagerWindow().ShowDialog();
            orders = bl.Order.GetOrderList();
            obsColOrderForList = new ObservableCollection<BO.OrderForList>(orders);

        }
        private void UpdateOrderButton(object sender, MouseButtonEventArgs e)
        {
            if (Order == null)
                return;
            new OrderWindow("UPDATE_CUSTOMER", Order.ID).ShowDialog(); // can't do anything else until it closed
            orders = bl.Order.GetOrderList();
            obsColOrderForList = new ObservableCollection<BO.OrderForList>(orders);
        }

    }
}
