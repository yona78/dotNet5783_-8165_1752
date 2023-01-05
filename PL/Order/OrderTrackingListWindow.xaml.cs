using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<BO.OrderTracking> obsColOrderTraking
        {
            set
            {
                SetValue(ProductsProperty, value);
            }

            get
            {
                return (ObservableCollection<BO.OrderTracking>)GetValue(ProductsProperty);
            }

        }
        public static readonly DependencyProperty ProductsProperty = DependencyProperty.Register("obsColOrderTraking", typeof(ObservableCollection<BO.OrderTracking>),
            typeof(Window), new PropertyMetadata(new ObservableCollection<BO.OrderTracking>()));

        public BO.OrderTracking Select { get; set; }

        IEnumerable<BO.OrderTracking> listOrderTracking = new List<BO.OrderTracking>();
        public OrderTrackingListWindow()
        {
            IEnumerable<BO.Order?> tmpList = new List<BO.Order?>();
            try
            {
                tmpList = bl.Order.GetDataOf(); // that in the beginning it will be initialized

                listOrderTracking = (from p in tmpList select bl.Order.TrackOrder(p.ID));
                obsColOrderTraking = new ObservableCollection<BO.OrderTracking>(listOrderTracking);

                //foreach (BO.Order? element in tmpList)
                //{
                //    listOrderTracking.Add(bl.Order.TrackOrder(element.ID));
                //}
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            InitializeComponent();
         
        }
        private void UpdateOrderButton(object sender, MouseButtonEventArgs e)
        {
            BO.OrderTracking orderTracking = Select; // the order we want to update
            if (orderTracking == null)
                return;
            new OrderWindow("WATCH", orderTracking.ID).ShowDialog(); // can't do anything else until it closed
        }
    }
}
