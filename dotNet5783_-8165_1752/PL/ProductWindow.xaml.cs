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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        List<Object> pacads = new List<object>();
        IBl blP = new Bl();
        public ProductWindow(IBl bl)
        {
            InitializeComponent();
            blP = bl;
            Viewbox inputId = creteTextBox();
            pacads.Add(inputId);
            ComboBox combo1 = creteComboBox(Enum.GetValues(typeof(BO.Enums.Status)));
            pacads.Add(combo1);
            Viewbox inputName = creteTextBox();
            pacads.Add(inputName);
            Viewbox inputPrice = creteTextBox();
            pacads.Add(inputPrice);
            Viewbox inputInStock = creteTextBox();
            pacads.Add(inputInStock);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Product prdct = new BO.Product();
                prdct.ID = Convert.ToInt32((((pacads[0] as Viewbox).Child as TextBox).Text));
                prdct.Category = (BO.Enums.Category)(pacads[1] as ComboBox).SelectedItem;
                prdct.Name = Convert.ToString((((pacads[0] as Viewbox).Child as TextBox).Text));
                prdct.Price = Convert.ToInt32((((pacads[3] as Viewbox).Child as TextBox).Text));
                prdct.InStock = Convert.ToInt32((((pacads[4] as Viewbox).Child as TextBox).Text));
                blP.Product.Add(prdct);
            }
            catch (Exception err)
            {

                MessageBox.Show(err.ToString(), "Exception", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            finally
            {

                this.Close();

            }
        }

        private Viewbox creteTextBox()
        {
            Viewbox view1 = new Viewbox();
            TextBox textBox1 = new TextBox();
            textBox1.Text = "";
            view1.Child = textBox1;
            return view1;


        }

        private ComboBox creteComboBox(Array enumy)
        {

            ComboBox comboBox1 = new ComboBox();
            comboBox1.ItemsSource = enumy;

            return comboBox1;


        }
    }
}
