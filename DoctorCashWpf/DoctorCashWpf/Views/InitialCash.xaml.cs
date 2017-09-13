﻿using System;
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
    /// Interaction logic for InitialCash.xaml
    /// </summary>
    public partial class InitialCash : UserControl
    {
        public InitialCash()
        {
            InitializeComponent();
            hiddenAndCollapsed();
            if (!checkInitialTransaction())
            {
                //No pedir la transaccion inicial o cerrar este dialogo
            }
        }
        private BrushConverter brushConverter = new BrushConverter();
        private transactionService transaction = new transactionService();
        private MoneyComponentService moneyComponent = new MoneyComponentService();
        private dateService date = new dateService();

        private void hiddenAndCollapsed()
        {
            BtnAgree.Visibility = Visibility.Hidden;
            BtnAgree.Visibility = Visibility.Collapsed;
            BtnDisagree.Visibility = Visibility.Hidden;
            BtnDisagree.Visibility = Visibility.Collapsed;
        }

        private void visibility()
        {
            BtnAgree.Visibility = Visibility.Visible;            
            BtnDisagree.Visibility = Visibility.Visible;           
        }

        private bool checkInitialTransaction()
        {
            bool check = false;

            var list = transaction.getCurrentTransactions(4); //Obtener ID de login
            for (int i = 0; i < list.Count; i++)
            {
                if(list[i].type == (int)TRANSACTIONTYPE.INITIAL)
                { 
                    check = true;
                    break;
                }
                else
                {
                    check = false;
                }               
            }
            return check;
        }

        private void setInitialCash()
        {
            
            if (txtbox_initialCash.Text != "$0.00")
            {                
                var items = new transaction();
                var cash = txtbox_initialCash.Text.Remove(0, 1);
                cash = cash.Remove(cash.Length - 3, 3);
                items.cash = (float)Convert.ToDouble(cash);
                items.type = (int)TRANSACTIONTYPE.INITIAL;
                items.comment = "Initial Cash";
                items.userId = userInformation.user.usr_ID;
                items.registerId = userInformation.user.usr_Username;

                transaction.setTransactionInitialCash(items);

                MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }

        private void designOfAlertInError()
        {                        
            txtbox_initialCash.Clear();
            txtbox_initialCash.Focus();
            txtbox_initialCash.Foreground = (Brush)brushConverter.ConvertFrom("#e74c3c");            
        }

        private void verifyCash()
        {
            string dat1 = "120";            
            dat1 = moneyComponent.convertComponentToMoneyFormat(dat1).txtComponent;

            if (Convert.ToDouble(txtbox_initialCash.Text.Remove(0, 1)) == 0)
            {
                labelCash.Content = "Insert initial Cash";
                designOfAlertInError();
            }
            else if (Convert.ToDouble(txtbox_initialCash.Text.Remove(0, 1)) == Convert.ToDouble(dat1.Remove(0, 1)))
            {
                labelCash.Content = "";                
                moneyComponent.convertComponentToMoneyFormat(txtbox_initialCash, () => { });
                setInitialCash();
            }
            else if (Convert.ToDouble(txtbox_initialCash.Text.Remove(0, 1)) > Convert.ToDouble(dat1.Remove(0, 1)))
            {
                labelCash.Content = "Are you sure to enter more "+"\n"+"     than $120 in the box?";                               
                visibility();

            }
            else if (Convert.ToDouble(txtbox_initialCash.Text.Remove(0, 1)) < Convert.ToDouble(dat1.Remove(0, 1)))
            {              
                labelCash.Content = "Are you sure to enter " +"\n  "+ txtbox_initialCash.Text + " in the box?";                
                visibility();
            }

        }

        private void setInitialCash_KeyUp(object sender, KeyEventArgs e)
        {
            labelCash.Content = "";
        }

        private void setInitialCash_click(object sender, RoutedEventArgs e)
        {
            verifyCash();                      
        }

        private void txtbox_initialCash_LostFocus(object sender, RoutedEventArgs e)
        {
            moneyComponent.convertComponentToMoneyFormat(txtbox_initialCash, () => { });
        }

        private void BtnAgree_Click(object sender, RoutedEventArgs e)
        {
            /*labelCash.Content = "";
                var response = moneyComponent.convertComponentToMoneyFormat(txtbox_initialCash, () => { });
                txtbox_initialCash = response.TextboxComponent;
                labelCash.Content = response.error;*/
            moneyComponent.convertComponentToMoneyFormat(txtbox_initialCash, () => { });
            setInitialCash();
            labelCash.Content = "";
            hiddenAndCollapsed();
        }

        private void BtnDisagree_Click(object sender, RoutedEventArgs e)
        {
            txtbox_initialCash.Focus();
            txtbox_initialCash.Clear();
            labelCash.Content = "";
            hiddenAndCollapsed();
        }
    }
}
