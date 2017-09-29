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
    /// Lógica de interacción para Credentials.xaml
    /// </summary>
    public partial class Credentials : UserControl
    {
        public Credentials()
        {
            InitializeComponent();
            txtbox_username.Text=MissingUser.missing.usr_Username;
            txtbox_username.Focus();
        }
        private userService user = new userService();
        private BrushConverter brushConverter = new BrushConverter();
        private logService serviceslog = new logService();

        private  void authentification()
        {
            var userData = user.authentication(txtbox_username.Text, txtbox_password.Password.ToString());

            //En esta parte se verificara si es admin o supervisor
            if (userData != null)
            {                

                var items = new log();
                items.log_Username = userData.usr_Username;
                items.log_DateTime = DateTime.Now.ToString();
                items.log_Actions = "Loging of missing user: " + userData.usr_FirstName + " " + userData.usr_LastName + ", Level of user: " + userData.usr_SecurityLevel;
                serviceslog.CreateLog(items);

                MissingUser.isMissing = false;
                userInformation.user = userData;
                MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
            }
            else
            {                      
                labelError.Content = "Password Incorrect";
                txtbox_password.Focus();
                txtbox_password.Foreground = (Brush)brushConverter.ConvertFrom("#e74c3c");

                var items = new log();
                items.log_Username = userData.usr_Username;
                items.log_DateTime = DateTime.Now.ToString();
                items.log_Actions = "Intent To Login Not Authorized, Data to Access: UserName= " + txtbox_username.Text + ", PassWord= " + txtbox_password.Password.ToString();
                serviceslog.CreateLog(items);

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtbox_password.Password.ToString() == "")
            {
                txtbox_password.Focus();

                txtbox_password.Foreground = (Brush)brushConverter.ConvertFrom("#e74c3c");
                labelError.Content = "Complete the fields marked";
            }
            else
            {
                authentification();
            }
        }

        private void txtbox_password_KeyUp(object sender, KeyEventArgs e)
        {
            labelError.Content = "";
        }
    }
}
