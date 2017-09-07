using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for DailyTransactions.xaml
    /// </summary>
    public partial class DailyTransactions : UserControl
    {
        public DailyTransactions()
        {
            InitializeComponent();
        }

        private reportService getreport = new reportService();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(txtbox_question.Text==""&& Patient_Name.Text==""&& fromdate.Text=="" && todate.Text == "")
            {
                labelerror.Content = "complete a field or dates";
            }
            else
            {
                double charge = 0, cash = 0, Credit = 0, Check = 0, Change = 0;
                var list = getreport.getDailyTransactions(txtbox_question.Text, Patient_Name.Text, fromdate.Text, todate.Text).list;
                dataGridViewDailyTransactions.ItemsSource = null;

                DataTable dt = new DataTable();
                dt.Columns.Add("Transaction ID");
                dt.Columns.Add("User");
                dt.Columns.Add("Date");
                dt.Columns.Add("Patient Name");
                dt.Columns.Add("Type");
                dt.Columns.Add("Charge");
                dt.Columns.Add("Cash");
                dt.Columns.Add("Credit");
                dt.Columns.Add("Check");
                dt.Columns.Add("Change");
                dt.Columns.Add("Check #");
                dt.Columns.Add("Closed");
                dt.Columns.Add("Register");

                for (int i = 0; i < list.Count(); i++)
                {
                    dt.Rows.Add(list[i].trn_id, list[i].userId, list[i].dateRegistered, list[i].patientFirstName, list[i].type, list[i].amountCharged, list[i].cash, list[i].credit, list[i].check, list[i].change, list[i].checkNumber, list[i].closed, list[i].registerId);
                    charge += list[i].amountCharged;
                    cash += list[i].cash;
                    Credit += list[i].credit;
                    Check += list[i].check;
                    Change += list[i].change;
                }
                dt.Rows.Add("Total´s","","","","",charge,cash,Credit,Check,Change,"","","");

                dataGridViewDailyTransactions.ItemsSource = dt.DefaultView;
            }            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (txtbox_question.Text == "" && Patient_Name.Text == "" && fromdate.Text == "" && todate.Text == "")
            {
                labelerror.Content = "complete a field or dates";
            }
            else
            {
                //vamos a crear el reporte desde codigo

                Reportings.Report1 miventana = new Reportings.Report1(getreport.getDailyTransactions(txtbox_question.Text, Patient_Name.Text, fromdate.Text, todate.Text).dataTable);
                miventana.ShowDialog();
            }
                       
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            dataGridViewDailyTransactions.ItemsSource = null;
            txtbox_question.Clear();
            Patient_Name.Clear();
            fromdate.Text = "";
            todate.Text = "";
        }

        private void txtbox_question_KeyUp(object sender, KeyEventArgs e)
        {
            labelerror.Content = "";
        }
    }
}
