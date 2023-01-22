using BlApi;
using System;
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

        private void MoveToManagerDataWindow(object sender, RoutedEventArgs e)
        {
            new LoginWindow().ShowDialog(); // i send him to a login window
        }
        private void MoveToCustomerDataWindow(object sender, RoutedEventArgs e)
        {
            new CustomerWindow().ShowDialog(); // i send him to a login window
        }
        
        private void StartSimulator(object sender, RoutedEventArgs e)
        {
            new SimulatorWindow().Show();
        }

    }
}
