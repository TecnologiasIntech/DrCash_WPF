using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorCashWpf
{
    public class transaction
    {
        public int trn_id;
        public int userId;
        public string dateRegistered;
        public string comment;
        public int type;
        public float amountCharged;
        public float cash;
        public float credit;
        public float check;
        public int checkNumber;
        public float change;
        public string patientFirstName;
        public bool copayment;
        public bool selfPay;
        public bool deductible;
        public bool labs;
        public bool other;
        public string otherComments;
        public bool closed;
        public string registerId;
        public int modifiedById;
        public string modificationDate;
    }
}
