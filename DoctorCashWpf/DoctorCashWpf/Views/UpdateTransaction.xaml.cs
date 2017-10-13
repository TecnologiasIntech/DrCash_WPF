﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
            LoadInformation();
        }
        
        private logService serviceslog = new logService();
        private transactionService servicestransaction = new transactionService();
        private dateService date = new dateService();


        private void LoadInformation()
        {            
            var list = servicestransaction.getAllTransactionsByRegisterID(cashInUpdate.transactionID.ToString(), cashInUpdate.saveSearchFromDate, cashInUpdate.saveSearchToDate);

            for (int i = 0; i < list.Count(); i++)
            {

                if (list[i].trn_id == cashInUpdate.transactionID)
                {
                    patienName.Text = list[i].patientFirstName;
                    chequedcopayment.IsChecked = (bool)list[i].copayment;
                    chequeddeductible.IsChecked = (bool)list[i].deductible;
                    chequedlaps.IsChecked = (bool)list[i].labs;
                    chequedother.IsChecked = (bool)list[i].other;
                    chequedselfpay.IsChecked = (bool)list[i].selfPay;
                    txtamountcharge.Text = list[i].amountCharged.ToString();
                    txttotal.Text = list[i].cash.ToString();
                    txtchange.Text = list[i].change.ToString();
                    txtcash.Text = list[i].cash.ToString();
                    txtcredid.Text = list[i].credit.ToString();
                    txtcheck.Text = list[i].check.ToString().ToString();
                    txtchecknumber.Text = list[i].checkNumber.ToString();
                    txtcomment.Text=list[i].comment;
                    break;
                }                
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var trn = new transaction();

            trn.patientFirstName = patienName.Text;
            trn.amountCharged = (float)Convert.ToDouble(txtamountcharge.Text);
            trn.cash = (float)Convert.ToDouble(txtcash.Text);
            trn.credit = (float)Convert.ToDouble(txtcredid.Text);
            trn.check = (float)Convert.ToDouble(txtcheck.Text);
            trn.checkNumber = Convert.ToInt32(txtchecknumber.Text);
            trn.copayment = (bool)chequedcopayment.IsChecked;
            trn.selfPay = (bool)chequedselfpay.IsChecked;
            trn.deductible = (bool)chequeddeductible.IsChecked;
            trn.labs = (bool)chequedlaps.IsChecked;
            trn.other = (bool)chequedother.IsChecked;
            trn.otherComments = "";
            trn.comment = txtcomment.Text;
            trn.change = (float)Convert.ToDouble(txtchange.Text);

            servicestransaction.updateTransaction(trn);

            var items = new log();
            items.log_Username = userInformation.user.usr_Username;
            items.log_DateTime = date.getCurrentDate();
            items.log_Actions = "Transaction Updated by UserName= " + userInformation.user.usr_Username + ", Full Name: " + userInformation.user.usr_FirstName + " " + userInformation.user.usr_LastName +" Transaction ID Modified: " + cashInUpdate.transactionID;
            serviceslog.CreateLog(items);

            MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoadInformation();
        }
    }
}
