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
    /// Interaction logic for ShowProductItemsWindow.xaml
    /// </summary>
    public partial class ShowProductItemsWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get()!;
        BO.Product product = new BO.Product();
        BO.Cart cart = new BO.Cart();
        public ShowProductItemsWindow()
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

            CategorySelector.Items.Clear();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category)); // the itemSource is all of the possible categories 
        }
        /*public int ID { set; get; } // id of product
        public string? Name1 { set; get; } // name of product
        public double Price { set; get; } // price of product
        public Enums.Category? Category { set; get; } // category of product
        public bool InStock { set; get; } // is the product has enugh in the Dbase
        public int InStock { set; get; } // amount of items from this product in the cart*/
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string? selected = CategorySelector.SelectedItem.ToString(); // the category that was selected in the comboBox
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
            BO.Enums.Category category;
            BO.Enums.Category.TryParse(selected, out category); // convert it into a category
            Func<BO.Product?, bool>? func = item => (item ?? new BO.Product()).Category == category; // the condition \ predict we create checks if the categories are equal or not
            listProduct = (bl ?? BlApi.Factory.Get()).Product.GetDataOf(func); // get A list with all the products that answer the deserve condition
            listProductItems.Clear();
            foreach (BO.Product? item in listProduct)
                listProductItems.Add(bl.Product.GetForCustomer(item.ID, cart));

            ProductItemListView.ItemsSource = listProductItems;// get A list with all the productItems that answer the deserve condition
        }

        private void MoveToCart(object sender, RoutedEventArgs e)
        {
            new CartWindow().ShowDialog();
            // move to cart staff
        }

        private void ShowProduct(object sender, MouseButtonEventArgs e)
        {
            BO.ProductItem prdct = (BO.ProductItem)ProductItemListView.SelectedItem; // the product we want to show
            if (prdct == null)
                return;
            new ProductWindow((bl ?? BlApi.Factory.Get()), "WATCH", prdct.ID).ShowDialog(); // can't do anything else until it closed
        }
    }
}
