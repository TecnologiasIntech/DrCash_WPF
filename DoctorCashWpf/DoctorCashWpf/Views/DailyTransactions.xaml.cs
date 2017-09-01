using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace DoctorCashWpf.Views
{
    /// <summary>
    /// Interaction logic for DailyTransactions.xaml
    /// </summary>
    public partial class DailyTransactions : UserControl
    {
        public DailyTransactions()
        {
            InitializeComponent();
        }

        private reportService getreport = new reportService();

        private void Button_Click(object sender, RoutedEventArgs e)
        {            
            this.dataGridViewDailyTransactions.ItemsSource = null;
            dataGridViewDailyTransactions.DataContext= getreport.getDailyTransactions(txtbox_question.Text, Patient_Name.Text, fromdate.Text, todate.Text);            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Reportings.Report1 miventana = new Reportings.Report1(getreport.getDailyTransactions(txtbox_question.Text, Patient_Name.Text, fromdate.Text, todate.Text));            
            miventana.ShowDialog();            
        }
    }
}
