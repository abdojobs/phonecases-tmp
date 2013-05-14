﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace PhoneCases.WPFGUI
{
    public static class PCCommands
    {
        private static readonly RoutedUICommand openCaseWindowCommand = new RoutedUICommand("Open Case Window", "OpenCaseWindow", typeof(PCCommands));
        private static readonly RoutedUICommand openLoginWindowCommand = new RoutedUICommand("Open Login Window", "OpenLoginWindow", typeof(PCCommands));
        private static readonly RoutedUICommand updateCasesCommand = new RoutedUICommand("Update Cases", "UpdateCases", typeof(PCCommands));
        private static readonly RoutedUICommand deleteCaseCommand = new RoutedUICommand("Delete Case", "DeleteCase", typeof(PCCommands));
        private static readonly RoutedUICommand applyFiltersCommand = new RoutedUICommand("Apply Filters", "ApplyFilters", typeof(PCCommands));
        private static readonly RoutedUICommand clearFiltersCommand = new RoutedUICommand("Clear Filters", "ClearFilters", typeof(PCCommands));

        public static RoutedUICommand OpenCaseWindow { get { return openCaseWindowCommand; } }
        public static RoutedUICommand OpenLoginWindow { get { return openLoginWindowCommand; } }
        public static RoutedUICommand UpdateCases { get { return updateCasesCommand; } }
        public static RoutedUICommand DeleteCase { get { return deleteCaseCommand; } }
        public static RoutedUICommand ApplyFilters { get { return applyFiltersCommand; } }
        public static RoutedUICommand ClearFilters { get { return clearFiltersCommand; } }
    }
}