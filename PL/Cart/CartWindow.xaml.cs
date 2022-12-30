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
            IEnumerable<BO.OrderItem?> listOrderItems = cart.Items;
            OrderItemsListView.ItemsSource = listOrderItems;  // that in the beginning it will be initialized

        }
        /*
    public int ID { set; get; } // id of order item
    public int ProductID { set; get; } // id of prodcut, which product is this item
    public string? Name { set; get; } // name of the product, what is the name of the product that the item is it
    public double Price { set; get; } // price of the product
    public int Amount { set; get; } // amount of things in the item. eg, i can order 4 items from the winter dress product
    public double TotalPrice { set; get; } // total price of this item
        */
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IEnumerable<BO.OrderItem?> listOrderItems = cart.Items;
            OrderItemsListView.ItemsSource = listOrderItems; // put all the productItems in the itemSource of the productItemListView
        }


        private void ShowProduct(object sender, MouseButtonEventArgs e)
        {
            BO.ProductItem prdct = (BO.ProductItem)OrderItemsListView.SelectedItem; // the product we want to show
            if (prdct == null)
                return;
            new ProductWindow((bl ?? BlApi.Factory.Get()), "WATCH", prdct.ID).ShowDialog(); // can't do anything else until it closed
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {

        }
        private void UpdateItem(object sender, RoutedEventArgs e)
        {
            new OrderItemInCartWindow("UPDATE", orderItem.ID, cart).ShowDialog();
        }

        private void MakeOrder(object sender, RoutedEventArgs e)
        {

        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            new OrderItemInCartWindow("ADD", orderItem.ID, cart).ShowDialog();
        }
    }
}
