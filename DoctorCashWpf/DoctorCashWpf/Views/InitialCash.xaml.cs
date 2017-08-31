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

namespace DoctorCashWpf.Views
{
    /// <summary>
    /// Interaction logic for InitialCash.xaml
    /// </summary>
    public partial class InitialCash : UserControl
    {
        public InitialCash()
        {
            InitializeComponent();

            if (!checkInitialTransaction())
            {
                //No pedir la transaccion inicial o cerrar este dialogo
            }
        }
        private BrushConverter brushConverter = new BrushConverter();
        private transactionService transaction = new transactionService();
        private MoneyComponentService moneyComponent = new MoneyComponentService();
        private dateService date = new dateService();

        private bool checkInitialTransaction()
        {
            bool check = false;

            var list = transaction.getCurrentTransactions(4); //Obtener ID de login
            for (int i = 0; i < list.Count; i++)
            {
                if(list[i].type == (int)TRANSACTIONTYPE.INITIAL)
                { 
                    check = true;
                    break;
                }
                else
                {
                    check = false;
                }               
            }
            return check;
        }

        private void setInitialCash()
        {
            
            if (txtbox_initialCash.Text != "$0.00")
            {                
                var items = new transaction();

                items.cash = (float)Convert.ToDouble(txtbox_initialCash.Text.Remove(0, 1));
                items.type = (int)TRANSACTIONTYPE.INITIAL;
                items.comment = "Initial Cash";
                items.userId = userInformation.user.usr_ID;
                items.registerId = userInformation.user.usr_Username;

                transaction.setTransactionInitialCash(items);

                MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }

        private void Desing()
        {
            txtbox_initialCash.Clear();
            txtbox_initialCash.Focus();            
            txtbox_initialCash.Foreground = (Brush)brushConverter.ConvertFrom("#e74c3c");            
        }

        private void verificar()
        {
            try
            {
                if (txtbox_initialCash.Text == "")
                {
                    labelCash.Content = "Insert initial Cash";
                    Desing();
                }
                else if (Convert.ToDouble(txtbox_initialCash.Text.Remove(0, 1)) == 0)
                {
                    labelCash.Content = "Insert initial Cash";
                    Desing();
                }
                else if (Convert.ToDouble(txtbox_initialCash.Text.Remove(0, 1)) > 120)
                {
                    labelCash.Content = "Add less Cash";
                    Desing();
                }
                else if (Convert.ToDouble(txtbox_initialCash.Text.Remove(0, 1)) < 0)
                {
                    labelCash.Content = "Negative Values";
                    Desing();
                }
                else
                {
                    labelCash.Content = "";
                    moneyComponent.convertComponentToMoneyFormat(txtbox_initialCash, () => { });
                    setInitialCash();
                }
            }
            catch
            {
                labelCash.Content = "Only Numbers";
                Desing();
            }                       
        }

        private void setInitialCash_KeyUp(object sender, KeyEventArgs e)
        {
            labelCash.Content = "";
        }

        private void setInitialCash_click(object sender, RoutedEventArgs e)
        {
            verificar();                      
        }

        private void txtbox_initialCash_LostFocus(object sender, RoutedEventArgs e)
        {
            moneyComponent.convertComponentToMoneyFormat(txtbox_initialCash, () => { });
        }
    }
}
