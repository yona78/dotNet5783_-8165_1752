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
    /// Interaction logic for OrderItemWindow.xaml
    /// </summary>
    public partial class OrderItemWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get()!; // as it was asked...
        string option;
        BO.OrderItem? orderItem;
        int idOrder;
        public OrderItemWindow(int idOfOrder, int idOfOrderItem, string opt)
        {
            InitializeComponent();
            option = opt;
            orderItem = bl.Order.GetOrderManager(idOfOrder).Items.Find(x => x.ID == idOfOrderItem);
            //orderItem = (bl// we only want to update this orderItem.
            idOrder = idOfOrder;

            ID.Text = (orderItem ?? new BO.OrderItem()).ID.ToString(); // we want to display this to the window
            ProductID.Text = (orderItem ?? new BO.OrderItem()).ProductID.ToString();// as before
            Name.Text = (orderItem ?? new BO.OrderItem()).Name;// as before
            Price.Text = (orderItem ?? new BO.OrderItem()).Price.ToString();// as before
            Amount.Text = (orderItem ?? new BO.OrderItem()).Amount.ToString();// as before
            TotalPrice.Text = (orderItem ?? new BO.OrderItem()).TotalPrice.ToString();// as before

            if (option == "WATCH")
            {
                Amount.IsEnabled = false;
                update.Visibility = Visibility.Hidden;
            }
        }

        private void UpdateOption(object sender, RoutedEventArgs e)
        {
            try
            {

                int tmp;

                bool validInput = int.TryParse(Amount.Text, out tmp);// getting the Amount from the TextBox, and insert it into the orderItem
                if (!validInput)
                    throw new Exception("amount is invalid");
                (orderItem ?? new BO.OrderItem()).Amount = tmp;
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
