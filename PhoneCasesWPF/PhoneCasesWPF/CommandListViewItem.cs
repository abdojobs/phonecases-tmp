using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhoneCases.WPFGUI
{

    public class CommandListViewItem : ListViewItem
    {
        public CommandListViewItem():base() {
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(CommandListViewItem),
            //new FrameworkPropertyMetadata(typeof(ListViewItem)));

            this.MouseDoubleClick += CommandListViewItem_MouseDoubleClick;

        }

        private void CommandListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            if(Command!=null)
                Command.Execute(((CommandListViewItem)e.Source).Parameter);
        }

        public static DependencyProperty CommandProperty
        = DependencyProperty.Register(
            "Command",
            typeof(ICommand),
            typeof(CommandListViewItem));

        public static DependencyProperty ParameterProperty
        = DependencyProperty.Register(
            "Parameter",
            typeof(object),
            typeof(CommandListViewItem));

        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }

            set
            {
                SetValue(CommandProperty, value);
            }
        }
        public object Parameter
        {
            get
            {
                return GetValue(ParameterProperty);
            }

            set
            {
                SetValue(ParameterProperty, value);
            }
        }


        
    }
}
