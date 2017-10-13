namespace DoctorCashWpf
{
    class MissingUser
    {
        public static bool isMissing = false;

        public static missing missing = new missing();
    }


    public class missing
    {
        public int usr_ID;
        public string usr_Username;
        public string usr_FirstName;
        public string usr_LastName;
        public string usr_Password;
        public string usr_SecurityQuestion;
        public string usr_SecurityAnswer;
        public string usr_Email;
        public int usr_SecurityLevel;
        public bool usr_ActiveAccount;
        public bool usr_PasswordReset;
        public int usr_ModifiedBy;
        public string usr_ModificationDate;
        public int usr_CreatedBy;
        public string usr_CreationDate;
    }
}
