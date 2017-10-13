using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
        private userService user = new userService();
        private sqlQueryService createQuery = new sqlQueryService();
        private createItemsForListService createItem = new createItemsForListService();
        private logService serviceslog = new logService();
        private dateService date = new dateService();

        private void txtbox_Confirm_Password_KeyUp(object sender, KeyEventArgs e)
        {
            labelError.Content = "";
            if ((txtbox_Confirm_Password.Password.ToString() != txtbox_password.Password.ToString())&&(txtbox_Confirm_Password.Password.ToString()!=""))
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
                txtbox_password.Foreground = (Brush)brushConverter.ConvertFrom("#e74c3c");                
            }
            else if (txtbox_Confirm_Password.Password.ToString() == "")
            {
                labelError.Content = "Complete the fields marked";
                txtbox_Confirm_Password.Focus();               
                txtbox_Confirm_Password.Foreground = (Brush)brushConverter.ConvertFrom("#e74c3c");                
            }
            else if (txtbox_question.Text == "")
            {
                labelError.Content= "Complete the fields marked";
                txtbox_question.Focus();                
                txtbox_question.Foreground = (Brush)brushConverter.ConvertFrom("#e74c3c");                
            }
            else
            {
                var items = new log();
                items.log_Username = userInformation.user.usr_Username;
                items.log_DateTime = date.getCurrentDate();
                items.log_Actions = "Reset User: " + userInformation.user.usr_FirstName + " " + userInformation.user.usr_LastName + ", Level of user: " + userInformation.user.usr_SecurityLevel;
                serviceslog.CreateLog(items);

                user.userNew(txtbox_password.Password.ToString(), Combo_question.Text, txtbox_question.Text);

                MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }

        public char xcaracter;
        public int valor = 0;
        public int text; 
        private void txtbox_password_KeyUp(object sender, KeyEventArgs e)
        {
            labelError.Content = "";
            valor = 0;
            progressbar.Foreground = (Brush)brushConverter.ConvertFrom("#ffffff");
            progressbar.Value = 0;
            text = txtbox_password.Password.Count();
            for (int i = 0; i < text; i++)
            {
                xcaracter = txtbox_password.Password.ToString()[i];
                switch (xcaracter)
                {
                    case '!': valor = valor + 1; break;
                    case '"': valor = valor + 1; break;
                    case '#': valor = valor + 1; break;
                    case '$': valor = valor + 1; break;
                    case '%': valor = valor + 1; break;
                    case '&': valor = valor + 1; break;
                    case '/': valor = valor + 1; break;
                    case '(': valor = valor + 1; break;
                    case ')': valor = valor + 1; break;
                    case '?': valor = valor + 1; break;
                    case '¡': valor = valor + 1; break;
                    case '_': valor = valor + 1; break;
                    case '[': valor = valor + 1; break;
                    case ']': valor = valor + 1; break;
                    case '{': valor = valor + 1; break;
                    case '}': valor = valor + 1; break;
                    case '°': valor = valor + 1; break;
                    case '|': valor = valor + 1; break;
                    case '.': valor = valor + 1; break;
                    case ',': valor = valor + 1; break;
                    default: break;
                }
                if (char.IsDigit(xcaracter)) valor = valor + 1;                
            }
            if (valor == 1)
            {                
                progressbar.Foreground = (Brush)brushConverter.ConvertFrom("#ed0909");
                progressbar.Value = 1;
            }
            else if (valor == 2)
            {
                progressbar.Foreground = (Brush)brushConverter.ConvertFrom("#f28b04");
                progressbar.Value = 2;
            }
            else if (valor == 3)
            {
                progressbar.Foreground = (Brush)brushConverter.ConvertFrom("#dbc523");
                progressbar.Value = 3;
            }
            else if (valor == 4)
            {
                progressbar.Foreground = (Brush)brushConverter.ConvertFrom("#cbea19");
                progressbar.Value = 4;
            }
            else if (valor == 5)
            {
                progressbar.Foreground = (Brush)brushConverter.ConvertFrom("#c3db3b");
                progressbar.Value = 5;
            }
            else if (valor == 6)
            {
                progressbar.Foreground = (Brush)brushConverter.ConvertFrom("#96f902");
                progressbar.Value = 6;
            }
            else if (valor == 7)
            {
                progressbar.Foreground = (Brush)brushConverter.ConvertFrom("#7cbf18");
                progressbar.Value = 7;
            }
            else if (valor == 8)
            {
                progressbar.Foreground = (Brush)brushConverter.ConvertFrom("#43680b");
                progressbar.Value = 8;
            }
            else if (valor >= 9)
            {
                progressbar.Foreground = (Brush)brushConverter.ConvertFrom("#15fc00");
                progressbar.Value = 9;
            }
            else if ((txtbox_password.Password.Count()>0)&&(valor<=1))
            {
                valor = 1;
                progressbar.Foreground = (Brush)brushConverter.ConvertFrom("#ed0909");
                progressbar.Value = 1;
            }
        }
    }
}
