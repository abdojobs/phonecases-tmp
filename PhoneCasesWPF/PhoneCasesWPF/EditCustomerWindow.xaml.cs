﻿using System;
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

namespace PhoneCases.WPFGUI
{
    /// <summary>
    /// Interaction logic for EditCustomerWindow.xaml
    /// </summary>
    public partial class EditCustomerWindow : Window
    {
        public EditCustomerWindow()
        {
            InitializeComponent();
            InitBindings();
        }
        private void InitBindings()
        {
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, CloseCommandHandler));

            this.InputBindings.Add(new InputBinding(ApplicationCommands.Close, new KeyGesture(Key.Escape)));
        }
        private void CloseCommandHandler(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}