using BlApi;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;


namespace PL
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get()!; // as it was asked...
        public BO.Enums.Category? Select { get; set; }
        public ObservableCollection<BO.ProductForList> obsColProductForList
        {
            set
            {
                SetValue(ProductsProperty, value);
            }

            get
            {
                return (ObservableCollection<BO.ProductForList>)GetValue(ProductsProperty);
            }

        }
        public static readonly DependencyProperty ProductsProperty = DependencyProperty.Register("obsColProductForList", typeof(ObservableCollection<BO.ProductForList>),
            typeof(Window), new PropertyMetadata(new ObservableCollection<BO.ProductForList>()));
        public Array arr { set; get; }
        IEnumerable<BO.ProductForList> products;
        public BO.ProductForList Prdct { set; get; }
        public ProductListWindow()
        {
            products = bl.Product.GetList();
            obsColProductForList = new ObservableCollection<BO.ProductForList>(products);
            arr = Enum.GetValues(typeof(BO.Enums.Category));
            InitializeComponent();
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //string? selected = CategorySelector.SelectedItem.ToString(); // the category that was selected in the comboBox
            // ProductListView.DataContext = (bl ?? BlApi.Factory.Get()).Product.GetList(); // put all the products in the itemSource of the productListView
            Func<BO.ProductForList?, bool>? func = item => (item ?? new BO.ProductForList()).Category == Select; // the condition \ predict we create checks if the categories are equal or not
            products = (bl ?? BlApi.Factory.Get()).Product.GetList(func); // get A list with all the products that answer the deserve condition
            obsColProductForList = new ObservableCollection<BO.ProductForList>(products);
            //ProductListView.DataContext = obsColProductForList;
        }

        private void AddProduct(object sender, RoutedEventArgs e)
        {
            new ProductWindow((bl ?? BlApi.Factory.Get()), "ADD", 0).ShowDialog(); // can't do anything else until it closed
            products = (bl ?? BlApi.Factory.Get()).Product.GetList(); // get A list with all the products that answer the deserve condition
            obsColProductForList = new ObservableCollection<BO.ProductForList>(products);
            //ProductListView.DataContext = (bl ?? BlApi.Factory.Get()).Product.GetList(); // print the new list on the board
        }

        private void UpdateProductButton(object sender, MouseButtonEventArgs e)
        {
            if (Prdct == null)
                return;
            new ProductWindow((bl ?? BlApi.Factory.Get()), "UPDATE", Prdct.ID).ShowDialog();
            products = (bl ?? BlApi.Factory.Get()).Product.GetList(); // get A list with all the products that answer the deserve condition
            obsColProductForList = new ObservableCollection<BO.ProductForList>(products);// can't do anything else until it closed
            //ProductListView.DataContext = (bl ?? BlApi.Factory.Get()).Product.GetList(); // print the new list on the board
        }

        private void DeleteProduct(object sender, RoutedEventArgs e)
        {
            new InputIdForDeleteProductWindow().ShowDialog();
            products = (bl ?? BlApi.Factory.Get()).Product.GetList(); // get A list with all the products that answer the deserve condition
            obsColProductForList = new ObservableCollection<BO.ProductForList>(products);
            //ProductListView.DataContext = (bl ?? BlApi.Factory.Get()).Product.GetList(); // print the new list on the board
        }
    }
}