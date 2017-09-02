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

namespace DoctorCashWpf
{
    /// <summary>
    /// Lógica de interacción para RefundAuth.xaml
    /// </summary>
    public partial class RefundAuth : UserControl
    {
        public RefundAuth()
        {
            InitializeComponent();
        }

        private userService user = new userService();
        private BrushConverter brushConverter = new BrushConverter();

        private void authentification()
        {
            var userData = user.authentication(txtbox_username.Text, txtbox_password.Password.ToString());

            //En esta parte se verificara si es admin o supervisor
            if (userData != null && (userData.usr_SecurityLevel==3||userData.usr_SecurityLevel==2))
            {
                //Si es administrador o supervisor hara lo demas en esta parte
            }
            else
            {                      
                labelError.Content = "Invalid User";
            }
        }

        private void verify()
        {
            if (txtbox_username.Text == "")
            {
                txtbox_username.Focus();

                txtbox_username.Background = (Brush)brushConverter.ConvertFrom("#f1c40f");
                txtbox_username.Foreground = (Brush)brushConverter.ConvertFrom("#ffffff");
                txtbox_username.FontWeight = FontWeights.Bold;
                labelError.Content = "Complete the fields marked";
            }
            else if (txtbox_password.Password.ToString() == "")
            {
                txtbox_password.Focus();

                txtbox_password.Background = (Brush)brushConverter.ConvertFrom("#f1c40f");
                txtbox_password.Foreground = (Brush)brushConverter.ConvertFrom("#ffffff");
                txtbox_password.FontWeight = FontWeights.Bold;
                labelError.Content = "Complete the fields marked";
            }
            else
            {
                authentification();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            verify();
        }

        private void authentification_KeyUp(object sender, KeyEventArgs e)
        {
            labelError.Content = "";
            if (e.Key == Key.Enter)
            {
                verify();
            }
        }

    }
}
