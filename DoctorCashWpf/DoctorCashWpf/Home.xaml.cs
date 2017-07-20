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
using MaterialDesignThemes.Wpf;
using System.Data;

namespace DoctorCashWpf
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {

        public Home()
        {
            InitializeComponent();

            Application.Current.MainWindow.WindowState = WindowState.Maximized;

            chargeTransactionsList();

        }

        private transactionService transactionService = new transactionService();
        private List<transaction> transactionList = new List<transaction>();

        private void chargeTransactionsList()
        {

            transactionList = transactionService.getCurrentTransactions(4);
            
            dataGridView1.ItemsSource = null;
            
            DataTable dt = new DataTable();
            dt.Columns.Add("Transactions");
            dt.Columns.Add("Processed By");
            dt.Columns.Add("Date");

            for (int i = 0; i < transactionList.Count(); i++)
            {
                dt.Rows.Add(transactionList[i].comment, transactionList[i].userId, transactionList[i].dateRegistered);
            }

            dataGridView1.ItemsSource = dt.DefaultView;

            getSumOfTransactions();

        }

        private void getSumOfTransactions()
        {

            //Transactions IN
            label_cashIn.Text = "$" + transactionList.Distinct().Sum(obj => obj.cash).ToString();
            label_credit.Text = "$" + transactionList.Distinct().Sum(obj => obj.credit).ToString();
            label_checks.Text = "$" + transactionList.Distinct().Sum(obj => obj.check).ToString();

            label_totalIn.Text = "$" + (transactionList.Distinct().Sum(obj => obj.cash) + transactionList.Distinct().Sum(obj => obj.credit) + transactionList.Distinct().Sum(obj => obj.check)).ToString();

        }

        private void CashInButton_Click(object sender, RoutedEventArgs e)
        {

            // Opens a new Modal Window
            CashInWindow modalWindow = new CashInWindow();
            modalWindow.ShowDialog();

            chargeTransactionsList();

        }

        private void CashOutButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RefundButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseDateButton_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
