using DoctorCashWpf.Printer;
using MaterialDesignColors.WpfExample.Domain;
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
    /// Interaction logic for CashOut.xaml
    /// </summary>
    public partial class CashOut : UserControl
    {
        public CashOut()
        {
            InitializeComponent();
            DataContext = new TextFieldsViewModel();

            setValuesInitials();
        }

        MoneyComponentService moneyComponent = new MoneyComponentService();
        private BrushConverter brushConverter = new BrushConverter();

        private void setValuesInitials()
        {
            moneyComponent.convertComponentToMoneyFormat(label_totalCash);
            moneyComponent.convertComponentToMoneyFormat(label_1);
            moneyComponent.convertComponentToMoneyFormat(label_10);
            moneyComponent.convertComponentToMoneyFormat(label_20);
            moneyComponent.convertComponentToMoneyFormat(label_50);
            moneyComponent.convertComponentToMoneyFormat(label_100);
            moneyComponent.convertComponentToMoneyFormat(label_5);

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
                label = moneyComponent.getMoneyComponentInZero(label);
                getTotalCash();
            }
        }

        private void getTotalCash()
        {
            double totalCash = 0;
            
            if(textbox_bills100.Text != "")
            {
                totalCash += Convert.ToDouble(label_100.Text.Remove(0, 1));
            }

            if (textbox_bills50.Text != "")
            {
                totalCash += Convert.ToDouble(label_50.Text.Remove(0, 1));
            }

            if (textbox_bills20.Text != "")
            {
                totalCash += Convert.ToDouble(label_20.Text.Remove(0, 1));
            }

            if (textbox_bills10.Text != "")
            {
                totalCash += Convert.ToDouble(label_10.Text.Remove(0, 1));
            }

            if (textbox_bills5.Text != "")
            {
                totalCash += Convert.ToDouble(label_5.Text.Remove(0, 1));
            }

            if (textbox_bills1.Text != "")
            {
                totalCash += Convert.ToDouble(label_1.Text.Remove(0, 1));
            }

            label_totalCash.Text = totalCash.ToString();
            label_totalCash = moneyComponent.convertComponentToMoneyFormat(label_totalCash).labelComponent;
        }

        private void clearInputs()
        {
            textbox_bills100.Text = "";
            textbox_bills50.Text = "";
            textbox_bills20.Text = "";
            textbox_bills10.Text = "";
            textbox_bills5.Text = "";
            textbox_bills1.Text = "";

            textbox_comment.Text = "";

            plusOrLess(textbox_bills100, label_100, (int)OPERATOR.EQUALITY, 0);
            plusOrLess(textbox_bills50, label_50, (int)OPERATOR.EQUALITY, 0);
            plusOrLess(textbox_bills20, label_20, (int)OPERATOR.EQUALITY, 0);
            plusOrLess(textbox_bills10, label_10, (int)OPERATOR.EQUALITY, 0);
            plusOrLess(textbox_bills5, label_5, (int)OPERATOR.EQUALITY, 0);
            plusOrLess(textbox_bills1, label_1, (int)OPERATOR.EQUALITY, 0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            plusOrLess(textbox_bills100, label_100, (int)OPERATOR.SUM, 100);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (textbox_bills100.Text != "")
            {
                plusOrLess(textbox_bills100, label_100, (int)OPERATOR.REMOVE, 100);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (textbox_bills50.Text != "")
            {
                plusOrLess(textbox_bills50, label_50, (int)OPERATOR.REMOVE, 50);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            plusOrLess(textbox_bills50, label_50, (int)OPERATOR.SUM, 50);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            plusOrLess(textbox_bills20, label_20, (int)OPERATOR.SUM, 20);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (textbox_bills20.Text != "")
            {
                plusOrLess(textbox_bills20, label_20, (int)OPERATOR.REMOVE, 20);
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (textbox_bills10.Text != "")
            {
                plusOrLess(textbox_bills10, label_10, (int)OPERATOR.REMOVE, 10);
            }
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            plusOrLess(textbox_bills10, label_10, (int)OPERATOR.SUM, 10);
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            plusOrLess(textbox_bills5, label_5, (int)OPERATOR.SUM, 5);
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            if (textbox_bills5.Text != "")
            {
                plusOrLess(textbox_bills5, label_5, (int)OPERATOR.REMOVE, 5);
            }
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            if (textbox_bills1.Text != "")
            {
                plusOrLess(textbox_bills1, label_1, (int)OPERATOR.REMOVE, 1);
            }
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            plusOrLess(textbox_bills1, label_1, (int)OPERATOR.SUM, 1);
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            clearInputs();
        }

        private void textbox_bills100_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void textbox_bills100_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                plusOrLess(textbox_bills100, label_100, (int)OPERATOR.EQUALITY, 100);
            }
        }

        private void textbox_bills100_LostFocus(object sender, RoutedEventArgs e)
        {
            plusOrLess(textbox_bills100, label_100, (int)OPERATOR.EQUALITY, 100);
        }

        private void textbox_bills50_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                plusOrLess(textbox_bills50, label_50, (int)OPERATOR.EQUALITY, 50);
            }
        }

        private void textbox_bills50_LostFocus(object sender, RoutedEventArgs e)
        {
            plusOrLess(textbox_bills50, label_50, (int)OPERATOR.EQUALITY, 50);
        }

        private void textbox_bills20_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                plusOrLess(textbox_bills20, label_20, (int)OPERATOR.EQUALITY, 20);
            }
        }

        private void textbox_bills20_LostFocus(object sender, RoutedEventArgs e)
        {
            plusOrLess(textbox_bills20, label_20, (int)OPERATOR.EQUALITY, 20);
        }

        private void textbox_bills10_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                plusOrLess(textbox_bills10, label_10, (int)OPERATOR.EQUALITY, 10);
            }
        }

        private void textbox_bills10_LostFocus(object sender, RoutedEventArgs e)
        {
            plusOrLess(textbox_bills10, label_10, (int)OPERATOR.EQUALITY, 10);
        }

        private void textbox_bills5_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                plusOrLess(textbox_bills5, label_5, (int)OPERATOR.EQUALITY, 5);
            }
        }

        private void textbox_bills5_LostFocus(object sender, RoutedEventArgs e)
        {
            plusOrLess(textbox_bills5, label_5, (int)OPERATOR.EQUALITY, 5);
        }

        private void textbox_bills1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                plusOrLess(textbox_bills1, label_1, (int)OPERATOR.EQUALITY, 1);
            }
        }

        private void textbox_bills1_LostFocus(object sender, RoutedEventArgs e)
        {
            plusOrLess(textbox_bills1, label_1, (int)OPERATOR.EQUALITY, 1);
        }

        private void applydesign(TextBox value)
        {            
            value.Foreground = (Brush)brushConverter.ConvertFrom("#e74c3c");
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            if (textbox_bills1.Text==""||textbox_bills10.Text==""||textbox_bills100.Text==""||textbox_bills20.Text==""||textbox_bills5.Text==""||textbox_bills50.Text==""||textbox_comment.Text=="")
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
                else if (textbox_comment.Text == "")
                {
                    applydesign(textbox_comment);
                }

                #endregion
            }
            else
            {
                var transaction = new transactionService();
                var items = new transaction();
                items.registerId = userInformation.user.usr_Username;
                items.userId = userInformation.user.usr_ID;
                items.cash = (float)Convert.ToDouble(label_totalCash.Text.Remove(0, 1));
                items.comment = textbox_comment.Text;
                items.type = (int)TRANSACTIONTYPE.OUT;

                transaction.setTransactionOut(items);

                Print printer = new Print();
                printer.print();

                MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
            } 

        }

        private void textbox_bills100_GotFocus(object sender, RoutedEventArgs e)
        {
            textbox_bills100.SelectAll();
        }

        private void textbox_bills50_GotFocus(object sender, RoutedEventArgs e)
        {
            textbox_bills50.SelectAll();
        }

        private void textbox_bills20_GotFocus(object sender, RoutedEventArgs e)
        {
            textbox_bills20.SelectAll();
        }

        private void textbox_bills10_GotFocus(object sender, RoutedEventArgs e)
        {
            textbox_bills10.SelectAll();
        }

        private void textbox_bills5_GotFocus(object sender, RoutedEventArgs e)
        {
            textbox_bills5.SelectAll();
        }

        private void textbox_bills1_GotFocus(object sender, RoutedEventArgs e)
        {
            textbox_bills1.SelectAll();
        }
    }
}
