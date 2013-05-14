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
using System.ComponentModel;

using PhoneCases.DB;
using model = PhoneCases.DB.ModelContainerHolder;

namespace PhoneCases.WPFGUI
{
    /// <summary>
    /// Interaction logic for CaseWindow.xaml
    /// </summary>
    public partial class CaseWindow : Window
    {
        private Cases m_case;
        public Cases Case { get { return m_case; } set { m_case = value; } }
        public CaseWindow(Cases pCase)
        {
            InitializeComponent();
            m_case = pCase;
            this.Title = pCase.Id.ToString() + " - " + pCase.Customer.Name;
            InitBindings();
        }
        private void InitBindings()
        {
            //Command bindings
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, SaveChangesCommandHandler));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, CloseCommandHandler));
            this.CommandBindings.Add(new CommandBinding(PCCommands.EditCase, EditCaseCommandHandler));
            this.CommandBindings.Add(new CommandBinding(PCCommands.UpdateCase, UpdateCaseCommandHandler));

            this.InputBindings.Add(new InputBinding(ApplicationCommands.Save, new KeyGesture(Key.S,ModifierKeys.Control)));
            this.InputBindings.Add(new InputBinding(ApplicationCommands.Close, new KeyGesture(Key.Escape)));
            this.InputBindings.Add(new InputBinding(PCCommands.EditCase, new KeyGesture(Key.U,ModifierKeys.Control)));


            this.DataContext = m_case;
            CustomerComboBox.Items.Add(m_case.Customer);
            CustomerComboBox.SelectedValue = m_case.Customer;
        }
        private void SaveChangesCommandHandler(object sender, RoutedEventArgs e)
        {
            SaveChanges();
        }
        private void CloseCommandHandler(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void EditCaseCommandHandler(object sender, RoutedEventArgs e)
        {
            if (SaveButton.Visibility == Visibility.Hidden)
            {
                if (!CustomerComboBox.Items.IsEmpty)
                {
                    CustomerComboBox.ItemsSource = null;
                    CustomerComboBox.Items.Clear();
                }
                CustomerComboBox.IsEnabled = true;
                CustomerComboBox.ItemsSource = model.Model.Customers.ToList();
                CustomerComboBox.SelectedValue = m_case.Customer;
                SaveButton.Visibility = Visibility.Visible;
            }
        }
        private void UpdateCaseCommandHandler(object sender, RoutedEventArgs e)
        {
            m_case.Customer = CustomerComboBox.SelectedItem as Customers;
            SaveButton.Visibility = Visibility.Hidden;
            CustomerComboBox.IsEnabled = false;
            SaveChanges();
        }
        private void SaveChanges()
        {
            model.Model.SaveChanges();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            SaveChanges();

            base.OnClosing(e);
        }
    }
}
