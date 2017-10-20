using DoctorCashWpf.Views;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;

namespace DoctorCashWpf
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : UserControl
    {
        public Reports()
        {
            InitializeComponent();
        }

        private async void Button_Click_Daily_Transacions(object sender, RoutedEventArgs e)
        {
            var DailyTransacions = new DailyTransactions(); 

            await DialogHost.Show(DailyTransacions, "RootDialog");
        }

        private async void Button_Click_Closed_Statements(object sender, RoutedEventArgs e)
        {
            var ClosedStatements = new ClosedStatements();
            await DialogHost.Show(ClosedStatements, "RootDialog");
        }

        private async void Button_Click_Log(object sender, RoutedEventArgs e)
        {
            var Log = new Log();
            await DialogHost.Show(Log, "RootDialog");
        }

        private async void Button_Click_View_Receipt(object sender, RoutedEventArgs e)
        {
            var ViewReceipt = new ViewReceipt();
            await DialogHost.Show(ViewReceipt, "RootDialog");

            if (cashInUpdate.isUpdate)
            {
                await DialogHost.Show(new UpdateTransaction(), "RootDialog");

                cashInUpdate.isUpdate = false;

                Button_Click_View_Receipt(null, null);
            }
        }
    }
}
