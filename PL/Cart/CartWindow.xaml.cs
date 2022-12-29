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
        BO.Product product = new BO.Product();
        BO.Cart cart = new BO.Cart();
        public CartWindow()
        {
            InitializeComponent();
            IEnumerable<BO.Product?> listProduct = (bl ?? BlApi.Factory.Get()).Product.GetDataOf();
            List<BO.ProductItem> listProductItems = new List<BO.ProductItem>();
            try
            {
                foreach (BO.Product? item in listProduct)
                    listProductItems.Add(bl.Product.GetForCustomer(item.ID, cart));
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            ProductItemListView.ItemsSource = listProductItems;  // that in the beginning it will be initialized

        }
        /*public int ID { set; get; } // id of product
        public string? Name { set; get; } // name of product
        public double Price { set; get; } // price of product
        public Enums.Category? Category { set; get; } // category of product
        public bool InStock { set; get; } // is the product has enugh in the Dbase
        public int Amount { set; get; } // amount of items from this product in the cart*/
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IEnumerable<BO.Product?> listProduct = (bl ?? BlApi.Factory.Get()).Product.GetDataOf();
            List<BO.ProductItem> listProductItems = new List<BO.ProductItem>();
            try
            {
                foreach (BO.Product? item in listProduct)
                    listProductItems.Add(bl.Product.GetForCustomer(item.ID, cart));
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            ProductItemListView.ItemsSource = listProductItems; // put all the productItems in the itemSource of the productItemListView
        }


        private void ShowProduct(object sender, MouseButtonEventArgs e)
        {
            BO.ProductItem prdct = (BO.ProductItem)ProductItemListView.SelectedItem; // the product we want to show
            if (prdct == null)
                return;
            new ProductWindow((bl ?? BlApi.Factory.Get()), "WATCH", prdct.ID).ShowDialog(); // can't do anything else until it closed
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {

        }
        private void ChangeItem(object sender, RoutedEventArgs e)
        {
            new ProductItemWindow("UPDATE", product.ID).ShowDialog();
        }

        private void MakeOrder(object sender, RoutedEventArgs e)
        {

        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            new ProductItemWindow("ADD", product.ID).ShowDialog();
        }
    }
}
