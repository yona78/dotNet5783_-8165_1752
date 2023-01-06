using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.RightsManagement;
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
using System.Xml.Linq;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderItemInCartWindow.xaml
    /// </summary>
    public partial class OrderItemInCartWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get()!; // as it was asked...
        string option;
        BO.Product? product;
        BO.Cart cart = new BO.Cart();
        public string Amount { get; set; }      
        public object SeeAmountTextBox { get; set; }
        public object SeeAmountTextBlock { get; set; }
        public object SeeAdd { get; set; }
        public object SeeUpdate { get; set; }
        public object SeeDelete { get; set; }
        public OrderItemInCartWindow(string opt, BO.Product prdct, BO.Cart crt)
        {
            option = opt;
            cart = crt;
            product = prdct;
            //orderItem = (bl// we only want to update this orderItem.


            if (option == "UPDATE")
            {
                SeeAdd = Visibility.Hidden;
                SeeDelete = Visibility.Hidden;                
                SeeUpdate = Visibility.Visible;                
                SeeAmountTextBox = Visibility.Visible;                
                SeeAmountTextBlock = Visibility.Visible;                
            }
            else if (option == "ADD")
            {
                SeeAdd = Visibility.Visible;
                SeeDelete = Visibility.Hidden;
                SeeUpdate = Visibility.Hidden;
                SeeAmountTextBox = Visibility.Hidden;
                SeeAmountTextBlock = Visibility.Hidden;
            }
            else if (option == "DELETE")
            {
                SeeAdd = Visibility.Hidden;
                SeeDelete = Visibility.Visible;
                SeeUpdate = Visibility.Hidden;
                SeeAmountTextBox = Visibility.Hidden;
                SeeAmountTextBlock = Visibility.Hidden;
            }

            InitializeComponent();

        }

        private void UpdateOption(object sender, RoutedEventArgs e)
        {
            try
            {
                int amount;

                bool validInput = int.TryParse(Amount, out amount);// getting the InStock from the TextBox, and insert it into the orderItem
                if (!validInput)
                    throw new Exception("amount is invalid");
                cart = bl.Cart.UpdateAmount(cart, (product ?? new BO.Product()).ID, amount);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                this.Close();
            }
        }
        private void DeleteOption(object sender, RoutedEventArgs e)
        {
            try
            {
                cart = bl.Cart.UpdateAmount(cart, (product ?? new BO.Product()).ID, 0);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                this.Close();
            }
        }

        private void AddOption(object sender, RoutedEventArgs e)
        {
            try
            {
                cart = bl.Cart.AddProduct(cart, (product ?? new BO.Product()).ID);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                this.Close();
            }
        }
    }
}
