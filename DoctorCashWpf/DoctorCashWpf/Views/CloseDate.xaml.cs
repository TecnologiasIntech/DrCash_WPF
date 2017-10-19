using DoctorCashWpf.Printer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DoctorCashWpf.Views
{
    /// <summary>
    /// Interaction logic for CloseDate.xaml
    /// </summary>
    public partial class CloseDate : UserControl
    {
        private transactionService transaction = new transactionService();
        private MoneyFormatService moneyComponent = new MoneyFormatService();
        private BrushConverter brushConverter = new BrushConverter();
        private logService serviceslog = new logService();
        private sqlQueryService createQuery = new sqlQueryService();
        private createItemsForListService createItem = new createItemsForListService();
        private dateService date = new dateService();

        public CloseDate()
        {
            InitializeComponent();

            setValuesInitials();

            getCurrentTransactions();

        }

        private void setValuesInitials()
        {
            moneyComponent.convertToMoneyFormat(label_totalCash);
            moneyComponent.convertToMoneyFormat(label_bills1);
            moneyComponent.convertToMoneyFormat(label_bills5);
            moneyComponent.convertToMoneyFormat(label_bills20);
            moneyComponent.convertToMoneyFormat(label_bills50);
            moneyComponent.convertToMoneyFormat(label_bills100);
            moneyComponent.convertToMoneyFormat(label_bills10);
            moneyComponent.convertToMoneyFormat(label_totalEntered);
            moneyComponent.convertToMoneyFormat(label_totalRegistered);
            moneyComponent.convertToMoneyFormat(label_short);
            moneyComponent.convertToMoneyFormat(textbox_credit, () => { });
            moneyComponent.convertToMoneyFormat(textbox_leftInRegister, () => { });
            moneyComponent.convertToMoneyFormat(textbox_check, () => { });
        }

        private void plusOrLess(TextBox txtbox, TextBlock label, int Operator, int typeBills)
        {
            if (txtbox.Text == "")
            {
                txtbox.Text = 0.ToString();
            }

            try
            {
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
                    label = moneyComponent.convertToMoneyFormat(label).labelComponent;

                    txtbox.Text = txtbox.Text;

                    if (txtbox.Text == 0.ToString())
                    {
                        txtbox.Text = "";
                    }

                    getTotalCash();
                }
            }
            // TODO: Temperal: Bad practice exceptions should be more specific
            catch (Exception e)
            {
                txtbox.Text = "";
                label= moneyComponent.getMoneyFormatInZero(label);
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
            label_totalCash = moneyComponent.convertToMoneyFormat(label_totalCash).labelComponent;

            label_totalEntered.Text = (Convert.ToDouble(label_totalCash.Text.Remove(0, 1)) + Convert.ToDouble(textbox_credit.Text.Remove(0, 1)) + Convert.ToDouble(textbox_check.Text.Remove(0, 1)) + Convert.ToDouble(textbox_leftInRegister.Text.Remove(0, 1))).ToString();
            label_totalEntered = moneyComponent.convertToMoneyFormat(label_totalEntered).labelComponent;

            label_short.Text = (Convert.ToDouble(label_totalEntered.Text.Remove(0, 1)) - Convert.ToDouble(label_totalRegistered.Text.Remove(0, 1))).ToString();
            label_short = moneyComponent.convertToMoneyFormat(label_short).labelComponent;
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

            moneyComponent.AddFloat(label_totalEntered);
            moneyComponent.AddFloat(label_totalRegistered);
            moneyComponent.AddFloat(label_short);
        }
        
        private void clearInputs()
        {
            labelerror.Content = "";
            textbox_bills100.Text = "";
            textbox_bills50.Text = "";
            textbox_bills20.Text = "";
            textbox_bills10.Text = "";
            textbox_bills5.Text = "";
            textbox_bills1.Text = "";

            label_totalCash= moneyComponent.getMoneyFormatInZero(label_totalCash);
            textbox_credit = moneyComponent.getMoneyFormatInZero(textbox_credit);
            textbox_check= moneyComponent.getMoneyFormatInZero(textbox_check);
            label_totalEntered = moneyComponent.getMoneyFormatInZero(label_totalEntered);
            textbox_leftInRegister = moneyComponent.getMoneyFormatInZero(textbox_leftInRegister);
            label_short= moneyComponent.getMoneyFormatInZero(label_short);

            plusOrLess(textbox_bills100, label_bills100, (int)OPERATOR.EQUALITY, 0);
            plusOrLess(textbox_bills50, label_bills50, (int)OPERATOR.EQUALITY, 0);
            plusOrLess(textbox_bills20, label_bills20, (int)OPERATOR.EQUALITY, 0);
            plusOrLess(textbox_bills10, label_bills10, (int)OPERATOR.EQUALITY, 0);
            plusOrLess(textbox_bills5, label_bills5, (int)OPERATOR.EQUALITY, 0);
            plusOrLess(textbox_bills1, label_bills1, (int)OPERATOR.EQUALITY, 0);
        }

        private void Button_Click_Subtract_100_Dollar(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            plusOrLess(textbox_bills100, label_bills100, (int)OPERATOR.REMOVE, 100);
        }

        private void Button_Click_Add_100_Dollar(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            plusOrLess(textbox_bills100, label_bills100, (int)OPERATOR.SUM, 100);
        }

        private void Button_Click_Subtract_50_Dollar(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            plusOrLess(textbox_bills50, label_bills50, (int)OPERATOR.REMOVE, 50);
        }

        private void Button_Click_Add_50_Dollar(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            plusOrLess(textbox_bills50, label_bills50, (int)OPERATOR.SUM, 50);
        }

        private void Button_Click_Subtract_20_Dollar(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            plusOrLess(textbox_bills20, label_bills20, (int)OPERATOR.REMOVE, 20);
        }

        private void Button_Click_Add_20_Dollar(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            plusOrLess(textbox_bills20, label_bills20, (int)OPERATOR.SUM, 20);
        }

        private void Button_Click_Subtract_10_Dollar(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            plusOrLess(textbox_bills10, label_bills10, (int)OPERATOR.REMOVE, 10);
        }

        private void Button_Click_Add_10_Dollar(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            plusOrLess(textbox_bills10, label_bills10, (int)OPERATOR.SUM, 10);
        }

        private void Button_Click_Subtract_5_Dollar(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            plusOrLess(textbox_bills5, label_bills5, (int)OPERATOR.REMOVE, 5);
        }

        private void Button_Click_Add_5_Dollar(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            plusOrLess(textbox_bills5, label_bills5, (int)OPERATOR.SUM, 5);
        }

        private void Button_Click_Subtract_1_Dollar(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            plusOrLess(textbox_bills1, label_bills1, (int)OPERATOR.REMOVE, 1);
        }

        private void Button_Click_Add_1_Dollar(object sender, RoutedEventArgs e)
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

        private void Button_Click_Clear(object sender, RoutedEventArgs e)
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
                moneyComponent.convertToMoneyFormat(textbox_credit, () => { getCurrentTransactions(); });
               // verifyTxtBox(textbox_credit);
            }
        }

        private void textbox_credit_LostFocus(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            moneyComponent.convertToMoneyFormat(textbox_credit, () => { getCurrentTransactions(); });
        }

        private void textbox_check_KeyUp(object sender, KeyEventArgs e)
        {
            labelerror.Content = "";
            if (e.Key == Key.Enter)
            {
                moneyComponent.convertToMoneyFormat(textbox_check, () => { getCurrentTransactions(); });
            }
            
        }

        private void textbox_check_LostFocus(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            moneyComponent.convertToMoneyFormat(textbox_check, () => { getCurrentTransactions(); });
        }

        private void textbox_leftRegister_KeyUp(object sender, KeyEventArgs e)
        {
            labelerror.Content = "";
            if (e.Key == Key.Enter)
            {
                moneyComponent.convertToMoneyFormat(textbox_leftInRegister, () => { getCurrentTransactions(); });
            }
        }

        private void textbox_leftRegister_LostFocus(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            moneyComponent.convertToMoneyFormat(textbox_leftInRegister, () => { getCurrentTransactions(); });
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

        private void applydesign(TextBox value)
        {            
            value.Foreground = (Brush)brushConverter.ConvertFrom("#e74c3c");
            value.Focus();
            labelerror.Content = "Complete Field";
        }

        private void Button_Click_Print(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            var clDate = new closeDate();

            if (textbox_bills1.Text == "" && textbox_bills10.Text == "" && textbox_bills100.Text == "" && textbox_bills20.Text == "" && textbox_bills5.Text == "" && textbox_bills50.Text == "" && textbox_credit.Text== "$0,00" && textbox_check.Text== "$0,00" && textbox_leftInRegister.Text=="$0,00")
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

                Print printer = new Print();
                printer.printCloseDate(clDate);

                var item = new log();
                item.log_Username = userInformation.user.usr_Username;
                item.log_DateTime = date.getCurrentDate();
                item.log_Actions = "Close Date Created by UserName= " + userInformation.user.usr_Username + ", Full Name: " + userInformation.user.usr_FirstName + " " + userInformation.user.usr_LastName+", Cash= "+label_totalCash;
                serviceslog.CreateLog(item);

                MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
            }            
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            var items = new log();
            items.log_Username = userInformation.user.usr_Username;
            items.log_DateTime = date.getCurrentDate();
            items.log_Actions = "Close Date Cancel by UserName=" + userInformation.user.usr_Username + ", Full Name" + userInformation.user.usr_FirstName + " " + userInformation.user.usr_LastName;
            serviceslog.CreateLog(items);
        }

    }
}
