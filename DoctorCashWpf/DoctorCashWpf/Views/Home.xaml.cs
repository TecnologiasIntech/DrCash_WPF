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
        }

        public ICommand mostrar => new AnotherCommandImplementation(ExecuteRunDialog);
        private MoneyComponentService moneyComponent = new MoneyComponentService();
        private userService user = new userService();
        private transactionService transactionService = new transactionService();
        private List<transaction> transactionList = new List<transaction>();

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

        public void chargeTransactionsList()
        {

            transactionList = transactionService.getCurrentTransactions(userInformation.user.usr_ID);
            
            dataGridView1.ItemsSource = null;
            
            DataTable dt = new DataTable();
            dt.Columns.Add("Transactions");
            dt.Columns.Add("Processed By");
            dt.Columns.Add("Date");

            for (int i = 0; i < transactionList.Count(); i++)
            {
                var response = user.getUserByID((transactionList[i].userId).ToString());
               
                dt.Rows.Add(getTransactionComment(transactionList[i]), response.usr_FirstName + " " + response.usr_LastName, transactionList[i].dateRegistered);
            }

            dataGridView1.ItemsSource = dt.DefaultView;

            getSumOfTransactions();

        }

        private string getTransactionComment(transaction trn)
        {
            var comment = "";

            switch (trn.type)
            {
                case (int)TRANSACTIONTYPE.INITIAL:
                    comment = "Initial Cash In for total amount: $" + trn.cash + ".00";
                    break;

                case (int)TRANSACTIONTYPE.IN:

                    if (trn.copayment)
                    {
                        comment = "Payment for total amount: $" + getTotalAmount(trn).ToString() + ".00";
                    }
                    else if (trn.selfPay)
                    {
                        comment = "SelPay for total amount: $" + getTotalAmount(trn).ToString() + ".00";
                    }
                    else if (trn.deductible)
                    {
                        comment = "Deductible for total amount: $" + getTotalAmount(trn).ToString() + ".00";
                    }
                    else if (trn.other)
                    {
                        comment = trn.otherComments + " for total amount: $" + getTotalAmount(trn).ToString() + ".00";
                    }
                    else if (trn.labs)
                    {
                        comment = "Labs for total amount: $" + getTotalAmount(trn).ToString() + ".00";
                    }

                    break;

                case (int)TRANSACTIONTYPE.REFOUND:
                    comment = "Refound for total amount: $" + getTotalAmount(trn).ToString() + ".00";
                    break;
            }

            

            return comment;
        }

        private float getTotalAmount(transaction trn)
        {
            return trn.cash + trn.credit + trn.check;
        }

        private void getSumOfTransactions()
        {
            float cashIn = 0, credit = 0, checks = 0, cashOut = 0, initialCash = 0, refound = 0;

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
                }else if(transactionList[i].type == (int)TRANSACTIONTYPE.INITIAL)
                {
                    initialCash = transactionList[i].cash;
                }else if(transactionList[i].type == (int)TRANSACTIONTYPE.REFOUND)
                {
                    refound += transactionList[i].amountCharged;
                }
            }

            label_cashIn.Text = cashIn.ToString();
            label_credit.Text = credit.ToString();
            label_checks.Text = checks.ToString();
            label_refounds.Text = refound.ToString();
            label_balance.Text = (initialCash + cashIn + credit + checks - cashOut - refound).ToString();

            label_totalIn.Text = (cashIn + credit + checks).ToString();

            label_cashOut.Text = (cashOut + refound).ToString();

            label_totalOut.Text = (cashOut).ToString();

            label_initialCash.Text = initialCash.ToString();
            label_initialCash = moneyComponent.convertComponentToMoneyFormat(label_initialCash).labelComponent;

            moneyComponent.convertComponentToMoneyFormat(label_cashIn);
            moneyComponent.convertComponentToMoneyFormat(label_credit);
            moneyComponent.convertComponentToMoneyFormat(label_checks);
            moneyComponent.convertComponentToMoneyFormat(label_totalIn);
            moneyComponent.convertComponentToMoneyFormat(label_cashOut);
            moneyComponent.convertComponentToMoneyFormat(label_totalOut);
            moneyComponent.convertComponentToMoneyFormat(label_refounds);
            moneyComponent.convertComponentToMoneyFormat(label_balance);
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

        private async void RefundButton_Click(object sender, RoutedEventArgs e)
        {

            var refundAuth = new RefundAuth();
            await DialogHost.Show(refundAuth, "RootDialog");

        }

        private async void CloseDateButton_Click(object sender, RoutedEventArgs e)
        {
            var closeDate = new CloseDate();

            await DialogHost.Show(closeDate, "RootDialog");

            
        }

        private async void Capture_Initial_Cash(object sender, RoutedEventArgs e)
        {
            //para verificar las pantallas
            //await DialogHost.Show(new RefundAuth(), "RootDialog");
            //await DialogHost.Show(new UserNew(), "RootDialog");
            //await DialogHost.Show(new DailyTransactions(), "RootDialog");
            //await DialogHost.Show(new ClosedStatements(), "RootDialog");
            //await DialogHost.Show(new ViewReceipt(), "RootDialog");
            ///asyrdutiuydtsyuifduytayuyta


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

            chargeTransactionsList();

        }

    }
}
