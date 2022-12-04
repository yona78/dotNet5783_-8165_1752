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
        int id= 0;
        IBl blP = new Bl();
        public ProductWindow(IBl bl,int ect)
        {
            InitializeComponent();
            blP = bl;
            CatgoryChoise.Items.Clear();
            CatgoryChoise.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
            if (ect == 0)
            {
                update.Visibility = Visibility.Hidden;
            }
            else
            {
                add.Visibility = Visibility.Hidden;
                BO.Product prdct = blP.Product.GetForManager(ect);
                ID.Text = prdct.ID.ToString();
                CatgoryChoise.SelectedItem = prdct.Category;
                name.Text= prdct.Name;
                price.Text=prdct.Price.ToString();
                inStock.Text=prdct.InStock.ToString();

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Product prdct = new BO.Product();
                int h;
                bool validInput = int.TryParse(ID.Text, out h);
                if (!validInput)
                    throw new Exception();
                prdct.ID = h;
                 validInput = int.TryParse(price.Text, out h);
                if (!validInput)
                    throw new Exception();
                prdct.Price = h;
                validInput = int.TryParse(inStock.Text, out h);
                if (!validInput)
                    throw new Exception();
                prdct.InStock = h;
                prdct.Name = name.Text;
                string selected = CatgoryChoise.SelectedItem.ToString();
                BO.Enums.Category category;
                BO.Enums.Category.TryParse(selected, out category);
                prdct.Category = category;
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

        private void Left_Click(object sender, RoutedEventArgs  e)
        {
            try
            {
                BO.Product prdct = new BO.Product();
                int h;
                bool validInput = int.TryParse(price.Text, out h);
                if (!validInput)
                    throw new Exception();
                prdct.Price = h;
                validInput = int.TryParse(inStock.Text, out h);
                if (!validInput)
                    throw new Exception();
                prdct.InStock = h;
                prdct.Name = name.Text;
                string selected = CatgoryChoise.SelectedItem.ToString();
                BO.Enums.Category category;
                BO.Enums.Category.TryParse(selected, out category);
                prdct.Category = category;
                blP.Product.Update(prdct);
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
    }
}
