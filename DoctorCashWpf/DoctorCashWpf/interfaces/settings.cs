using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorCashWpf
{
    public class settings
    {
        public int ID;
        public string SMTPServer;
        public int SMTPPort;
        public string SMTPEmailFrom;
        public string SMTPEmailBCC;
        public string SMTPUsername;
        public string SMTPPassword;
        public bool LockAutomatically;
        public int TimeOutLock;
        public bool RefreshSummary;
        public int TimeRefreshSummary;
        public string Logo;
        public bool ResetPasswordEmail;
        public bool DefaultPassword;
        public string DefaultPasswordValue;
        public bool LeaveMoneyInRegister;
    }
}
