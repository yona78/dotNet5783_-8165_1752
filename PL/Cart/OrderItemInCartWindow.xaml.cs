using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public OrderItemInCartWindow(string opt, BO.Product prdct, BO.Cart crt)
        {
            InitializeComponent();
            option = opt;
            cart = crt;
            product = prdct;
            //orderItem = (bl// we only want to update this orderItem.


            if (option == "UPDATE")
            {
                add.Visibility = Visibility.Hidden;
                delete.Visibility = Visibility.Hidden;

            }
            else if (option == "ADD")
            {
                update.Visibility = Visibility.Hidden;
                delete.Visibility = Visibility.Hidden;
                AmountTextBlock.Visibility = Visibility.Hidden;
                AmountTextBox.Visibility = Visibility.Hidden;
            }
            else if (option == "DELETE")
            {
                update.Visibility = Visibility.Hidden;
                add.Visibility = Visibility.Hidden;
                AmountTextBlock.Visibility = Visibility.Hidden;
                AmountTextBox.Visibility = Visibility.Hidden;
            }


        }

        private void UpdateOption(object sender, RoutedEventArgs e)
        {
            try
            {
                int amount;

                bool validInput = int.TryParse(AmountTextBox.Text, out amount);// getting the Amount from the TextBox, and insert it into the orderItem
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
