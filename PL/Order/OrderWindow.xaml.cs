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
using BlApi;
using BO;
using DalApi;
using DO;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        BlApi.IBl? blP = BlApi.Factory.Get();
        string option;
        BO.Order order = new BO.Order();
        int idOfOrder;
        int? idProduct;int? amount;


        public OrderWindow(string opt, int id) // idOrder of order, option of action that we want to do
        {
            InitializeComponent();
            option = opt;
            OrderStatusChoise.Items.Clear();
            OrderStatusChoise.ItemsSource = Enum.GetValues(typeof(BO.Enums.Status)); // in order to print 
            try
            {
                order = blP.Order.GetOrderManager(id); // we only want to update this order.
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
                return;
            }
            DataContext = order;
            id = order.ID;
            idOfOrder = order.ID;
            if (option == "WATCH")
            {
                updateCustomer.Visibility = Visibility.Hidden; // hiding the update button
                updateManager.Visibility = Visibility.Hidden; // hiding the update button
                ID.IsEnabled = false;
                CustomerName.IsEnabled = false;
                CustomerEmail.IsEnabled = false;
                CustomerAddress.IsEnabled = false;
                OrderStatusChoise.IsEnabled = false;
                PaymentDate.IsEnabled = false;
                ShipDate.IsEnabled = false;
                DeliveryDate.IsEnabled = false;
                TotelPrice.IsEnabled = false;


            }
            else if (option == "UPDATE_MANAGER")
            {
                updateCustomer.Visibility = Visibility.Hidden; // hiding the updateCustomer button

                ID.IsEnabled = false;
                //CustomerName.IsEnabled = false;
                //CustomerEmail.IsEnabled = false;
                //CustomerAddress.IsEnabled = false;
                OrderStatusChoise.IsEnabled = false;
                PaymentDate.IsEnabled = false;
                ShipDate.IsEnabled = false;
                DeliveryDate.IsEnabled = false;
                TotelPrice.IsEnabled = false;

            }
            else if (option == "UPDATE_CUSTOMER")
            {
                updateManager.Visibility = Visibility.Hidden; // hiding the updateManager button

                ID.IsEnabled = false;
                //CustomerName.IsEnabled = false;
                //CustomerEmail.IsEnabled = false;
                //CustomerAddress.IsEnabled = false;
                OrderStatusChoise.IsEnabled = false;
                PaymentDate.IsEnabled = false;
                ShipDate.IsEnabled = false;
                DeliveryDate.IsEnabled = false;
                TotelPrice.IsEnabled = false;

            }
        }
        private void UpdateManagerOption(object sender, RoutedEventArgs e)
        {
            blP.Order.UpdateNameEmailAddress(order.CustomerAddress, order.CustomerEmail, order.CustomerAddress, order.ID); // the bonus we addes
            //if (func != null)
            //{
            //    (int, int) res = func();
            //    idOfProduct = res.Item1;
            //    amount = res.Item2;
            //}
            try
            {
                (blP ?? BlApi.Factory.Get()).Order.Update(idOfOrder, idProduct??new int(), amount??new int());
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

        private void UpdateCutomerOption(object sender, RoutedEventArgs e)
        {
            blP.Order.UpdateNameEmailAddress(order.CustomerAddress, order.CustomerEmail, order.CustomerAddress, order.ID); // the bonus we addes
            this.Close();
        }
        private void showItemsInOrder(object sender, RoutedEventArgs e)
        {
            new OrderItemsWindow(idOfOrder, option=="UPDATE_MANAGER"?"UPDATE":"WATCH", idProduct, amount).ShowDialog();
            // here i should also give values to amount and idOfProduct
        }


    }
}
