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
using System.Globalization;

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
        private GridViewColumnHeader m_lastHeaderClicked = null;
        private ListSortDirection m_lastDirection = ListSortDirection.Ascending;


        public MainWindow()
        {
            InitializeComponent();


            //Timer updateTimer = new Timer(50*1000);
            //updateTimer.Elapsed += new ElapsedEventHandler(UpdateCasesTimerHandler);
            //updateTimer.Enabled = true;
            
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
            m_interpreter.OnUpdateCases(UpdateCases);
            //Do this with dialog
            //m_interpreter.Init(1);//Tommy 
            if (Properties.Settings.Default.AutoLogin && Properties.Settings.Default.LastUser != null)
                m_interpreter.Init(Properties.Settings.Default.LastUser); //AndroidTest user.
            else
            {
                OpenLoginWindow();
                
            }
            //m_interpreter.StartReceiving();

        }
        private void InitBindings()
        {
            //Command bindings
            this.CommandBindings.Add(new CommandBinding(PCCommands.OpenCaseWindow, OpenCaseWindowCommandHandler));
            this.CommandBindings.Add(new CommandBinding(PCCommands.OpenLoginWindow, OpenLoginWindowCommandHandler));
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
            
            

            MainListView.DataContext = model.Model.Cases.ToList();

            MainListView.ItemsSource = model.Model.Cases.ToList();

            model.Model.Users.Load();
            OwnerCombobox.ItemsSource = model.Model.Users.ToList();
            OwnerCombobox.SelectedIndex = 0; //BAD
            
        }
        public void OpenLoginWindowCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            OpenLoginWindow();
        }
        public void OpenLoginWindow()
        {
            LoginWindow lw = new LoginWindow();
            lw.OnLogin(m_interpreter.Init);
            lw.ShowDialog();
            lw = null;
        }
        public void OpenCaseWindowCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            if (e != null)
                if (e.Parameter != null)
                    OpenCaseWindow((Cases)e.Parameter);
        }
        public void OpenCaseWindowClickHandler(object sender, RoutedEventArgs e)
        {
            if(((Cases)((ListView)e.Source).SelectedItem) != null)
                OpenCaseWindow((Cases)((ListView)e.Source).SelectedItem);
        }
        public void GridViewColumnHeaderClickHandler(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    string header = "ID";//headerClicked.Column.Header.ToString();
                    if (!string.IsNullOrEmpty(header))
                    {
                        if (headerClicked != m_lastHeaderClicked)
                            direction = ListSortDirection.Ascending;
                        else
                        {
                            if (m_lastDirection == ListSortDirection.Ascending)
                                direction = ListSortDirection.Descending;
                            else
                                direction = ListSortDirection.Ascending;
                        }
                        SortListView(MainListView, header, direction);
                        m_lastDirection = direction;
                        m_lastHeaderClicked = headerClicked;
                    }
                }
            }

        }
        private void SortListView(ListView list, string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView = CollectionViewSource.GetDefaultView(list.ItemsSource);
            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }
        public void OpenCaseWindow(string caseId)
        {
            //try
            {

                this.Dispatcher.Invoke((Action)delegate()
                {
                    UpdateCases();
                    Cases Case = ModelContainerHolder.Model.Cases.Find(int.Parse(caseId));
                    OpenCaseWindow(Case);
                }
                );
                //OpenCaseWindow(Case);
            }
            //catch (System.Exception e)
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
            this.Dispatcher.Invoke((Action)delegate()
            {  
                //model.Model.SaveChanges();
                model.UpdateModel();
                model.Model.Cases.Load();
                MainListView.ItemsSource = model.Model.Cases.Local;
                FilterMainListView();
            });
        }
        private void DeleteCaseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            if(((ListView)e.Source).SelectedItem!=null)
                DeleteCase((Cases)((ListView)e.Source).SelectedItem);
        }
        private void DeleteCase(Cases pCase)
        {
            pCase.Active = false;
            model.Model.SaveChanges();
            UpdateCases();
        }


        //Filter-related
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
            
            
            MultiFilter filter = new MultiFilter();

            //Checkboxfilters
            
            filter.AddFilter(ActiveFilter);
            filter.AddFilter(PrioFilter);
            filter.AddFilter(ReconnectFilter);
            filter.AddFilter(ClosedFilter);

            //TextBoxFilters
            filter.AddFilter(CustomerFilter);
            filter.AddFilter(CompanyFilter);
            filter.AddFilter(PhoneFilter);
            filter.AddFilter(CaseNumberFilter);
            filter.AddFilter(LocationFilter);
            filter.AddFilter(InfoTextFilter);
            filter.AddFilter(OwnerFilter);
            filter.AddFilter(TimeFilter);

            ICollectionView view = CollectionViewSource.GetDefaultView(MainListView.ItemsSource);
            view.Filter = new Predicate<object>(filter.Filter);

        }
