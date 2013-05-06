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


using model = PhoneCases.WPFGUI.ModelContainerHolder;

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

            this.InputBindings.Add(new InputBinding(ApplicationCommands.Save, new KeyGesture(Key.S,ModifierKeys.Control)));

            this.DataContext = m_case;
        }
        private void SaveChangesCommandHandler(object sender, RoutedEventArgs e)
        {
            SaveChanges();
        }
        private void SaveChanges()
        {
            model.Model.SaveChanges();
        }
        protected override void OnClosed(EventArgs e)
        {
            SaveChanges();
        }
    }
}
