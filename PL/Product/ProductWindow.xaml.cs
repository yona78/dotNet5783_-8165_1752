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
        public object seeA { get; set; }
        public object seeU { get; set; }
        public string Id { set; get; }
        public string Name1 { set; get; }
        public string Price { set; get; }
        public string Amount { set; get; }
        public string Select { set; get; }
        public bool IDState { set; get; }
        public bool CState { set; get; }
        public bool NState { set; get; }
        public bool PState { set; get; }
        public bool IState { set; get; }
        public Array list { set; get; }
        int idTmp = 0;
        public ProductWindow(IBl bl, string option, int id) // idOrder of product, option of action that we want to do
        {
            if (option == "ADD")
            {
                IDState = true;
                CState = true;
                NState = true;
                PState = true;
                IState = true;
                seeA = Visibility.Visible;
                seeU = Visibility.Hidden;

            }
            else if (option == "UPDATE")
            {
                BO.Product prdct = blP.Product.GetForManager(id); // we only want to update this product.
                IDState = false;
                CState = true;
                NState = true;
                PState = true;
                IState = true;
                seeA = Visibility.Hidden;
                seeU = Visibility.Visible;
                Prdct = prdct;
                Id = prdct.ID.ToString();
                Name1 = prdct.Name;
                Price = prdct.Price.ToString();
                Amount = prdct.InStock.ToString();
                Select = prdct.Category.ToString();
                idTmp = prdct.ID; // we want to have in our hands the old id, in order he wouldn't be able to replace it
                                  //add.Visibility = Visibility.Hidden; // hiding the add button


                
            }
            else if (option == "WATCH")
            {
                BO.Product prdct = blP.Product.GetForManager(id); // we only want to show this product.
                IDState = false;
                CState = false;
                NState = false;
                PState = false;
                IState = false;
                seeA = Visibility.Hidden;
                seeU = Visibility.Hidden;
                Prdct = prdct;
                Id = prdct.ID.ToString();
                Name1 = prdct.Name;
                Price = prdct.Price.ToString();
                Amount = prdct.InStock.ToString();
                Select = prdct.Category.ToString();
                //add.Visibility = Visibility.Hidden; // hiding the add button
                //update.Visibility = Visibility.Hidden; // hiding the update button

                //ID.Text = prdct.ID.ToString(); // we want to display this to the window
                //CatgoryChoise.SelectedItem = prdct.Category; // as before
                //name.Text = prdct.Name; // as before
                //price.Text = prdct.Price.ToString(); // as before
                //inStock.Text = prdct.InStock.ToString(); // as before

                //ID.IsEnabled = false;
                //CatgoryChoise.IsEnabled = false;
                //name.IsEnabled = false;
                //price.IsEnabled = false;
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
                BO.Enums.Category category;

                prdct.ID = idTmp; // the ID stays the same 

                bool validInput = int.TryParse(Id, out tmp); // getting the ID from the TextBox, and insert it into the product
                if (!validInput)
                    throw new Exception("idOrder is invalid"); // i need to check whether it is realy int
                prdct.ID = tmp;
                validInput = double.TryParse(Price, out tmpPrice); // getting the price from the TextBox, and insert it into the product
                if (!validInput)
                    throw new Exception("price is invalid");
                prdct.Price = tmpPrice;
                validInput = int.TryParse(Amount, out tmp);// getting the inStock from the TextBox, and insert it into the product
                if (!validInput)
                    throw new Exception("inStock is invalid");
                prdct.InStock = tmp;
                prdct.Name = Name1; // // getting the Name from the TextBox, and insert it into the product
                //string? selected = CatgoryChoise.SelectedItem.ToString();

                validInput = BO.Enums.Category.TryParse(Select, out category);// getting the category from the TextBox, and insert it into the product
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
                bool validInput = double.TryParse(Price, out tmpPrice); // getting the price from the TextBox, and insert it into the product
                if (!validInput)
                    throw new Exception("price is invalid");
                prdct.Price = tmpPrice;
                validInput = int.TryParse(Amount, out tmp);// getting the inStock from the TextBox, and insert it into the product
                if (!validInput)
                    throw new Exception("inStock is invalid");
                prdct.InStock = tmp;
                prdct.Name = Name1; // // getting the Name from the TextBox, and insert it into the product
                //string? selected = CatgoryChoise.SelectedItem.ToString();

                validInput = BO.Enums.Category.TryParse(Select, out category);// getting the category from the TextBox, and insert it into the product
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