#region Filters

        //Filters

        private int DaysSince(DateTime time)
        {
            return (int)(DateTime.Now - time).TotalDays;
        }
        private bool TimeFilter(object obj)
        {
            Cases item = obj as Cases;
            if (item == null)
                return false;

            if (TimeAll.IsChecked == true)
                return true;

            if (Time1Day.IsChecked == true)
                return (DaysSince(item.StartTime) < 1);
            if (Time7Day.IsChecked == true)
                return (DaysSince(item.StartTime) < 7);
            if (Time30Day.IsChecked == true)
                return (DaysSince(item.StartTime) < 30);

            if (TimeThisDay.IsChecked == true)
                return item.StartTime.Year == DateTime.Now.Year && item.StartTime.Month == DateTime.Now.Month && item.StartTime.Day == DateTime.Now.Day;
            if (TimeThisWeek.IsChecked == true)
            {
                System.Globalization.Calendar cal = new System.Globalization.GregorianCalendar();
                return cal.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday) == cal.GetWeekOfYear(item.StartTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            }
            if (TimeThisMonth.IsChecked == true)
                return item.StartTime.Year == DateTime.Now.Year && item.StartTime.Month == DateTime.Now.Month;



            return false;
        }
        private bool OwnerFilter(object obj)
        {
            Cases item = obj as Cases;
            if (item == null)
                return false;

            if(((Users)OwnerCombobox.SelectedItem).Id ==0)
                return true;

            if(((Users)OwnerCombobox.SelectedItem).Id == item.Owner.Id)
                return true;

            return false;
        }
        private bool InfoTextFilter(object obj)
        {
            Cases item = obj as Cases;
            if (item == null)
                return false;

            return TextSearchFilter(item.Info, TextTextBox.Text);
        }
        private bool LocationFilter(object obj)
        {
            Cases item = obj as Cases;
            if (item == null)
                return false;

            return TextSearchFilter(item.Customer.Company.Location.Name, LocationTextBox.Text);
        }
        private bool CaseNumberFilter(object obj)
        {
            Cases item = obj as Cases;
            if (item == null)
                return false;

            if (item.Id.ToString().IndexOf(CaseNumberTextBox.Text) > -1)
                return true;

            return false;
        }
        private bool PhoneFilter(object obj)
        {
            Cases item = obj as Cases;
            if (item == null)
                return false;

            return TextSearchFilter(item.Customer.PhoneNumber, PhoneTextBox.Text);
        }
        private bool CompanyFilter(object obj)
        {
            Cases item = obj as Cases;
            if (item == null)
                return false;

            return TextSearchFilter(item.Customer.Company.Name, CompanyTextBox.Text);
        }
        private bool CustomerFilter(object obj)
        {
            Cases item = obj as Cases;
            if (item == null)
                return false;

            return TextSearchFilter(item.Customer.Name, CustomerTextBox.Text);
        }
        private bool TextSearchFilter(String item, String textBoxText)
        {
            if (textBoxText == "")
                return true;

            if (item.ToLower().IndexOf(textBoxText.ToLower()) > -1)
                return true;

            return false;
        }
        private bool ClosedFilter(object obj)
        {
            Cases item = obj as Cases;
            if (item == null)
                return false;

            // [J]
            //Resultat innehåller avslutade
            if (ShowClosed.IsChecked == true)
                return true;

            // [ ]
            //Resultat innehåller inte avslutade
            if (ShowClosed.IsChecked == false && item.Closed == false)
                return true;

            // [O]
            //Resultat innehåller endast avslutade
            if (ShowClosed.IsChecked == null && item.Closed == true)
                return true;

            return false;
        }
        private bool ReconnectFilter(object obj)
        {
            Cases item = obj as Cases;
            if (item == null)
                return false;

            // [J]
            //Resultat innehåller återkoppla
            if (ShowReconnect.IsChecked == true)
                return true;

            // [ ]
            //Resultat innehåller inte återkoppla
            if (ShowReconnect.IsChecked == false && item.Reconnect == false)
                return true;

            // [O]
            //Resultat innehåller endast återkoppla
            if (ShowReconnect.IsChecked == null && item.Reconnect == true)
                return true;

            return false;
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
#endregion

        protected override void OnClosing(CancelEventArgs e)
        {
            m_interpreter.StopReceiving();

            base.OnClosing(e);
        }
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

    [ValueConversion(typeof(object), typeof(string))]
    public class SecondsToTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                string valueString = value.ToString();
                int seconds = int.Parse(valueString);
                TimeSpan time = TimeSpan.FromSeconds(seconds);

                return string.Format("{0:D2}:{1:D2}:{2:D2}",time.Hours,time.Minutes,time.Seconds);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
   
}
