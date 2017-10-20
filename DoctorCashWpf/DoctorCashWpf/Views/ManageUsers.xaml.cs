using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

            getUsersAndLoadUserTable();
        }

        private userService userService = new userService();
        private BrushConverter brushConverter = new BrushConverter();
        private logService logService = new logService();
        private int userID;
        private DataTable usersTable;
        private dateService dateService = new dateService();

        int userIdLog;
        string firstNameLog;
        string lastNameLog;
        string emailLog;
        bool passwordResetLog;
        bool activeAcountLog;
        string securityLevel, securityLevelLog;

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

            userService.updateUser(items);

            getUsersAndLoadUserTable();

            label_applyChanges.Foreground = (Brush)brushConverter.ConvertFrom("#FFFFFF");
            Apply_Changes.Background = (Brush)brushConverter.ConvertFrom("#27ae60");

            var item = new log();
            item.log_Username = userInformation.user.usr_Username;
            item.log_DateTime = dateService.getCurrentDate();
            item.log_Actions = "User Update Before: ( " + userIdLog + ", " + firstNameLog + ", " + lastNameLog + ", " + passwordResetLog.ToString() + ", " + activeAcountLog.ToString() + ", " + securityLevelLog + ", " + emailLog + " ), Now: ( " + items.usr_ID + ", " + items.usr_FirstName + ", " + items.usr_LastName + ", " + items.usr_Password + ", " + items.usr_ActiveAccount + ", " + items.usr_SecurityLevel + " ) by UserName= " + userInformation.user.usr_Username + ", Full Name= " + userInformation.user.usr_FirstName + " " + userInformation.user.usr_LastName + " in ManageUser";
            logService.CreateLog(item);

        }

        private int getSecurityLevel()
        {
            if (radio_user.IsChecked == true)
            {
                return (int)SECURIRYLEVEL.USER;
            }
            else if (radio_super.IsChecked == true)
            {
                return (int)SECURIRYLEVEL.SUPERVISOR;
            }
            else
            {
                return (int)SECURIRYLEVEL.ADMINISTRATOR;
            }
        }

        private void getUsersAndLoadUserTable()
        {
            usersTable = userService.getUsers();

            grid_users.ItemsSource = null;

            DataTable dt = new DataTable();
            dt.Columns.Add("Username");

            for (int i = 0; i < usersTable.Rows.Count; i++)
            {
                DataRow filas = usersTable.Rows[i];

                dt.Rows.Add(filas["usr_Username"]);
            }

            grid_users.ItemsSource = dt.DefaultView;

            grid_users.MaxHeight = 400;
        }

        private void saveTemporalUserInfo()
        {
            DataRow fila = usersTable.Rows[grid_users.SelectedIndex];

            userIdLog = (int)fila["usr_ID"];
            firstNameLog = fila["usr_FirstName"].ToString();
            lastNameLog = fila["usr_LastName"].ToString();
            emailLog = (string)fila["usr_Email"];
            passwordResetLog = (bool)fila["usr_PasswordReset"];
            activeAcountLog = (bool)fila["usr_ActiveAccount"];
            setSecurityLevel((int)fila["usr_SecurityLevel"]);
            securityLevelLog = securityLevel;

        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRow fila = usersTable.Rows[grid_users.SelectedIndex];
            saveTemporalUserInfo();
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
                    this.securityLevel = "User";
                    break;

                case (int)SECURIRYLEVEL.SUPERVISOR:
                    radio_super.IsChecked = true;
                    this.securityLevel = "Supervisor";
                    break;

                case (int)SECURIRYLEVEL.ADMINISTRATOR:
                    radio_admin.IsChecked = true;
                    this.securityLevel = "Administrator";
                    break;
            }
        }

        private void txtbox_email_KeyUp(object sender, KeyEventArgs e)
        {
            if (!txtbox_email.Text.Contains("@") || !txtbox_email.Text.Contains(".com"))
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