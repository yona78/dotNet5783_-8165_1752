using BlApi;
using System;

using System.Windows;
using System.Windows.Documents;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        BlApi.IBl? blP = BlApi.Factory.Get();
        public BO.Product Prdct { get; set; }
        public object seeAdd { get; set; }
        public object seeUpdate { get; set; }
        public string Id { set; get; }
        public string Name1 { set; get; }
        public string Price { set; get; }
        public string InStock { set; get; }
        public BO.Enums.Category? Select { set; get; }
        
        // State means whether we can edit it or no. (true means we can). It is binding with IsEnabled
        public bool IDState { set; get; }
        public bool CategoryState { set; get; }
        public bool NameState { set; get; }
        public bool PriceState { set; get; }
        public bool InStockState { set; get; }
        public Array list { set; get; }
        int idTmp = 0;
        public ProductWindow(IBl bl, string option, int id) // idOrder of product, option of action that we want to do
        {
            if (option == "ADD")
            {
                IDState = true;
                CategoryState = true;
                NameState = true;
                PriceState = true;
                InStockState = true;
                seeAdd = Visibility.Visible;
                seeUpdate = Visibility.Hidden;

            }
            else if (option == "UPDATE")
            {
                BO.Product prdct= new BO.Product();
                try
                {
                    prdct = blP.Product.GetForManager(id); // we only want to update this product.
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                IDState = false;
                CategoryState = true;
                NameState = true;
                PriceState = true;
                InStockState = true;
                seeAdd = Visibility.Hidden;
                seeUpdate = Visibility.Visible;
                Prdct = prdct;
                Id = prdct.ID.ToString();
                Name1 = prdct.Name;
                Price = prdct.Price.ToString();
                InStock = prdct.InStock.ToString();
                Select = prdct.Category;
                idTmp = prdct.ID; // we want to have in our hands the old ID, in order he wouldn't be able to replace it
                                  //add.Visibility = Visibility.Hidden; // hiding the add button


                
            }
            else if (option == "WATCH")
            {
                BO.Product prdct = new BO.Product();
                try
                {
                    prdct = blP.Product.GetForManager(id); // we only want to update this product.
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                IDState = false;
                CategoryState = false;
                NameState = false;
                PriceState = false;
                InStockState = false;
                seeAdd = Visibility.Hidden;
                seeUpdate = Visibility.Hidden;
                Prdct = prdct;
                Id = prdct.ID.ToString();
                Name1 = prdct.Name;
                Price = prdct.Price.ToString();
                InStock = prdct.InStock.ToString();
                Select = prdct.Category;
                //add.Visibility = Visibility.Hidden; // hiding the add button
                //update.Visibility = Visibility.Hidden; // hiding the update button

                //ID.Text = Prdct.ID.ToString(); // we want to display this to the window
                //CatgoryChoise.SelectedItem = Prdct.Category; // as before
                //CustomerName.Text = Prdct.Name1; // as before
                //Price.Text = Prdct.Price.ToString(); // as before
                //inStock.Text = Prdct.InStock.ToString(); // as before

                //ID.IsEnabled = false;
                //CatgoryChoise.IsEnabled = false;
                //CustomerName.IsEnabled = false;
                //Price.IsEnabled = false;
                //inStock.IsEnabled = false;
            }
            list = Enum.GetValues(typeof(BO.Enums.Category)); // in order to print
            InitializeComponent();
            blP = bl;
            //CatgoryChoise.Items.Clear(); 
            
        }
        private void AddOption(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Product prdct = new BO.Product();
                int tmp;
                double tmpPrice;

                bool validInput = int.TryParse(Id, out tmp); // getting the ID from the TextBox, and insert it into the product
                if (!validInput)
                    throw new Exception("idOrder is invalid"); // i need to check whether it is realy int
                prdct.ID = tmp;
                validInput = double.TryParse(Price, out tmpPrice); // getting the Price from the TextBox, and insert it into the product
                if (!validInput)
                    throw new Exception("Price is invalid");
                prdct.Price = tmpPrice;
                validInput = int.TryParse(InStock, out tmp);// getting the inStock from the TextBox, and insert it into the product
                if (!validInput)
                    throw new Exception("inStock is invalid");
                prdct.InStock = tmp;
                prdct.Name = Name1; // // getting the Name1 from the TextBox, and insert it into the product
                //string? selected = CatgoryChoise.SelectedItem.ToString();
                prdct.Category = Select;

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

                prdct.ID = idTmp; // the ID stays the same 
                bool validInput = double.TryParse(Price, out tmpPrice); // getting the Price from the TextBox, and insert it into the product
                if (!validInput)
                    throw new Exception("Price is invalid");
                prdct.Price = tmpPrice;
                validInput = int.TryParse(InStock, out tmp);// getting the inStock from the TextBox, and insert it into the product
                if (!validInput)
                    throw new Exception("inStock is invalid");
                prdct.InStock = tmp;
                prdct.Name = Name1; // // getting the Name1 from the TextBox, and insert it into the product
                //string? selected = CatgoryChoise.SelectedItem.ToString();
                prdct.Category = Select;
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