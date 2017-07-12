using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DoctorCashWpf
{
    class transactionService : transaction
    {

        public DataTable getTransactions()
        {
            DataTable data = new DataTable();




            return data;
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
