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
    /// Interaction logic for UpdateTransaction.xaml
    /// </summary>
    public partial class UpdateTransaction : UserControl
    {
        public UpdateTransaction()
        {
            InitializeComponent();
            LoadInformation();
        }

        private reportService getreport = new reportService();
        private logService serviceslog = new logService();
        private transactionService servicestransaction = new transactionService();


        private void LoadInformation()
        {            
            var list = getreport.getDailyTransactions(cashInUpdate.transactionID.ToString(), "", cashInUpdate.saveSearchFromDate, cashInUpdate.saveSearchToDate).list;

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
            items.log_DateTime = DateTime.Now.ToString();
            items.log_Actions = "Transaction Updated by UserName= " + userInformation.user.usr_Username + ", Full Name: " + userInformation.user.usr_FirstName + " " + userInformation.user.usr_LastName +" Transaction ID Modified: " + cashInUpdate.transactionID;
            serviceslog.CreateLog(items);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoadInformation();
        }
    }
}
