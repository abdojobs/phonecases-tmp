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

namespace PhoneCasesWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.CommandBindings.Add(new CommandBinding(PCCommands.OpenCaseWindow, OpenCaseWindow));
            InitCommandBindings();

            //ModelContainer mc = new ModelContainer();
            //mc.Users.Add(new Users() { Name = "Tommy", PhoneNumber = "0734186405" });
            //mc.SaveChanges();

        }
        private void InitCommandBindings()
        {
           // this.CommandBindings.Add(new CommandBinding(PCCommands.OpenCaseWindow, OpenCaseWindow));
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
