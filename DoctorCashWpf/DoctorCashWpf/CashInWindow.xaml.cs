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
using System.Windows.Shapes;

namespace DoctorCashWpf
{
    /// <summary>
    /// Interaction logic for CashInWindow.xaml
    /// </summary>
    public partial class CashInWindow : Window
    {
        public CashInWindow()
        {
            InitializeComponent();
            Application.Current.MainWindow.WindowState = WindowState.Maximized;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var transactionService = new transactionService();
            var transaction = new transaction();

                          transaction.userId = 4;
            //transaction.dateRegistered = DateTime.Today.ToString("d");
            transaction.comment = comment.Text;
                          transaction.type = 1;

            transaction.amountCharged = (float)amountCharged.Value;
            transaction.cash = (float)cash.Value;
            transaction.credit = (float)credit.Value;
            transaction.check = (float)check.Value;
            transaction.checkNumber = (int)checkNumber.Value;
                          transaction.change = 10;
            transaction.patientFirstName = patientFirstName.Text;
            transaction.copayment = (bool)copayment.IsChecked;
            transaction.selfPay = (bool)selfPay.IsChecked; ;
            transaction.deductible = (bool)deductible.IsChecked; ;
            transaction.labs = (bool)labs.IsChecked; ;
            transaction.other = (bool)other.IsChecked; ;
                          transaction.closed = false;
                          transaction.registerId = "Asd";
                          transaction.modifiedById = 4;

            transactionService.registerTransaction(transaction);

            this.Close();
        }
    }
}
