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
    /// Interaction logic for OrderItemsWindow.xaml
    /// </summary>
    public partial class OrderItemsWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get()!; // as it was asked...

        public ObservableCollection<BO.OrderItem> obsColOrderItem
        {
            set
            {
                SetValue(ProductsProperty, value);
            }

            get
            {
                return (ObservableCollection<BO.OrderItem?>)GetValue(ProductsProperty);
            }

        }
        public static readonly DependencyProperty ProductsProperty = DependencyProperty.Register("obsColOrderItem", typeof(ObservableCollection<BO.OrderItem>),
            typeof(Window), new PropertyMetadata(new ObservableCollection<BO.OrderItem>()));
        IEnumerable<BO.OrderItem> orderItems;

        public BO.OrderItem OrderItem { set; get; }

  

       OrderWindow.Bonus bonus = new OrderWindow.Bonus();
        string option;
        int id;
        int? idProduct;
        int? amount;
        public OrderItemsWindow(int idOfOrder, string opt, OrderWindow.Bonus bns)
        {
            bonus = bns;
            id = idOfOrder;
            option = opt;
            this.Left = System.Windows.SystemParameters.PrimaryScreenWidth - Width; // i want that the window will be in the right side of the screen.
            orderItems = bl.Order.GetOrderManager(id).Items;
            obsColOrderItem = new ObservableCollection<BO.OrderItem>(orderItems);

            InitializeComponent();

        }
        private void UpdateOrderItem(object sender, MouseButtonEventArgs e)
        {
            if (OrderItem == null)
                return;
            new OrderItemWindow(id, OrderItem.ID, option, bonus).ShowDialog(); // can't do anything else until it closed
            orderItems = bl.Order.GetOrderManager(id).Items;
            obsColOrderItem = new ObservableCollection<BO.OrderItem>(orderItems);
        }
    }
}
