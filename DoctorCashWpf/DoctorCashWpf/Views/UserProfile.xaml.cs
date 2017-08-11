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

            InitializeVisualComponent();

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

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            UserEditInformationShow();
        }

        private void Accept_Button_Click(object sender, RoutedEventArgs e)
        {
            UserInformationShow();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            UserInformationShow();
        }

        private void UserInformationShow()
        {
            // Buttons
            EditButton.Visibility = Visibility.Visible;
            CancelButton.Visibility = AcceptButton.Visibility = Visibility.Collapsed;

            // Grid
            UserInformationEditGridHide();
            UserInformationGridShow();
        }

        private void UserEditInformationShow()
        {
            // Buttons
            EditButton.Visibility = Visibility.Collapsed;
            CancelButton.Visibility = AcceptButton.Visibility = Visibility.Visible;

            // Grid
            UserInformationGridHide();
            UserInformationEditGridShow();
        }

        private void InitializeVisualComponent()
        {

            CancelButton.Visibility = AcceptButton.Visibility = Visibility.Collapsed;
            UserInformationEditGridHide();

        }

        private void UserInformationGridHide()
        {
            UserInformationCol1.Width = new GridLength(0);
            UserInformationCol2.Width = new GridLength(0);
        }

        private void UserInformationGridShow() 
        {
            UserInformationCol1.Width = new GridLength(1, GridUnitType.Star);
            UserInformationCol2.Width = new GridLength(1, GridUnitType.Star);
        }

        private void UserInformationEditGridHide()
        {
            UserEditInformationCol1.Width = new GridLength(0);
            UserEditInformationCol2.Width = new GridLength(0);
        }

        private void UserInformationEditGridShow()
        {
            UserEditInformationCol1.Width = new GridLength(1, GridUnitType.Star);
            UserEditInformationCol2.Width = new GridLength(1, GridUnitType.Star);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
