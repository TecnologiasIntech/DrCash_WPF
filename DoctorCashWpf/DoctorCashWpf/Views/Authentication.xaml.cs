using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Web.Script.Serialization;
using System.IO;

namespace DoctorCashWpf.Views
{
    /// <summary>
    /// Interaction logic for Authentication.xaml
    /// </summary>
    /// 

    class Person
    {
        public string name { get; set; }
        public int age { get; set; }
        public override string ToString()
        {
            return "Nombre: " + name + "\nEdad: " + age;
        }
    }

    public partial class Authentication : UserControl
    {
        public Authentication()
        {
            InitializeComponent();
            generar();
        }

        private userService user = new userService();
        private BrushConverter brushConverter = new BrushConverter();
        private logService serviceslog = new logService();
        private dateService date = new dateService();       

        private void authentification()
        {
            var userData = user.authentication(txtbox_username.Text, txtbox_password.Password.ToString());

            if (userData != null)
            {
                labelError.Content = String.Empty;
                setLog(userData);
                userInformation.user = userData;
                closeDialog();
            }
            else
            {   
                labelError.Content = "                    Verify Data"+"\n"+
                        "Username or Password is Incorrect";
            }
        }

        private void verifyFields()
        {
            if (txtbox_username.Text == String.Empty)
            {
                txtbox_username.Focus();
                showErrorAndFocusField(txtbox_username);
            }
            else if (txtbox_password.Password.ToString() == "")
            {
                txtbox_password.Focus();
                showErrorAndFocusField(txtbox_password);
            }
            else
            {
                authentification();
            }
        }

        private void showErrorAndFocusField(TextBox txtBox)
        {
            txtBox.Foreground = (Brush)brushConverter.ConvertFrom("#e74c3c");
            labelError.Content = "Complete the fields marked";
        }

        private void showErrorAndFocusField(PasswordBox txtBox)
        {
            txtBox.Foreground = (Brush)brushConverter.ConvertFrom("#e74c3c");
            labelError.Content = "Complete the fields marked";
        }

        private void closeDialog()
        {
            MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void setLog(user userData)
        {
            var items = new log();
            items.log_Username = txtbox_username.Text;
            items.log_DateTime = date.getCurrentDate();
            items.log_Actions = "Login of: " + userData.usr_FirstName + " " + userData.usr_LastName + ", Level of user: " + userData.usr_SecurityLevel;
            serviceslog.CreateLog(items);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            verifyFields();
        }

        private void authentification_KeyUp(object sender, KeyEventArgs e)
        {
            labelError.Content = String.Empty;
            if (e.Key == Key.Enter)
            {
                verifyFields();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void generar()
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            string outputJSON = File.ReadAllText("MiPrimerJSON.json");
            Person p1 = ser.Deserialize<Person>(outputJSON);
            Console.WriteLine(p1.age.ToString());
            Console.ReadLine();
        }
    }
}
