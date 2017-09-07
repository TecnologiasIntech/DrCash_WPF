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
        private int userID;
        private DataTable usersData;

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
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRow fila = usersData.Rows[grid_users.SelectedIndex];

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
                    break;

                case (int)SECURIRYLEVEL.SUPERVISOR:
                    radio_super.IsChecked = true;
                    break;

                case (int)SECURIRYLEVEL.ADMINISTRATOR:
                    radio_admin.IsChecked = true;
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