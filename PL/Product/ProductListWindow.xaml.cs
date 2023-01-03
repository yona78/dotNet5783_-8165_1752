﻿using BlApi;

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
        ObservableCollection<BO.ProductForList> obsColProductForList;
        IEnumerable<BO.ProductForList> products;
        public ProductListWindow()
        {
            InitializeComponent();
            products = bl.Product.GetList();
            obsColProductForList = new ObservableCollection<BO.ProductForList>(products);
            ProductListView.DataContext = obsColProductForList; // that in the beginning it will be initialized
            CategorySelector.Items.Clear();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category)); // the itemSource is all of the possible categories 
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string? selected = CategorySelector.SelectedItem.ToString(); // the category that was selected in the comboBox
            ProductListView.DataContext = (bl ?? BlApi.Factory.Get()).Product.GetList(); // put all the products in the itemSource of the productListView
            BO.Enums.Category category;
            BO.Enums.Category.TryParse(selected, out category); // convert it into a category
            Func<BO.ProductForList?, bool>? func = item => (item ?? new BO.ProductForList()).Category == category; // the condition \ predict we create checks if the categories are equal or not
             products = (bl ?? BlApi.Factory.Get()).Product.GetList(func); // get A list with all the products that answer the deserve condition
            obsColProductForList = new ObservableCollection<BO.ProductForList>(products);
            ProductListView.DataContext = obsColProductForList;
        }

        private void AddProduct(object sender, RoutedEventArgs e)
        {
            new ProductWindow((bl ?? BlApi.Factory.Get()), "ADD", 0).ShowDialog(); // can't do anything else until it closed
            //ProductListView.DataContext = (bl ?? BlApi.Factory.Get()).Product.GetList(); // print the new list on the board
        }

        private void UpdateProductButton(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList prdct = (BO.ProductForList)ProductListView.SelectedItem; // the product we want to update
            if (prdct == null)
                return;
            new ProductWindow((bl ?? BlApi.Factory.Get()), "UPDATE", prdct.ID).ShowDialog(); // can't do anything else until it closed
            //ProductListView.DataContext = (bl ?? BlApi.Factory.Get()).Product.GetList(); // print the new list on the board
        }

        private void DeleteProduct(object sender, RoutedEventArgs e)
        {
            new InputIdForDeleteProductWindow().ShowDialog();
            //ProductListView.DataContext = (bl ?? BlApi.Factory.Get()).Product.GetList(); // print the new list on the board
        }
    }
}