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

namespace PL
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public class User
        {
            public string Username { get; set; }
            public int Salt { get; set; }
            public string HashedPassword { get; set; }
        }

        public LoginWindow()
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


/*

namespace passwordStore
{
    [Serializable]
    public class User
    {
        public int Id { get; set; }
        public int Salt { get; set; }
        public string HashedPassword { get; set; }
    }

    class Program
    {
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            Console.WriteLine("what do you want to do?\n1)register\n2)login");
            int choice = int.Parse(Console.ReadLine());
            string password = readPassword();
            switch (choice)
            {
                case 1:
                    int salt = rnd.Next();
                    User u = new User { Id = 1, Salt = salt, HashedPassword = hashPassword(password + salt) };
                    FileStream wfs = new FileStream("users.xml", FileMode.Create); 
                    XmlSerializer x = new XmlSerializer(u.GetType()); 
                    x.Serialize(wfs, u);
                    break;
                case 2:
                    FileStream rfs = new FileStream("../xml/users.xml", FileMode.Open); 
                    XmlSerializer rx = new XmlSerializer(typeof(User)); 
                    User ru = rx.Deserialize(rfs) as User;
                    if (hashPassword(password + ru.Salt) == ru.HashedPassword)
                    {
                        Console.WriteLine("congratulation, hello user ");
                    } else
                    {
                        Console.WriteLine("wrong password");
                    }
                    break;
            }
        }

        private static string hashPassword(string passwordWithSalt)
        {
            SHA512 shaM = new SHA512Managed();
            return Convert.ToBase64String(shaM.ComputeHash(Encoding.UTF8.GetBytes(passwordWithSalt)));
        }

        private static string readPassword()
        {
            Console.WriteLine("Please enter password");
            return Console.ReadLine();
        }
    }
}
*/