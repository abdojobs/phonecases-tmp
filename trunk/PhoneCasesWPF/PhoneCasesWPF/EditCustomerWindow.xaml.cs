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
    /// Interaction logic for EditCustomerWindow.xaml
    /// </summary>
    public partial class EditCustomerWindow : Window
    {
        private Customers m_customer;
        public EditCustomerWindow(Customers customer)
        {
            m_customer = customer;
            InitializeComponent();
            InitBindings();
        }
        private void InitBindings()
        {
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, CloseCommandHandler));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, SaveCommandHandler));

            this.InputBindings.Add(new InputBinding(ApplicationCommands.Close, new KeyGesture(Key.Escape)));
            this.InputBindings.Add(new InputBinding(ApplicationCommands.Save, new KeyGesture(Key.S,ModifierKeys.Control)));


            TheEditCustomerWindow.DataContext = m_customer;
            

        }
        private void CloseCommandHandler(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void SaveCommandHandler(object sender, RoutedEventArgs e)
        {
            ModelContainerHolder.Model.SaveChanges();
        }
    }
}
