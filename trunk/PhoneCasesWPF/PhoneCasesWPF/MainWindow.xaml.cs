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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.ComponentModel;
using System.Data.Entity;

namespace PhoneCasesWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {
        private ModelContainer m_mc = new ModelContainer();
        //public ModelContainer Mc { get { return m_mc; } set { m_mc = value; } } 

        public MainWindow()
        {
            InitializeComponent();

            
            InitBindings();

            

        
            
            //mc.Users.Add(new Users() { Name = "Tommy", PhoneNumber = "0734186405" });
            //mc.SaveChanges();

        }
        private void InitBindings()
        {
            //Command bindings
            this.CommandBindings.Add(new CommandBinding(PCCommands.OpenCaseWindow, OpenCaseWindow));
           
            //Data bindings
            //var UsrList = m_mc.Users.Where(p => p.Name == "Tommy").Select(p=> p.Name).ToList();
            //TheMainWindow.DataContext = m_mc.Users;

            m_mc.Users.Load();
            
            MainListView.DataContext = m_mc.Users.Local;
            
        }

        public void OpenCaseWindow(object sender, ExecutedRoutedEventArgs e)
        {
            CaseWindow cw = new CaseWindow();
            if (e != null)
                cw.Title = e.Parameter.ToString();
            cw.Show();
        }

    }
    public static class PCCommands
    {
        private static readonly RoutedUICommand openCaseWindowCommand = new RoutedUICommand("Open Case Window", "OpenCaseWindow", typeof(PCCommands));

        public static RoutedUICommand OpenCaseWindow { get { return openCaseWindowCommand; } }

    }
}
