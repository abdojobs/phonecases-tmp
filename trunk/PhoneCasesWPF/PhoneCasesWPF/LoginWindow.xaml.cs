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

using PhoneCases.DB;

namespace PhoneCases.WPFGUI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public delegate void LoginHandler(int userId);
        public event LoginHandler DoLogin;

        public LoginWindow()
        {
            InitializeComponent();
            MainLoginGrid.DataContext = ModelContainerHolder.Model.Users.ToList();
        }
        
        public void OnLogin(LoginHandler loginHanlder)
        {
            DoLogin += loginHanlder;
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.LastUser != null)
            {
                if (DoLogin != null)
                    DoLogin(Properties.Settings.Default.LastUser);
                this.Close();
            }
        }
    }
}
