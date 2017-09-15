using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for ClosedStatements.xaml
    /// </summary>
    public partial class ClosedStatements : UserControl
    {
        public ClosedStatements()
        {
            InitializeComponent();
        }
        MoneyComponentService moneyService = new MoneyComponentService();
        reportService getreport = new reportService();
        transactionService transaction = new transactionService();
        logService serviceslog = new logService();

        private DataTable transactionsData;

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            double charged = 0, cash = 0, credit = 0, check = 0, change = 0;
            //var dato = dataGridViewClosedStatement.SelectedItem;
            DataRowView item = dataGridViewClosedStatement.SelectedItem as DataRowView;
            DataRow fila = transactionsData.Rows[dataGridViewClosedStatement.SelectedIndex];            
            //dataClear();
            int dato = Convert.ToInt32(item.Row.ItemArray[0]);
            var registerID = fila["clt_reg_RegisterID"];

            var list = transaction.getTransactionsByRegisterID(registerID.ToString(), fromdate.Text, todate.Text);

            DataTable dt = new DataTable();
            dt.Columns.Add("Transaction ID");
            dt.Columns.Add("Charged");
            dt.Columns.Add("Cash");
            dt.Columns.Add("Credit");
            dt.Columns.Add("Check");
            dt.Columns.Add("Change");

            for (int i = 0; i < list.Count(); i++)
            {
                dt.Rows.Add(list[i].trn_id, moneyService.convertComponentToMoneyFormat(list[i].amountCharged.ToString()).txtComponent, moneyService.convertComponentToMoneyFormat(list[i].cash.ToString()).txtComponent, moneyService.convertComponentToMoneyFormat(list[i].credit.ToString()).txtComponent, moneyService.convertComponentToMoneyFormat(list[i].check.ToString()).txtComponent, moneyService.convertComponentToMoneyFormat(list[i].change.ToString()).txtComponent);
                charged += list[i].amountCharged;
                cash += list[i].cash;
                credit += list[i].credit;
                check += list[i].check;
                change += list[i].change;
            }
            cash = cash - change;
                     

            txt_cash.Text = cash.ToString();
            moneyService.convertComponentToMoneyFormat(txt_cash);
            txt_credit.Text = credit.ToString();
            moneyService.convertComponentToMoneyFormat(txt_credit);
            txt_check.Text =  check.ToString();
            moneyService.convertComponentToMoneyFormat(txt_check);
            txt_amount.Text = charged.ToString();
            moneyService.convertComponentToMoneyFormat(txt_amount);

            txt_cash2.Text =  cash.ToString();
            moneyService.convertComponentToMoneyFormat(txt_cash2);
            txt_credit2.Text = credit.ToString();
            moneyService.convertComponentToMoneyFormat(txt_credit2);
            txt_check2.Text = check.ToString();
            moneyService.convertComponentToMoneyFormat(txt_check2);

            dt.Rows.Add("Total´s", charged, cash, credit, check, change);
            dataGridViewStatment.ItemsSource = dt.DefaultView;        
           
        }


        private void Row_DoubleClick1(object sender,MouseButtonEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            labelerror.Content = "";
            if (todate.Text == "" && fromdate.Text == "" && txtbox_ID.Text == "") 
            { 
                labelerror.Content = "Complete the Date or ID fields";
            }
            else
            {
                var items = new log();
                items.log_Username = userInformation.user.usr_Username;
                items.log_DateTime = DateTime.Now.ToString();
                items.log_Actions = "Search Information by UserName=" + userInformation.user.usr_Username + ", Full Name" + userInformation.user.usr_FirstName + " " + userInformation.user.usr_LastName + " in Closed Statement, Search Data: Transaction Number=" + txtbox_ID.Text + ", Dates: From=" + fromdate.Text + ", To=" + todate.Text;
                serviceslog.CreateLog(items);

                double initial_cash = 0, amount = 0, cash = 0, credit = 0, check = 0, balance = 0, cien = 0, cincuenta = 0, veinte = 0, diez = 0, cinco = 0, uno = 0;
                var response = getreport.getCloseTransactions(txtbox_ID.Text, fromdate.Text, todate.Text);
                var list = response.list;
                transactionsData = response.dataTable;
                //dataClear();
                dataGridViewClosedStatement.ItemsSource = null;                
                
                if (transactionsData.Rows.Count == 0)
                {
                    labelerror.Content = "Data Not Found";
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Closed Statement ID");
                    dt.Columns.Add("Register");
                    dt.Columns.Add("Proceced By");
                    dt.Columns.Add("Date");

                    for (int i = 0; i < transactionsData.Rows.Count; i++)
                    {
                        DataRow filas = transactionsData.Rows[i];

                        dt.Rows.Add(filas["clt_closed_ID"], filas["clt_reg_RegisterID"], filas["clt_Username"], filas["clt_Datetime"]);

                        initial_cash += list[i].clt_initial_cash;

                        amount += list[i].clt_checks_amount + list[i].clt_credits_amount;

                        cash += list[i].clt_total_cash;
                        credit += list[i].clt_total_credit;
                        check += list[i].clt_total_check;

                        cien += list[i].clt_100_bills;
                        cincuenta += list[i].clt_50_bills;
                        veinte += list[i].clt_20_bills;
                        diez += list[i].clt_10_bills;
                        cinco += list[i].clt_5_bills;
                        uno += list[i].clt_1_bills;
                        balance += list[i].clt_balance;


                    }
                    txt_initialCash.Text = moneyService.convertComponentToMoneyFormat(initial_cash.ToString()).txtComponent;

                    txt_cash.Text = moneyService.convertComponentToMoneyFormat(cash.ToString()).txtComponent;
                    txt_credit.Text = moneyService.convertComponentToMoneyFormat(credit.ToString()).txtComponent;
                    txt_check.Text = moneyService.convertComponentToMoneyFormat(check.ToString()).txtComponent;
                    txt_amount.Text = moneyService.convertComponentToMoneyFormat(amount.ToString()).txtComponent;
                    txt_ciens.Text = moneyService.convertComponentToMoneyFormat(cien.ToString()).txtComponent;
                    txt_cincuentas.Text = moneyService.convertComponentToMoneyFormat(cincuenta.ToString()).txtComponent;
                    txt_veintes.Text = moneyService.convertComponentToMoneyFormat(veinte.ToString()).txtComponent;
                    txt_diez.Text = moneyService.convertComponentToMoneyFormat(diez.ToString()).txtComponent;
                    txt_cincos.Text = moneyService.convertComponentToMoneyFormat(cinco.ToString()).txtComponent;
                    txt_unos.Text = moneyService.convertComponentToMoneyFormat(uno.ToString()).txtComponent;
                    txt_balance.Text = moneyService.convertComponentToMoneyFormat(balance.ToString()).txtComponent;

                    txt_cash2.Text = moneyService.convertComponentToMoneyFormat(cash.ToString()).txtComponent;
                    txt_credit2.Text = moneyService.convertComponentToMoneyFormat(credit.ToString()).txtComponent;
                    txt_check2.Text = moneyService.convertComponentToMoneyFormat(check.ToString()).txtComponent;

                    dataGridViewClosedStatement.ItemsSource = dt.DefaultView;                    
                }                
            }

        }

        private void dataClear()
        {
            labelerror.Content = "";
            txtbox_ID.Clear();
            fromdate.Text = "";
            todate.Text = "";
            txt_initialCash =moneyService.getMoneyComponentInZero(txt_initialCash);
            txt_amount= moneyService.getMoneyComponentInZero(txt_amount);
            txt_cash = moneyService.getMoneyComponentInZero(txt_cash);
            txt_credit = moneyService.getMoneyComponentInZero(txt_credit);
            txt_check = moneyService.getMoneyComponentInZero(txt_check);

            txt_ciens = moneyService.getMoneyComponentInZero(txt_ciens);
            txt_cincuentas = moneyService.getMoneyComponentInZero(txt_cincuentas);
            txt_veintes = moneyService.getMoneyComponentInZero(txt_veintes);
            txt_diez = moneyService.getMoneyComponentInZero(txt_diez);
            txt_cincos = moneyService.getMoneyComponentInZero(txt_cincos);
            txt_unos = moneyService.getMoneyComponentInZero(txt_unos);
            txt_balance = moneyService.getMoneyComponentInZero(txt_balance);

            txt_cash2 = moneyService.getMoneyComponentInZero(txt_cash2);
            txt_credit2 = moneyService.getMoneyComponentInZero(txt_credit2);
            txt_check2 = moneyService.getMoneyComponentInZero(txt_check2);

            dataGridViewClosedStatement.ItemsSource = null;
            dataGridViewStatment.ItemsSource = null;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            dataClear();
            dataGridViewClosedStatement.ItemsSource = null;
            dataGridViewStatment.ItemsSource = null;
        }

        private void txtbox_ID_KeyUp(object sender, KeyEventArgs e)
        {
            labelerror.Content = "";            
        }
    }
}
