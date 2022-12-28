using System;
using System.Collections.Generic;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderTrackingListWindow.xaml
    /// </summary>
    public partial class OrderTrackingListWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get()!;
        public OrderTrackingListWindow()
        {
            IEnumerable<BO.Order?> tmpList = new List<BO.Order?>();
            List<BO.OrderTracking> listOrderTracking = new List<BO.OrderTracking>();
            InitializeComponent();
            try
            {
                tmpList = bl.Order.GetDataOf(); // that in the beginning it will be initialized

                listOrderTracking = new List<BO.OrderTracking>();

                foreach (BO.Order? element in tmpList)
                {
                    listOrderTracking.Add(bl.Order.TrackOrder(element.ID));
                }
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            OrderTrackingListView.ItemsSource = listOrderTracking;
        }
        private void UpdateOrderButton(object sender, MouseButtonEventArgs e)
        {
            BO.OrderTracking orderTracking = (BO.OrderTracking)OrderTrackingListView.SelectedItem; // the order we want to update
            if (orderTracking == null)
                return;
            new OrderWindow("WATCH", orderTracking.ID).ShowDialog(); // can't do anything else until it closed
        }
    }
}
