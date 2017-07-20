using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorCashWpf
{
    public class transaction
    {
        public int trn_id { get; set; }
        public int userId { get; set; }
        public string dateRegistered { get; set; }
        public string comment { get; set;}
        public int type { get; set; }
        public float amountCharged { get; set; }
        public float cash { get; set; }
        public float credit { get; set; }
        public float check { get; set; }
        public int checkNumber { get; set; }
        public float change { get; set; }
        public string patientFirstName { get; set; }
        public bool copayment { get; set; }
        public bool selfPay { get; set; }
        public bool deductible { get; set; }
        public bool labs { get; set; }
        public bool other { get; set; }
        public string otherComments { get; set; }
        public bool closed { get; set; }
        public string registerId { get; set; }
        public int modifiedById { get; set; }
        public string modificationDate { get; set; }
    }
}
