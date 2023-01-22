using DL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ShowAdminsWindow.xaml
    /// </summary>
    public partial class ShowAdminsWindow : Window
    {
        public class User
        {
            public string Username { get; set; }
            public int Salt { get; set; }
            public string HashedPassword { get; set; }
        }
        public ObservableCollection<User> obsColAdmins
        {
            set
            {
                SetValue(UserNameProperty, value);
            }

            get
            {
                return (ObservableCollection<User>)GetValue(UserNameProperty);
            }

        }
        public static readonly DependencyProperty UserNameProperty = DependencyProperty.Register("obsColAdmins", typeof(ObservableCollection<User>),
            typeof(Window), new PropertyMetadata(new ObservableCollection<User>()));

        public string Admin { get; set; }
        public ShowAdminsWindow()
        {
            const string usersFileName = "users.xml";

            string FPath_n = @"..\xml\config.xml";
            List<User?> users = XMLTools.LoadListFromXMLSerializer<User?>(usersFileName);

            
            this.Left = System.Windows.SystemParameters.PrimaryScreenWidth - Width; // i want that the window will be in the right side of the screen.
            obsColAdmins = new ObservableCollection<User>(users);

            InitializeComponent();
        }
    }
}

/*
  public ObservableCollection<BO.OrderItem> obsColOrderItem
        {
            set
            {
                SetValue(UserNameProperty, value);
            }

            get
            {
                return (ObservableCollection<BO.OrderItem?>)GetValue(UserNameProperty);
            }

        }
        public static readonly DependencyProperty UserNameProperty = DependencyProperty.Register("obsColOrderItem", typeof(ObservableCollection<BO.OrderItem>),
            typeof(Window), new PropertyMetadata(new ObservableCollection<BO.OrderItem>()));
        IEnumerable<BO.OrderItem> orderItems;

  
 */