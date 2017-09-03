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
                //dt.Rows.Add(list[i].clt_closed_ID, list[i].clt_reg_RegisterID, list[i].clt_Username, list[i].clt_Datetime);
            }
            dataGridViewClosedStatement.ItemsSource = dt.DefaultView;

        }
    }
}
