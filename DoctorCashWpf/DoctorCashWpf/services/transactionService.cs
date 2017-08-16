using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DoctorCashWpf
{
    class transactionService
    {

        private dateService date = new dateService();
        private sqlQueryService createQuery = new sqlQueryService();
        private createItemsForListService createList = new createItemsForListService();

        public List<transaction> getCurrentTransactions(int currentUserID)
        {
            DataTable data = new DataTable();
            var list = new List<transaction>();

            var columns = new List<string>();
            columns.Add("trn_ID");
            columns.Add("trn_User_ID");
            columns.Add("trn_DateRegistered");
            columns.Add("trn_Comment");
            columns.Add("trn_Type");
            columns.Add("trn_AmountCharged");
            columns.Add("trn_Cash");
            columns.Add("trn_Credit");
            columns.Add("trn_Check");
            columns.Add("trn_CheckNumber");
            columns.Add("trn_Change");
            columns.Add("trn_PatientFirstName");
            columns.Add("trn_Copayment");
            columns.Add("trn_SelfPay");
            columns.Add("trn_Deductible");
            columns.Add("trn_Labs");
            columns.Add("trn_Other");
            columns.Add("trn_OtherComments");
            columns.Add("trn_Closed");
            columns.Add("trn_RegisterID");
            columns.Add("trn_ModifiedBy_ID");
            columns.Add("trn_ModificationDate");
            
            var listTerms = new List<valuesWhere>();

            listTerms.Add(createList.ofTypeValuesWhere(false, "trn_User_ID", currentUserID.ToString(), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.EQUALITY));
            listTerms.Add(createList.ofTypeValuesWhere(true, "trn_DateRegistered", date.getInitialDate(), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.GREATER_THAN_OR_EQUAL));
            listTerms.Add(createList.ofTypeValuesWhere(true, "trn_DateRegistered", date.getEndDate(), -1, (int)OPERATOR.LESS_THAN_OR_EQUAL));

            data = createQuery.toSelect(columns, "transactions", listTerms);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow filas = data.Rows[i];
                var items = new transaction();

                items.trn_id = Convert.ToInt32(filas["trn_ID"]);
                items.userId = Convert.ToInt32(filas["trn_User_ID"]);
                items.dateRegistered = Convert.ToString(filas["trn_DateRegistered"]);
                items.comment = Convert.ToString(filas["trn_Comment"]);
                items.type = Convert.ToInt32(filas["trn_Type"]);
                items.amountCharged = Convert.ToInt32(filas["trn_AmountCharged"]);
                items.cash = Convert.ToInt32(filas["trn_Cash"]);
                items.credit = Convert.ToInt32(filas["trn_Credit"]);
                items.check = Convert.ToInt32(filas["trn_Check"]);
                items.checkNumber = Convert.ToInt32(filas["trn_CheckNumber"]);
                items.change = Convert.ToInt32(filas["trn_Change"]);
                items.patientFirstName = Convert.ToString(filas["trn_PatientFirstName"]);
                items.copayment = Convert.ToBoolean(filas["trn_Copayment"]);
                items.selfPay = Convert.ToBoolean(filas["trn_SelfPay"]);
                items.deductible = Convert.ToBoolean(filas["trn_Deductible"]);
                items.labs = Convert.ToBoolean(filas["trn_Labs"]);
                items.other = Convert.ToBoolean(filas["trn_Other"]);
                items.otherComments = Convert.ToString(filas["trn_OtherComments"]);
                items.closed = Convert.ToBoolean(filas["trn_Closed"]);
                items.registerId = Convert.ToString(filas["trn_RegisterID"]);
                items.modifiedById = Convert.ToInt32(filas["trn_ModifiedBy_ID"]);
                items.modificationDate = Convert.ToString(filas["trn_ModificationDate"]);

                list.Add(items);
            }

            return list;
        }

        public void getTransactionsByRange(string dateInitial, string dateEnd)
        {
            var listTerms = new List<valuesWhere>();
            listTerms.Add(createList.ofTypeValuesWhere(true, "trn_DateRegistered", dateInitial, (int)OPERATORBOOLEAN.AND, (int)OPERATOR.GREATER_THAN_OR_EQUAL));
            listTerms.Add(createList.ofTypeValuesWhere(true, "trn_DateRegistered", dateEnd, -1, (int)OPERATOR.LESS_THAN_OR_EQUAL));

            createQuery.toSelectAll("transactions", listTerms);
        }

        public void setTransaction(transaction transactionArray)
        {
            List<columnsValues> valuesArray = new List<columnsValues>();
            valuesArray.Add(createList.ofTypeColumnsValues("trn_User_ID", transactionArray.userId));
            //valuesArray.Add(createList.ofTypeColumnsValues("trn_DateRegistered", transactionArray.dateRegistered));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_Comment", transactionArray.comment));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_Type", transactionArray.type));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_AmountCharged", transactionArray.amountCharged));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_Cash", transactionArray.cash));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_Credit", transactionArray.credit));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_Check", transactionArray.check));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_CheckNumber", transactionArray.checkNumber));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_Change", transactionArray.change));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_PatientFirstName", transactionArray.patientFirstName));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_Copayment", transactionArray.copayment));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_SelfPay", transactionArray.selfPay));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_Deductible", transactionArray.deductible));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_Labs", transactionArray.labs));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_Other", transactionArray.other));
            //valuesArray.Add(createList.ofTypeColumnsValues("trn_OtherComments", transactionArray.otherComments));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_Closed", transactionArray.closed));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_RegisterID", transactionArray.registerId));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_ModifiedBy_ID", transactionArray.modifiedById));
            //valuesArray.Add(listService.ofTypeColumnsValues("trn_ModificationDate", transactionArray.modificationDate));

            createQuery.toInsert("transactions", valuesArray);
        }

        public void setTransactionOut(transaction transactionArray)
        {
            List<columnsValues> valuesArray = new List<columnsValues>();
            valuesArray.Add(createList.ofTypeColumnsValues("trn_User_ID", transactionArray.userId));
            //valuesArray.Add(createList.ofTypeColumnsValues("trn_DateRegistered", transactionArray.dateRegistered));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_Comment", transactionArray.comment));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_Type", transactionArray.type));
            //valuesArray.Add(createList.ofTypeColumnsValues("trn_AmountCharged", transactionArray.amountCharged));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_Cash", transactionArray.cash));
            //valuesArray.Add(createList.ofTypeColumnsValues("trn_Credit", transactionArray.credit));
            //valuesArray.Add(createList.ofTypeColumnsValues("trn_Check", transactionArray.check));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_CheckNumber", transactionArray.checkNumber));
           // valuesArray.Add(createList.ofTypeColumnsValues("trn_Change", transactionArray.change));
           // valuesArray.Add(createList.ofTypeColumnsValues("trn_PatientFirstName", transactionArray.patientFirstName));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_Copayment", transactionArray.copayment));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_SelfPay", transactionArray.selfPay));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_Deductible", transactionArray.deductible));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_Labs", transactionArray.labs));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_Other", transactionArray.other));
            //valuesArray.Add(createList.ofTypeColumnsValues("trn_OtherComments", transactionArray.otherComments));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_Closed", transactionArray.closed));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_RegisterID", transactionArray.registerId));
            //valuesArray.Add(createList.ofTypeColumnsValues("trn_ModifiedBy_ID", transactionArray.modifiedById));
            //valuesArray.Add(listService.ofTypeColumnsValues("trn_ModificationDate", transactionArray.modificationDate));

            createQuery.toInsert("transactions", valuesArray);
        }

        public void setTransactionInitialCash(transaction transactionArray)
        {
            List<columnsValues> valuesArray = new List<columnsValues>();
            valuesArray.Add(createList.ofTypeColumnsValues("trn_User_ID", transactionArray.userId));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_DateRegistered", date.getCurrentDate()));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_Comment", transactionArray.comment));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_Type", transactionArray.type));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_Cash", transactionArray.cash));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_CheckNumber", transactionArray.checkNumber));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_Copayment", transactionArray.copayment));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_SelfPay", transactionArray.selfPay));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_Deductible", transactionArray.deductible));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_Labs", transactionArray.labs));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_Other", transactionArray.other));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_Closed", transactionArray.closed));
            valuesArray.Add(createList.ofTypeColumnsValues("trn_RegisterID", transactionArray.registerId));

            createQuery.toInsert("transactions", valuesArray);
        }

        public void closeDate(closeDate closeDate)
        {
            var list = new List<columnsValues>();
            list.Add(createList.ofTypeColumnsValues("clt_100_bills", closeDate.clt_100_bills));
            list.Add(createList.ofTypeColumnsValues("clt_50_bills", closeDate.clt_50_bills));
            list.Add(createList.ofTypeColumnsValues("clt_20_bills", closeDate.clt_20_bills));
            list.Add(createList.ofTypeColumnsValues("clt_10_bills", closeDate.clt_10_bills));
            list.Add(createList.ofTypeColumnsValues("clt_5_bills", closeDate.clt_5_bills));
            list.Add(createList.ofTypeColumnsValues("clt_1_bills", closeDate.clt_1_bills));
            list.Add(createList.ofTypeColumnsValues("clt_checks_amount", closeDate.clt_checks_amount));
            list.Add(createList.ofTypeColumnsValues("clt_credits_amount", closeDate.clt_credits_amount));
            list.Add(createList.ofTypeColumnsValues("clt_total_charged", closeDate.clt_total_charged));
            list.Add(createList.ofTypeColumnsValues("clt_total_cash", closeDate.clt_total_cash));
            list.Add(createList.ofTypeColumnsValues("clt_total_check", closeDate.clt_total_check));
            list.Add(createList.ofTypeColumnsValues("clt_total_credit", closeDate.clt_total_credit));
            list.Add(createList.ofTypeColumnsValues("clt_initial_cash", closeDate.clt_initial_cash));
            list.Add(createList.ofTypeColumnsValues("clt_balance", closeDate.clt_balance));
            list.Add(createList.ofTypeColumnsValues("clt_transaction_count", closeDate.clt_transaction_count));
            list.Add(createList.ofTypeColumnsValues("clt_reg_RegisterID", closeDate.clt_reg_RegisterID));
            list.Add(createList.ofTypeColumnsValues("clt_Username", closeDate.clt_Username));
            list.Add(createList.ofTypeColumnsValues("clt_Datetime", closeDate.clt_Datetime));

            createQuery.toInsert("ClosedTransactions", list);
        }

    }
}
