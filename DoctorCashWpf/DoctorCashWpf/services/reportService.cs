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

        public DataTable getCloseTransactions(string closeID, string fromDate, string toDate)
        {
            if(closeID != "" && fromDate == "" && toDate == "")
            {
                return getCloseTransactionByID(closeID);
            }
            else if (closeID == "" && fromDate != "" && toDate != "")
            {
                return getCloseTransactionByRange(fromDate, toDate);
            }
            else if (closeID != "" && fromDate != "" && toDate != "")
            {
                return getCloseTransactionByIdAndRange(closeID, fromDate, toDate);
            }
            else
            {
                return new DataTable();
            }
        }

        public transactionsObj getDailyTransactions(string transactionID, string patientName, string fromDate, string toDate)
        {
            var data = new DataTable();
            var dailyTrnObj = new transactionsObj();

            if (transactionID == "" && patientName == "" && fromDate != "" && toDate != "")
            {
                data =  getDailyTransactionsByOnlyRange(fromDate, toDate);

            }
            else if (transactionID == "" && patientName != "" && fromDate == "" && toDate == "")
            {
                data = getDailyTransactionsByOnlyPatientname(patientName);

            }
            else if (transactionID != "" && patientName == "" && fromDate == "" && toDate == "")
            {
                data = getDailyTransactionsByOnlyTransactionID(transactionID);
            }
            else if (transactionID != "" && patientName != "" && fromDate != "" && toDate != "")
            {
                data = getDailyTransactionsByRangeAndPatientNameAndTransactionID(patientName, transactionID, fromDate, toDate);

            }
            else if (transactionID == "" && patientName != "" && fromDate != "" && toDate != "")
            {
                data = getDailyTransactionsByRangeAndPatientName(patientName, fromDate, toDate);

            }
            else if (transactionID != "" && patientName == "" && fromDate != "" && toDate != "")
            {
                data = getDailyTransactionsByRangeAndTransactionID(transactionID, fromDate, toDate);

            }
            else if (transactionID != "" && patientName != "" && fromDate == "" && toDate == "")
            {
                data = getDailyTransactionsByPatientNameAndTransactionID(patientName, transactionID);
            }

            dailyTrnObj.dataTable = data;
            dailyTrnObj.list = setTransactionsInList(data);

            return dailyTrnObj;
        }

        private DataTable getCloseTransactionByRange(string fromDate, string toDate)
        {
            var termsList = new List<valuesWhere>();
            termsList.Add(createItem.ofTypeValuesWhere(true, "clt_Datetime", date.convertToFormatDate(fromDate), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.GREATER_THAN_OR_EQUAL));
            termsList.Add(createItem.ofTypeValuesWhere(true, "clt_Datetime", date.convertToFormatDate(toDate), (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.LESS_THAN_OR_EQUAL));

            return createQuery.toSelectAll("ClosedTransactios", termsList);
        }

        private DataTable getCloseTransactionByID(string ID)
        {
            var termsList = new List<valuesWhere>();
            termsList.Add(createItem.ofTypeValuesWhere(false, "clt_closed_ID", ID, (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.EQUALITY));

            return createQuery.toSelectAll("ClosedTransactios", termsList);
        }

        private DataTable getCloseTransactionByIdAndRange(string ID, string fromDate, string toDate)
        {
            var termsList = new List<valuesWhere>();
            termsList.Add(createItem.ofTypeValuesWhere(true, "clt_Datetime", date.convertToFormatDate(fromDate), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.GREATER_THAN_OR_EQUAL));
            termsList.Add(createItem.ofTypeValuesWhere(true, "clt_Datetime", date.convertToFormatDate(toDate), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.LESS_THAN_OR_EQUAL));
            termsList.Add(createItem.ofTypeValuesWhere(false, "clt_closed_ID", ID, (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.EQUALITY));

            return createQuery.toSelectAll("ClosedTransactios", termsList);
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

        private List<transaction> setTransactionsInList(DataTable data)
        {
            var list = new List<transaction>();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow filas = data.Rows[i];
                var items = new transaction();

                items.trn_id = Convert.ToInt32(filas["trn_ID"]);
                items.userId = Convert.ToInt32(filas["trn_User_ID"]);
                items.dateRegistered = Convert.ToString(filas["trn_DateRegistered"]);
                items.type = Convert.ToInt32(filas["trn_Type"]);
                items.amountCharged = Convert.ToInt32(filas["trn_AmountCharged"]);
                items.cash = Convert.ToInt32(filas["trn_Cash"]);
                items.credit = Convert.ToInt32(filas["trn_Credit"]);
                items.check = Convert.ToInt32(filas["trn_Check"]);
                items.checkNumber = Convert.ToInt32(filas["trn_CheckNumber"]);
                items.change = Convert.ToInt32(filas["trn_Change"]);
                items.patientFirstName = Convert.ToString(filas["trn_PatientFirstName"]);
                items.closed = Convert.ToBoolean(filas["trn_Closed"]);
                items.registerId = Convert.ToString(filas["trn_RegisterID"]);

                list.Add(items);
            }

            return list;
        }

    }
}
