using MaterialDesignThemes.Wpf;
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

        private userService user = new userService();
        private dateService date = new dateService();
        private logService serviceslog = new logService();


        private async void Manage_User_Button_Click(object sender, RoutedEventArgs e)
        {
            await DialogHost.Show(new ManageUsers(), "RootDialog");

            if (createUser.isCreateUser)
            {
                await DialogHost.Show(new UserCreate(), "RootDialog");

                createUser.isCreateUser = false;

                Manage_User_Button_Click(null, null);
            }

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

            if (userInformation.user.usr_SecurityLevel == (int)SECURIRYLEVEL.USER)
            {
                btn_manageUsers.Visibility = Visibility.Collapsed;
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
            txtbox_email.Text = userInformation.user.usr_Email;
            txtbox_firstname.Text = userInformation.user.usr_FirstName;
            txtbox_lastname.Text = userInformation.user.usr_LastName;
            Combo_question.Text = userInformation.user.usr_SecurityQuestion;

            UserEditInformationShow();
        }

        private void Accept_Button_Click(object sender, RoutedEventArgs e)
        {
            var items = new user();
            items.usr_Email = txtbox_email.Text;
            items.usr_FirstName = txtbox_firstname.Text;
            items.usr_LastName = txtbox_lastname.Text;
            items.usr_SecurityQuestion = Combo_question.Text;
            items.usr_ModifiedBy = userInformation.user.usr_ID;
            items.usr_ModificationDate = date.getCurrentDate();

            if (txtbox_newpassword.Password == txtbox_confirmpassword.Password && txtbox_newpassword.Password != "")
            {
                items.usr_Password = txtbox_confirmpassword.Password;
            }
            else
            {
                items.usr_Password = null;
            }

            var item = new log();
            item.log_Username = userInformation.user.usr_Username;
            item.log_DateTime = DateTime.Now.ToString();
            item.log_Actions = "The (" + txtbox_firstname.Text + " " + txtbox_lastname.Text + ") Information Was Modified by:" + userInformation.user.usr_FirstName + " " + userInformation.user.usr_LastName + ", Level of user: " + userInformation.user.usr_SecurityLevel;
            serviceslog.CreateLog(item);

            user.updateBasicInformation(items);

            UserInformationShow();

            txtbox_newpassword.Password = "";
            txtbox_confirmpassword.Password = "";
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
