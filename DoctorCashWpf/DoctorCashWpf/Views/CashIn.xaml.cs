using DoctorCashWpf.Printer;
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

            if (cashInUpdate.isUpdate)
            {
                loadValuesToUpdate(cashInUpdate.transactionID);
            }

            setValesInitials();

        }

        private MoneyComponentService moneyComponent = new MoneyComponentService();
        private BrushConverter brushConverter = new BrushConverter();
        private transactionService transaction = new transactionService();

        private void loadValuesToUpdate(int transactionID)
        {
            var trn = transaction.getObjTransactionByTransactionID(transactionID.ToString());

            txtbox_patientFirstName.Text = trn.patientFirstName;
            txtbox_amountCharge.Text = trn.amountCharged.ToString();
            txtbox_cash.Text = trn.cash.ToString();
            txtbox_credit.Text = trn.credit.ToString();
            txtbox_check.Text = trn.check.ToString();
            txtbox_numberChecks.Text = trn.checkNumber.ToString();
            txtbox_comment.Text = trn.comment;

            if (trn.copayment)
            {
                checkbox_copayment.IsChecked = true;
            }
            if (trn.selfPay)
            {
                checkbox_selfPay.IsChecked = true;
            }
            if(trn.deductible)
            {
                checkbox_deductible.IsChecked = true;
            }
            if (trn.labs)
            {
                checkbox_labs.IsChecked = true;
            }
            if (trn.other)
            {
                checkbox_other.IsChecked = true;
                txtbox_other.Text = trn.otherComments;
            }
        }

        private void setValesInitials()
        {
            moneyComponent.convertComponentToMoneyFormat(label_amount);
            moneyComponent.convertComponentToMoneyFormat(label_change);
            moneyComponent.convertComponentToMoneyFormat(label_total);
            moneyComponent.convertComponentToMoneyFormat(txtbox_cash, () => { });
            moneyComponent.convertComponentToMoneyFormat(txtbox_credit, () => { });
            moneyComponent.convertComponentToMoneyFormat(txtbox_check, () => { });
            moneyComponent.convertComponentToMoneyFormat(txtbox_amountCharge, () => { });
        }

        private bool verifyTransactionsType()
        {
            var ready = false;

            if (checkbox_copayment.IsChecked != false)
            {
                ready = true;
            }
            if (checkbox_deductible.IsChecked != false)
            {
                ready = true;
            }
            if (checkbox_labs.IsChecked != false)
            {
                ready = true;
            }
            if (checkbox_other.IsChecked != false)
            {
                ready = true;
            }
            if (checkbox_selfPay.IsChecked != false)
            {
                ready = true;
            }

            if (!ready)
            {
                checkbox_copayment.Foreground = (Brush)brushConverter.ConvertFrom("#e74c3c");
            }

            return ready;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (txtbox_patientFirstName.Text != "" && label_total.Text != moneyComponent.getFormatMoneyComponentInZero() && verifyTransactionsType())
            { 
                var transactionService = new transactionService();
                var transaction = new transaction();

                transaction.userId = userInformation.user.usr_ID;
                //transaction.dateRegistered = DateTime.Today.ToString("d");
                transaction.comment = txtbox_comment.Text;
                transaction.type = (int)TRANSACTIONTYPE.IN;

                transaction.amountCharged = (float)Convert.ToDouble(txtbox_amountCharge.Text.Remove(0, 1));
                transaction.cash = (float)Convert.ToDouble(txtbox_cash.Text.Remove(0, 1));
                transaction.credit = (float)Convert.ToDouble(txtbox_credit.Text.Remove(0, 1));
                transaction.check = (float)Convert.ToDouble(txtbox_check.Text.Remove(0, 1));
                transaction.checkNumber = Convert.ToInt32(txtbox_numberChecks.Text);
                transaction.change = (float)Convert.ToDouble(label_change.Text.Remove(0,1));
                transaction.patientFirstName = txtbox_patientFirstName.Text;
                transaction.copayment = (bool)checkbox_copayment.IsChecked;
                transaction.selfPay = (bool)checkbox_selfPay.IsChecked; ;
                transaction.deductible = (bool)checkbox_deductible.IsChecked; ;
                transaction.labs = (bool)checkbox_labs.IsChecked; ;
                transaction.other = (bool)checkbox_other.IsChecked; ;
                transaction.closed = false;
                transaction.registerId = userInformation.user.usr_Username;
                transaction.modifiedById = userInformation.user.usr_ID;
                transaction.type = (int)TRANSACTIONTYPE.IN;

                transactionService.setTransaction(transaction);

                // Imprime Recibo
                Print print = new Print();
                print.print();

                MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
            }
            else
            {
                if(txtbox_patientFirstName.Text == "")
                {
                    txtbox_patientFirstName.Focus();

                    txtbox_patientFirstName.Foreground = (Brush)brushConverter.ConvertFrom("#e74c3c");
                }
                else
                {
                    txtbox_cash.Focus();

                    txtbox_cash.Foreground = (Brush)brushConverter.ConvertFrom("#e74c3c");
                }
            }
        }

        private void clearInputs()
        {
            txtbox_amountCharge.Text = moneyComponent.getFormatMoneyComponentInZero();
            txtbox_cash.Text = moneyComponent.getFormatMoneyComponentInZero();
            txtbox_credit.Text = moneyComponent.getFormatMoneyComponentInZero();
            txtbox_check.Text = moneyComponent.getFormatMoneyComponentInZero();
            txtbox_numberChecks.Text = "0";
            txtbox_patientFirstName.Text = "";
            txtbox_comment.Text = "";

            label_amount.Text = moneyComponent.getFormatMoneyComponentInZero();
            label_change.Text = moneyComponent.getFormatMoneyComponentInZero();
            label_total.Text = moneyComponent.getFormatMoneyComponentInZero();

            checkbox_copayment.IsChecked = false;
            checkbox_deductible.IsChecked = false;
            checkbox_labs.IsChecked = false;
            checkbox_other.IsChecked = false;
            checkbox_selfPay.IsChecked = false;
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

            label_total.Text = "$" + total.ToString();

            label_change.Text = "$" + ( total - Convert.ToDouble(txtbox_amountCharge.Text.Remove(0, 1)) ).ToString();

            moneyComponent.AddFloatToComponent(label_change);
            moneyComponent.AddFloatToComponent(label_total);
        }

        private void txtbox_amountCharge_LostFocus(object sender, RoutedEventArgs e)
        {
            moneyComponent.convertComponentToMoneyFormat(txtbox_amountCharge, ()=> { });

            label_amount.Text = txtbox_amountCharge.Text;
            label_change.Text = "$" + (Convert.ToDouble(label_total.Text.Remove(0, 1)) - Convert.ToDouble(txtbox_amountCharge.Text.Remove(0, 1))).ToString();

            moneyComponent.AddFloatToComponent(label_change);
        }

        private void txtbox_amountCharge_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                moneyComponent.convertComponentToMoneyFormat(txtbox_amountCharge, () => { });

                label_amount.Text = txtbox_amountCharge.Text;
                label_change.Text = "$" + (Convert.ToDouble(label_total.Text.Remove(0, 1)) - Convert.ToDouble(txtbox_amountCharge.Text.Remove(0, 1))).ToString();

                moneyComponent.AddFloatToComponent(label_change);
            }
        }

        private void txtbox_cash_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                moneyComponent.convertComponentToMoneyFormat(txtbox_cash, () => { });
                getTotal_amount_change();
            }
        }

        private void txtbox_cash_LostFocus(object sender, RoutedEventArgs e)
        {
            moneyComponent.convertComponentToMoneyFormat(txtbox_cash, () => { });
            getTotal_amount_change();
        }

        private void txtbox_credit_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                moneyComponent.convertComponentToMoneyFormat(txtbox_credit, () => { });
                getTotal_amount_change();
            }
        }

        private void txtbox_credit_LostFocus(object sender, RoutedEventArgs e)
        {
            moneyComponent.convertComponentToMoneyFormat(txtbox_credit, () => { });
            getTotal_amount_change();
        }

        private void txtbox_check_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                moneyComponent.convertComponentToMoneyFormat(txtbox_check, () => { });
                getTotal_amount_change();
            }
        }

        private void txtbox_check_LostFocus(object sender, RoutedEventArgs e)
        {
            moneyComponent.convertComponentToMoneyFormat(txtbox_check, () => { });
            getTotal_amount_change();
        }

        private void txtbox_numberChecks_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if (!Char.IsNumber(txtbox_numberChecks.Text[0]))
                {
                    txtbox_numberChecks.Text = 0.ToString();
                }
            }
        }

        private void txtbox_numberChecks_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!Char.IsNumber(txtbox_numberChecks.Text[0]))
            {
                txtbox_numberChecks.Text = 0.ToString();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            clearInputs();
        }
        
        private void checkbox_other_Checked(object sender, RoutedEventArgs e)
        {
            txtbox_other.IsEnabled = true;
        }

        private void checkbox_other_Unchecked(object sender, RoutedEventArgs e)
        {
            txtbox_other.IsEnabled = false;
        }

        private void txtbox_amountCharge_GotFocus_1(object sender, RoutedEventArgs e)
        {
            txtbox_amountCharge.SelectAll();
        }

        private void txtbox_cash_GotFocus_1(object sender, RoutedEventArgs e)
        {
            txtbox_cash.SelectAll();
        }

        private void txtbox_credit_GotFocus_1(object sender, RoutedEventArgs e)
        {
            txtbox_credit.SelectAll();
        }

        private void txtbox_check_GotFocus_1(object sender, RoutedEventArgs e)
        {
            txtbox_check.SelectAll();
        }

        private void txtbox_numberChecks_GotFocus_1(object sender, RoutedEventArgs e)
        {
            txtbox_numberChecks.SelectAll();
        }

        private void txtbox_patientFirstName_GotFocus_1(object sender, RoutedEventArgs e)
        {
            txtbox_patientFirstName.SelectAll();
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            var trn = new transaction();

            trn.patientFirstName = txtbox_patientFirstName.Text;
            trn.amountCharged = (float)Convert.ToDouble(txtbox_amountCharge.Text.Remove(0,1));
            trn.cash = (float)Convert.ToDouble(txtbox_cash.Text.Remove(0, 1));
            trn.credit = (float)Convert.ToDouble(txtbox_credit.Text.Remove(0, 1));
            trn.check = (float)Convert.ToDouble(txtbox_check.Text.Remove(0, 1));
            trn.checkNumber = Convert.ToInt32(txtbox_numberChecks.Text);
            trn.copayment = (bool)checkbox_copayment.IsChecked;
            trn.selfPay = (bool)checkbox_selfPay.IsChecked;
            trn.deductible = (bool)checkbox_deductible.IsChecked;
            trn.labs = (bool)checkbox_labs.IsChecked;
            trn.other = (bool)checkbox_other.IsChecked;
            trn.otherComments = txtbox_other.Text;
            trn.comment = txtbox_comment.Text;
            trn.change = (float)Convert.ToDouble(label_change.Text.Remove(0, 1));

            transaction.updateTransaction(trn);
        }
    }
}
