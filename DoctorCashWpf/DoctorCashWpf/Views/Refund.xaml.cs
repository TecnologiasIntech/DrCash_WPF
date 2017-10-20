using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

        private userService userservice = new userService();
        private BrushConverter brushConverter = new BrushConverter();
        private logService serviceslog = new logService();
        private dateService dateservice = new dateService();


        private void setLog(user userData)
        {
            if (userData != null && (userData.usr_SecurityLevel >= (int)SECURIRYLEVEL.SUPERVISOR))
            {
                var items = new log();
                items.log_Username = userData.usr_Username;
                items.log_DateTime = dateservice.getCurrentDate();
                items.log_Actions = "Refund Authorized by: " + userData.usr_FirstName + " " + userData.usr_LastName + ", Level of user: " + userData.usr_SecurityLevel;
                serviceslog.CreateLog(items);
            }
            else
            {
                var items = new log();
                items.log_Username = userData.usr_Username;
                items.log_DateTime = dateservice.getCurrentDate();
                items.log_Actions = "Intent To Refund Not Authorized, Data to Access: UserName= " + txtbox_username.Text + ", PassWord= " + txtbox_password.Password.ToString();
                serviceslog.CreateLog(items);
            }
        }
        private void authentification()
        {
            var userData = userservice.authentication(txtbox_username.Text, txtbox_password.Password.ToString());

            //only user valid
            if (userData != null && (userData.usr_SecurityLevel >= (int)SECURIRYLEVEL.SUPERVISOR))
            {
                setLog(userData);
                createRefund.isRefund = true;
                MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
            }
            else
            {
                labelError.Content = "Invalid User";
                setLog(userData);                
            }
        }

        private void checkFields()
        {
            if (txtbox_username.Text == "")
            {
                txtbox_username.Focus();
                txtbox_username.Foreground = (Brush)brushConverter.ConvertFrom("#e74c3c");
                labelError.Content = "Complete the fields marked";
            }
            else if (txtbox_password.Password.ToString() == "")
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            checkFields();
        }

        private void authentification_KeyUp(object sender, KeyEventArgs e)
        {
            labelError.Content = "";
            if (e.Key == Key.Enter)
            {
                checkFields();
            }
        }

    }
}
