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
    /// Interaction logic for UserProfile.xaml
    /// </summary>
    public partial class UserProfile : UserControl
    {
        public UserProfile()
        {
            InitializeComponent();

            
        }
        private void loadeds(object sender, RoutedEventArgs e)
        {
            if (userInformation.user != null)
            {
                label_email.Text = userInformation.user.usr_Email;
                label_firstName.Text = userInformation.user.usr_FirstName;
                label_lastName.Text = userInformation.user.usr_LastName;
                label_securityQuestion.Text = userInformation.user.usr_SecurityQuestion;
                label_userFullName.Text = userInformation.user.usr_Username;

                checkTextBlock(label_email);
                checkTextBlock(label_firstName);
                checkTextBlock(label_lastName);
                checkTextBlock(label_securityQuestion);
                checkTextBlock(label_userFullName);
            }
        }

        private void checkTextBlock(TextBlock label)
        {
            if(label.Text == "")
            {
                label.Text = "Unknown";
            }
        }
    }
}
