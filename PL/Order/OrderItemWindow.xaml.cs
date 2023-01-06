using Microsoft.VisualBasic.FileIO;
using PL;
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
using static BO.Enums;

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
        public bool IDState { get; set; }
        public bool ProductIDState { get; set; }
        public bool NameState { get; set; }
        public bool PriceState { get; set; }
        public bool AmountState { get; set; }
        public bool TotalPriceState { get; set; }

        public object SeeUpdate { get; set; }
        public string ID { get; set; }
        public string ProductID { get; set; }
        public string Name1 { get; set; }
        public string Price { get; set; }
        public string Amount { get; set; }
        public string TotalPrice { get; set; }

        public OrderItemWindow(int idOfOrder, int idOfOrderItem, string opt, int? idProductFunc = null, int? amountFunc = null)
        {
            option = opt;
            orderItem = bl.Order.GetOrderManager(idOfOrder).Items.Find(x => x.ID == idOfOrderItem);
            //orderItem = (bl// we only want to update this orderItem.
         
            ID = orderItem.ID.ToString();
            ProductID=orderItem.ProductID.ToString();
            Name1 = orderItem.Name.ToString();
            Price = orderItem.Price.ToString();
            Amount = orderItem.Amount.ToString();
            TotalPrice = orderItem.TotalPrice.ToString();

            idOrder = idOfOrder;
            idProductFunc = orderItem.ProductID;
            amountFunc = orderItem.Amount;
            if (option == "WATCH")
            {
                IDState = false;
                ProductIDState = false;
                NameState = false;
                PriceState = false;
                AmountState = false;
                TotalPriceState = false;
                SeeUpdate = Visibility.Hidden;
            }
            else if(option == "UPDATE")
            {
                IDState = false;
                ProductIDState = false;
                NameState = false;
                PriceState = false;
                AmountState = true;
                TotalPriceState = false;
                SeeUpdate = Visibility.Visible;
            }
            InitializeComponent();

        }

        private void UpdateOption(object sender, RoutedEventArgs e)
        {
            try
            {

                int tmp;

                bool validInput = int.TryParse(Amount, out tmp);// getting the InStock from the TextBox, and insert it into the orderItem
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
