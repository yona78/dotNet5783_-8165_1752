using BlApi;
using BlImplementation;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBl bl = new Bl(); // as it was asked from us
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MoveToDataWindow(object sender, RoutedEventArgs e)
        {
            new ProductListWindow().ShowDialog(); // if he clicked the button, we want to show him the list of the avilable products. can't do anything else until it closed
        }

    }
}
