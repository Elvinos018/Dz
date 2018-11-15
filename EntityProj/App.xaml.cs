using EntityProj.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EntityProj
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var locator = new ViewModelLocator();

            var window = new AppView();
            window.DataContext = locator.GetMainViewModel();
            window.ShowDialog();
        }
    }
}
