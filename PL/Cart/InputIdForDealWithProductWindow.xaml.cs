﻿using System;
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
    /// Interaction logic for InputIdForDealWithProductWindow.xaml
    /// </summary>
    public partial class InputIdForDealWithProductWindow : Window
    {
        BlApi.IBl? blP = BlApi.Factory.Get()!;
        BO.Cart cart = new BO.Cart();
        string option;
        int id;
        public string Input { get; set; }   
        public InputIdForDealWithProductWindow(string opt, BO.Cart crt)
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
                bool validInput = int.TryParse(Input, out idOfProduct); // getting the ID from the TextBox
                if (!validInput || id < 0)
                    throw new Exception("ID is invalid"); // i need to check whether it is realy int
                BO.Product product = blP.Product.Get(x => x.ID == idOfProduct);
                if(option == "ADD")
                {
                    try
                    {
                        cart = blP.Cart.AddProduct(cart, (product ?? new BO.Product()).ID);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    finally
                    {
                        this.Close();
                    }
                }
                else if(option == "DELETE")
                {
                    try
                    {
                        cart = blP.Cart.UpdateAmount(cart, (product ?? new BO.Product()).ID,0);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    finally
                    {
                        this.Close();
                    }
                }
                else if(option == "UPDATE")
                    new OrderItemInCartWindow("UPDATE", product, cart).Show();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Close();
        }
    }
}

