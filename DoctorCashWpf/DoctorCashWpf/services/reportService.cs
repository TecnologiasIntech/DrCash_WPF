using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DoctorCashWpf
{
    class reportService
    {
        private sqlQueryService createQuery = new sqlQueryService();
        private createItemsForListService createItem = new createItemsForListService();
        private dateService date = new dateService();

        public DataTable getCloseTransactionByRange(string fromDate, string toDate)
        {
            var termsList = new List<valuesWhere>();
            termsList.Add(createItem.ofTypeValuesWhere(true, "clt_Datetime", date.convertToFormatDate(fromDate), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.GREATER_THAN_OR_EQUAL));
            termsList.Add(createItem.ofTypeValuesWhere(true, "clt_Datetime", date.convertToFormatDate(toDate), (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.LESS_THAN_OR_EQUAL));

            return createQuery.toSelectAll("ClosedTransactios", termsList);
        }

        public DataTable getDailyTransactionsByRange(string fromDate, string toDate)
        {
            var columns = new List<string>();
            var terms = new List<valuesWhere>();

            columns.Add("trn_ID");
            columns.Add("trn_User_ID");
            columns.Add("trn_DateRegistered");
            columns.Add("trn_PatientFirstName");
            columns.Add("trn_Type");
            columns.Add("trn_AmountCharged");
            columns.Add("trn_Cash");
            columns.Add("trn_Credit");
            columns.Add("trn_Check");
            columns.Add("trn_Change");
            columns.Add("trn_CheckNumber");
            columns.Add("trn_Closed");
            columns.Add("trn_RegisterID");

            terms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", date.convertToFormatDate(fromDate), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.GREATER_THAN_OR_EQUAL));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", date.convertToFormatDate(toDate), (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.LESS_THAN_OR_EQUAL));

            return createQuery.toSelect(columns, "transactions", terms);
        }

    }
}
