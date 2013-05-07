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

using System.Timers;

using PhoneCases.DB;
using PhoneCases.Server;
using model = PhoneCases.DB.ModelContainerHolder;

namespace PhoneCases.WPFGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 



    public partial class MainWindow : Window
    {
        //public ModelContainer Mc { get { return m_mc; } set { m_mc = value; } } 

        public PcInterpreter m_interpreter;



        public MainWindow()
        {
            InitializeComponent();


            Timer updateTimer = new Timer(20*1000);
            updateTimer.Elapsed += new ElapsedEventHandler(UpdateCasesTimerHandler);
            updateTimer.Enabled = true;
            
            InitBindings();

            InitServerConnection();

            FilterMainListView();
            
            

            //m_mc.Users.Add(new Users() { Name = "Tommy", PhoneNumber = "0734186405" });            
            //m_mc.Users.Add(new Users() { Name = "Pelle", PhoneNumber = "0734186415" });

            //m_mc.Locations.Add(new Locations() { Name = "Värmland" });

            //m_mc.CompanyTypes.Add(new CompanyTypes() { Name = "AMB" });
            //m_mc.CompanyTypes.Add(new CompanyTypes() { Name = "RTJ" });
            //m_mc.CompanyTypes.Add(new CompanyTypes() { Name = "Polis" });
            //m_mc.SaveChanges();

            //m_mc.Companies.Add(new Companies() { Name = "Landstinget Värmland", CompanyTypesId = 1, LocationId = 1, });
            //m_mc.SaveChanges();

            //m_mc.Customers.Add(new Customers() {Name = "Emil Palm", CompanyId = 2, PhoneNumber="0"});
            //m_mc.SaveChanges();


            //m_mc.Cases.Add(new Cases() { UserId = 1, CustomersId = 3, StartTime = DateTime.Now });

            //m_mc.SaveChanges();

        }

        private void InitServerConnection()
        {
            m_interpreter = new PcInterpreter(new Receiver(new PcParser(), 21339), new Transmitter());
            
            m_interpreter.OnCaseCreated(OpenCaseWindow);
            //Do this with dialog
            m_interpreter.Init(3); //CUrrently initiating with AndroidTest user.
            
            //m_interpreter.StartReceiving();

        }
        private void InitBindings()
        {
            //Command bindings
            this.CommandBindings.Add(new CommandBinding(PCCommands.OpenCaseWindow, OpenCaseWindowCommandHandler));
            this.CommandBindings.Add(new CommandBinding(PCCommands.UpdateCases, UpdateCasesCommandHandler));
            this.CommandBindings.Add(new CommandBinding(PCCommands.DeleteCase, DeleteCaseCommandHandler));
            this.CommandBindings.Add(new CommandBinding(PCCommands.ApplyFilters, ApplyFiltersCommandHandler));
            this.CommandBindings.Add(new CommandBinding(PCCommands.ClearFilters, ClearFiltersCommandHandler));
           
            this.InputBindings.Add(new InputBinding(PCCommands.UpdateCases,new KeyGesture(Key.F5)));
            //Data bindings
            //var UsrList = m_mc.Users.Where(p => p.Name == "Tommy").Select(p=> p.Name).ToList();
            //TheMainWindow.DataContext = m_mc.Users;

            model.Model.Cases.Load();

            //MainListView.AddHandler(,)
            MainListView.MouseDoubleClick += OpenCaseWindowClickHandler;
            

            MainListView.DataContext = model.Model.Cases.Local;

            MainListView.ItemsSource = model.Model.Cases.Local;
            
        }

        public void OpenCaseWindowCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            if (e != null)
                if (e.Parameter != null)
                    OpenCaseWindow((Cases)e.Parameter);
        }
        public void OpenCaseWindowClickHandler(object sender, RoutedEventArgs e)
        {
            OpenCaseWindow(((Cases)((ListView)e.Source).SelectedItem));
        }
        public void OpenCaseWindow(string caseId)
        {
            try
            {
                System.Windows.Application.Current.Dispatcher.Invoke((Action) delegate()
                {
                    Cases Case = ModelContainerHolder.Model.Cases.Find(int.Parse(caseId));
                    OpenCaseWindow(Case);
                }
                );
                //OpenCaseWindow(Case);
            }
            catch (System.Exception e)
            {
            	//Could not find case with caseid.
            }
            
        }
        public void OpenCaseWindow(Cases Case)
        {
           
            CaseWindow cw = new CaseWindow(Case);
            cw.Show();
        }
        private void UpdateCasesTimerHandler(object sender, ElapsedEventArgs e)
        {
            UpdateCases();
        }
        private void UpdateCasesCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            UpdateCases();
        }
        private void UpdateCases()
        {
            model.UpdateModel();
            model.Model.Cases.Load();
            MainListView.ItemsSource = model.Model.Cases.Local;
            FilterMainListView();
        }
        private void DeleteCaseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            DeleteCase((Cases)((ListView)e.Source).SelectedItem);
        }
        private void DeleteCase(Cases pCase)
        {
            pCase.Active = false;
            model.Model.SaveChanges();
            UpdateCases();
        }


        //filters
        private void ApplyFiltersCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            FilterMainListView();
        }
        private void ClearFiltersCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            FilterMainListView();
        }
        private void FilterMainListView()
        {
            
            ICollectionView view = CollectionViewSource.GetDefaultView(MainListView.ItemsSource);
            MultiFilter filter = new MultiFilter();

            filter.AddFilter(ActiveFilter);
            //filter.AddFilter(PrioFilter);
            

            view.Filter = new Predicate<object>(filter.Filter);

        }     
        private bool ActiveFilter(object obj)
        {
            Cases item = obj as Cases;
            if (item == null)
                return false;

            // [J]
            //Resultat innehåller borttagna
            if (ShowDeleted.IsChecked == true)
                return true;

            // [ ]
            //Resultat innehåller inte borttagna
            if (ShowDeleted.IsChecked == false && item.Active == true)
                return true;

            // [O]
            //Resultat innehåller endast borttagna
            if (ShowDeleted.IsChecked == null && item.Active == false)
                return true;
            
            return false;
        }
        private bool PrioFilter(object obj)
        {
            Cases item = obj as Cases;
            if (item == null)
                return false;

            // [J]
            //Resultat innehåller högprio
            if (ShowPrio.IsChecked == true)
                return true;

            // [ ]
            //Resultat innehåller inte högprio
            if (ShowPrio.IsChecked == false && item.HighPrio == false)
                return true;

            // [O]
            //Resultat innehåller endast högprio
            if (ShowPrio.IsChecked == null && item.HighPrio == true)
                return true;

            

            return false;
        }


    }
    public static class PCCommands
    {
        private static readonly RoutedUICommand openCaseWindowCommand = new RoutedUICommand("Open Case Window", "OpenCaseWindow", typeof(PCCommands));
        private static readonly RoutedUICommand updateCasesCommand = new RoutedUICommand("Update Cases", "UpdateCases", typeof(PCCommands));
        private static readonly RoutedUICommand deleteCaseCommand = new RoutedUICommand("Delete Case", "DeleteCase", typeof(PCCommands));
        private static readonly RoutedUICommand applyFiltersCommand = new RoutedUICommand("Apply Filters", "ApplyFilters", typeof(PCCommands));
        private static readonly RoutedUICommand clearFiltersCommand = new RoutedUICommand("Clear Filters", "ClearFilters", typeof(PCCommands));

        public static RoutedUICommand OpenCaseWindow { get { return openCaseWindowCommand; } }
        public static RoutedUICommand UpdateCases { get { return updateCasesCommand; } }
        public static RoutedUICommand DeleteCase { get { return deleteCaseCommand; } }
        public static RoutedUICommand ApplyFilters { get { return applyFiltersCommand; } }
        public static RoutedUICommand ClearFilters { get { return clearFiltersCommand; } }

    }

    public class MultiFilter
    {
        private List<Predicate<object>> m_filters = new List<Predicate<object>>();
        public Predicate<object> Filter {get; set;}
        public MultiFilter()
        {
            Filter = InternalFilter;
        }
        private bool InternalFilter(object obj)
        {
            foreach (var filter in m_filters)
                if (!filter(obj))
                    return false;
            return true;
        }
        public void AddFilter(Predicate<object> filter)
        {
            m_filters.Add(filter);
        }
        public void RemoveFilter(Predicate<object> filter)
        {
            if (m_filters.Contains(filter))
                m_filters.Remove(filter);
        }  

    }
   
}
