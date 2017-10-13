using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DoctorCashWpf
{
    class transactionService
    {

        private dateService date = new dateService();
        private sqlQueryService createQuery = new sqlQueryService();
        private createItemsForListService createItem = new createItemsForListService();

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

            listTerms.Add(createItem.ofTypeValuesWhere(false, "trn_User_ID", currentUserID.ToString(), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.EQUALITY));
            listTerms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", date.getInitialDate(), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.GREATER_THAN_OR_EQUAL));
            listTerms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", date.getEndDate(), (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.LESS_THAN_OR_EQUAL));
            //listTerms.Add(createItem.ofTypeValuesWhere(true, "trn_Comment", "Initial Cash", (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.INEGUALITY));

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
            listTerms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", dateInitial, (int)OPERATORBOOLEAN.AND, (int)OPERATOR.GREATER_THAN_OR_EQUAL));
            listTerms.Add(createItem.ofTypeValuesWhere(true, "trn_Comment", "Initial Cash", (int)OPERATORBOOLEAN.AND, (int)OPERATOR.INEGUALITY));
            listTerms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", dateEnd, (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.LESS_THAN_OR_EQUAL));

            createQuery.toSelectAll("transactions", listTerms);
        }

        public void setTransaction(transaction transactionArray)
        {
            List<columnsValues> valuesArray = new List<columnsValues>();
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_User_ID", transactionArray.userId));
            //valuesArray.Add(createList.ofTypeColumnsValues("trn_DateRegistered", transactionArray.dateRegistered));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Comment", transactionArray.comment));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Type", transactionArray.type));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_AmountCharged", transactionArray.amountCharged));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Cash", transactionArray.cash));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Credit", transactionArray.credit));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Check", transactionArray.check));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_CheckNumber", transactionArray.checkNumber));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Change", transactionArray.change));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_PatientFirstName", transactionArray.patientFirstName));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Copayment", transactionArray.copayment));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_SelfPay", transactionArray.selfPay));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Deductible", transactionArray.deductible));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Labs", transactionArray.labs));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Other", transactionArray.other));
            //valuesArray.Add(createList.ofTypeColumnsValues("trn_OtherComments", transactionArray.otherComments));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Closed", transactionArray.closed));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_RegisterID", "1")); //Cambiar esto
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_ModifiedBy_ID", transactionArray.modifiedById));
            //valuesArray.Add(listService.ofTypeColumnsValues("trn_ModificationDate", transactionArray.modificationDate));

            createQuery.toInsert("transactions", valuesArray);
        }

        public void setTransactionOut(transaction transactionArray)
        {
            List<columnsValues> valuesArray = new List<columnsValues>();
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_User_ID", transactionArray.userId));
            //valuesArray.Add(createList.ofTypeColumnsValues("trn_DateRegistered", transactionArray.dateRegistered));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Comment", transactionArray.comment));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Type", transactionArray.type));
            //valuesArray.Add(createList.ofTypeColumnsValues("trn_AmountCharged", transactionArray.amountCharged));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Cash", transactionArray.cash));
            //valuesArray.Add(createList.ofTypeColumnsValues("trn_Credit", transactionArray.credit));
            //valuesArray.Add(createList.ofTypeColumnsValues("trn_Check", transactionArray.check));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_CheckNumber", transactionArray.checkNumber));
           // valuesArray.Add(createList.ofTypeColumnsValues("trn_Change", transactionArray.change));
           // valuesArray.Add(createList.ofTypeColumnsValues("trn_PatientFirstName", transactionArray.patientFirstName));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Copayment", transactionArray.copayment));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_SelfPay", transactionArray.selfPay));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Deductible", transactionArray.deductible));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Labs", transactionArray.labs));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Other", transactionArray.other));
            //valuesArray.Add(createList.ofTypeColumnsValues("trn_OtherComments", transactionArray.otherComments));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Closed", transactionArray.closed));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_RegisterID", "1")); //cambiar esto
            //valuesArray.Add(createList.ofTypeColumnsValues("trn_ModifiedBy_ID", transactionArray.modifiedById));
            //valuesArray.Add(listService.ofTypeColumnsValues("trn_ModificationDate", transactionArray.modificationDate));

            createQuery.toInsert("transactions", valuesArray);
        }

        public void setTransactionInitialCash(transaction transactionArray)
        {
            List<columnsValues> valuesArray = new List<columnsValues>();
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_User_ID", transactionArray.userId));
            //valuesArray.Add(createItem.ofTypeColumnsValues("trn_DateRegistered", date.getCurrentDate()));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Comment", transactionArray.comment));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Type", transactionArray.type));
            //valuesArray.Add(createItem.ofTypeColumnsValues("trn_Initial_Cash", transactionArray.cash));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Cash", transactionArray.cash));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_CheckNumber", transactionArray.checkNumber));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Copayment", transactionArray.copayment));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_SelfPay", transactionArray.selfPay));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Deductible", transactionArray.deductible));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Labs", transactionArray.labs));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Other", transactionArray.other));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Closed", transactionArray.closed));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_RegisterID", "1")); //cambiar esto

            createQuery.toInsert("transactions", valuesArray);
        }

        public double getInitialCashbyRegisterIDAndRange(string registerID, string fromDate, string toDate)
        {
            double cash=0;
            var columns = new List<string>();
            var terms = new List<valuesWhere>();
            columns.Add("trn_Cash");
            
            terms.Add(createItem.ofTypeValuesWhere(false, "trn_RegisterID", registerID, (int)OPERATORBOOLEAN.AND, (int)OPERATOR.EQUALITY));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", date.convertToFormatDate(fromDate), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.GREATER_THAN_OR_EQUAL));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", date.convertToFormatDateFinal(toDate), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.LESS_THAN_OR_EQUAL));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_Comment", "Initial Cash", (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.EQUALITY));            

            var data = createQuery.toSelect(columns, "transactions", terms);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow filas = data.Rows[i];
                 cash += (float)Convert.ToDouble(filas["trn_Cash"]);
            }
            return cash;           
        }

        public double getInitialCashbyRegisterID(string registerID, string Date)
        {
            double cash = 0;
            var columns = new List<string>();
            var terms = new List<valuesWhere>();
            columns.Add("trn_Cash");

            terms.Add(createItem.ofTypeValuesWhere(false, "trn_RegisterID", registerID, (int)OPERATORBOOLEAN.AND, (int)OPERATOR.EQUALITY));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", date.convertToFormatDate(Date), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.GREATER_THAN_OR_EQUAL));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", date.convertToFormatDateFinal(Date), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.LESS_THAN_OR_EQUAL));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_Comment", "Initial Cash", (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.EQUALITY));

            var data = createQuery.toSelect(columns, "transactions", terms);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow filas = data.Rows[i];
                cash += (float)Convert.ToDouble(filas["trn_Cash"]);
            }
            return cash;
        }

        public double getInitialCashbyLoginID()
        {
            double cash = 0;
            var columns = new List<string>();
            var terms = new List<valuesWhere>();
            columns.Add("trn_Cash");

            terms.Add(createItem.ofTypeValuesWhere(false, "trn_User_ID", userInformation.user.usr_ID.ToString(), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.EQUALITY));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", date.getInitialDate(), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.GREATER_THAN_OR_EQUAL));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", date.getEndDate(), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.LESS_THAN_OR_EQUAL));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_Comment", "Initial Cash", (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.EQUALITY));

            var data = createQuery.toSelect(columns, "transactions", terms);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow filas = data.Rows[i];
                cash += (float)Convert.ToDouble(filas["trn_Cash"]);
            }
            return cash;
        }

        public void setTransactionRefund(transaction trn)
        {
            List<columnsValues> valuesArray = new List<columnsValues>();
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_User_ID", trn.userId));
            //valuesArray.Add(createItem.ofTypeColumnsValues("trn_DateRegistered", date.getCurrentDate()));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Comment", trn.comment));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Type", trn.type));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_AmountCharged", trn.amountCharged));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_CheckNumber", 0)); //checar esto bien
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Copayment", trn.copayment));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_SelfPay", trn.selfPay));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Deductible", trn.deductible));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Labs", trn.labs));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Other", trn.other));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_Closed", trn.closed));
            valuesArray.Add(createItem.ofTypeColumnsValues("trn_RegisterID", "1")); //cambiar esto

            createQuery.toInsert("transactions", valuesArray);
        }

        public void setClosedTransaction(closeDate trn)
        {
            int maxid = 0;
            var list = new List<columnsValues>();
            
            var trns = getCurrentTransactions(userInformation.user.usr_ID);
            var checkCount = 0;
            var creditAmount = 0;
            var totalCharged = 0.00;

            for (int i = 0; i < trns.Count(); i++)
            {
                checkCount += trns[i].checkNumber;

                if(trns[i].credit > 0)
                {
                    creditAmount++;
                }

                totalCharged += trns[i].cash + trns[i].credit + trns[i].check;
            }
            var lis = new List<valuesWhere>();
            maxid =createQuery.toMax("clt_closed_ID", "ClosedTransactions", lis);

            list.Add(createItem.ofTypeColumnsValues("clt_closed_ID", maxid)); 
            list.Add(createItem.ofTypeColumnsValues("clt_100_bills", (float)trn.clt_100_bills));
            list.Add(createItem.ofTypeColumnsValues("clt_50_bills", (float)trn.clt_50_bills));
            list.Add(createItem.ofTypeColumnsValues("clt_20_bills", (float)trn.clt_20_bills));
            list.Add(createItem.ofTypeColumnsValues("clt_10_bills", (float)trn.clt_10_bills));
            list.Add(createItem.ofTypeColumnsValues("clt_5_bills", (float)trn.clt_5_bills));
            list.Add(createItem.ofTypeColumnsValues("clt_1_bills", (float)trn.clt_1_bills));
            list.Add(createItem.ofTypeColumnsValues("clt_checks_amount", checkCount));
            list.Add(createItem.ofTypeColumnsValues("clt_total_charged", (float)totalCharged));
            list.Add(createItem.ofTypeColumnsValues("clt_credits_amount", creditAmount));
            list.Add(createItem.ofTypeColumnsValues("clt_total_cash", (float)trn.clt_total_cash));
            list.Add(createItem.ofTypeColumnsValues("clt_total_check", (float)trn.clt_total_check));
            list.Add(createItem.ofTypeColumnsValues("clt_total_credit", (float)trn.clt_total_credit));
            list.Add(createItem.ofTypeColumnsValues("clt_initial_cash", (float)getInitialCashbyLoginID()));
            list.Add(createItem.ofTypeColumnsValues("clt_balance", (float)closeDateInformation.closeDate.clt_balance));
            list.Add(createItem.ofTypeColumnsValues("clt_transaction_count", (float)closeDateInformation.closeDate.clt_transaction_count));
            list.Add(createItem.ofTypeColumnsValues("clt_reg_RegisterID", 1)); // Falta guardar el ID de la caja registradora en la que se encuentra
            list.Add(createItem.ofTypeColumnsValues("clt_Username", userInformation.user.usr_Username));
            list.Add(createItem.ofTypeColumnsValues("clt_Datetime", date.getCurrentDate()));

            createQuery.toInsert("ClosedTransactions", list);
        }

        public transaction getTransactionByTrnID(string transactionID)
        {
            var terms = new List<valuesWhere>();

            terms.Add(createItem.ofTypeValuesWhere(false, "trn_ID", transactionID, (int)OPERATORBOOLEAN.AND, (int)OPERATOR.EQUALITY));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_Comment", "Initial Cash", (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.INEGUALITY));

            var data = createQuery.toSelectAll("transactions", terms);
            var items = new transaction();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow filas = data.Rows[i];

                items.amountCharged = Convert.ToInt32(filas["trn_AmountCharged"]);
                items.copayment = Convert.ToBoolean(filas["trn_Copayment"]);
                items.selfPay = Convert.ToBoolean(filas["trn_SelfPay"]);
                items.deductible = Convert.ToBoolean(filas["trn_Deductible"]);
                items.labs = Convert.ToBoolean(filas["trn_Labs"]);
                items.other = Convert.ToBoolean(filas["trn_Other"]);
                items.otherComments = Convert.ToString(filas["trn_OtherComments"]);
                items.cash = Convert.ToInt32(filas["trn_Cash"]);
                items.credit = Convert.ToInt32(filas["trn_Credit"]);
                items.check = Convert.ToInt32(filas["trn_Check"]);

            }

            return items;
        }

        public List<transaction> getTransactionsByUserID(string userID)
        {
            var terms = new List<valuesWhere>();
            var list = new List<transaction>();

            terms.Add(createItem.ofTypeValuesWhere(false, "trn_User_ID", userID, (int)OPERATORBOOLEAN.AND, (int)OPERATOR.EQUALITY));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_Comment", "Initial Cash", (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.INEGUALITY));

            var data = createQuery.toSelectAll("transactions", terms);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow filas = data.Rows[i];
                var items = new transaction();

                items.trn_id = Convert.ToInt32(filas["trn_ID"]);
                items.amountCharged = Convert.ToInt32(filas["trn_AmountCharged"]);
                items.cash = Convert.ToInt32(filas["trn_Cash"]);
                items.credit = Convert.ToInt32(filas["trn_Credit"]);
                items.check = Convert.ToInt32(filas["trn_Check"]);
                items.change = Convert.ToInt32(filas["trn_Change"]);

                list.Add(items);
            }

            return list;
        }

        public List<transaction> getTransactionByTransactionID(string transactionID)
        {
            var terms = new List<valuesWhere>();
            var list = new List<transaction>();

            terms.Add(createItem.ofTypeValuesWhere(false, "trn_ID", transactionID, (int)OPERATORBOOLEAN.AND, (int)OPERATOR.EQUALITY));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_Comment", "Initial Cash", (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.INEGUALITY));

            var data = createQuery.toSelectAll("transactions", terms);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow filas = data.Rows[i];
                var items = new transaction();

                items.amountCharged = Convert.ToInt32(filas["trn_AmountCharged"]);
                items.copayment = Convert.ToBoolean(filas["trn_Copayment"]);
                items.selfPay = Convert.ToBoolean(filas["trn_SelfPay"]);
                items.deductible = Convert.ToBoolean(filas["trn_Deductible"]);
                items.labs = Convert.ToBoolean(filas["trn_Labs"]);
                items.otherComments = Convert.ToString(filas["trn_OtherComments"]);

                list.Add(items);
            }

            return list;
        }

        public transaction getObjTransactionByTransactionID(string transactionID)
        {
            var terms = new List<valuesWhere>();
            var items = new transaction();

            terms.Add(createItem.ofTypeValuesWhere(false, "trn_ID", transactionID, (int)OPERATORBOOLEAN.AND, (int)OPERATOR.EQUALITY));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_Comment", "Initial Cash", (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.INEGUALITY));

            var data = createQuery.toSelectAll("transactions", terms);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow filas = data.Rows[i];

                items.amountCharged = Convert.ToInt32(filas["trn_AmountCharged"]);
                items.copayment = Convert.ToBoolean(filas["trn_Copayment"]);
                items.selfPay = Convert.ToBoolean(filas["trn_SelfPay"]);
                items.deductible = Convert.ToBoolean(filas["trn_Deductible"]);
                items.labs = Convert.ToBoolean(filas["trn_Labs"]);
                items.otherComments = Convert.ToString(filas["trn_OtherComments"]);
                items.comment = Convert.ToString(filas["trn_Comment"]);
                items.cash = (float)Convert.ToDouble(filas["trn_Cash"]);
                items.credit = (float)Convert.ToDouble(filas["trn_Credit"]);
                items.check = (float)Convert.ToDouble(filas["trn_Check"]);
                items.checkNumber = Convert.ToInt32(filas["trn_CheckNumber"]);
                items.change = (float)Convert.ToDouble(filas["trn_Change"]);
                items.patientFirstName = Convert.ToString(filas["trn_PatientFirstName"]);

            }

            return items;
        }

        public List<transaction> getTransactionsByRegisterID(string registerID, string fromDate)
        {
            var terms = new List<valuesWhere>();
            var list = new List<transaction>();

            terms.Add(createItem.ofTypeValuesWhere(false, "trn_RegisterID", registerID, (int)OPERATORBOOLEAN.AND, (int)OPERATOR.EQUALITY));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", date.convertToFormatDate(fromDate), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.GREATER_THAN_OR_EQUAL));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", date.convertToFormatDateFinal(fromDate), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.LESS_THAN_OR_EQUAL));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_Comment", "Initial Cash", (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.INEGUALITY));

            var data = createQuery.toSelectAll("transactions", terms);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow filas = data.Rows[i];
                var items = new transaction();

                items.trn_id = Convert.ToInt32(filas["trn_ID"]);
                items.amountCharged = Convert.ToInt32(filas["trn_AmountCharged"]);
                items.cash = Convert.ToInt32(filas["trn_Cash"]);                
                items.credit = Convert.ToInt32(filas["trn_Credit"]);
                items.check = Convert.ToInt32(filas["trn_Check"]);
                items.change = Convert.ToInt32(filas["trn_Change"]);

                list.Add(items);
            }

            return list;
        }

        public List<transaction> getAllTransactionsByRegisterID(string registerID, string fromDate, string toDate)
        {
            var terms = new List<valuesWhere>();
            var list = new List<transaction>();

            terms.Add(createItem.ofTypeValuesWhere(false, "trn_ID", registerID, (int)OPERATORBOOLEAN.AND, (int)OPERATOR.EQUALITY));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", date.convertToFormatDate(fromDate), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.GREATER_THAN_OR_EQUAL));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_DateRegistered", date.convertToFormatDateFinal(toDate), (int)OPERATORBOOLEAN.AND, (int)OPERATOR.LESS_THAN_OR_EQUAL));
            terms.Add(createItem.ofTypeValuesWhere(true, "trn_Comment", "Initial Cash", (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.INEGUALITY));

            var data = createQuery.toSelectAll("transactions", terms);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow filas = data.Rows[i];
                var items = new transaction();

                items.trn_id = Convert.ToInt32(filas["trn_ID"]);                
                items.amountCharged = Convert.ToInt32(filas["trn_AmountCharged"]);
                items.cash = Convert.ToInt32(filas["trn_Cash"]);
                items.credit = Convert.ToInt32(filas["trn_Credit"]);
                items.check = Convert.ToInt32(filas["trn_Check"]);
                items.change = Convert.ToInt32(filas["trn_Change"]);
                items.patientFirstName = filas["trn_PatientFirstName"].ToString();
                items.selfPay = (bool)filas["trn_SelfPay"];
                items.copayment = (bool)filas["trn_Copayment"];
                items.comment = filas["trn_Comment"].ToString();
                items.deductible = (bool)filas["trn_Deductible"];
                items.labs = (bool)filas["trn_Labs"];
                items.other = (bool)filas["trn_Other"];
                items.checkNumber = Convert.ToInt32(filas["trn_CheckNumber"]);
                list.Add(items);
            }

            return list;
        }

        public void updateTransaction(transaction trn)
        {
            var columns = new List<columnsValues>();
            var terms = new List<valuesWhere>();

            columns.Add(createItem.ofTypeColumnsValues("trn_PatientFirstName", trn.patientFirstName));
            columns.Add(createItem.ofTypeColumnsValues("trn_AmountCharged", trn.amountCharged));
            columns.Add(createItem.ofTypeColumnsValues("trn_Cash", trn.cash));
            columns.Add(createItem.ofTypeColumnsValues("trn_Credit", trn.credit));
            columns.Add(createItem.ofTypeColumnsValues("trn_Check", trn.check));
            columns.Add(createItem.ofTypeColumnsValues("trn_CheckNumber", trn.checkNumber));
            columns.Add(createItem.ofTypeColumnsValues("trn_Change", trn.change));
            columns.Add(createItem.ofTypeColumnsValues("trn_Copayment", trn.copayment));
            columns.Add(createItem.ofTypeColumnsValues("trn_SelfPay", trn.selfPay));
            columns.Add(createItem.ofTypeColumnsValues("trn_Deductible", trn.deductible));
            columns.Add(createItem.ofTypeColumnsValues("trn_Labs", trn.labs));
            columns.Add(createItem.ofTypeColumnsValues("trn_Other", trn.other));
            columns.Add(createItem.ofTypeColumnsValues("trn_OtherComments", trn.otherComments));
            columns.Add(createItem.ofTypeColumnsValues("trn_ModifiedBy_ID", userInformation.user.usr_ID));
            columns.Add(createItem.ofTypeColumnsValues("trn_Comment", trn.comment));
            columns.Add(createItem.ofTypeColumnsValues("trn_ModificationDate", date.getCurrentDate()));

            terms.Add(createItem.ofTypeValuesWhere(false, "trn_ID", cashInUpdate.transactionID.ToString(), (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.EQUALITY));

            createQuery.toUpdate("transactions", columns, terms);
        }
    }
}
