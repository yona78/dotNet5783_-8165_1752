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
    /// Interaction logic for InputIdForGetOrderWindow.xaml
    /// </summary>
    public partial class InputIdForGetOrderWindow : Window
    {
        BlApi.IBl? blP = BlApi.Factory.Get()!;
        public string Input { set; get; }
        public InputIdForGetOrderWindow()
        {    
            this.Left = System.Windows.SystemParameters.PrimaryScreenWidth - Width; // i want that the window will be in the right side of the screen.
            InitializeComponent();

        }
        private void ButtonOfAccept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = 0;
                bool validInput = int.TryParse(Input, out id); // getting the ID from the TextBox
                if (!validInput || id < 0)
                    throw new Exception("ID is invalid"); // i need to check whether it is realy int
                blP.Order.GetOrderManager(id);
                new OrderWindow("WATCH", id).Show();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            this.Close();
        }
    }
}
