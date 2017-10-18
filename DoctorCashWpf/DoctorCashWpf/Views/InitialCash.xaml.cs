using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DoctorCashWpf.Views
{
    /// <summary>
    /// Interaction logic for InitialCash.xaml
    /// </summary>
    public partial class InitialCash : UserControl
    {
        private BrushConverter brushConverter = new BrushConverter();
        private transactionService transaction = new transactionService();
        private MoneyFormatService moneyComponent = new MoneyFormatService();
        private dateService date = new dateService();
        private logService serviceslog = new logService();
        private static double INITIAL_CASH_BASE = 120;

        public InitialCash()
        {
            InitializeComponent();
            hiddenAndCollapsedButtonsAgreeAndDisagree();
        }

        private void hiddenAndCollapsedButtonsAgreeAndDisagree()
        {
            BtnAgree.Visibility = Visibility.Hidden;
            BtnAgree.Visibility = Visibility.Collapsed;
            BtnDisagree.Visibility = Visibility.Hidden;
            BtnDisagree.Visibility = Visibility.Collapsed;
        }

        private void showButtonsAgreeAndDisagree()
        {
            BtnAgree.Visibility = Visibility.Visible;            
            BtnDisagree.Visibility = Visibility.Visible;           
        }

        private void setInitialCash()
        {
          
            transaction.setTransactionInitialCash(txtbox_initialCash.Text);

            var item = new log();
            item.log_Username = userInformation.user.usr_Username;
            item.log_DateTime = date.getCurrentDate();
            item.log_Actions = "Set Initial Cash by UserName= " + userInformation.user.usr_Username + ", Full Name: " + userInformation.user.usr_FirstName + " " + userInformation.user.usr_LastName + " For= " + txtbox_initialCash.Text;
            serviceslog.CreateLog(item);

            MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void designOfAlertInError()
        {                        
            txtbox_initialCash.Clear();
            txtbox_initialCash.Focus();
            txtbox_initialCash.Foreground = (Brush)brushConverter.ConvertFrom("#e74c3c");            
        }

        private void verifyInitialCash(Double initialcash)
        {

            double initialCashBase = Convert.ToDouble(moneyComponent.AddFloat(INITIAL_CASH_BASE.ToString()));

            if (initialcash == 0)
            {
                labelInitialCashError.Content = "Insert initial Cash";
                designOfAlertInError();
            }
            else if (initialcash == initialCashBase)
            {
                labelInitialCashError.Content = "";                
                moneyComponent.convertToMoneyFormat(txtbox_initialCash, () => { });
                setInitialCash();
            }
            else if (initialcash > initialCashBase)
            {
                labelInitialCashError.Content = "Are you sure to enter more "+"\n"+"     than $120 in the box?";                               
                showButtonsAgreeAndDisagree();

            }
            else if (initialcash < initialCashBase)
            {              
                labelInitialCashError.Content = "Are you sure to enter " +"\n  "+ txtbox_initialCash.Text + " in the box?";                
                showButtonsAgreeAndDisagree();
            }

        }

        private void setInitialCash_click(object sender, RoutedEventArgs e)
        {
            moneyComponent.convertToMoneyFormat(txtbox_initialCash, () => { });
            double initialCash = Convert.ToDouble(txtbox_initialCash.Text.Remove(0, 1));
            verifyInitialCash(initialCash);                      
        }

        private void BtnAgree_Click(object sender, RoutedEventArgs e)
        {
            moneyComponent.convertToMoneyFormat(txtbox_initialCash, () => { });
            setInitialCash();
            labelInitialCashError.Content = "";
            hiddenAndCollapsedButtonsAgreeAndDisagree();
        }

        private void BtnDisagree_Click(object sender, RoutedEventArgs e)
        {
            txtbox_initialCash.Focus();
            txtbox_initialCash.Clear();
            labelInitialCashError.Content = "";
            hiddenAndCollapsedButtonsAgreeAndDisagree();
        }
    }
}
