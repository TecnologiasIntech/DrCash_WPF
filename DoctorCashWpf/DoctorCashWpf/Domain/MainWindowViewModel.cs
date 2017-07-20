using System.Configuration;
using MaterialDesignDemo;
using MaterialDesignDemo.Domain;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
using System.Windows.Controls;
using DoctorCashWpf;

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
                new DemoItem("Settings", new Settings()),
                new DemoItem("Help", new Help())

            };
        }

        public DemoItem[] DemoItems { get; }
    }
}