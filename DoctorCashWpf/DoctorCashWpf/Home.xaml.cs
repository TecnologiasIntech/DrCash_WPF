using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
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
        }

        private void GitHubButton_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start(ConfigurationManager.AppSettings["GitHub"]);
        }

        private void TwitterButton_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://twitter.com/James_Willock");
        }

        private void ChatButton_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://gitter.im/ButchersBoy/MaterialDesignInXamlToolkit");
        }

        private void EmailButton_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("mailto://james@dragablz.net");
        }

        private void DonateButton_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://pledgie.com/campaigns/31029");
        }


        //public Home()
        //{
        //    var transactionService = new transactionService();
        //    var list = new List<transaction>();
        //    list = transactionService.getTransactions();

        //    InitializeComponent();
        //}

        //private void CashInButton_Click(object sender, RoutedEventArgs e)
        //{
        //    // Opens a new Modal Window
        //    CashInWindow modalWindow = new CashInWindow();
        //    modalWindow.ShowDialog();
        //    /*  var transactionService = new transactionService();
        //      var list = new List<transaction>();
        //      list = transactionService.getTransactions();*/

        //}

        //private void CashOutButton_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void RefundButton_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void CloseDateButton_Click(object sender, RoutedEventArgs e)
        //{

        //}
    }
}
