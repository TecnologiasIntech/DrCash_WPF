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

        public DataTable getDailyTransactions(string transactionID, string patientName, string fromDate, string toDate)
        {
            if(transactionID == "" && patientName == "" && fromDate != "" && toDate != "")
            {
                return getDailyTransactionsByOnlyRange(fromDate, toDate);

            }
            else if (transactionID == "" && patientName != "" && fromDate == "" && toDate == "")
            {
                return getDailyTransactionsByOnlyPatientname(patientName);

            }
            else if (transactionID != "" && patientName == "" && fromDate == "" && toDate == "")
            {
                return getDailyTransactionsByOnlyTransactionID(transactionID);
            }
            else if (transactionID != "" && patientName != "" && fromDate != "" && toDate != "")
            {
                return getDailyTransactionsByRangeAndPatientNameAndTransactionID(patientName, transactionID, fromDate, toDate);

            }
            else if (transactionID == "" && patientName != "" && fromDate != "" && toDate != "")
            {
                return getDailyTransactionsByRangeAndPatientName(patientName, fromDate, toDate);

            }
            else if (transactionID != "" && patientName == "" && fromDate != "" && toDate != "")
            {
                return getDailyTransactionsByRangeAndTransactionID(transactionID, fromDate, toDate);

            }
            else if (transactionID != "" && patientName != "" && fromDate == "" && toDate == "")
            {
                return getDailyTransactionsByPatientNameAndTransactionID(patientName, transactionID);

            }
            else
            {
                return new DataTable();
            }
        }

        private DataTable getDailyTransactionsByOnlyRange(string fromDate, string toDate)
        {
            var columns = getTransactionsColumns();
            var terms = new List<valuesWhere>();

            terms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", date.convertToFormatDate(fromDate), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.GREATER_THAN_OR_EQUAL));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", date.convertToFormatDate(toDate), (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.LESS_THAN_OR_EQUAL));

            return createQuery.toSelect(columns, "transactions", terms);
        }

        private DataTable getDailyTransactionsByRangeAndPatientName(string patientName, string fromDate, string toDate)
        {
            var columns = getTransactionsColumns();
            var terms = new List<valuesWhere>();

            terms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", date.convertToFormatDate(fromDate), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.GREATER_THAN_OR_EQUAL));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", date.convertToFormatDate(toDate), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.LESS_THAN_OR_EQUAL));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_PatientName", patientName, (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.EQUALITY));

            return createQuery.toSelect(columns, "transactions", terms);
        }

        private DataTable getDailyTransactionsByRangeAndPatientNameAndTransactionID(string patientName, string transactionID, string fromDate, string toDate)
        {
            var columns = getTransactionsColumns();
            var terms = new List<valuesWhere>();

            terms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", date.convertToFormatDate(fromDate), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.GREATER_THAN_OR_EQUAL));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", date.convertToFormatDate(toDate), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.LESS_THAN_OR_EQUAL));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_PatientName", patientName, (int)OPERATORBOOLEAN.AND, (int)OPERATOR.EQUALITY));
            terms.Add(createItem.ofTypeValuesWhere(false, "trn_ID", transactionID, (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.EQUALITY));

            return createQuery.toSelect(columns, "transactions", terms);
        }

        private DataTable getDailyTransactionsByRangeAndTransactionID(string transactionID, string fromDate, string toDate)
        {
            var columns = getTransactionsColumns();
            var terms = new List<valuesWhere>();

            terms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", date.convertToFormatDate(fromDate), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.GREATER_THAN_OR_EQUAL));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", date.convertToFormatDate(toDate), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.LESS_THAN_OR_EQUAL));
            terms.Add(createItem.ofTypeValuesWhere(false, "trn_ID", transactionID, (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.EQUALITY));

            return createQuery.toSelect(columns, "transactions", terms);
        }

        private DataTable getDailyTransactionsByPatientNameAndTransactionID(string patientName, string transactionID)
        {
            var columns = getTransactionsColumns();
            var terms = new List<valuesWhere>();

            terms.Add(createItem.ofTypeValuesWhere(true, "trn_PatientName", patientName, (int)OPERATORBOOLEAN.AND, (int)OPERATOR.EQUALITY));
            terms.Add(createItem.ofTypeValuesWhere(false, "trn_ID", transactionID, (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.EQUALITY));

            return createQuery.toSelect(columns, "transactions", terms);
        }

        private DataTable getDailyTransactionsByOnlyPatientname(string patientName)
        {
            var columns = getTransactionsColumns();
            var terms = new List<valuesWhere>();

            terms.Add(createItem.ofTypeValuesWhere(true, "trn_PatientName", patientName, (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.EQUALITY));

            return createQuery.toSelect(columns, "transactions", terms);
        }

        private DataTable getDailyTransactionsByOnlyTransactionID(string transactionID)
        {
            var columns = getTransactionsColumns();
            var terms = new List<valuesWhere>();

            terms.Add(createItem.ofTypeValuesWhere(false, "trn_ID", transactionID, (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.EQUALITY));

            return createQuery.toSelect(columns, "transactions", terms);
        }

        private List<string> getTransactionsColumns()
        {
            var columns = new List<string>();
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

            return columns;
        }

    }
}
