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
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        IBl bl = new Bl();
        public ProductListWindow()
        {
            
            InitializeComponent();
            ProductListView.ItemsSource = bl.Product.GetList();
            GategorySelector.Items.Clear();
            GategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selected = GategorySelector.SelectedItem.ToString();
            ProductListView.ItemsSource = bl.Product.GetList();
            BO.Enums.Category category;
            BO.Enums.Category.TryParse(selected, out category);
            Func<BO.ProductForList?, bool>? func= item=>item.Category==category;
            ProductListView.ItemsSource = bl.Product.GetList(func);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow(bl).Show();
        }
    }
}
