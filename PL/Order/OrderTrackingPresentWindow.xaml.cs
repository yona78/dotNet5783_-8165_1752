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
    /// Interaction logic for OrderTrackingPresentWindow.xaml
    /// </summary>
    public partial class OrderTrackingPresentWindow : Window
    {
        BlApi.IBl blP = BlApi.Factory.Get()!;
        public OrderTrackingPresentWindow(int id)
        {
            InitializeComponent();
            this.Left = System.Windows.SystemParameters.PrimaryScreenWidth - Width; // i want that the window will be in the right side of the screen.

            OrderStatusChoise.Items.Clear();
            OrderStatusChoise.ItemsSource = Enum.GetValues(typeof(BO.Enums.Status)); // in order to print 

            BO.OrderTracking orderTracking = blP.Order.TrackOrder(id);
            ID.Text = orderTracking.ID.ToString();
            OrderStatusChoise.SelectedItem = orderTracking.OrderStatus;
            Status.Text = orderTracking.status[0].ToString()+"\n"+ orderTracking.status[1].ToString() + "\n"+orderTracking.status[2].ToString() + "\n";
        }
    }
}
