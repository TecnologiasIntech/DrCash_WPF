using DoctorCashWpf.Printer;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DoctorCashWpf.Views
{
    /// <summary>
    /// Interaction logic for refundTotal.xaml
    /// </summary>
    public partial class refundTotal : UserControl
    {
        public refundTotal()
        {
            InitializeComponent();

            label_amountCharged = moneyComponent.getMoneyFormatInZero(label_amountCharged);
            txtbox_amountRefund = moneyComponent.getMoneyFormatInZero(txtbox_amountRefund);
        }

        private transactionService transactionservice = new transactionService();
        private transaction transactionInfo = new transaction();
        private MoneyFormatService moneyComponent = new MoneyFormatService();
        private logService serviceslog = new logService();
        private dateService dateservice = new dateService();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(txtbox_transactionNumber.Text != "")
            {
                transactionInfo = transactionservice.getTransactionByTrnID(txtbox_transactionNumber.Text);

                label_amountCharged.Text = transactionInfo.amountCharged.ToString();
                txtbox_log.Text = getTransactionComment(transactionInfo);

                moneyComponent.convertToMoneyFormat(label_amountCharged);

                setLog("Search");
            }
        }

        private string getTransactionComment(transaction trn)
        {
            var comment = "";

            switch (trn.type)
            {
                case (int)TRANSACTIONTYPE.IN:

                    if (trn.copayment)
                    {
                        comment = "Payment for total amount: $" + getTotalAmount(trn).ToString() + ".00";
                    }
                    else if (trn.selfPay)
                    {
                        comment = "SelPay for total amount: $" + getTotalAmount(trn).ToString() + ".00";
                    }
                    else if (trn.deductible)
                    {
                        comment = "Deductible for total amount: $" + getTotalAmount(trn).ToString() + ".00";
                    }
                    else if (trn.other)
                    {
                        comment = trn.otherComments + " for total amount: $" + getTotalAmount(trn).ToString() + ".00";
                    }
                    else if (trn.labs)
                    {
                        comment = "Labs for total amount: $" + getTotalAmount(trn).ToString() + ".00";
                    }

                    break;
            }

            return comment;
        }

        private float getTotalAmount(transaction trn)
        {
            return trn.cash + trn.credit + trn.check;
        }

        private void setLog(string Value)
        {
            var items = new log();
            items.log_Username = userInformation.user.usr_Username;
            items.log_DateTime = dateservice.getCurrentDate();

            if (Value == "Print")
            {                
                items.log_Actions = "Print Information in RefundTotal with Transaction Number: " + txtbox_transactionNumber.Text + " by: " + userInformation.user.usr_FirstName + " " + userInformation.user.usr_LastName + ", Level of user: " + userInformation.user.usr_SecurityLevel;
                serviceslog.CreateLog(items);
            }
            if (Value == "Search")
            {
                items.log_Actions = "Search Information in RefundTotal with Transaction Number: " + txtbox_transactionNumber.Text + " by: " + userInformation.user.usr_FirstName + " " + userInformation.user.usr_LastName + ", Level of user: " + userInformation.user.usr_SecurityLevel;
                serviceslog.CreateLog(items);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(txtbox_amountRefund.Text != "")
            {
                var trn = new transaction();

                trn.amountCharged = (float)Convert.ToDouble(txtbox_amountRefund.Text.Remove(0,1));
                trn.comment = txtbox_newLog.Text;
                trn.type = (int)TRANSACTIONTYPE.REFOUND;
                trn.userId = userInformation.user.usr_ID;

                transactionservice.setTransactionRefund(trn);

                setLog("Print");

                Print printer = new Print();
                printer.printRefund(trn);

                MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void txtbox_amountRefund_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                moneyComponent.convertToMoneyFormat(txtbox_amountRefund, () => { });
            }
        }

        private void txtbox_amountRefund_LostFocus(object sender, RoutedEventArgs e)
        {
            moneyComponent.convertToMoneyFormat(txtbox_amountRefund, () => { });
        }

        private void txtbox_amountRefund_GotFocus(object sender, RoutedEventArgs e)
        {
            txtbox_amountRefund.SelectAll();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            label_amountCharged = moneyComponent.getMoneyFormatInZero(label_amountCharged);
            txtbox_amountRefund = moneyComponent.getMoneyFormatInZero(txtbox_amountRefund);

            txtbox_transactionNumber.Text = "";
            txtbox_newLog.Text = "";
            txtbox_log.Text = "";
        }
    }
}
