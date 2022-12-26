using BlApi;
using System;
using System.Windows;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBl bl = BlApi.Factory.Get(); // as it was asked from us
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MoveToManagerDataWindow(object sender, RoutedEventArgs e)
        {
            new ProductListWindow().ShowDialog(); // if he clicked the button, we want to show him the list of the avilable products. can't do anything else until it closed
            new OrderListWindow().ShowDialog(); // if he clicked the button, we want to show him the list of the avilable orders. can't do anything else until it closed
        }
        private void MoveToNewOrderDataWindow(object sender, RoutedEventArgs e)
        {
            
            
                new ShowProductItemsWindow().ShowDialog();
          


        }
        private void MoveToOrderTrackingDataWindow(object sender, RoutedEventArgs e)
        {
            new OrderTrackingListWindow().ShowDialog(); // if he clicked the button, we want to show him the list of the avilable products. can't do anything else until it closed
        }
        private void MoveToBonusWindow(object sender, RoutedEventArgs e)
        {
           // new OrderTrackingWindow().ShowDialog(); // if he clicked the button, we want to show him the list of the avilable products. can't do anything else until it closed
        }

    }
}
