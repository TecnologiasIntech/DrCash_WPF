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
            /* CashInWindow modalWindow = new CashInWindow();
             modalWindow.ShowDialog();*/

            var transactionService = new transactionService();
            var transaction = new transaction();
            transaction.userId = 4;
            transaction.comment = "sdfsdf";
            transaction.type = 1;
            transaction.amountCharged = 100;
            transaction.cash = 100;
            transaction.credit = 10;
            transaction.check = 100;
            transaction.checkNumber = 2;
            transaction.change = 10;
            transaction.patientFirstName = "Carlos Alatorre";
            transaction.copayment = true;
            transaction.selfPay = false;
            transaction.deductible = false;
            transaction.labs = false;
            transaction.other = false;
            transaction.closed = false;
            transaction.registerId = "Asd";
            transaction.modifiedById = 4;

            transactionService.registerTransaction(transaction);
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
