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

        reportService getreport = new reportService();
        transactionService transaction = new transactionService();
        private DataTable transactionsData;

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
           //var dato = dataGridViewClosedStatement.SelectedItem;
           DataRowView item = dataGridViewClosedStatement.SelectedItem as DataRowView;
            DataRow fila = transactionsData.Rows[dataGridViewClosedStatement.SelectedIndex];

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
                dt.Rows.Add(list[i].trn_id, list[i].amountCharged, list[i].cash, list[i].credit, list[i].check, list[i].change);                

            }
            dataGridViewStatment.ItemsSource = dt.DefaultView;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            double initial_cash=0,cash=0,credit=0,check=0,balance=0,cien=0, cincuenta=0, veinte=0, diez=0, cinco=0, uno=0;
            var response = getreport.getCloseTransactions(txtbox_ID.Text, fromdate.Text, todate.Text);
            var list = response.list;
            transactionsData = response.dataTable;
            dataGridViewClosedStatement.ItemsSource = null;

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
            txt_initialCash.Text = initial_cash.ToString();

            txt_cash.Text = cash.ToString();
            txt_credit.Text = credit.ToString();
            txt_check.Text = check.ToString();

            txt_ciens.Text = cien.ToString();
            txt_cincuentas.Text = cincuenta.ToString();
            txt_veintes.Text = veinte.ToString();
            txt_diez.Text = diez.ToString();
            txt_cincos.Text = cinco.ToString();
            txt_unos.Text = uno.ToString();
            txt_balance.Text = balance.ToString();

            txt_cash2.Text = cash.ToString();
            txt_credit2.Text = credit.ToString();
            txt_check2.Text = check.ToString();

            dataGridViewClosedStatement.ItemsSource = dt.DefaultView;

        }
       
    }
}
