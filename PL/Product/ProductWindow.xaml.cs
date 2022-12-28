using BlApi;
using System;

using System.Windows;


namespace PL
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        BlApi.IBl? blP = BlApi.Factory.Get();
        int idTmp = 0;
        public ProductWindow(IBl bl, string option, int id) // idOrder of product, option of action that we want to do
        {
            InitializeComponent();
            blP = bl;
            CatgoryChoise.Items.Clear();
            CatgoryChoise.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category)); // in order to print 
            if (option == "ADD")
            {
                update.Visibility = Visibility.Hidden; // hiding the update button
            }
            else if (option == "UPDATE")
            {
                add.Visibility = Visibility.Hidden; // hiding the add button

                BO.Product prdct = blP.Product.GetForManager(id); // we only want to update this product.
                ID.Text = prdct.ID.ToString(); // we want to display this to the window
                CatgoryChoise.SelectedItem = prdct.Category; // as before
                name.Text = prdct.Name; // as before
                price.Text = prdct.Price.ToString(); // as before
                inStock.Text = prdct.InStock.ToString(); // as before
                idTmp = prdct.ID; // we want to have in our hands the old id, in order he wouldn't be able to replace it

                ID.IsEnabled = false; // you can't change it
            }
            else if (option == "WATCH")
            {
                add.Visibility = Visibility.Hidden; // hiding the add button
                update.Visibility = Visibility.Hidden; // hiding the update button

                BO.Product prdct = blP.Product.GetForManager(id); // we only want to show this product.
                ID.Text = prdct.ID.ToString(); // we want to display this to the window
                CatgoryChoise.SelectedItem = prdct.Category; // as before
                name.Text = prdct.Name; // as before
                price.Text = prdct.Price.ToString(); // as before
                inStock.Text = prdct.InStock.ToString(); // as before

                ID.IsEnabled = false;
                CatgoryChoise.IsEnabled = false;
                name.IsEnabled = false;
                price.IsEnabled = false;
                inStock.IsEnabled = false;
            }
        }

        private void AddOption(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Product prdct = new BO.Product();
                int tmp;
                double tmpPrice;
                BO.Enums.Category category;

                prdct.ID = idTmp; // the ID stays the same 

                bool validInput = int.TryParse(ID.Text, out tmp); // getting the ID from the TextBox, and insert it into the product
                if (!validInput)
                    throw new Exception("idOrder is invalid"); // i need to check whether it is realy int
                prdct.ID = tmp;
                validInput = double.TryParse(price.Text, out tmpPrice); // getting the price from the TextBox, and insert it into the product
                if (!validInput)
                    throw new Exception("price is invalid");
                prdct.Price = tmpPrice;
                validInput = int.TryParse(inStock.Text, out tmp);// getting the inStock from the TextBox, and insert it into the product
                if (!validInput)
                    throw new Exception("inStock is invalid");
                prdct.InStock = tmp;
                prdct.Name = name.Text; // // getting the Name from the TextBox, and insert it into the product
                string? selected = CatgoryChoise.SelectedItem.ToString();

                validInput = BO.Enums.Category.TryParse(selected, out category);// getting the category from the TextBox, and insert it into the product
                if (!validInput)
                    throw new Exception("category is invalid");
                prdct.Category = category;
                (blP ?? BlApi.Factory.Get()).Product.Add(prdct);
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

        private void UpdateOption(object sender, RoutedEventArgs e)
        {
            try
            {

                BO.Product prdct = new BO.Product();
                int tmp;
                double tmpPrice;
                BO.Enums.Category category;

                prdct.ID = idTmp; // the ID stays the same 
                bool validInput = double.TryParse(price.Text, out tmpPrice); // getting the price from the TextBox, and insert it into the product
                if (!validInput)
                    throw new Exception("price is invalid");
                prdct.Price = tmpPrice;
                validInput = int.TryParse(inStock.Text, out tmp);// getting the inStock from the TextBox, and insert it into the product
                if (!validInput)
                    throw new Exception("inStock is invalid");
                prdct.InStock = tmp;
                prdct.Name = name.Text; // // getting the Name from the TextBox, and insert it into the product
                string? selected = CatgoryChoise.SelectedItem.ToString();

                validInput = BO.Enums.Category.TryParse(selected, out category);// getting the category from the TextBox, and insert it into the product
                if (!validInput)
                    throw new Exception("category is invalid");
                prdct.Category = category;
                (blP ?? BlApi.Factory.Get()).Product.Update(prdct);
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


    }
}