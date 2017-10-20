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
            var DailyTransacions = new Settings_Register();

            await DialogHost.Show(DailyTransacions, "RootDialog");
        }

    }
}
