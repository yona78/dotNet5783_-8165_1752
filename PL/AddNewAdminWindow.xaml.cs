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

using System.IO;
using System.Security.Cryptography;
using System.Xml.Serialization;
using DL;
using System.Configuration;
using System.Xml.Linq;


namespace PL
{
    /// <summary>
    /// Interaction logic for AddNewAdminWindow.xaml
    /// </summary>
    public partial class AddNewAdminWindow : Window
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public class User
        {
            public string Username { get; set; }
            public int Salt { get; set; }
            public string HashedPassword { get; set; }
        }

        public AddNewAdminWindow()
        {
            InitializeComponent();
        }

        // login option
        private void RegisterFunc(object sender, RoutedEventArgs e)
        {
            string usersFileName = "users.xml";

            string FPath_n = @"..\xml\config.xml";
            List<User?> users = XMLTools.LoadListFromXMLSerializer<User?>(usersFileName);
            if (users.Any(x=>x.Username==Username))
            {
                MessageBox.Show("username already exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Random rnd = new Random();
            int salt = rnd.Next();
            User user = new User { Username = Username, Salt = salt, HashedPassword = hashPassword(Password + salt) };

            XElement root = XElement.Load(FPath_n);
            
            users.Add(user);
            XMLTools.SaveListToXMLSerializer<User>(users, usersFileName);
            root.Save(FPath_n);
            
            if (hashPassword(Password + user.Salt) == user.HashedPassword)
            {
                new AdminWindow().ShowDialog();
                Close();
            }
            else
            {
                MessageBox.Show("wrong password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Close();
        }
        private static string hashPassword(string passwordWithSalt)
        {
            SHA512 shaM = new SHA512Managed();
            return Convert.ToBase64String(shaM.ComputeHash(Encoding.UTF8.GetBytes(passwordWithSalt)));
        }
    }
}


