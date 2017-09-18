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
    /// Interaction logic for CloseDate.xaml
    /// </summary>
    public partial class CloseDate : UserControl
    {
        public CloseDate()
        {
            InitializeComponent();

            setValuesInitials();

            getCurrentTransactions();

        }

        private transactionService transaction = new transactionService();
        private MoneyComponentService moneyComponent = new MoneyComponentService();
        private BrushConverter brushConverter = new BrushConverter();
        private logService serviceslog = new logService();

        private void setValuesInitials()
        {
            moneyComponent.convertComponentToMoneyFormat(label_totalCash);
            moneyComponent.convertComponentToMoneyFormat(label_bills1);
            moneyComponent.convertComponentToMoneyFormat(label_bills5);
            moneyComponent.convertComponentToMoneyFormat(label_bills20);
            moneyComponent.convertComponentToMoneyFormat(label_bills50);
            moneyComponent.convertComponentToMoneyFormat(label_bills100);
            moneyComponent.convertComponentToMoneyFormat(label_bills10);
            moneyComponent.convertComponentToMoneyFormat(label_totalEntered);
            moneyComponent.convertComponentToMoneyFormat(label_totalRegistered);
            moneyComponent.convertComponentToMoneyFormat(label_short);
            moneyComponent.convertComponentToMoneyFormat(textbox_credit, () => { });
            moneyComponent.convertComponentToMoneyFormat(textbox_leftInRegister, () => { });
            moneyComponent.convertComponentToMoneyFormat(textbox_check, () => { });
        }

        private void plusOrLess(TextBox txtbox, TextBlock label, int Operator, int typeBills)
        {
            if (txtbox.Text == "")
            {
                txtbox.Text = 0.ToString();
            }

            if (Convert.ToInt32(txtbox.Text) >= 0)
            {
                switch (Operator)
                {
                    case (int)OPERATOR.SUM:
                        txtbox.Text = (Convert.ToInt32(txtbox.Text) + 1).ToString();
                        break;

                    case (int)OPERATOR.REMOVE:
                        txtbox.Text = (Convert.ToInt32(txtbox.Text) - 1).ToString();
                        break;

                    case (int)OPERATOR.EQUALITY:
                        break;
                }

                label.Text = (Convert.ToInt32(txtbox.Text) * typeBills).ToString();
                label = moneyComponent.convertComponentToMoneyFormat(label).labelComponent;

                txtbox.Text = txtbox.Text;

                if (txtbox.Text == 0.ToString())
                {
                    txtbox.Text = "";
                }

                getTotalCash();
            }
            else
            {
                txtbox.Text = "";
                label= moneyComponent.getMoneyComponentInZero(label);
                getTotalCash();
            }
        }

        private void getTotalCash()
        {
            double totalCash = 0;

            if (textbox_bills100.Text != "")
            {
                totalCash += Convert.ToDouble(label_bills100.Text.Remove(0, 1));
            }

            if (textbox_bills50.Text != "")
            {
                totalCash += Convert.ToDouble(label_bills50.Text.Remove(0, 1));
            }

            if (textbox_bills20.Text != "")
            {
                totalCash += Convert.ToDouble(label_bills20.Text.Remove(0, 1));
            }

            if (textbox_bills10.Text != "")
            {
                totalCash += Convert.ToDouble(label_bills10.Text.Remove(0, 1));
            }

            if (textbox_bills5.Text != "")
            {
                totalCash += Convert.ToDouble(label_bills5.Text.Remove(0, 1));
            }

            if (textbox_bills1.Text != "")
            {
                totalCash += Convert.ToDouble(label_bills1.Text.Remove(0, 1));
            }

            label_totalCash.Text = totalCash.ToString();
            label_totalCash = moneyComponent.convertComponentToMoneyFormat(label_totalCash).labelComponent;

            label_totalEntered.Text = (Convert.ToDouble(label_totalCash.Text.Remove(0, 1)) + Convert.ToDouble(textbox_credit.Text.Remove(0, 1)) + Convert.ToDouble(textbox_check.Text.Remove(0, 1)) + Convert.ToDouble(textbox_leftInRegister.Text.Remove(0, 1))).ToString();
            label_totalEntered = moneyComponent.convertComponentToMoneyFormat(label_totalEntered).labelComponent;

            label_short.Text = (Convert.ToDouble(label_totalEntered.Text.Remove(0, 1)) - Convert.ToDouble(label_totalRegistered.Text.Remove(0, 1))).ToString();
            label_short = moneyComponent.convertComponentToMoneyFormat(label_short).labelComponent;
        }

        private void getCurrentTransactions()
        {
            int currentUserID = userInformation.user.usr_ID;

            var listCurrentTransactions = new List<transaction>();

            listCurrentTransactions = transaction.getCurrentTransactions(currentUserID);

            float totalCash = 0, totalCredit = 0, totalChecks = 0, totalNumberChecks = 0, totalEntered = 0, totalRegistered = 0;

            for (int i = 0; i < listCurrentTransactions.Count(); i++)
            {
                if (listCurrentTransactions[i].type == (int)TRANSACTIONTYPE.IN)
                {
                    totalCredit += listCurrentTransactions[i].credit;
                    totalChecks += listCurrentTransactions[i].check;
                    totalCash += listCurrentTransactions[i].cash;
                    totalNumberChecks += listCurrentTransactions[i].checkNumber;
                }
            }

            totalRegistered = totalCredit + totalChecks + totalCash;
            totalEntered = (float)(Convert.ToDouble(textbox_credit.Text.Remove(0, 1)) + Convert.ToDouble(textbox_check.Text.Remove(0, 1)) + Convert.ToDouble(textbox_leftInRegister.Text.Remove(0, 1)) + Convert.ToDouble(label_totalCash.Text.Remove(0, 1)));

            label_totalEntered.Text = "$" + totalEntered.ToString();

            label_totalRegistered.Text = "$" + totalRegistered.ToString();

            label_short.Text = "$" + (totalEntered - totalRegistered).ToString();

            moneyComponent.AddFloatToComponent(label_totalEntered);
            moneyComponent.AddFloatToComponent(label_totalRegistered);
            moneyComponent.AddFloatToComponent(label_short);
        }

       /* private void isFloat(TextBox txtbox)
        {
            if (!txtbox.Text.Contains('.'))
            {
                txtbox.Text = txtbox.Text + ".00";
            }
        }

        private void isFloat(TextBlock label)
        {
            if (!label.Text.Contains('.'))
            {
                label.Text = label.Text + ".00";
            }
        }

        private void verifyTxtBox(TextBox txtBox)
        {
            if (txtBox.Text[0] != '$')
            {
                if (Char.IsNumber(txtBox.Text[0]))
                {

                    txtBox.Text = "$" + txtBox.Text;
                }
                else
                {
                    txtBox.Text = "$0.00";
                }
            }
            else
            {
                if (!Char.IsNumber(txtBox.Text.Remove(0, 1)[0]))
                {
                    txtBox.Text = "$0.00";
                }
            }

            getCurrentTransactions();

            isFloat(txtBox);
        }*/

        private void clearInputs()
        {
            labelerror.Content = "";
            textbox_bills100.Text = "";
            textbox_bills50.Text = "";
            textbox_bills20.Text = "";
            textbox_bills10.Text = "";
            textbox_bills5.Text = "";
            textbox_bills1.Text = "";

            label_totalCash= moneyComponent.getMoneyComponentInZero(label_totalCash);
            textbox_credit = moneyComponent.getMoneyComponentInZero(textbox_credit);
            textbox_check= moneyComponent.getMoneyComponentInZero(textbox_check);
            label_totalEntered = moneyComponent.getMoneyComponentInZero(label_totalEntered);
            textbox_leftInRegister = moneyComponent.getMoneyComponentInZero(textbox_leftInRegister);
            label_short= moneyComponent.getMoneyComponentInZero(label_short);

            plusOrLess(textbox_bills100, label_bills100, (int)OPERATOR.EQUALITY, 0);
            plusOrLess(textbox_bills50, label_bills50, (int)OPERATOR.EQUALITY, 0);
            plusOrLess(textbox_bills20, label_bills20, (int)OPERATOR.EQUALITY, 0);
            plusOrLess(textbox_bills10, label_bills10, (int)OPERATOR.EQUALITY, 0);
            plusOrLess(textbox_bills5, label_bills5, (int)OPERATOR.EQUALITY, 0);
            plusOrLess(textbox_bills1, label_bills1, (int)OPERATOR.EQUALITY, 0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            plusOrLess(textbox_bills100, label_bills100, (int)OPERATOR.REMOVE, 100);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            plusOrLess(textbox_bills100, label_bills100, (int)OPERATOR.SUM, 100);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            plusOrLess(textbox_bills50, label_bills50, (int)OPERATOR.REMOVE, 50);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            plusOrLess(textbox_bills50, label_bills50, (int)OPERATOR.SUM, 50);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            plusOrLess(textbox_bills20, label_bills20, (int)OPERATOR.REMOVE, 20);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            plusOrLess(textbox_bills20, label_bills20, (int)OPERATOR.SUM, 20);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            plusOrLess(textbox_bills10, label_bills10, (int)OPERATOR.REMOVE, 10);
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            plusOrLess(textbox_bills10, label_bills10, (int)OPERATOR.SUM, 10);
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            plusOrLess(textbox_bills5, label_bills5, (int)OPERATOR.REMOVE, 5);
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            plusOrLess(textbox_bills5, label_bills5, (int)OPERATOR.SUM, 5);
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            plusOrLess(textbox_bills1, label_bills1, (int)OPERATOR.REMOVE, 1);
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            plusOrLess(textbox_bills1, label_bills1, (int)OPERATOR.SUM, 1);
        }

        private void textbox_bills100_KeyUp(object sender, KeyEventArgs e)
        {
            labelerror.Content = "";
            if (e.Key == Key.Enter)
            {
                plusOrLess(textbox_bills100, label_bills100, (int)OPERATOR.EQUALITY, 100);
            }
        }

        private void textbox_bills100_LostFocus(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            plusOrLess(textbox_bills100, label_bills100, (int)OPERATOR.EQUALITY, 100);
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            clearInputs();
        }

        private void textbox_bills50_KeyUp(object sender, KeyEventArgs e)
        {
            labelerror.Content = "";
            if (e.Key == Key.Enter)
            {
                plusOrLess(textbox_bills50, label_bills50, (int)OPERATOR.EQUALITY, 50);
            }
        }

        private void textbox_bills50_LostFocus(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            plusOrLess(textbox_bills50, label_bills50, (int)OPERATOR.EQUALITY, 50);
        }

        private void textbox_bills20_KeyUp(object sender, KeyEventArgs e)
        {
            labelerror.Content = "";
            if (e.Key == Key.Enter)
            {
                plusOrLess(textbox_bills20, label_bills20, (int)OPERATOR.EQUALITY, 20);
            }
        }

        private void textbox_bills20_LostFocus(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            plusOrLess(textbox_bills20, label_bills20, (int)OPERATOR.EQUALITY, 20);
        }

        private void textbox_bills10_KeyUp(object sender, KeyEventArgs e)
        {
            labelerror.Content = "";
            if (e.Key == Key.Enter)
            {
                plusOrLess(textbox_bills10, label_bills10, (int)OPERATOR.EQUALITY, 10);
            }
        }

        private void textbox_bills10_LostFocus(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            plusOrLess(textbox_bills10, label_bills10, (int)OPERATOR.EQUALITY, 10);
        }

        private void textbox_bills5_KeyUp(object sender, KeyEventArgs e)
        {
            labelerror.Content = "";
            if (e.Key == Key.Enter)
            {
                plusOrLess(textbox_bills5, label_bills5, (int)OPERATOR.EQUALITY, 5);
            }
        }

        private void textbox_bills5_LostFocus(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            plusOrLess(textbox_bills5, label_bills5, (int)OPERATOR.EQUALITY, 5);
        }

        private void textbox_bills1_KeyUp(object sender, KeyEventArgs e)
        {
            labelerror.Content = "";
            if (e.Key == Key.Enter)
            {
                plusOrLess(textbox_bills1, label_bills1, (int)OPERATOR.EQUALITY, 1);
            }
        }

        private void textbox_bills1_LostFocus(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            plusOrLess(textbox_bills1, label_bills1, (int)OPERATOR.EQUALITY, 1);
        }

        private void textbox_credit_KeyUp(object sender, KeyEventArgs e)
        {
            labelerror.Content = "";
            if (e.Key == Key.Enter)
            {
                moneyComponent.convertComponentToMoneyFormat(textbox_credit, () => { getCurrentTransactions(); });
               // verifyTxtBox(textbox_credit);
            }
        }

        private void textbox_credit_LostFocus(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            moneyComponent.convertComponentToMoneyFormat(textbox_credit, () => { getCurrentTransactions(); });
        }

        private void textbox_check_KeyUp(object sender, KeyEventArgs e)
        {
            labelerror.Content = "";
            if (e.Key == Key.Enter)
            {
                moneyComponent.convertComponentToMoneyFormat(textbox_check, () => { getCurrentTransactions(); });
            }
            
        }

        private void textbox_check_LostFocus(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            moneyComponent.convertComponentToMoneyFormat(textbox_check, () => { getCurrentTransactions(); });
        }

        private void textbox_leftRegister_KeyUp(object sender, KeyEventArgs e)
        {
            labelerror.Content = "";
            if (e.Key == Key.Enter)
            {
                moneyComponent.convertComponentToMoneyFormat(textbox_leftInRegister, () => { getCurrentTransactions(); });
            }
        }

        private void textbox_leftRegister_LostFocus(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            moneyComponent.convertComponentToMoneyFormat(textbox_leftInRegister, () => { getCurrentTransactions(); });
        }

        private void textbox_credit_GotFocus(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            textbox_credit.SelectAll();
        }

        private void textbox_check_GotFocus(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            textbox_check.SelectAll();
        }

        private void textbox_leftInRegister_GotFocus(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            textbox_leftInRegister.SelectAll();
        }

        private void textbox_bills100_GotFocus(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            textbox_bills100.SelectAll();
        }

        private void textbox_bills50_GotFocus(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            textbox_bills50.SelectAll();
        }

        private void textbox_bills20_GotFocus(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            textbox_bills20.SelectAll();
        }

        private void textbox_bills10_GotFocus(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            textbox_bills10.SelectAll();
        }

        private void textbox_bills5_GotFocus(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            textbox_bills5.SelectAll();
        }

        private void textbox_bills1_GotFocus(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            textbox_bills1.SelectAll();
        }

        private void textbox_bills100_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void applydesign(TextBox value)
        {            
            value.Foreground = (Brush)brushConverter.ConvertFrom("#e74c3c");
            value.Focus();
            labelerror.Content = "Complete Field";
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            var clDate = new closeDate();

            if (textbox_bills1.Text == "" || textbox_bills10.Text == "" || textbox_bills100.Text == "" || textbox_bills20.Text == "" || textbox_bills5.Text == "" || textbox_bills50.Text == ""||textbox_credit.Text== "$0,00" || textbox_check.Text== "$0,00" || textbox_leftInRegister.Text=="$0,00")
            {
                #region Check                               
                if (textbox_bills100.Text == "")
                {
                    applydesign(textbox_bills100);
                }
                               
                else if (textbox_bills50.Text == "")
                {
                    applydesign(textbox_bills50);
                }
                else if (textbox_bills20.Text == "")
                {
                    applydesign(textbox_bills20);
                }
                else if (textbox_bills10.Text == "")
                {
                    applydesign(textbox_bills10);
                }
                else if (textbox_bills5.Text == "")
                {
                    applydesign(textbox_bills5);
                }
                else if (textbox_bills1.Text == "")
                {
                    applydesign(textbox_bills1);
                }
                else if (textbox_credit.Text == "$0,00")
                {
                    applydesign(textbox_credit);
                }
                else if (textbox_check.Text == "$0,00")
                {
                    applydesign(textbox_check);
                }
                else if (textbox_leftInRegister.Text == "$0,00")
                {
                    applydesign(textbox_leftInRegister);
                }
                #endregion
            }
            else
            {
                labelerror.Content = "";
                clDate.clt_100_bills = (float)Convert.ToDouble(label_bills100.Text.Remove(0, 1));
                clDate.clt_50_bills = (float)Convert.ToDouble(label_bills50.Text.Remove(0, 1));
                clDate.clt_20_bills = (float)Convert.ToDouble(label_bills20.Text.Remove(0, 1));
                clDate.clt_10_bills = (float)Convert.ToDouble(label_bills10.Text.Remove(0, 1));
                clDate.clt_5_bills = (float)Convert.ToDouble(label_bills5.Text.Remove(0, 1));
                clDate.clt_1_bills = (float)Convert.ToDouble(label_bills1.Text.Remove(0, 1));
                clDate.clt_total_cash = (float)Convert.ToDouble(label_totalCash.Text.Remove(0, 1));
                clDate.clt_total_check = (float)Convert.ToDouble(textbox_check.Text.Remove(0, 1));
                clDate.clt_total_credit = (float)Convert.ToDouble(textbox_credit.Text.Remove(0, 1));

                transaction.setClosedTransaction(clDate);

                var item = new log();
                item.log_Username = userInformation.user.usr_Username;
                item.log_DateTime = DateTime.Now.ToString();
                item.log_Actions = "Close Date Created by UserName= " + userInformation.user.usr_Username + ", Full Name: " + userInformation.user.usr_FirstName + " " + userInformation.user.usr_LastName+", Cash= "+label_totalCash;
                serviceslog.CreateLog(item);

                MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
            }            
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            var items = new log();
            items.log_Username = userInformation.user.usr_Username;
            items.log_DateTime = DateTime.Now.ToString();
            items.log_Actions = "Close Date Cancel by UserName=" + userInformation.user.usr_Username + ", Full Name" + userInformation.user.usr_FirstName + " " + userInformation.user.usr_LastName;
            serviceslog.CreateLog(items);
        }
    }
}
