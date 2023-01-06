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
        BO.Product? product;
        BO.Cart cart = new BO.Cart();
        public string Amount { get; set; }
        public OrderItemInCartWindow(string opt, BO.Product prdct, BO.Cart crt)
        {
            cart = crt;
            product = prdct;
            //orderItem = (bl// we only want to update this orderItem.
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
    }
}
