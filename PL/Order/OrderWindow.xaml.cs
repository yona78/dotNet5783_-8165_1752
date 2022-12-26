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
        BO.Order order = new BO.Order();
        int idOfOrder;
        int idOfProduct = 0, amount = 0;

        public OrderWindow(string option, int id) // idOrder of order, option of action that we want to do
        {
            InitializeComponent();
            OrderStatusChoise.Items.Clear();
            OrderStatusChoise.ItemsSource = Enum.GetValues(typeof(BO.Enums.Status)); // in order to print 
            //if (option == "ADD")
            //{
            //    update.Visibility = Visibility.Hidden; // hiding the update button
            //}
            BO.Order order = blP.Order.GetOrderManager(id); // we only want to update this order.
            id = order.ID;
            if (option == "UPDATE")
            {
                //add.Visibility = Visibility.Hidden; // hiding the add button


                ID.Text = order.ID.ToString(); // we want to display this to the window
                CustomerName.Text = order.CustomerName;// as before
                CustomerEmail.Text = order.CustomerEmail;// as before
                CustomerAddress.Text = order.CustomerAddress;// as before
                OrderStatusChoise.SelectedItem = order.OrderStatus; // as before
                PaymentDate.Text = order.PaymentDate.ToString();// as before
                ShipDate.Text = order.ShipDate.ToString();// as before
                DeliveryDate.Text = order.DeliveryDate.ToString();// as before
                TotelPrice.Text = order.TotelPrice.ToString();// as before
            }
            /* public int ID { set; get; } // idOrder of order
    public string? CustomerName { set; get; } // customer name
    public string? CustomerEmail { set; get; } // customer email
    public string? CustomerAddress { set; get; } // customer address
    public Enums.Status? OrderStatus { set; get; } // status of order
    public DateTime? PaymentDate { set; get; } // day of payment on this order
    public DateTime? ShipDate { set; get; } // when did you send the order
    public DateTime? DeliveryDate { set; get; } // when did the order actually arrived
    public List<OrderItem>? Items { set; get; } // items in order
    public double TotelPrice { set; get; } // total price of order*/
        }


        private void UpdateOption(object sender, RoutedEventArgs e)
        {
            try
            {
                

                (blP ?? BlApi.Factory.Get()).Order.Update(idOfOrder, idOfProduct, amount);
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

       

        private void showItemsInOrder(object sender, RoutedEventArgs e)
        {
            new OrderItemsWindow(idOfOrder).ShowDialog();
            // here i should also give values to amount and idOfProduct
        }

      
    }
}
