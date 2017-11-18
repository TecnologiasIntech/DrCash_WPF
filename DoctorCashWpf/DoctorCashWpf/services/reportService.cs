using System;
using System.Collections.Generic;
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

        public closeDateObj getCloseTransactions(string closeID, string fromDate, string toDate)
        {
            var data = new DataTable();
            var dailyTrnObj = new closeDateObj();

            if (closeID != "" && fromDate == "" && toDate == "")
            {
                data= getCloseTransactionByID(closeID);
            }
            else if (closeID == "" && fromDate != "" && toDate != "")
            {
                data= getCloseTransactionByRange(fromDate, toDate);
            }
            else if (closeID != "" && fromDate != "" && toDate != "")
            {
                data= getCloseTransactionByIdAndRange(closeID, fromDate, toDate);
            }
            /*else
            {
                return new DataTable();
            }*/
            dailyTrnObj.dataTable = data;
            dailyTrnObj.list = setCloseDateInList(data);

            return dailyTrnObj;
        }

        public transactionsObj getTransactionsByRangeAndTransactionId(string transactionID, string patientName, string fromDate, string toDate)
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
            termsList.Add(createItem.ofTypeValuesWhere(true, "clt_Datetime", date.convertToFormatDateFinal(toDate), (int)OPERATORBOOLEAN.NONE, (int)OPERATOR.LESS_THAN_OR_EQUAL));

            return createQuery.toSelectAll("ClosedTransactions", termsList);
        }

        private DataTable getCloseTransactionByID(string ID)
        {
            var termsList = new List<valuesWhere>();
            termsList.Add(createItem.ofTypeValuesWhere(false, "clt_closed_ID", ID, (int)OPERATORBOOLEAN.NONE, (int)OPERATOR.EQUALITY));

            return createQuery.toSelectAll("ClosedTransactions", termsList);
        }

        private DataTable getCloseTransactionByIdAndRange(string ID, string fromDate, string toDate)
        {
            var termsList = new List<valuesWhere>();
            termsList.Add(createItem.ofTypeValuesWhere(true, "clt_Datetime", date.convertToFormatDate(fromDate), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.GREATER_THAN_OR_EQUAL));
            termsList.Add(createItem.ofTypeValuesWhere(true, "clt_Datetime", date.convertToFormatDateFinal(toDate), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.LESS_THAN_OR_EQUAL));
            termsList.Add(createItem.ofTypeValuesWhere(false, "clt_closed_ID", ID, (int)OPERATORBOOLEAN.NONE, (int)OPERATOR.EQUALITY));

            return createQuery.toSelectAll("ClosedTransactions", termsList);
        }

        public DataTable getLastInformation(string ID)
        {
            var columns = new List<string>();
            columns.Add("clt_100_bills");
            columns.Add("clt_50_bills");
            columns.Add("clt_20_bills");
            columns.Add("clt_10_bills");
            columns.Add("clt_5_bills");
            columns.Add("clt_1_bills");            
            columns.Add("clt_Username");
            columns.Add("clt_total_cash");
            var termsList = new List<valuesWhere>();
            //termsList.Add(createItem.ofTypeValuesWhere(true, "clt_Datetime", date.convertToFormatDate(fromDate), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.GREATER_THAN_OR_EQUAL));
            //termsList.Add(createItem.ofTypeValuesWhere(true, "clt_Datetime", date.convertToFormatDateFinal(toDate), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.LESS_THAN_OR_EQUAL));
            termsList.Add(createItem.ofTypeValuesWhere(false, "clt_closed_ID", ID, (int)OPERATORBOOLEAN.NONE, (int)OPERATOR.EQUALITY));

            return createQuery.toSelect(columns, "ClosedTransactions", termsList);            
        }

        private DataTable getDailyTransactionsByOnlyRange(string fromDate, string toDate)
        {
            var columns = getTransactionsColumns();
            var terms = new List<valuesWhere>();

            terms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", date.convertToFormatDate(fromDate), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.GREATER_THAN_OR_EQUAL));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", date.convertToFormatDateFinal(toDate), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.LESS_THAN_OR_EQUAL));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_Comment", "Initial Cash", (int)OPERATORBOOLEAN.AND, (int)OPERATOR.INEGUALITY));
            terms.Add(createItem.ofTypeValuesWhere(false, "trn_Type", ((int)TRANSACTIONTYPE.OUT).ToString(), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.INEGUALITY));
            terms.Add(createItem.ofTypeValuesWhere(false, "trn_Type", ((int)TRANSACTIONTYPE.REFOUND).ToString(), (int)OPERATORBOOLEAN.NONE, (int)OPERATOR.INEGUALITY));

            return createQuery.toSelect(columns, "transactions", terms);
        }

        private DataTable getDailyTransactionsByRangeAndPatientName(string patientName, string fromDate, string toDate)
        {
            var columns = getTransactionsColumns();
            var terms = new List<valuesWhere>();

            terms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", date.convertToFormatDate(fromDate), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.GREATER_THAN_OR_EQUAL));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", date.convertToFormatDateFinal(toDate), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.LESS_THAN_OR_EQUAL));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_PatientName", patientName, (int)OPERATORBOOLEAN.AND, (int)OPERATOR.EQUALITY));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_Comment", "Initial Cash", (int)OPERATORBOOLEAN.AND, (int)OPERATOR.INEGUALITY));
            terms.Add(createItem.ofTypeValuesWhere(false, "trn_Type", ((int)TRANSACTIONTYPE.OUT).ToString(), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.INEGUALITY));
            terms.Add(createItem.ofTypeValuesWhere(false, "trn_Type", ((int)TRANSACTIONTYPE.REFOUND).ToString(), (int)OPERATORBOOLEAN.NONE, (int)OPERATOR.INEGUALITY));

            return createQuery.toSelect(columns, "transactions", terms);
        }

        private DataTable getDailyTransactionsByRangeAndPatientNameAndTransactionID(string patientName, string transactionID, string fromDate, string toDate)
        {
            var columns = getTransactionsColumns();
            var terms = new List<valuesWhere>();

            terms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", date.convertToFormatDate(fromDate), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.GREATER_THAN_OR_EQUAL));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", date.convertToFormatDateFinal(toDate), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.LESS_THAN_OR_EQUAL));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_PatientName", patientName, (int)OPERATORBOOLEAN.AND, (int)OPERATOR.EQUALITY));
            terms.Add(createItem.ofTypeValuesWhere(false, "trn_ID", transactionID, (int)OPERATORBOOLEAN.AND, (int)OPERATOR.EQUALITY));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_Comment", "Initial Cash", (int)OPERATORBOOLEAN.AND, (int)OPERATOR.INEGUALITY));
            terms.Add(createItem.ofTypeValuesWhere(false, "trn_Type", ((int)TRANSACTIONTYPE.OUT).ToString(), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.INEGUALITY));
            terms.Add(createItem.ofTypeValuesWhere(false, "trn_Type", ((int)TRANSACTIONTYPE.REFOUND).ToString(), (int)OPERATORBOOLEAN.NONE, (int)OPERATOR.INEGUALITY));

            return createQuery.toSelect(columns, "transactions", terms);
        }

        private DataTable getDailyTransactionsByRangeAndTransactionID(string transactionID, string fromDate, string toDate)
        {
            var columns = getTransactionsColumns();
            var terms = new List<valuesWhere>();

            terms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", date.convertToFormatDate(fromDate), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.GREATER_THAN_OR_EQUAL));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", date.convertToFormatDateFinal(toDate), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.LESS_THAN_OR_EQUAL));
            terms.Add(createItem.ofTypeValuesWhere(false, "trn_ID", transactionID, (int)OPERATORBOOLEAN.AND, (int)OPERATOR.EQUALITY));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_Comment", "Initial Cash", (int)OPERATORBOOLEAN.AND, (int)OPERATOR.INEGUALITY));
            terms.Add(createItem.ofTypeValuesWhere(false, "trn_Type", ((int)TRANSACTIONTYPE.OUT).ToString(), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.INEGUALITY));
            terms.Add(createItem.ofTypeValuesWhere(false, "trn_Type", ((int)TRANSACTIONTYPE.REFOUND).ToString(), (int)OPERATORBOOLEAN.NONE, (int)OPERATOR.INEGUALITY));

            return createQuery.toSelect(columns, "transactions", terms);
        }

        private DataTable getDailyTransactionsByPatientNameAndTransactionID(string patientName, string transactionID)
        {
            var columns = getTransactionsColumns();
            var terms = new List<valuesWhere>();

            terms.Add(createItem.ofTypeValuesWhere(true, "trn_PatientName", patientName, (int)OPERATORBOOLEAN.AND, (int)OPERATOR.EQUALITY));
            terms.Add(createItem.ofTypeValuesWhere(false, "trn_ID", transactionID, (int)OPERATORBOOLEAN.AND, (int)OPERATOR.EQUALITY));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_Comment", "Initial Cash", (int)OPERATORBOOLEAN.AND, (int)OPERATOR.INEGUALITY));
            terms.Add(createItem.ofTypeValuesWhere(false, "trn_Type", ((int)TRANSACTIONTYPE.OUT).ToString(), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.INEGUALITY));
            terms.Add(createItem.ofTypeValuesWhere(false, "trn_Type", ((int)TRANSACTIONTYPE.REFOUND).ToString(), (int)OPERATORBOOLEAN.NONE, (int)OPERATOR.INEGUALITY));

            return createQuery.toSelect(columns, "transactions", terms);
        }

        private DataTable getDailyTransactionsByOnlyPatientname(string patientName)
        {
            var columns = getTransactionsColumns();
            var terms = new List<valuesWhere>();

            terms.Add(createItem.ofTypeValuesWhere(true, "trn_PatientName", patientName, (int)OPERATORBOOLEAN.AND, (int)OPERATOR.EQUALITY));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_Comment", "Initial Cash", (int)OPERATORBOOLEAN.AND, (int)OPERATOR.INEGUALITY));
            terms.Add(createItem.ofTypeValuesWhere(false, "trn_Type", ((int)TRANSACTIONTYPE.OUT).ToString(), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.INEGUALITY));
            terms.Add(createItem.ofTypeValuesWhere(false, "trn_Type", ((int)TRANSACTIONTYPE.REFOUND).ToString(), (int)OPERATORBOOLEAN.NONE, (int)OPERATOR.INEGUALITY));

            return createQuery.toSelect(columns, "transactions", terms);
        }

        private DataTable getDailyTransactionsByOnlyTransactionID(string transactionID)
        {
            var columns = getTransactionsColumns();
            var terms = new List<valuesWhere>();

            terms.Add(createItem.ofTypeValuesWhere(false, "trn_ID", transactionID, (int)OPERATORBOOLEAN.AND, (int)OPERATOR.EQUALITY));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_Comment", "Initial Cash", (int)OPERATORBOOLEAN.AND, (int)OPERATOR.INEGUALITY));
            terms.Add(createItem.ofTypeValuesWhere(false, "trn_Type", ((int)TRANSACTIONTYPE.OUT).ToString(), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.INEGUALITY));
            terms.Add(createItem.ofTypeValuesWhere(false, "trn_Type", ((int)TRANSACTIONTYPE.REFOUND).ToString(), (int)OPERATORBOOLEAN.NONE, (int)OPERATOR.INEGUALITY));

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

        private List<closeDate> setCloseDateInList(DataTable data)
        {
            var list = new List<closeDate>();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow filas = data.Rows[i];
                var items = new closeDate();

                items.clt_closed_ID = Convert.ToInt32(filas["clt_closed_ID"]);
                items.clt_100_bills = Convert.ToDouble(filas["clt_100_bills"]);
                items.clt_50_bills = Convert.ToDouble(filas["clt_50_bills"]);
                items.clt_20_bills = Convert.ToDouble(filas["clt_20_bills"]);
                items.clt_10_bills = Convert.ToDouble(filas["clt_10_bills"]);
                items.clt_5_bills = Convert.ToDouble(filas["clt_5_bills"]);
                items.clt_1_bills = Convert.ToDouble(filas["clt_1_bills"]);
                items.clt_checks_amount = Convert.ToInt32(filas["clt_checks_amount"]);
                items.clt_credits_amount = Convert.ToInt32(filas["clt_credits_amount"]);
                items.clt_total_charged = Convert.ToDouble(filas["clt_total_charged"]);
                items.clt_total_cash = Convert.ToDouble(filas["clt_total_cash"]);
                items.clt_total_check = Convert.ToDouble(filas["clt_total_check"]);
                items.clt_total_credit = Convert.ToDouble(filas["clt_total_credit"]);
                items.clt_initial_cash = Convert.ToDouble(filas["clt_initial_cash"]);
                items.clt_balance = Convert.ToDouble(filas["clt_balance"]);
                items.clt_transaction_count = Convert.ToInt32(filas["clt_transaction_count"]);
                items.clt_reg_RegisterID = Convert.ToString(filas["clt_reg_RegisterID"]);
                items.clt_Username = Convert.ToString(filas["clt_Username"]);
                items.clt_Datetime = Convert.ToString(filas["clt_Datetime"]);

                list.Add(items);
            }

            return list;
        }

    }
}
