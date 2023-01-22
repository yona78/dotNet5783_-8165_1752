
using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for LoginCustomerWindow.xaml
    /// </summary>
    public partial class LoginCustomerWindow : Window
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public class User
        {
            public string Username { get; set; }
            public int Salt { get; set; }
            public string HashedPassword { get; set; }
        }

        public LoginCustomerWindow()
        {
            InitializeComponent();
        }
        private void LoginFunc(object sender, RoutedEventArgs e)
        {
            const string usersFileName = "users.xml";

            string FPath_n = @"..\xml\config.xml";
            IEnumerable<User?> users = XMLTools.LoadListFromXMLSerializer<User?>(usersFileName);
            User? user = users?.FirstOrDefault(x => x.Username == Username);
            if (user == null)
            {
                MessageBox.Show("username isn't exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (hashPassword(Password + user.Salt) == user.HashedPassword)
            {
                new AdminWindow().ShowDialog();
                Close();
            }
            else
            {
                MessageBox.Show("wrong password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private static string hashPassword(string passwordWithSalt)
        {
            SHA512 shaM = new SHA512Managed();
            return Convert.ToBase64String(shaM.ComputeHash(Encoding.UTF8.GetBytes(passwordWithSalt)));
        }
    }
}