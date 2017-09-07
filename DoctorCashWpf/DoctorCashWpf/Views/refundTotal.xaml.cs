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

            label_amountCharged = moneyComponent.getMoneyComponentInZero(label_amountCharged);
            txtbox_amountRefund = moneyComponent.getMoneyComponentInZero(txtbox_amountRefund);
        }

        private transactionService transaction = new transactionService();
        private transaction transactionInfo = new transaction();
        private MoneyComponentService moneyComponent = new MoneyComponentService();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(txtbox_transactionNumber.Text != "")
            {
                transactionInfo = transaction.getTransactionByTrnID(txtbox_transactionNumber.Text);

                label_amountCharged.Text = transactionInfo.amountCharged.ToString();
                txtbox_log.Text = getTransactionComment(transactionInfo);

                moneyComponent.convertComponentToMoneyFormat(label_amountCharged);
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(txtbox_amountRefund.Text != "")
            {
                var trn = new transaction();

                trn.amountCharged = (float)Convert.ToDouble(txtbox_amountRefund.Text.Remove(0,1));
                trn.comment = txtbox_newLog.Text;
                trn.type = (int)TRANSACTIONTYPE.REFOUND;
                trn.userId = userInformation.user.usr_ID;

                transaction.setTransactionRefund(trn);

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
                moneyComponent.convertComponentToMoneyFormat(txtbox_amountRefund, () => { });
            }
        }

        private void txtbox_amountRefund_LostFocus(object sender, RoutedEventArgs e)
        {
            moneyComponent.convertComponentToMoneyFormat(txtbox_amountRefund, () => { });
        }

        private void txtbox_amountRefund_GotFocus(object sender, RoutedEventArgs e)
        {
            txtbox_amountRefund.SelectAll();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            label_amountCharged = moneyComponent.getMoneyComponentInZero(label_amountCharged);
            txtbox_amountRefund = moneyComponent.getMoneyComponentInZero(txtbox_amountRefund);

            txtbox_transactionNumber.Text = "";
            txtbox_newLog.Text = "";
            txtbox_log.Text = "";
        }
    }
}
// refundTotal
