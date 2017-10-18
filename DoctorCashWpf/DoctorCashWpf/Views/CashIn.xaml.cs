using DoctorCashWpf.Printer;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

        private MoneyFormatService moneyFormatService = new MoneyFormatService();
        private BrushConverter brushConverter = new BrushConverter();
        private transactionService transaction = new transactionService();
        private logService serviceslog = new logService();
        private dateService date = new dateService();

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
            moneyFormatService.convertToMoneyFormat(label_amount);
            moneyFormatService.convertToMoneyFormat(label_change);
            moneyFormatService.convertToMoneyFormat(label_total);


            moneyFormatService.convertToMoneyFormat(txtbox_cash, () => { });



            moneyFormatService.convertToMoneyFormat(txtbox_credit, () => { });
            moneyFormatService.convertToMoneyFormat(txtbox_check, () => { });
            moneyFormatService.convertToMoneyFormat(txtbox_amountCharge, () => { });
        }

        public bool ready = false;
        private bool verifyTransactionsType()
        {            
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
            return ready;
        }

        private void ApplyDesignForError(TextBox value)
        {
            value.Foreground = (Brush)brushConverter.ConvertFrom("#e74c3c");
            value.Focus();
            labelerror.Content = "Complete Field";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            if (txtbox_patientFirstName.Text == "")
            {
                ApplyDesignForError(txtbox_patientFirstName);
            }
            else if (txtbox_amountCharge.Text == "$0,00")
            {
                ApplyDesignForError(txtbox_amountCharge);
            }
            else if (txtbox_cash.Text == "$0,00")
            {
                ApplyDesignForError(txtbox_cash);
            }
            else if (txtbox_credit.Text == "$0,00")
            {
                ApplyDesignForError(txtbox_credit);
            }
            else if (txtbox_check.Text == "$0,00")
            {
                ApplyDesignForError(txtbox_check);
            }
            else if (txtbox_numberChecks.Text == "0")
            {
                ApplyDesignForError(txtbox_numberChecks);
            }
            else if (txtbox_comment.Text == "")
            {
                ApplyDesignForError(txtbox_comment);
            }
            else
            {
                verifyTransactionsType();
                if (ready == false)
                {
                    checkbox_copayment.Foreground = (Brush)brushConverter.ConvertFrom("#e74c3c");
                    checkbox_deductible.Foreground = (Brush)brushConverter.ConvertFrom("#e74c3c");
                    checkbox_labs.Foreground = (Brush)brushConverter.ConvertFrom("#e74c3c");
                    checkbox_other.Foreground = (Brush)brushConverter.ConvertFrom("#e74c3c");
                    checkbox_selfPay.Foreground = (Brush)brushConverter.ConvertFrom("#e74c3c");
                    labelerror.Content = "Select a transaction type";
                }
                else
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
                    transaction.change = (float)Convert.ToDouble(label_change.Text.Remove(0, 1));
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


                    var items = new log();
                    items.log_Username = userInformation.user.usr_Username;
                    items.log_DateTime = date.getCurrentDate();
                    items.log_Actions = "Cash In Created by UserName= " + userInformation.user.usr_Username + ", Full Name: " + userInformation.user.usr_FirstName + " " + userInformation.user.usr_LastName + " Data: Total= " + label_total + ", Amount=" + label_amount + ", Change= " + label_change;                    
                    serviceslog.CreateLog(items);

                    // Imprime Recibo
                    Print print = new Print();
                    transaction.total_cash = (float)Convert.ToDouble(label_total.Text.Remove(0,1));
                    transaction.total_amount = (float)Convert.ToDouble(label_amount.Text.Remove(0,1));
                    print.printCashIn(transaction);

                    MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
                }
            }
        }

        private void clearInputs()
        {
            labelerror.Content = "";
            txtbox_amountCharge = moneyFormatService.getMoneyFormatInZero(txtbox_amountCharge);
            txtbox_cash = moneyFormatService.getMoneyFormatInZero(txtbox_cash);
            txtbox_credit = moneyFormatService.getMoneyFormatInZero(txtbox_credit);
            txtbox_check = moneyFormatService.getMoneyFormatInZero(txtbox_check);
            txtbox_numberChecks.Text = "0";
            txtbox_patientFirstName.Text = "";
            txtbox_comment.Text = "";

            label_amount = moneyFormatService.getMoneyFormatInZero(label_amount);
            label_change = moneyFormatService.getMoneyFormatInZero(label_change);
            label_total= moneyFormatService.getMoneyFormatInZero(label_total);

            checkbox_copayment.IsChecked = false;
            checkbox_deductible.IsChecked = false;
            checkbox_labs.IsChecked = false;
            checkbox_other.IsChecked = false;
            checkbox_selfPay.IsChecked = false;
        }

        private void getTotal_amount_change()
        {
            labelerror.Content = "";
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

            moneyFormatService.AddFloat(label_change);
            moneyFormatService.AddFloat(label_total);
        }

        private void txtbox_amountCharge_LostFocus(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            moneyFormatService.convertToMoneyFormat(txtbox_amountCharge, ()=> { });

            label_amount.Text = txtbox_amountCharge.Text;
            label_change.Text = "$" + (Convert.ToDouble(label_total.Text.Remove(0, 1)) - Convert.ToDouble(txtbox_amountCharge.Text.Remove(0, 1))).ToString();

            moneyFormatService.AddFloat(label_change);
        }

        private void txtbox_amountCharge_KeyUp(object sender, KeyEventArgs e)
        {
            labelerror.Content = "";
            if (e.Key == Key.Enter)
            {
                moneyFormatService.convertToMoneyFormat(txtbox_amountCharge, () => { });

                label_amount.Text = txtbox_amountCharge.Text;
                label_change.Text = "$" + (Convert.ToDouble(label_total.Text.Remove(0, 1)) - Convert.ToDouble(txtbox_amountCharge.Text.Remove(0, 1))).ToString();

                moneyFormatService.AddFloat(label_change);
            }
        }

        private void txtbox_cash_KeyUp(object sender, KeyEventArgs e)
        {
            labelerror.Content = "";
            if (e.Key == Key.Enter)
            {
                moneyFormatService.convertToMoneyFormat(txtbox_cash, () => { });
                getTotal_amount_change();
            }
        }

        private void txtbox_cash_LostFocus(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            moneyFormatService.convertToMoneyFormat(txtbox_cash, () => { });
            getTotal_amount_change();
        }

        private void txtbox_credit_KeyUp(object sender, KeyEventArgs e)
        {
            labelerror.Content = "";
            if (e.Key == Key.Enter)
            {
                moneyFormatService.convertToMoneyFormat(txtbox_credit, () => { });
                getTotal_amount_change();
            }
        }

        private void txtbox_credit_LostFocus(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            moneyFormatService.convertToMoneyFormat(txtbox_credit, () => { });
            getTotal_amount_change();
        }

        private void txtbox_check_KeyUp(object sender, KeyEventArgs e)
        {
            labelerror.Content = "";
            if (e.Key == Key.Enter)
            {
                moneyFormatService.convertToMoneyFormat(txtbox_check, () => { });
                getTotal_amount_change();
            }
        }

        private void txtbox_check_LostFocus(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            moneyFormatService.convertToMoneyFormat(txtbox_check, () => { });
            getTotal_amount_change();
        }

        private void txtbox_numberChecks_KeyUp(object sender, KeyEventArgs e)
        {
            labelerror.Content = "";
            if (e.Key == Key.Enter)
            {
                if (!Char.IsNumber(txtbox_numberChecks.Text[0]))
                {
                    txtbox_numberChecks.Text = 0.ToString();
                }
            }
        }

        private void txtbox_numberChecks_LostFocus(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
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
            labelerror.Content = "";
            txtbox_amountCharge.SelectAll();
        }

        private void txtbox_cash_GotFocus_1(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            txtbox_cash.SelectAll();
        }

        private void txtbox_credit_GotFocus_1(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            txtbox_credit.SelectAll();
        }

        private void txtbox_check_GotFocus_1(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            txtbox_check.SelectAll();
        }

        private void txtbox_numberChecks_GotFocus_1(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            txtbox_numberChecks.SelectAll();
        }

        private void txtbox_patientFirstName_GotFocus_1(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
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

            var items = new log();
            items.log_Username = userInformation.user.usr_Username;
            items.log_DateTime = date.getCurrentDate();
            items.log_Actions = "Cash In Cancel by UserName= " + userInformation.user.usr_Username + ", Full Name: " + userInformation.user.usr_FirstName + " " + userInformation.user.usr_LastName + " Data Captured: Total= " + label_total.Text + ", Amount= " + label_amount.Text + ", Change= " + label_change.Text;
            serviceslog.CreateLog(items);

        }

        private void txtbox_comment_KeyUp(object sender, KeyEventArgs e)
        {
            labelerror.Content = "";
        }
    }
}
