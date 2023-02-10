using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<BO.OrderItem> obsColOrderItemInCart
        {
            set
            {
                SetValue(ProductsProperty, value);
            }

            get
            {
                return (ObservableCollection<BO.OrderItem?>)GetValue(ProductsProperty);
            }

        }
        public static readonly DependencyProperty ProductsProperty = DependencyProperty.Register("obsColOrderItemInCart", typeof(ObservableCollection<BO.OrderItem>),
            typeof(Window), new PropertyMetadata(new ObservableCollection<BO.OrderItem>()));

        BO.Cart? cart = new BO.Cart();
        public CartWindow()
        {
            cart = new BO.Cart();
            obsColOrderItemInCart = new ObservableCollection<BO.OrderItem>(cart.Items??new List<BO.OrderItem>());// that in the beginning it will be initialized
            InitializeComponent();

        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            new InputIdForDealWithProductWindow("DELETE", cart).Show();

            obsColOrderItemInCart = new ObservableCollection<BO.OrderItem>(cart.Items ?? new List<BO.OrderItem>());
        }
        private void UpdateItem(object sender, RoutedEventArgs e)
        {
            new InputIdForDealWithProductWindow("UPDATE", cart).Show();

            obsColOrderItemInCart = new ObservableCollection<BO.OrderItem>(cart.Items ?? new List<BO.OrderItem>());
        }
        private void AddItem(object sender, RoutedEventArgs e)
        {
            new InputIdForDealWithProductWindow("ADD", cart).Show();
            obsColOrderItemInCart = new ObservableCollection<BO.OrderItem>(cart.Items ?? new List<BO.OrderItem>());
        }
        private void MakeOrder(object sender, RoutedEventArgs e)
        {
            new MakeOrderWindow(cart).Show();
            this.Close();
        }
    }
}
