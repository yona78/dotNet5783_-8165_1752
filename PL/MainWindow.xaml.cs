using BlApi;
using System.Windows;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBl bl = BlApi.Factory.Get(); // as it was asked from us
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
