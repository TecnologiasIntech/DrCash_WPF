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
                new DemoItem("User", new UserProfile()),
                new DemoItem("Settings", new Settings()),
            };
        }

        public DemoItem[] DemoItems { get; }
    }
}