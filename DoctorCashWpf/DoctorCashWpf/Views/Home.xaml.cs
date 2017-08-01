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
using MaterialDesignColors.WpfExample.Domain;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MaterialDesignDemo.Domain;
using DoctorCashWpf.Views;

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

        public ICommand mostrar => new AnotherCommandImplementation(ExecuteRunDialog);

        private async void ExecuteRunDialog(object o)
        {
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new CashInWindow();

            //show the dialog
            var result = await DialogHost.Show(view);

            //check the result...
            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("You can intercept the closing event, and cancel here.");
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
            float cashIn = 0;
            float credit = 0;
            float checks = 0;
            float cashOut = 0;

            for (int i = 0; i < transactionList.Count(); i++)
            {
                if(transactionList[i].type == (int)TRANSACTIONTYPE.IN)
                {
                    cashIn += transactionList[i].cash;
                    credit += transactionList[i].credit;
                    checks += transactionList[i].check;
                }
                else if(transactionList[i].type == (int)TRANSACTIONTYPE.OUT)
                {
                    cashOut += transactionList[i].cash;
                }
            }

            label_cashIn.Text = "$" + cashIn.ToString() + ".00";
            label_credit.Text = "$" + credit.ToString() + ".00";
            label_checks.Text = "$" + checks.ToString() + ".00";

            label_totalIn.Text = "$" + (cashIn + credit + checks).ToString() + ".00";

            label_cashOut.Text = "$" + cashOut.ToString() + ".00";

            label_totalOut.Text = "$" + (cashOut).ToString() + ".00";
        }

        private async void CashInButton_Click(object sender, RoutedEventArgs e)
        {

            var cashInWindow = new CashInWindow();

            await DialogHost.Show(cashInWindow, "RootDialog");

            chargeTransactionsList();

        }


        private async void CashOutButton_Click(object sender, RoutedEventArgs e)
        {

            var cashOut = new CashOut();

            await DialogHost.Show(cashOut, "RootDialog");

            chargeTransactionsList();
        }

        private void RefundButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void CloseDateButton_Click(object sender, RoutedEventArgs e)
        {
            var closeDate = new CloseDate();

            await DialogHost.Show(closeDate, "RootDialog");

            
        }

    }
}
