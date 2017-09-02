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
    /// Interaction logic for ViewReceipt.xaml
    /// </summary>
    public partial class ViewReceipt : UserControl
    {
        public ViewReceipt()
        {
            InitializeComponent();
        }

        private reportService getreport = new reportService();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            txtbox_question.Text = "";
            fromdate.Text = "";
            todate.Text = "";
            dataGridViewClosedStatement.ItemsSource = null;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (txtbox_question.Text == "" && fromdate.Text == "" && todate.Text == "")
            {

            }
            else
            {
                var list = getreport.getDailyTransactions(txtbox_question.Text, "", fromdate.Text, todate.Text).list;
                dataGridViewClosedStatement.ItemsSource = null;

                DataTable dt = new DataTable();
                dt.Columns.Add("Transaction ID");
                dt.Columns.Add("Patient Name");
                dt.Columns.Add("Register");
                dt.Columns.Add("Date");

                for (int i = 0; i < list.Count(); i++)
                {
                    dt.Rows.Add(list[i].trn_id, list[i].patientFirstName, list[i].modifiedById, list[i].dateRegistered);
                }
                dataGridViewClosedStatement.ItemsSource = dt.DefaultView;
            }            
        }

        private void dataGridViewClosedStatement_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {            
            /*
            //var dato = dataGridViewClosedStatement.SelectedItem;
            DataRowView item = dataGridViewClosedStatement.SelectedItem as DataRowView;
            int dato = Convert.ToInt32(item.Row.ItemArray[0]);            
            var list = getreport.getDailyTransactions(txtbox_question.Text, "", fromdate.Text, todate.Text).list;
            for (int i = 0; i < list.Count(); i++)
            {
                if (list[i].trn_id == dato)
                {
                    amounChange.Text = list[i].amountCharged.ToString();
                    cash.Text = list[i].cash.ToString();
                    creditCard.Text = list[i].credit.ToString();
                    check.Text = list[i].check.ToString();
                    total.Text = list[i].cash.ToString();
                    change.Text = list[i].change.ToString();
                    break;
                }
            }*/
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            //var dato = dataGridViewClosedStatement.SelectedItem;
            DataRowView item = dataGridViewClosedStatement.SelectedItem as DataRowView;
            int dato = Convert.ToInt32(item.Row.ItemArray[0]);
            var list = getreport.getDailyTransactions(txtbox_question.Text, "", fromdate.Text, todate.Text).list;
            for (int i = 0; i < list.Count(); i++)
            {
                if (list[i].trn_id == dato)
                {
                    amounChange.Text = list[i].amountCharged.ToString();
                    cash.Text = list[i].cash.ToString();
                    creditCard.Text = list[i].credit.ToString();
                    check.Text = list[i].check.ToString();
                    total.Text = list[i].cash.ToString();
                    change.Text = list[i].change.ToString();
                    break;
                }
            }
        }
    }
}
