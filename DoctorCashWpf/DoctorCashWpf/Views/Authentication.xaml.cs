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
    /// Interaction logic for Authentication.xaml
    /// </summary>
    public partial class Authentication : UserControl
    {
        public Authentication()
        {
            InitializeComponent();
        }

        private userService user = new userService();
        private BrushConverter brushConverter = new BrushConverter();

        private void authentification()
        {
            var userData = user.authentication(txtbox_username.Text, txtbox_password.Password.ToString());

            if (userData != null)
            {
                labelError.Content = "";
                // Abrir Initial Cash
                /* userInformation.userName = userData.usr_Username;
                 userInformation.userID = userData.usr_ID;*/
                userInformation.user = userData;
                MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
            }
            else
            {
                //Mensaje de error por datos incorrectos
                //poner que no se encontro el usuario
                labelError.Content = "        Verificar Datos"+"\n"+
                                     "No se encontro el usuario";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtbox_username.Text == "")
            {
                txtbox_username.Focus();

                txtbox_username.Background = (Brush)brushConverter.ConvertFrom("#f1c40f");
                txtbox_username.Foreground = (Brush)brushConverter.ConvertFrom("#ffffff");
                txtbox_username.FontWeight = FontWeights.Bold;
            }
            else if (txtbox_password.Password.ToString() =="")
            {
                txtbox_password.Focus();

                txtbox_password.Background = (Brush)brushConverter.ConvertFrom("#f1c40f");
                txtbox_password.Foreground = (Brush)brushConverter.ConvertFrom("#ffffff");
                txtbox_password.FontWeight = FontWeights.Bold;
            }
            else
            {
                authentification();
            }            
        }

        private void authentification_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                authentification();
            }
        }
    }
}
