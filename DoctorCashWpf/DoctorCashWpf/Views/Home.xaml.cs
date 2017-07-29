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

          //  chargeTransactionsList();

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

            //Transactions IN
            label_cashIn.Text = "$" + transactionList.Distinct().Sum(obj => obj.cash).ToString();
            label_credit.Text = "$" + transactionList.Distinct().Sum(obj => obj.credit).ToString();
            label_checks.Text = "$" + transactionList.Distinct().Sum(obj => obj.check).ToString();

            label_totalIn.Text = "$" + (transactionList.Distinct().Sum(obj => obj.cash) + transactionList.Distinct().Sum(obj => obj.credit) + transactionList.Distinct().Sum(obj => obj.check)).ToString();

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

            userService user = new userService();

            user userDetails = new user();
            /*//userDetails.usr_ID = 5;
            //userDetails.usr_Username = "psdzfgsapa";
            //userDetails.usr_FirstName = "pepsdfsdfse";
            //userDetails.usr_LastName = "pasdfsdfpa";
            //userDetails.usr_Password = "pepe123";
            //userDetails.usr_SecurityQuestion = "";
            //userDetails.usr_SecurityAnswer = "";
            //userDetails.usr_Email = "as@as.com"; // puede ser nullo
            //userDetails.usr_SecurityLevel = 1;
            //userDetails.usr_ActiveAccount = true;
            //userDetails.usr_PasswordReset = false;
            //userDetails.usr_ModifiedBy = 4; // puede ser nullo
            //userDetails.usr_CreatedBy = 4;

            //user.setUser(userDetails);*/

            //userDetails.usr_ID = 8;

            //user.deleteByID(8);


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
