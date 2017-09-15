using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorCashWpf
{
    class logService
    {
        private sqlQueryService createQuery = new sqlQueryService();
        private createItemsForListService createItem = new createItemsForListService();
        private dateService date = new dateService();

        private List<string> getLogColumns()
        {
            var columns = new List<string>();            
            columns.Add("log_ID");
            columns.Add("log_Username");
            columns.Add("log_DateTime");
            columns.Add("log_Actions");

            return columns;
        }

        public logObj getLogs(string proccessedBy, string fromDate, string toDate)
        {            
            var obj = new logObj();
            var data = new DataTable();            

            if (proccessedBy != "" && fromDate != "" && toDate != "")
            {
                data = getproccessedByByIdAndRange(proccessedBy, fromDate, toDate);
            }

            else if (proccessedBy != "" && fromDate == "" && toDate == "")
            {
                data = getproccessedByById(proccessedBy);
            }

            else if (proccessedBy==""&&fromDate != "" && toDate != "")
            {
                data = getLogOnlyByRange(fromDate, toDate);
            }

            else if (proccessedBy == "" && fromDate != "" && toDate == "")
            {
                data = getLogOnlyByfromDate(fromDate);
            }
            else if (proccessedBy == "" && fromDate == "" && toDate != "")
            {
                data = getLogOnlyBytoDate(toDate);
            }

            obj.Datatable = data;
            obj.list = setLogDateInList(data);

            return obj;
        }

        private List<log> setLogDateInList(DataTable data)
        {
            var list = new List<log>();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow filas = data.Rows[i];
                var items = new log();
                items.log_ID = Convert.ToInt32(filas["log_ID"]);
                items.log_Username = Convert.ToString(filas["log_Username"]);
                items.log_DateTime = Convert.ToString(filas["log_DateTime"]);
                items.log_Actions = Convert.ToString(filas["log_Actions"]);

                list.Add(items);
            }

            return list;
        }

        private DataTable getproccessedByByIdAndRange(string proccessedBy, string fromDate, string toDate)
        {            
            var terms = new List<valuesWhere>();
            terms.Add(createItem.ofTypeValuesWhere(true, "log_Username", proccessedBy, (int)OPERATORBOOLEAN.AND, (int)OPERATOR.EQUALITY));
            terms.Add(createItem.ofTypeValuesWhere(true, "log_DateTime", fromDate, (int)OPERATORBOOLEAN.AND, (int)OPERATOR.GREATER_THAN_OR_EQUAL));
            terms.Add(createItem.ofTypeValuesWhere(true, "log_DateTime", toDate, (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.LESS_THAN_OR_EQUAL));

            return createQuery.toSelectAll("Log", terms);
        }

        private DataTable getproccessedByById(string proccessedBy)
        {
            var terms = new List<valuesWhere>();
            terms.Add(createItem.ofTypeValuesWhere(true, "log_Username", proccessedBy, (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.EQUALITY));            

            return createQuery.toSelectAll("Log", terms);
        }

        private DataTable getLogOnlyByRange(string fromDate,string toDate)
        {
            var terms = new List<valuesWhere>();
            terms.Add(createItem.ofTypeValuesWhere(true, "log_DateTime", fromDate, (int)OPERATORBOOLEAN.AND, (int)OPERATOR.GREATER_THAN_OR_EQUAL));
            terms.Add(createItem.ofTypeValuesWhere(true, "log_DateTime", toDate, (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.LESS_THAN_OR_EQUAL));
            return createQuery.toSelectAll("Log", terms);
        }

        private DataTable getLogOnlyByfromDate(string fromDate)
        {
            var terms = new List<valuesWhere>();
            terms.Add(createItem.ofTypeValuesWhere(true, "log_DateTime", fromDate, (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.GREATER_THAN_OR_EQUAL));            
            return createQuery.toSelectAll("Log", terms);
        }

        private DataTable getLogOnlyBytoDate( string toDate)
        {
            var terms = new List<valuesWhere>();            
            terms.Add(createItem.ofTypeValuesWhere(true, "log_DateTime", toDate, (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.LESS_THAN_OR_EQUAL));
            return createQuery.toSelectAll("Log", terms);
        }

        public void CreateLog(log log)
        {
            List<columnsValues> list = new List<columnsValues>();
            
            list.Add(createItem.ofTypeColumnsValues("log_Username", log.log_Username));
            list.Add(createItem.ofTypeColumnsValues("log_DateTime", log.log_DateTime));
            list.Add(createItem.ofTypeColumnsValues("log_Actions", log.log_Actions));

            createQuery.toInsert("Log", list);            
        }
    }
}
