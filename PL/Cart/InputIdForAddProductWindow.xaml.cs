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
    /// Interaction logic for InputIdForAddProductWindow.xaml
    /// </summary>
    public partial class InputIdForAddProductWindow : Window
    {
        BlApi.IBl? blP = BlApi.Factory.Get()!;
        BO.Cart cart = new BO.Cart();
        string option;
        int id;
        public InputIdForAddProductWindow(string opt, BO.Cart crt)
        {
            InitializeComponent();
            option = opt;
            cart = crt;
            this.Left = System.Windows.SystemParameters.PrimaryScreenWidth - Width; // i want that the window will be in the right side of the screen.
           
        }
        private void ButtonOfAccept_Click(object sender, RoutedEventArgs e)
        {
            int idOfProduct = 0;
            try
            {
                bool validInput = int.TryParse(TextBoxOfID.Text, out idOfProduct); // getting the ID from the TextBox
                if (!validInput || id < 0)
                    throw new Exception("ID is invalid"); // i need to check whether it is realy int
                BO.Product product = blP.Product.Get(x => x.ID == idOfProduct);
                new OrderItemInCartWindow(option, product, cart).ShowDialog();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.Close();


        }
    }
}

