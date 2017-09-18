using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Threading;

namespace DoctorCashWpf.Views
{
    /// <summary>
    /// Interaction logic for ManageUsers.xaml
    /// </summary>
    public partial class ManageUsers : UserControl
    {
        public ManageUsers()
        {
            InitializeComponent();

            getUsers();
        }

        private userService user = new userService();
        private BrushConverter brushConverter = new BrushConverter();
        private logService serviceslog = new logService();
        private int userID;
        private DataTable usersData;

        int uID;
        string firstName;
        string lastName;
        string email;
        bool passwordReset;
        bool activeAcount;
        string security,security1;

        private void Apply_Changes_Click(object sender, RoutedEventArgs e)
        {
            var items = new user();

            items.usr_FirstName = txtbox_firstName.Text;
            items.usr_LastName = txtbox_lastName.Text;
            items.usr_PasswordReset = (bool)check_passwordReset.IsChecked;
            items.usr_ActiveAccount = (bool)check_activeAcount.IsChecked;
            items.usr_SecurityLevel = getSecurityLevel();
            items.usr_Email = txtbox_email.Text;
            items.usr_ID = userID;

            user.updateUser(items);

            getUsers();

            label_applyChanges.Foreground = (Brush)brushConverter.ConvertFrom("#FFFFFF");
            Apply_Changes.Background = (Brush)brushConverter.ConvertFrom("#27ae60");

            var item = new log();
            item.log_Username = userInformation.user.usr_Username;
            item.log_DateTime = DateTime.Now.ToString();
            item.log_Actions = "User Update Before: ( "+ uID+", "+firstName+", "+lastName+", "+passwordReset.ToString()+", "+activeAcount.ToString()+", "+security1+", "+email+" ), Now: ( "+items.usr_ID+", "+items.usr_FirstName+", "+items.usr_LastName+", "+items.usr_Password+", "+items.usr_ActiveAccount+", "+items.usr_SecurityLevel+" ) by UserName= " + userInformation.user.usr_Username + ", Full Name= " + userInformation.user.usr_FirstName + " " + userInformation.user.usr_LastName + " in ManageUser";
            serviceslog.CreateLog(item);

        }

        private int getSecurityLevel()
        {
            if (radio_user.IsChecked == true)
            {
                return (int)SECURIRYLEVEL.USER;
            }else if(radio_super.IsChecked == true)
            {
                return (int)SECURIRYLEVEL.SUPERVISOR;
            }
            else
            {
                return (int)SECURIRYLEVEL.ADMINISTRATOR;
            }
        }

        private void getUsers()
        {
            usersData = user.getUsers();

            grid_users.ItemsSource = null;

            DataTable dt = new DataTable();
            dt.Columns.Add("Username");

            for (int i = 0; i < usersData.Rows.Count; i++)
            {
                DataRow filas = usersData.Rows[i];

                dt.Rows.Add(filas["usr_Username"]);
            }

            grid_users.ItemsSource = dt.DefaultView;

            grid_users.MaxHeight = 400;
        }

        private void saveTemporalDataUser()
        {
            DataRow fila = usersData.Rows[grid_users.SelectedIndex];

            uID = (int)fila["usr_ID"];
            firstName = fila["usr_FirstName"].ToString();
            lastName = fila["usr_LastName"].ToString();
            email = (string)fila["usr_Email"];
            passwordReset = (bool)fila["usr_PasswordReset"];
            activeAcount = (bool)fila["usr_ActiveAccount"];
            setSecurityLevel((int)fila["usr_SecurityLevel"]);
            security1 = security;

        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRow fila = usersData.Rows[grid_users.SelectedIndex];
            saveTemporalDataUser();
            userID = (int)fila["usr_ID"];
            txtbox_firstName.Text = fila["usr_FirstName"].ToString();
            txtbox_lastName.Text = fila["usr_LastName"].ToString();
            txtbox_email.Text = (string)fila["usr_Email"];
            check_passwordReset.IsChecked = (bool)fila["usr_PasswordReset"];
            check_activeAcount.IsChecked = (bool)fila["usr_ActiveAccount"];
            setSecurityLevel((int)fila["usr_SecurityLevel"]);

            label_applyChanges.Foreground = (Brush)brushConverter.ConvertFrom("#6fa8dc");
            Apply_Changes.Background = (Brush)brushConverter.ConvertFrom("#00FFFFFF");

        }

        private void setSecurityLevel(int securityLevel)
        {
            switch (securityLevel)
            {
                case (int)SECURIRYLEVEL.USER:
                    radio_user.IsChecked = true;
                    security = "User";
                    break;

                case (int)SECURIRYLEVEL.SUPERVISOR:
                    radio_super.IsChecked = true;
                    security = "Supervisor";
                    break;

                case (int)SECURIRYLEVEL.ADMINISTRATOR:
                    radio_admin.IsChecked = true;
                    security = "Administrator";
                    break;
            }
        }

        private void txtbox_email_KeyUp(object sender, KeyEventArgs e)
        {
            if (!txtbox_email.Text.Contains("@")||!txtbox_email.Text.Contains(".com"))
            {
                labelerror.Content = "Does not match an email";
            }
            else
            {
                labelerror.Content = "";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            createUser.isCreateUser = true;

            MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}