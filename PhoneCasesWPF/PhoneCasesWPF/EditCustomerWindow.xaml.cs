using System;
using System.Collections.Generic;
using System.Data;
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
        private string m_name;
        private string m_phone;
        private Companies m_company;
        public EditCustomerWindow(Customers customer)
        {
            ModelContainerHolder.UpdateModel();
            m_customer = customer;

            m_name = m_customer.Name;
            m_phone = m_customer.PhoneNumber;
            m_company = m_customer.Company;


            ModelContainerHolder.Model.Customers.Attach(m_customer);
            InitializeComponent();
            InitBindings();
        }
        private void InitBindings()
        {
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, SaveCommandHandler)); //NOTE
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, SaveCommandHandler));

            this.InputBindings.Add(new InputBinding(ApplicationCommands.Close, new KeyGesture(Key.Escape)));
            this.InputBindings.Add(new InputBinding(ApplicationCommands.Save, new KeyGesture(Key.S,ModifierKeys.Control)));

            
            TheEditCustomerWindow.DataContext = m_customer;
            CompanyComboBox.ItemsSource = ModelContainerHolder.Model.Companies.ToList();
            CompanyComboBox.SelectedItem = m_customer.Company;
            

        }
        private void CloseCommandHandler(object sender, RoutedEventArgs e)
        {
            /*//Reset changes and close
            m_customer.Company = m_company;
            m_customer.Name = m_name;
            m_customer.PhoneNumber = m_phone;

            ModelContainerHolder.Model.Entry(m_customer).State = EntityState.Unchanged;
            

            this.Close();*/
        }
        private void SaveCommandHandler(object sender, RoutedEventArgs e)
        {
            if (CompanyComboBox != null)
                m_customer.Company = CompanyComboBox.SelectedItem as Companies;

            ModelContainerHolder.Model.SaveChanges();
            ModelContainerHolder.UpdateModel();
            this.Close();
        }
    }
}
