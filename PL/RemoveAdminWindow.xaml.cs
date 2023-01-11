using DL;
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
using System.Xml.Linq;

namespace PL
{
    /// <summary>
    /// Interaction logic for RemoveAdminWindow.xaml
    /// </summary>
    public partial class RemoveAdminWindow : Window
    {
        public class User
        {
            public string Username { get; set; }
            public int Salt { get; set; }
            public string HashedPassword { get; set; }
        }

        public string Username { get; set; }
        public RemoveAdminWindow()
        {
            InitializeComponent();
        }
        private void ButtonOfAccept_Click(object sender, RoutedEventArgs e)
        {

            const string usersFileName = "users.xml";

            string FPath_n = @"..\xml\config.xml";
            List<User?> users = XMLTools.LoadListFromXMLSerializer<User?>(usersFileName);

            if (!users.Any(x => x.Username == Username))
            {
                MessageBox.Show("admin isn't exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            XElement root = XElement.Load(FPath_n);
            User? user = users?.FirstOrDefault(x => x.Username == Username);
            users.Remove(user);
            XMLTools.SaveListToXMLSerializer<User>(users, usersFileName);
            root.Save(FPath_n);

            this.Close();
        }
    }
}
