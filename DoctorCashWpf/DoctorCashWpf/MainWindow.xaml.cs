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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CashInButton_Click(object sender, RoutedEventArgs e)
        {
            // Opens a new Modal Window
            CashInWindow modalWindow = new CashInWindow();
             modalWindow.ShowDialog();
          /*  var transactionService = new transactionService();
            var list = new List<transaction>();
            list = transactionService.getTransactions();*/
          
        }

        private void CashOutButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RefundButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseDateButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
