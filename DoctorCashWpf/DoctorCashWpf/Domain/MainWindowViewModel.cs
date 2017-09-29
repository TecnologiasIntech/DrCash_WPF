using System.Configuration;
using MaterialDesignDemo;
using MaterialDesignDemo.Domain;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
using System.Windows.Controls;
using DoctorCashWpf;
using DoctorCashWpf.Views;

namespace MaterialDesignColors.WpfExample.Domain
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            DemoItems = new[]
            {
                new DemoItem("Home", new Home()),
                new DemoItem("Reports", new Reports()),
                new DemoItem("Statistics", new Statistics()),
                new DemoItem("User", new UserProfile()),
                new DemoItem("Settings", new Settings()),
                new DemoItem("Help", new Help())/*,
                new DemoItem("Look", new Credentials())*/
            };
        }

        public DemoItem[] DemoItems { get; }
    }
}