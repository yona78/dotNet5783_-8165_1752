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
        public string ID { get; set; }
        public BO.Enums.Status? Status { get; set; }
        public string OrderStatus { get; set; }
        public bool IDState { get; set; }
        public bool StatusState { get; set; }
        public bool OrderStatusState { get; set; }
        public Array list { get; set; }
        public OrderTrackingPresentWindow(int id)
        {
            this.Left = System.Windows.SystemParameters.PrimaryScreenWidth - Width; // i want that the window will be in the right side of the screen.
            BO.OrderTracking orderTracking = blP.Order.TrackOrder(id);
            ID = orderTracking.ID.ToString();
            Status = orderTracking.OrderStatus;
            OrderStatus = orderTracking.status[0].ToString() + "\n" + orderTracking.status[1].ToString() + "\n" + orderTracking.status[2].ToString() + "\n";
            list = Enum.GetValues(typeof(BO.Enums.Status)); // in order to print 

            IDState = false;
            StatusState = false;
            OrderStatusState = false;
            
            InitializeComponent();

           }
    }
}
