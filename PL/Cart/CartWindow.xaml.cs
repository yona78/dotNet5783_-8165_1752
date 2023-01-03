using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
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
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get()!;
        BO.OrderItem orderItem = new BO.OrderItem();
        BO.Cart? cart = new BO.Cart();
        public CartWindow()
        {
            InitializeComponent();
            cart = new BO.Cart();
            OrderItemsListView.ItemsSource = cart.Items;  // that in the beginning it will be initialized

        }
        /*
    public int ID { set; get; } // id of order item
    public int ProductID { set; get; } // id of prodcut, which product is this item
    public string? Name { set; get; } // name of the product, what is the name of the product that the item is it
    public double Price { set; get; } // price of the product
    public int Amount { set; get; } // amount of things in the item. eg, i can order 4 items from the winter dress product
    public double TotalPrice { set; get; } // total price of this item
        */



        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            new InputIdForDealWithProductWindow("DELETE", cart).ShowDialog();

            OrderItemsListView.ItemsSource = cart.Items;
        }
        private void UpdateItem(object sender, RoutedEventArgs e)
        {
            new InputIdForDealWithProductWindow("UPDATE", cart).ShowDialog();

            OrderItemsListView.ItemsSource = cart.Items;
        }
        private void AddItem(object sender, RoutedEventArgs e)
        {
            new InputIdForDealWithProductWindow("ADD", cart).ShowDialog();
            OrderItemsListView.ItemsSource = cart.Items;
        }
        private void MakeOrder(object sender, RoutedEventArgs e)
        {
            new MakeOrderWindow(cart).ShowDialog();
            this.Close();

        }


    }
}
