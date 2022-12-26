using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderItemsWindow.xaml
    /// </summary>
    public partial class OrderItemsWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get()!; // as it was asked...
        int id;
        public OrderItemsWindow(int idOfOrder)
        {
            id=idOfOrder; 
            InitializeComponent();
            OrderItemsListView.ItemsSource = bl.Order.GetOrderManager(id).Items; // that in the beginning it will be initialized
        }
        private void UpdateOrderItem(object sender, MouseButtonEventArgs e)
        {
            BO.OrderItem orderItem = (BO.OrderItem)OrderItemsListView.SelectedItem; // the orderItem we want to update
            if (orderItem == null)
                return;
            new OrderItemWindow(id, orderItem.ID).ShowDialog(); // can't do anything else until it closed
            OrderItemsListView.ItemsSource = (bl ?? BlApi.Factory.Get()).Order.GetOrderManager(id).Items; // print the new list on the board
        }
    }
}
