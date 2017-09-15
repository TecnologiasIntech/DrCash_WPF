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
    /// Interaction logic for UserCreate.xaml
    /// </summary>
    public partial class UserCreate : UserControl
    {
        public UserCreate()
        {
            InitializeComponent();
        }
        userService user = new userService();
        private BrushConverter brushConverter = new BrushConverter();
        private logService serviceslog = new logService();

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtbox_email.Text != "") 
            {
                labeldata.Content = "";
            }
            if ((!txtbox_email.Text.Contains("@") || !txtbox_email.Text.Contains(".com")))
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
            if (txtbox_username.Text == "")
            {
                txtbox_username.Focus();
                txtbox_username.Foreground = (Brush)brushConverter.ConvertFrom("#e74c3c");
                labeldata.Content = "Complete the fields marked";
            }
            else if (txtbox_firtname.Text == "")
            {
                txtbox_firtname.Focus();
                txtbox_firtname.Foreground = (Brush)brushConverter.ConvertFrom("#e74c3c");
                labeldata.Content = "Complete the fields marked";
            }
            else if (txtbox_lastname.Text == "")
            {
                txtbox_lastname.Focus();
                txtbox_lastname.Foreground = (Brush)brushConverter.ConvertFrom("#e74c3c");
                labeldata.Content = "Complete the fields marked";
            }
            else if (txtbox_email.Text == "")
            {
                txtbox_email.Focus();
                txtbox_email.Foreground = (Brush)brushConverter.ConvertFrom("#e74c3c");
                labeldata.Content = "Complete the fields marked";
            }            
            else
            {

                var item = new log();
                item.log_Username = txtbox_username.Text;
                item.log_DateTime = DateTime.Now.ToString();
                item.log_Actions = "New User Created= ( "+txtbox_firtname.Text+" "+txtbox_lastname.Text+" ) by: " + userInformation.user.usr_FirstName + " " + userInformation.user.usr_LastName + ", Level of user: " + userInformation.user.usr_SecurityLevel;
                serviceslog.CreateLog(item);


                var items = new user();
                items.usr_Username = txtbox_username.Text;
                items.usr_FirstName = txtbox_firtname.Text;
                items.usr_LastName = txtbox_lastname.Text;
                items.usr_Email = txtbox_email.Text;
                items.usr_SecurityLevel = getSecurityLevel();
                items.usr_Password = "";

                user.createUser(items);

                MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
            }        
        }
        public string password;
        private string PassWord(string firtsname, string lastname)
        {
            Random rnd = new Random();
            password = "";
            password += onlyFirstLastName(lastname);
            password += firtsname;
            password += rnd.Next(0, 100);            
            password += rnd.Next(0, 100);

           // MessageBox.Show(password);
            return password;
        }
        public string value;
        private string onlyFirstLastName(string lastname)
        {
            
            for (int i = 0; i < lastname.Length; i++)
            {
                if(!lastname.Contains(" "))
                {
                    value= lastname;
                    break;
                }
                else if (lastname.Substring(0,i).Contains(" "))
                {
                    value= lastname.Substring(0, (i - 1));
                    break;                    
                }
                else
                {
                    value=lastname.Substring(0, i);
                }
            }
            return value;            
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

        private void txtbox_username_KeyUp(object sender, KeyEventArgs e)
        {
            labeldata.Content = "";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var item = new log();
            item.log_Username = txtbox_username.Text;
            item.log_DateTime = DateTime.Now.ToString();
            item.log_Actions = "User Creation Canceled by: " + userInformation.user.usr_FirstName + " " + userInformation.user.usr_LastName + ", Level of user: " + userInformation.user.usr_SecurityLevel;
            serviceslog.CreateLog(item);
        }
    }
}
