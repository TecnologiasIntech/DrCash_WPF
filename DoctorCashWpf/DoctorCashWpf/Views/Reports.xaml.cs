using DoctorCashWpf.Views;
using MaterialDesignThemes.Wpf;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var DailyTransacions = new DailyTransactions(); 

            await DialogHost.Show(DailyTransacions, "RootDialog");
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var ClosedStatements = new ClosedStatements();
            await DialogHost.Show(ClosedStatements, "RootDialog");
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var Log = new Log();
            await DialogHost.Show(Log, "RootDialog");
        }
    }
}
