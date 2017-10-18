using DoctorCashWpf.Printer;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        private MoneyFormatService moneyService = new MoneyFormatService();
        private reportService getreport = new reportService();
        private transactionService transaction = new transactionService();
        private logService serviceslog = new logService();
        private dateService date = new dateService();

        private DataTable transactionsData;
        public int id=-1;
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            double charged = 0, cash = 0, credit = 0, check = 0, change = 0;
            //var dato = dataGridViewClosedStatement.SelectedItem;
            DataRowView item = dataGridViewClosedStatement.SelectedItem as DataRowView;
            DataRow fila = transactionsData.Rows[dataGridViewClosedStatement.SelectedIndex];            
            //dataClear();
            id = Convert.ToInt32(item.Row.ItemArray[0]);
            var registerID = fila["clt_reg_RegisterID"];
            var DateOfSelection = fila["clt_Datetime"];

            var list = transaction.getTransactionsByRegisterID(registerID.ToString(), DateOfSelection.ToString());

            DataTable dt = new DataTable();
            dt.Columns.Add("Transaction ID");
            dt.Columns.Add("Charged");
            dt.Columns.Add("Cash");
            dt.Columns.Add("Credit");
            dt.Columns.Add("Check");
            dt.Columns.Add("Change");

            for (int i = 0; i < list.Count(); i++)
            {
                dt.Rows.Add(list[i].trn_id, moneyService.convertToMoneyFormat(list[i].amountCharged.ToString()).txtComponent, moneyService.convertToMoneyFormat(list[i].cash.ToString()).txtComponent, moneyService.convertToMoneyFormat(list[i].credit.ToString()).txtComponent, moneyService.convertToMoneyFormat(list[i].check.ToString()).txtComponent, moneyService.convertToMoneyFormat(list[i].change.ToString()).txtComponent);
                charged += list[i].amountCharged;
                cash += list[i].cash;
                credit += list[i].credit;
                check += list[i].check;
                change += list[i].change;                
            }
            txt_initialCash.Text = transaction.getInitialCashbyRegisterID(registerID.ToString(), DateOfSelection.ToString()).ToString();
            cash = cash - change;
            moneyService.convertToMoneyFormat(txt_initialCash);
            txt_cash.Text = cash.ToString();
            moneyService.convertToMoneyFormat(txt_cash);
            txt_credit.Text = credit.ToString();
            moneyService.convertToMoneyFormat(txt_credit);
            txt_check.Text =  check.ToString();
            moneyService.convertToMoneyFormat(txt_check);
            txt_amount.Text = charged.ToString();
            moneyService.convertToMoneyFormat(txt_amount);

            txt_cash2.Text =  cash.ToString();
            moneyService.convertToMoneyFormat(txt_cash2);
            txt_credit2.Text = credit.ToString();
            moneyService.convertToMoneyFormat(txt_credit2);
            txt_check2.Text = check.ToString();
            moneyService.convertToMoneyFormat(txt_check2);

            dt.Rows.Add("Total´s", charged, cash, credit, check, change);
            
            dataGridViewStatment.ItemsSource = dt.DefaultView;            
            dataGridViewStatment.MaxHeight = 180;
                            
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
                items.log_DateTime = date.getCurrentDate();
                items.log_Actions = "Search Information by UserName= " + userInformation.user.usr_Username + ", Full Name: " + userInformation.user.usr_FirstName + " " + userInformation.user.usr_LastName + " in Closed Statement, Search Data: Transaction Number= " + txtbox_ID.Text + ", Dates: From= " + fromdate.Text + ", To= " + todate.Text;
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
                    txt_initialCash.Text = moneyService.convertToMoneyFormat(initial_cash.ToString()).txtComponent;

                    txt_cash.Text = moneyService.convertToMoneyFormat(cash.ToString()).txtComponent;
                    txt_credit.Text = moneyService.convertToMoneyFormat(credit.ToString()).txtComponent;
                    txt_check.Text = moneyService.convertToMoneyFormat(check.ToString()).txtComponent;
                    txt_amount.Text = moneyService.convertToMoneyFormat(amount.ToString()).txtComponent;
                    txt_ciens.Text = moneyService.convertToMoneyFormat(cien.ToString()).txtComponent;
                    txt_cincuentas.Text = moneyService.convertToMoneyFormat(cincuenta.ToString()).txtComponent;
                    txt_veintes.Text = moneyService.convertToMoneyFormat(veinte.ToString()).txtComponent;
                    txt_diez.Text = moneyService.convertToMoneyFormat(diez.ToString()).txtComponent;
                    txt_cincos.Text = moneyService.convertToMoneyFormat(cinco.ToString()).txtComponent;
                    txt_unos.Text = moneyService.convertToMoneyFormat(uno.ToString()).txtComponent;
                    txt_balance.Text = moneyService.convertToMoneyFormat(balance.ToString()).txtComponent;

                    txt_cash2.Text = moneyService.convertToMoneyFormat(cash.ToString()).txtComponent;
                    txt_credit2.Text = moneyService.convertToMoneyFormat(credit.ToString()).txtComponent;
                    txt_check2.Text = moneyService.convertToMoneyFormat(check.ToString()).txtComponent;

                    dataGridViewClosedStatement.ItemsSource = dt.DefaultView;
                    dataGridViewClosedStatement.MaxHeight = 140;
                }                
            }

        }

        private void dataClear()
        {
            labelerror.Content = "";
            txtbox_ID.Clear();
            fromdate.Text = "";
            todate.Text = "";
            txt_initialCash =moneyService.getMoneyFormatInZero(txt_initialCash);
            txt_amount= moneyService.getMoneyFormatInZero(txt_amount);
            txt_cash = moneyService.getMoneyFormatInZero(txt_cash);
            txt_credit = moneyService.getMoneyFormatInZero(txt_credit);
            txt_check = moneyService.getMoneyFormatInZero(txt_check);

            txt_ciens = moneyService.getMoneyFormatInZero(txt_ciens);
            txt_cincuentas = moneyService.getMoneyFormatInZero(txt_cincuentas);
            txt_veintes = moneyService.getMoneyFormatInZero(txt_veintes);
            txt_diez = moneyService.getMoneyFormatInZero(txt_diez);
            txt_cincos = moneyService.getMoneyFormatInZero(txt_cincos);
            txt_unos = moneyService.getMoneyFormatInZero(txt_unos);
            txt_balance = moneyService.getMoneyFormatInZero(txt_balance);

            txt_cash2 = moneyService.getMoneyFormatInZero(txt_cash2);
            txt_credit2 = moneyService.getMoneyFormatInZero(txt_credit2);
            txt_check2 = moneyService.getMoneyFormatInZero(txt_check2);

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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var clDate = new closeDate();
            if (id != -1)
            {
                clDate.clt_initial_cash = (float)Convert.ToDouble(txt_initialCash.Text.Remove(0, 1));
                clDate.clt_100_bills = (float)Convert.ToDouble(txt_ciens.Text.Remove(0, 1));
                clDate.clt_50_bills = (float)Convert.ToDouble(txt_cincuentas.Text.Remove(0, 1));
                clDate.clt_20_bills = (float)Convert.ToDouble(txt_veintes.Text.Remove(0, 1));
                clDate.clt_10_bills = (float)Convert.ToDouble(txt_diez.Text.Remove(0, 1));
                clDate.clt_5_bills = (float)Convert.ToDouble(txt_cincos.Text.Remove(0, 1));
                clDate.clt_1_bills = (float)Convert.ToDouble(txt_unos.Text.Remove(0, 1));
                clDate.clt_total_cash = (float)Convert.ToDouble(txt_cash.Text.Remove(0, 1));
                clDate.clt_total_check = (float)Convert.ToDouble(txt_check.Text.Remove(0, 1));
                clDate.clt_total_credit = (float)Convert.ToDouble(txt_credit.Text.Remove(0, 1));
                clDate.clt_checks_amount= (float)Convert.ToDouble(txt_amount.Text.Remove(0, 1));
                clDate.clt_balance= (float)Convert.ToDouble(txt_balance.Text.Remove(0, 1));
                Print printer = new Print();
                printer.printClosedStatement(clDate);
            }
            else
            {
                labelerror.Content = "Select a Transaction ID";
            }
        }
    }
}
