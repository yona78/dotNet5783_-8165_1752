using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Shell;
using System.Xml;
using System.Xml.Linq;
using BlApi;
using BO;
using DalApi;
using DO;
using Microsoft.VisualBasic;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        BlApi.IBl? blP = BlApi.Factory.Get();
        private string option;
        private int? idProduct, amount;
        public BO.Order Order { set; get; }
        int idOfOrder;
        public bool IDState { get; set; }
        public bool CustomerNameState { get; set; }
        public bool CustomerAddressState { get; set; }
        public bool CustomerEmailState { get; set; }
        public bool ShipDateState { get; set; }
        public bool PaymentDateState { get; set; }
        public bool DeliveryDateState { get; set; }
        public bool StatusState { get; set; }

        public bool TotalPriceState { get; set; }

        public object SeeUpdateManager { get; set; }
        public object SeeUpdateCustomer { get; set; }
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public DateTime? ShipDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public double Price { get; set; }
        public BO.Enums.Status? Status { get; set; }
        public double TotalPrice { get; set; }
        public Array list { set; get; }

        public OrderWindow(string opt, int id) // idOrder of order, option of action that we want to do
        {
            option = opt;
            try
            {
                Order = blP.Order.GetOrderManager(id); // we only want to update this order.
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
                return;
            }
        if (option == "WATCH")
            {
                SeeUpdateManager = Visibility.Hidden;
                SeeUpdateCustomer = Visibility.Hidden;
                IDState = false;
                CustomerNameState = false;
                CustomerEmailState = false;
                CustomerAddressState = false;
                PaymentDateState = false;
                ShipDateState = false;
                DeliveryDateState = false;
                StatusState = false;
                TotalPriceState = false;

                ID = Order.ID;
                CustomerName = Order.CustomerName;
                CustomerEmail = Order.CustomerEmail;
                CustomerAddress = Order.CustomerAddress;
                Status = Order.OrderStatus;
                PaymentDate = Order.PaymentDate;
                ShipDate = Order.ShipDate;
                DeliveryDate = Order.DeliveryDate;
                TotalPrice = Order.TotalPrice;

                //ID.IsEnabled = false;
                //CustomerName.IsEnabled = false;
                //CustomerEmail.IsEnabled = false;
                //CustomerAddress.IsEnabled = false;
                //OrderStatusChoise.IsEnabled = false;
                //PaymentDate.IsEnabled = false;
                //ShipDate.IsEnabled = false;
                //DeliveryDate.IsEnabled = false;
                //TotalPrice.IsEnabled = false;

            }
            else if (option == "UPDATE_MANAGER")
            {
                SeeUpdateManager = Visibility.Visible;
                SeeUpdateCustomer = Visibility.Hidden;
                IDState = false;
                CustomerNameState = true;
                CustomerEmailState = true;
                CustomerAddressState = true;
                PaymentDateState = false;
                ShipDateState = false;
                DeliveryDateState = false;
                StatusState = false;
                TotalPriceState = false;

                ID = Order.ID;
                CustomerName = Order.CustomerName;
                CustomerEmail = Order.CustomerEmail;
                CustomerAddress = Order.CustomerAddress;
                Status = Order.OrderStatus;
                PaymentDate = Order.PaymentDate;
                ShipDate = Order.ShipDate;
                DeliveryDate = Order.DeliveryDate;
                TotalPrice = Order.TotalPrice;

                //updateCustomer.Visibility = Visibility.Hidden; // hiding the updateCustomer button

                //ID.IsEnabled = false;
                ////CustomerName.IsEnabled = false;
                ////CustomerEmail.IsEnabled = false;
                ////CustomerAddress.IsEnabled = false;
                //OrderStatusChoise.IsEnabled = false;
                //PaymentDate.IsEnabled = false;
                //ShipDate.IsEnabled = false;
                //DeliveryDate.IsEnabled = false;
                //TotalPrice.IsEnabled = false;

            }
            else if (option == "UPDATE_CUSTOMER")
            {
                SeeUpdateManager = Visibility.Hidden;
                SeeUpdateCustomer = Visibility.Visible;
                IDState = false;
                CustomerNameState = true;
                CustomerEmailState = true;
                CustomerAddressState = true;
                PaymentDateState = false;
                ShipDateState = false;
                DeliveryDateState = false;
                StatusState = false;
                TotalPriceState = false;

                ID = Order.ID;
                CustomerName = Order.CustomerName;
                CustomerEmail = Order.CustomerEmail;
                CustomerAddress = Order.CustomerAddress;
                Status = Order.OrderStatus;
                PaymentDate = Order.PaymentDate;
                ShipDate = Order.ShipDate;
                DeliveryDate = Order.DeliveryDate;
                TotalPrice = Order.TotalPrice;

                //updateManager.Visibility = Visibility.Hidden; // hiding the updateManager button

                //ID.IsEnabled = false;
                ////CustomerName.IsEnabled = true;
                ////CustomerEmail.IsEnabled = true;
                ////CustomerAddress.IsEnabled = true;
                //OrderStatusChoise.IsEnabled = false;
                //PaymentDate.IsEnabled = false;
                //ShipDate.IsEnabled = false;
                //DeliveryDate.IsEnabled = false;
                //TotalPrice.IsEnabled = false;


            }
            list = Enum.GetValues(typeof(BO.Enums.Status)); // in order to print
            InitializeComponent();
        }
        private void UpdateManagerOption(object sender, RoutedEventArgs e)
        {
            blP.Order.UpdateNameEmailAddress(Order.CustomerName, Order.CustomerEmail, Order.CustomerAddress, Order.ID); // the bonus we added
            CustomerName = Order.CustomerName;
            CustomerEmail = Order.CustomerEmail;
            CustomerAddress = Order.CustomerAddress;
            try
            {
                (blP ?? BlApi.Factory.Get()).Order.Update(idOfOrder, idProduct ?? new int(), amount ?? new int());
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
            blP.Order.UpdateNameEmailAddress(Order.CustomerName, Order.CustomerEmail, Order.CustomerAddress, Order.ID); // the bonus we added
            CustomerName = Order.CustomerName;
            CustomerEmail = Order.CustomerEmail;
            CustomerAddress = Order.CustomerAddress;
            this.Close();
        }
        private void showItemsInOrder(object sender, RoutedEventArgs e)
        {
            new OrderItemsWindow(idOfOrder, option == "UPDATE_MANAGER" ? "UPDATE" : "WATCH", idProduct, amount).ShowDialog();
            // here i should also give values to amount and idOfProduct
        }


    }
}
