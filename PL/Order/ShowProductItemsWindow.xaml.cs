using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public BO.Enums.Category? Select { get; set; }
        public ObservableCollection<BO.ProductItem> obsColProductItem
        {
            set
            {
                SetValue(ProductsProperty, value);
            }

            get
            {
                return (ObservableCollection<BO.ProductItem>)GetValue(ProductsProperty);
            }

        }
        public static readonly DependencyProperty ProductsProperty = DependencyProperty.Register("obsColProductItem", typeof(ObservableCollection<BO.ProductItem>),
            typeof(Window), new PropertyMetadata(new ObservableCollection<BO.ProductItem>()));
        public Array listCategory { set; get; }
        IEnumerable<BO.ProductItem> productItems;
        public BO.ProductItem PrdctItem { set; get; }

        public ShowProductItemsWindow()
        {

            IEnumerable<BO.Product?> listProduct = (bl ?? BlApi.Factory.Get()).Product.GetDataOf();
            IEnumerable<BO.ProductItem> listProductItems = new List<BO.ProductItem>();
            try
            {
                listProductItems = (from p in listProduct select bl.Product.GetForCustomer(p.ID, cart));
                var lst = from p in listProductItems let eval = p.InStock where eval ==true select p;
                obsColProductItem = new ObservableCollection<BO.ProductItem>(lst);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            listCategory = Enum.GetValues(typeof(BO.Enums.Category)); // the itemSource is all of the possible      
            InitializeComponent();

        }
        /*public int ID { set; get; } // ID of product
        public string? Name1 { set; get; } // CustomerName of product
        public double Price { set; get; } // Price of product
        public Enums.Category? Category { set; get; } // category of product
        public bool InStock { set; get; } // is the product has enugh in the Dbase
        public int InStock { set; get; } // amount of items from this product in the cart*/
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IEnumerable<BO.Product?> listProduct = (bl ?? BlApi.Factory.Get()).Product.GetDataOf();
            IEnumerable<BO.ProductItem> listProductItems = new List<BO.ProductItem>();
            try
            {
                listProductItems = (from p in listProduct select bl.Product.GetForCustomer(p.ID, cart));
                obsColProductItem = new ObservableCollection<BO.ProductItem>(listProductItems);

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Func<BO.Product?, bool>? func = item => (item ?? new BO.Product()).Category == Select; // the condition \ predict we create checks if the categories are equal or not

            listProduct = (bl ?? BlApi.Factory.Get()).Product.GetDataOf(func); // get A list with all the products that answer the deserve condition
            listProductItems = (from p in listProduct select bl.Product.GetForCustomer(p.ID, cart));
            obsColProductItem = new ObservableCollection<BO.ProductItem>(listProductItems);

        }

        private void MoveToCart(object sender, RoutedEventArgs e)
        {
            new CartWindow().ShowDialog();
            // move to cart staff
        }

        private void ShowProduct(object sender, MouseButtonEventArgs e)
        {
            BO.ProductItem prdctItem = PrdctItem; // the product we want to show
            if (prdctItem == null)
                return;
            new ProductWindow((bl ?? BlApi.Factory.Get()), "WATCH", prdctItem.ID).ShowDialog(); // can't do anything else until it closed
        }
    }
}
