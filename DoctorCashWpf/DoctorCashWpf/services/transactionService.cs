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
        public List<transaction> getCurrentTransactions(int currentUserID)
        {
            DataTable data = new DataTable();
            var list = new List<transaction>();

            var queryService = new sqlQueryService();

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
            var listService = new createListService();
            var formatService = new formatService();
            
            var currentDay = DateTime.Today;

            var day = currentDay.Day.ToString();
            var month = currentDay.Month.ToString();
            var year = currentDay.Year.ToString();
            
            var dateInitial = formatService.createFormatDateTimeToQuery(year, month, day, "T00:00:00.000");
            var dateEnd = formatService.createFormatDateTimeToQuery(year, month, day, "T23:59:59.999");

            listTerms.Add(listService.createListValuesWhere(false, "trn_User_ID", currentUserID.ToString(), "AND", (int)OPERATOR.EQUALITY));
            listTerms.Add(listService.createListValuesWhere(true, "trn_DateRegistered", dateInitial, "AND", (int)OPERATOR.GREATER_THAN_OR_EQUAL));
            listTerms.Add(listService.createListValuesWhere(true, "trn_DateRegistered", dateEnd, "", (int)OPERATOR.LESS_THAN_OR_EQUAL));

            data = queryService.selectData(columns, "transactions", listTerms);

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

        public void registerTransaction(transaction transactionArray)
        {
            sqlQueryService queryService = new sqlQueryService();
            var listService = new createListService();
            
            List<columnsValues> valuesArray = new List<columnsValues>();
            valuesArray.Add(listService.createListOfColumnsValues("trn_User_ID", transactionArray.userId));
            //valuesArray.Add(listService.createListOfColumnsValues("trn_DateRegistered", transactionArray.dateRegistered));
            valuesArray.Add(listService.createListOfColumnsValues("trn_Comment", transactionArray.comment));
            valuesArray.Add(listService.createListOfColumnsValues("trn_Type", transactionArray.type));
            valuesArray.Add(listService.createListOfColumnsValues("trn_AmountCharged", transactionArray.amountCharged));
            valuesArray.Add(listService.createListOfColumnsValues("trn_Cash", transactionArray.cash));
            valuesArray.Add(listService.createListOfColumnsValues("trn_Credit", transactionArray.credit));
            valuesArray.Add(listService.createListOfColumnsValues("trn_Check", transactionArray.check));
            valuesArray.Add(listService.createListOfColumnsValues("trn_CheckNumber", transactionArray.checkNumber));
            valuesArray.Add(listService.createListOfColumnsValues("trn_Change", transactionArray.change));
            valuesArray.Add(listService.createListOfColumnsValues("trn_PatientFirstName", transactionArray.patientFirstName));
            valuesArray.Add(listService.createListOfColumnsValues("trn_Copayment", transactionArray.copayment));
            valuesArray.Add(listService.createListOfColumnsValues("trn_SelfPay", transactionArray.selfPay));
            valuesArray.Add(listService.createListOfColumnsValues("trn_Deductible", transactionArray.deductible));
            valuesArray.Add(listService.createListOfColumnsValues("trn_Labs", transactionArray.labs));
            valuesArray.Add(listService.createListOfColumnsValues("trn_Other", transactionArray.other));
            //valuesArray.Add(listService.createListOfColumnsValues("trn_OtherComments", transactionArray.otherComments));
            valuesArray.Add(listService.createListOfColumnsValues("trn_Closed", transactionArray.closed));
            valuesArray.Add(listService.createListOfColumnsValues("trn_RegisterID", transactionArray.registerId));
            valuesArray.Add(listService.createListOfColumnsValues("trn_ModifiedBy_ID", transactionArray.modifiedById));
            //valuesArray.Add(listService.createListOfColumnsValues("trn_ModificationDate", transactionArray.modificationDate));

            queryService.insertData("transactions", valuesArray);
        }

    }
}
