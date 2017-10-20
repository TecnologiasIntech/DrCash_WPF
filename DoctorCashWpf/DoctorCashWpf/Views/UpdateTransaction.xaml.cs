using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DoctorCashWpf.Views
{
    /// <summary>
    /// Interaction logic for UpdateTransaction.xaml
    /// </summary>
    public partial class UpdateTransaction : UserControl
    {
        public UpdateTransaction()
        {
            InitializeComponent();
            loadTransactionInfo();
        }

        private logService logService = new logService();
        private transactionService transactionService = new transactionService();
        private dateService dateService = new dateService();
        private MoneyFormatService moneyFormatService = new MoneyFormatService();


        private void loadTransactionInfo()
        {
            transaction listTransaction = transactionService.getAllTransactionByTrnID(cashInUpdate.transactionID.ToString());

            patienName.Text = listTransaction.patientFirstName;
            chequedcopayment.IsChecked = (bool)listTransaction.copayment;
            chequeddeductible.IsChecked = (bool)listTransaction.deductible;
            chequedlaps.IsChecked = (bool)listTransaction.labs;
            chequedother.IsChecked = (bool)listTransaction.other;
            chequedselfpay.IsChecked = (bool)listTransaction.selfPay;
            txtamountcharge.Text = listTransaction.amountCharged.ToString();
            txttotal.Text = listTransaction.total_cash.ToString();
            txtchange.Text = listTransaction.change.ToString();
            txtcash.Text = listTransaction.cash.ToString();
            txtcredid.Text = listTransaction.credit.ToString();
            txtcheck.Text = listTransaction.check.ToString().ToString();
            txtchecknumber.Text = listTransaction.checkNumber.ToString();
            txtcomment.Text = listTransaction.comment;

            convertTextBoxToFormatMoney();

            setTotalCash();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            transaction trn = new transaction();

            trn.patientFirstName = patienName.Text;
            trn.amountCharged = (float)Convert.ToDouble(txtamountcharge.Text.Remove(0, 1));
            trn.cash = (float)Convert.ToDouble(txtcash.Text.Remove(0, 1));
            trn.credit = (float)Convert.ToDouble(txtcredid.Text.Remove(0, 1));
            trn.check = (float)Convert.ToDouble(txtcheck.Text.Remove(0, 1));
            trn.checkNumber = Convert.ToInt32(txtchecknumber.Text);
            trn.copayment = (bool)chequedcopayment.IsChecked;
            trn.selfPay = (bool)chequedselfpay.IsChecked;
            trn.deductible = (bool)chequeddeductible.IsChecked;
            trn.labs = (bool)chequedlaps.IsChecked;
            trn.other = (bool)chequedother.IsChecked;
            trn.otherComments = txtBoxOtherComment.Text;
            trn.comment = txtcomment.Text;
            trn.change = (float)Convert.ToDouble(txtchange.Text.Remove(0, 1));
            trn.total_cash = (float)Convert.ToDouble(txttotal.Text.Remove(0, 1));

            transactionService.updateTransaction(trn);

            var items = new log();
            items.log_Username = userInformation.user.usr_Username;
            items.log_DateTime = dateService.getCurrentDate();
            items.log_Actions = "Transaction Updated by UserName= " + userInformation.user.usr_Username + ", Full Name: " + userInformation.user.usr_FirstName + " " + userInformation.user.usr_LastName + " Transaction ID Modified: " + cashInUpdate.transactionID;
            logService.CreateLog(items);

            MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            loadTransactionInfo();
        }

        private void convertTextBoxToFormatMoney()
        {
            txtamountcharge.Text = moneyFormatService.convertToMoneyFormat(txtamountcharge.Text).txtComponent;
            txttotal.Text = moneyFormatService.convertToMoneyFormat(txttotal.Text).txtComponent;
            txtchange.Text = moneyFormatService.convertToMoneyFormat(txtchange.Text).txtComponent;
            txtcash.Text = moneyFormatService.convertToMoneyFormat(txtcash.Text).txtComponent;
            txtcredid.Text = moneyFormatService.convertToMoneyFormat(txtcredid.Text).txtComponent;
            txtcheck.Text = moneyFormatService.convertToMoneyFormat(txtcheck.Text).txtComponent;
        }

        private void chequedother_Checked(object sender, RoutedEventArgs e)
        {

            txtBoxOtherComment.IsEnabled = true;

        }

        private void chequedother_Unchecked(object sender, RoutedEventArgs e)
        {
            txtBoxOtherComment.IsEnabled = false;

        }

        private void txtcash_LostFocus(object sender, RoutedEventArgs e)
        {
            txtcash.Text = moneyFormatService.convertToMoneyFormat(txtcash.Text).txtComponent;
            setTotalCash();
        }

        private void txtcash_GotFocus(object sender, RoutedEventArgs e)
        {
            txtcash.SelectAll();
        }

        private void txtcredid_GotFocus(object sender, RoutedEventArgs e)
        {
            txtcredid.SelectAll();
        }

        private void txtcredid_LostFocus(object sender, RoutedEventArgs e)
        {
            txtcredid.Text = moneyFormatService.convertToMoneyFormat(txtcredid.Text).txtComponent;
            setTotalCash();
        }

        private void txtcheck_GotFocus(object sender, RoutedEventArgs e)
        {
            txtcheck.SelectAll();
        }

        private void txtcheck_LostFocus(object sender, RoutedEventArgs e)
        {
            txtcheck.Text = moneyFormatService.convertToMoneyFormat(txtcheck.Text).txtComponent;
            setTotalCash();
        }

        private void txtamountcharge_GotFocus(object sender, RoutedEventArgs e)
        {
            txtamountcharge.SelectAll();
        }

        private void txtamountcharge_LostFocus(object sender, RoutedEventArgs e)
        {
            txtamountcharge.Text = moneyFormatService.convertToMoneyFormat(txtamountcharge.Text).txtComponent;
            setTotalCash();
        }

        private void setTotalCash()
        {
            txttotal.Text = moneyFormatService.convertToMoneyFormat((Convert.ToDouble(txtcash.Text.Remove(0, 1)) + Convert.ToDouble(txtcredid.Text.Remove(0, 1)) + Convert.ToDouble(txtcheck.Text.Remove(0, 1))).ToString()).txtComponent;
            txtchange.Text = moneyFormatService.convertToMoneyFormat( ( Convert.ToDouble(txttotal.Text.Remove(0,1)) - Convert.ToDouble(txtamountcharge.Text.Remove(0, 1))).ToString() ).txtComponent;
        }
    }
}
