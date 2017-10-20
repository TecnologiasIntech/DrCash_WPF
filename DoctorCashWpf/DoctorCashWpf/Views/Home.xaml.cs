using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using System.Data;
using MaterialDesignColors.WpfExample.Domain;
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
        }

        public ICommand mostrar => new AnotherCommandImplementation(ExecuteRunDialog);
        private MoneyFormatService moneyFormatService = new MoneyFormatService();
        private userService userService = new userService();
        private transactionService transactionService = new transactionService();
        private List<transaction> transactionList = new List<transaction>();
        private dateService dateService = new dateService();

        private async void ExecuteRunDialog(object o)
        {
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new CashInWindow();

            //show the dialog
            var result = await DialogHost.Show(view);

            //check the result...
            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }

        public void loadTransactionsList()
        {
            transactionList = transactionService.getCurrentTransactions(userInformation.user.usr_ID);
            
            dataGridCurrentTransactions.ItemsSource = null;
            
            DataTable dt = new DataTable();
            dt.Columns.Add("Transactions");
            dt.Columns.Add("Processed By");
            dt.Columns.Add("Date");

            for (int i = 0; i < transactionList.Count(); i++)
            {
                user user = userService.getUserByID((transactionList[i].userId).ToString());
               
                dt.Rows.Add(getTransactionComment(transactionList[i]), user.usr_FirstName + " " + user.usr_LastName, transactionList[i].dateRegistered);
            }

            getSumOfTransactionsAndChangeTotalLabels();

            dataGridCurrentTransactions.ItemsSource = dt.DefaultView;
            dataGridCurrentTransactions.MaxHeight = 470;
        }

        private string getTransactionComment(transaction trn)
        {
            string transactionComment = "";
            string amount = "";

            switch (trn.type)
            {
                case (int)TRANSACTIONTYPE.INITIAL:

                    amount = moneyFormatService.AddFloat(trn.cash.ToString());
                    transactionComment = "Initial Cash In for total amount: $" + amount;
                    break;

                case (int)TRANSACTIONTYPE.IN:

                    amount = moneyFormatService.AddFloat(getTotalAmount(trn).ToString());

                    if (trn.copayment)
                    {
                        
                        transactionComment = "Payment for total amount: $";
                    }
                    else if (trn.selfPay)
                    {
                        transactionComment = "SelPay for total amount: $";
                    }
                    else if (trn.deductible)
                    {
                        transactionComment = "Deductible for total amount: $";
                    }
                    else if (trn.other)
                    {
                        transactionComment = trn.otherComments + " for total amount: $";
                    }
                    else if (trn.labs)
                    {
                        transactionComment = "Labs for total amount: $";
                    }

                    transactionComment += amount;

                    break;

                case (int)TRANSACTIONTYPE.REFOUND:
                    amount = moneyFormatService.AddFloat(trn.amountCharged.ToString());
                    transactionComment = "Refound for total amount: $" + amount;
                    break;

                case (int)TRANSACTIONTYPE.OUT:
                    amount = moneyFormatService.AddFloat(getTotalAmount(trn).ToString());
                    transactionComment = "Cash out for total amount: $" + amount;
                    break;
            }

            return transactionComment;
        }

        private float getTotalAmount(transaction trn)
        {
            return trn.cash + trn.credit + trn.check - trn.change;
        }

        private void getSumOfTransactionsAndChangeTotalLabels()
        {
            float totalCashIn = 0, totalCredit = 0, totalChecks = 0, totalCashOut = 0, totalInitialCash = 0, totalRefound = 0, totalChange = 0;

            for (int i = 0; i < transactionList.Count(); i++)
            {
                if(transactionList[i].type == (int)TRANSACTIONTYPE.IN)
                {
                    totalCashIn += transactionList[i].cash;
                    totalCredit += transactionList[i].credit;
                    totalChecks += transactionList[i].check;
                    totalChange += transactionList[i].change;
                }
                else if(transactionList[i].type == (int)TRANSACTIONTYPE.OUT)
                {
                    totalCashOut += transactionList[i].cash;
                }else if(transactionList[i].type == (int)TRANSACTIONTYPE.INITIAL)
                {
                    totalInitialCash = transactionList[i].cash;
                }else if(transactionList[i].type == (int)TRANSACTIONTYPE.REFOUND)
                {
                    totalRefound += transactionList[i].amountCharged; //TODO verificar que está bien esto
                }
            }

            label_cashIn.Text = totalCashIn.ToString();
            label_credit.Text = totalCredit.ToString();
            label_checks.Text = totalChecks.ToString();
            label_refounds.Text = totalRefound.ToString();
            label_balance.Text = (((totalInitialCash + totalCashIn + totalCredit + totalChecks) - totalCashOut) - totalRefound - totalChange).ToString();

            label_totalIn.Text = (totalCashIn + totalCredit + totalChecks - totalChange).ToString();

            label_cashOut.Text = (totalCashOut).ToString();

            label_totalOut.Text = (totalCashOut + totalRefound).ToString();

            label_initialCash.Text = totalInitialCash.ToString();
            label_initialCash = moneyFormatService.convertToMoneyFormat(label_initialCash).labelComponent;

            if(label_balance.Text[0] == '-')
            {
                label_balance.Text = label_balance.Text.Remove(0, 1);
                moneyFormatService.convertToMoneyFormat(label_balance);
                label_balance.Text = "- " + label_balance.Text;
            }
            else
            {
                moneyFormatService.convertToMoneyFormat(label_balance);
            }

            moneyFormatService.convertToMoneyFormat(label_cashIn);
            moneyFormatService.convertToMoneyFormat(label_credit);
            moneyFormatService.convertToMoneyFormat(label_checks);
            moneyFormatService.convertToMoneyFormat(label_totalIn);
            moneyFormatService.convertToMoneyFormat(label_cashOut);
            moneyFormatService.convertToMoneyFormat(label_totalOut);
            moneyFormatService.convertToMoneyFormat(label_refounds);

            closeDateInformation.closeDate.clt_balance = (float)(totalInitialCash + totalCashIn + totalCredit + totalChecks - totalCashOut - totalRefound);
            closeDateInformation.closeDate.clt_initial_cash = (float)(totalInitialCash);
        }

        private async void CashInButton_Click(object sender, RoutedEventArgs e)
        {

            var cashInWindow = new CashInWindow();

            await DialogHost.Show(cashInWindow, "RootDialog");

            loadTransactionsList();

        }

        private async void CashOutButton_Click(object sender, RoutedEventArgs e)
        {

            var cashOut = new CashOut();

            await DialogHost.Show(cashOut, "RootDialog");

            loadTransactionsList();
        }

        private async void RefundButton_Click(object sender, RoutedEventArgs e)
        {

            var refundAuth = new RefundAuth();
            await DialogHost.Show(refundAuth, "RootDialog");

            if (createRefund.isRefund)
            {
                createRefund.isRefund = false;
                await DialogHost.Show(new refundTotal(), "RootDialog");

                loadTransactionsList();
            }

        }

        private async void CloseDateButton_Click(object sender, RoutedEventArgs e)
        {
            var closeDate = new CloseDate();

            await DialogHost.Show(closeDate, "RootDialog");
            
        }

        private async void Capture_Initial_Cash(object sender, RoutedEventArgs e)
        {            
            if (userInformation.user == null)
            {
                await DialogHost.Show(new Authentication(), "RootDialog");

                if (userInformation.user.usr_PasswordReset)
                {
                    await DialogHost.Show(new UserNew(), "RootDialog");
                }
            }

            var transaction = new transactionService();

            var list = transaction.getCurrentTransactions(userInformation.user.usr_ID);
            bool initialCash = false;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].type == (int)TRANSACTIONTYPE.INITIAL)
                {
                    initialCash = true;
                    break;
                }
            }

            if (!initialCash)
            {
                await DialogHost.Show(new InitialCash(), "RootDialog");
            }

            loadTransactionsList();

        }

        private void clearData()
        {
            dataGridCurrentTransactions.ItemsSource = null;
            moneyFormatService.getMoneyFormatInZero(label_initialCash);
            moneyFormatService.getMoneyFormatInZero(label_cashIn);
            moneyFormatService.getMoneyFormatInZero(label_credit);
            moneyFormatService.getMoneyFormatInZero(label_checks);
            moneyFormatService.getMoneyFormatInZero(label_totalIn);
            moneyFormatService.getMoneyFormatInZero(label_cashOut);
            moneyFormatService.getMoneyFormatInZero(label_refounds);
            moneyFormatService.getMoneyFormatInZero(label_totalOut);
        }

        private async void Look_Click(object sender, RoutedEventArgs e)
        {
            clearData();
            MissingUser.missing.usr_Username = userInformation.user.usr_Username;
            var credentials = new Credentials();                        
            await DialogHost.Show(credentials, "RootDialog");


            var transaction = new transactionService();

            var list = transaction.getCurrentTransactions(userInformation.user.usr_ID);
            bool initialCash = false;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].type == (int)TRANSACTIONTYPE.INITIAL)
                {
                    initialCash = true;
                    break;
                }
            }

            if (!initialCash)
            {
                await DialogHost.Show(new InitialCash(), "RootDialog");
            }

            loadTransactionsList();
        }
    }
}
