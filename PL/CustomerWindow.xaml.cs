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
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public CustomerWindow()
        {
            InitializeComponent();
        }
        private void MoveToNewOrderDataWindow(object sender, RoutedEventArgs e)
        {
            new ShowProductItemsWindow().ShowDialog();
        }

        private void MoveToBonusWindow(object sender, RoutedEventArgs e)
        {
            // new OrderTrackingWindow().ShowDialog(); // if he clicked the button, we want to show him the list of the avilable products. can't do anything else until it closed
        }
    }
}
