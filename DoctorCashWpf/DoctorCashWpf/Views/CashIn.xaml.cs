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
    public partial class CashInWindow : UserControl
    {
        public CashInWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var transactionService = new transactionService();
            var transaction = new transaction();

                          transaction.userId = 4;
            //transaction.dateRegistered = DateTime.Today.ToString("d");
            transaction.comment = txtbox_comment.Text;
                          transaction.type = 1;

            transaction.amountCharged = (float)Convert.ToDouble(txtbox_amountCharge.Text.Remove(0, 1));
            transaction.cash = (float)Convert.ToDouble(txtbox_cash.Text.Remove(0, 1));
            transaction.credit = (float)Convert.ToDouble(txtbox_credit.Text.Remove(0, 1));
            transaction.check = (float)Convert.ToDouble(txtbox_check.Text.Remove(0, 1));
            transaction.checkNumber = Convert.ToInt32(txtbox_numberChecks.Text);
                          transaction.change = 10;
            transaction.patientFirstName = txtbox_patientFirstName.Text;
            transaction.copayment = (bool)checkbox_copayment.IsChecked;
            transaction.selfPay = (bool)checkbox_selfPay.IsChecked; ;
            transaction.deductible = (bool)checkbox_deductible.IsChecked; ;
            transaction.labs = (bool)checkbox_labs.IsChecked; ;
            transaction.other = (bool)checkbox_other.IsChecked; ;
                          transaction.closed = false;
                          transaction.registerId = "Asd";
                          transaction.modifiedById = 4;
            transaction.type = (int)TRANSACTIONTYPE.IN;

            transactionService.setTransaction(transaction);


        }


        private void getTotal_amount_change()
        {
            double total = 0;
            if(txtbox_cash.Text != "")
            {
                total += Convert.ToDouble(txtbox_cash.Text.Remove(0, 1));
            }

            if(txtbox_credit.Text != "")
            {
                total += Convert.ToDouble(txtbox_credit.Text.Remove(0, 1));
            }

            if (txtbox_check.Text != "")
            {
                total += Convert.ToDouble(txtbox_check.Text.Remove(0, 1));
            }

            label_total.Text = "$" + total.ToString() + ".00";

            label_change.Text = "$" + ( total - Convert.ToDouble(txtbox_amountCharge.Text.Remove(0, 1)) ).ToString() + ".00";

        }

        private void txtbox_amountCharge_LostFocus(object sender, RoutedEventArgs e)
        {
            label_amount.Text = txtbox_amountCharge.Text;
            label_change.Text = "$" + (Convert.ToDouble(label_total.Text.Remove(0, 1)) - Convert.ToDouble(txtbox_amountCharge.Text.Remove(0, 1))).ToString() + ".00";
        }

        private void txtbox_amountCharge_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                label_amount.Text = txtbox_amountCharge.Text;

                label_change.Text = "$" + (Convert.ToDouble(label_total.Text.Remove(0, 1)) - Convert.ToDouble(txtbox_amountCharge.Text.Remove(0, 1))).ToString() + ".00";
            }
        }

        private void txtbox_cash_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                getTotal_amount_change();
            }
        }

        private void txtbox_cash_LostFocus(object sender, RoutedEventArgs e)
        {
            getTotal_amount_change();
        }

        private void txtbox_credit_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                getTotal_amount_change();
            }
        }

        private void txtbox_credit_LostFocus(object sender, RoutedEventArgs e)
        {
            getTotal_amount_change();
        }

        private void txtbox_check_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                getTotal_amount_change();
            }
        }

        private void txtbox_check_LostFocus(object sender, RoutedEventArgs e)
        {
            getTotal_amount_change();
        }

        private void txtbox_numberChecks_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void txtbox_numberChecks_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            clearInputs();
        }
        
        private void clearInputs()
        {
            txtbox_amountCharge.Text = "$0.00";
            txtbox_cash.Text = "$0.00";
            txtbox_credit.Text = "$0.00";
            txtbox_check.Text = "$0.00";
            txtbox_numberChecks.Text = "0";
            txtbox_patientFirstName.Text = "";
            txtbox_comment.Text = "";

            label_amount.Text = "$0.00";
            label_change.Text = "$0.00";
            label_total.Text = "$0.00";

            checkbox_copayment.IsChecked = false;
            checkbox_deductible.IsChecked = false;
            checkbox_labs.IsChecked = false;
            checkbox_other.IsChecked = false;
            checkbox_selfPay.IsChecked = false;

        }
    }
}
