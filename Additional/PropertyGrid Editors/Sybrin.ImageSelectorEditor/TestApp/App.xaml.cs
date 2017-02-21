using Sybrin.ImageSelectorEditor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace TestApp {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            var window = new Sybrin.ImageSelectorEditor.Views.MainWindow();
            var mainVM = new MainVM();
            window.DataContext = mainVM;

            window.ShowDialog();
        }

    }
}
