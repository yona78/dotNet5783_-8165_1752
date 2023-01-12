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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void MoveToAddNewAdmin(object sender, RoutedEventArgs e)
        {
            new AddNewAdminWindow().ShowDialog();
        }

        private void MoveToProductsWindow(object sender, RoutedEventArgs e)
        {
            new ProductListWindow().ShowDialog(); // if he clicked the button, we want to show him the list of the avilable products. can't do anything else until it closed
        }
        private void MoveToOrdersWindow(object sender, RoutedEventArgs e)
        {
            new OrderListWindow().ShowDialog(); // if he clicked the button, we want to show him the list of the avilable orders. can't do anything else until it closed
        }

        private void ShowAdminsWindow(object sender, RoutedEventArgs e)
        {
            new ShowAdminsWindow().ShowDialog();
        }

        private void MoveToRemoveAdmin(object sender, RoutedEventArgs e)
        {
            new RemoveAdminWindow().ShowDialog();
        }
        private void MoveToOrderTrackingDataWindow(object sender, RoutedEventArgs e)
        {
            new OrderTrackingListWindow().ShowDialog(); // if he clicked the button, we want to show him the list of the avilable products. can't do anything else until it closed
        }
    }

}

///////////////////////////////////////
/// passwords:
/// admin:      admin
/// username:   admin   
/// yona:       123
/// avishai:    123
/// 1:          1
/// arye:       2
///////////////////////////////////////