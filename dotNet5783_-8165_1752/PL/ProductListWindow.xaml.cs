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
using System.Xml;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        IBl bl = new Bl(); // as it was asked...
        public ProductListWindow()
        {   
            InitializeComponent();
            ProductListView.ItemsSource = bl.Product.GetList(); // that in the beginning it will be initialized
            CategorySelector.Items.Clear();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category)); // the itemSource is all of the possible categories 
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selected = CategorySelector.SelectedItem.ToString(); // the category that was selected in the comboBox
            ProductListView.ItemsSource = bl.Product.GetList(); // put all the products in the itemSource of the productListView
            BO.Enums.Category category;
            BO.Enums.Category.TryParse(selected, out category); // convert it into a category
            Func<BO.ProductForList?, bool>? func= item=>item.Category==category; // the condition \ predict we create checks if the categories are equal or not
            ProductListView.ItemsSource = bl.Product.GetList(func); // get A list with all the products that answer the deserve condition
        }

        private void AddProductButton(object sender, RoutedEventArgs e)
        {
            new ProductWindow(bl,"ADD",0).Show(); 
            ProductListView.ItemsSource = bl.Product.GetList(); // print the new list on the board
        }

        private void UpdateProductButton(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList prdct = (BO.ProductForList)ProductListView.SelectedItem; // the product we want to update
            new ProductWindow(bl, "UPDATE",prdct.ID).Show();
            ProductListView.ItemsSource = bl.Product.GetList(); // print the new list on the board
        }

 
    }
}
