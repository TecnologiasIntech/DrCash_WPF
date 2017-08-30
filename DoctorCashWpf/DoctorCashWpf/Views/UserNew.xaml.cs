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
    /// Lógica de interacción para UserNew.xaml
    /// </summary>
    public partial class UserNew : UserControl
    {
        public UserNew()
        {
            InitializeComponent();
        }

        private BrushConverter brushConverter = new BrushConverter();

        private void txtbox_Confirm_Password_KeyUp(object sender, KeyEventArgs e)
        {
            labelError.Content = "";
            if (txtbox_Confirm_Password.Password.ToString() != txtbox_password.Password.ToString())
            {
                Password_Diferent.Content = "Password does not match";
            }
            else
            {
                Password_Diferent.Content = "";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtbox_password.Password.ToString() == "")
            {
                labelError.Content = "Complete the fields marked";
                txtbox_password.Focus();
                txtbox_password.Background = (Brush)brushConverter.ConvertFrom("#f1c40f");
                txtbox_password.Foreground = (Brush)brushConverter.ConvertFrom("#ffffff");
                txtbox_password.FontWeight = FontWeights.Bold;
            }
            else if (txtbox_Confirm_Password.Password.ToString() == "")
            {
                labelError.Content = "Complete the fields marked";
                txtbox_Confirm_Password.Focus();
                txtbox_Confirm_Password.Background = (Brush)brushConverter.ConvertFrom("#f1c40f");
                txtbox_Confirm_Password.Foreground = (Brush)brushConverter.ConvertFrom("#ffffff");
                txtbox_Confirm_Password.FontWeight = FontWeights.Bold;
            }
            else if (txtbox_question.Text == "")
            {
                labelError.Content= "Complete the fields marked";
                txtbox_question.Focus();
                txtbox_question.Background = (Brush)brushConverter.ConvertFrom("#f1c40f");
                txtbox_question.Foreground = (Brush)brushConverter.ConvertFrom("#ffffff");
                txtbox_question.FontWeight = FontWeights.Bold;
            }
            else
            {

            }
        }
    }
}
