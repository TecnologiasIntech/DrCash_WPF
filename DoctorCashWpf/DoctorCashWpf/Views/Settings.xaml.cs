using DoctorCashWpf.Views;
using MaterialDesignThemes.Wpf;
using System.Windows.Controls;

namespace DoctorCashWpf
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
        }

        private async void Button_Click_Register(object sender, System.Windows.RoutedEventArgs e)
        {
            var settings_Register = new Settings_Register();

            await DialogHost.Show(settings_Register, "RootDialog");
        }

        private async void Button_Click_General(object sender, System.Windows.RoutedEventArgs e)
        {
            var settings_General = new Settings_General();

            await DialogHost.Show(settings_General, "RootDialog");
        }


        private async void Button_Click_SMPT(object sender, System.Windows.RoutedEventArgs e)
        {
            var settings_SMPT = new Settings_SMPT();

            await DialogHost.Show(settings_SMPT, "RootDialog");
        }

    }
}
