using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Interaction logic for MakeOrderWindow.xaml
    /// </summary>
    public partial class MakeOrderWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get()!;
        BO.Cart cart = new BO.Cart();
        public string EmailInput { get; set; }
        public string NameInput { get; set; }
        public string AddressInput { get; set; }
        public MakeOrderWindow(BO.Cart crt)
        {
            InitializeComponent();
            cart = crt;
        }

        private void ButtonOfAccept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Cart.MakeOrder(cart, NameInput, AddressInput, EmailInput);
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
