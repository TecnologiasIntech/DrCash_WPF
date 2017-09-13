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

        public void setLog(log log)
        {
            var list = new List<columnsValues>();

            list.Add(createItem.ofTypeColumnsValues("log_Username", log.log_Username));
            list.Add(createItem.ofTypeColumnsValues("log_DateTime", date.getCurrentDate()));
            list.Add(createItem.ofTypeColumnsValues("log_Actions", log.log_Actions));

            createQuery.toInsert("Log", list);
        }

        public logObj getLogs(string proccessedBy, string fromDate, string toDate)
        {
            var terms = new List<valuesWhere>();
            var obj = new logObj();

            if(proccessedBy != "" && fromDate != "" && toDate != "")
            {
                terms.Add(createItem.ofTypeValuesWhere(true, "log_Username", proccessedBy, (int)OPERATORBOOLEAN.AND, (int)OPERATOR.EQUALITY));
            }

            if (proccessedBy != "" && fromDate == "" && toDate == "")
            {
                terms.Add(createItem.ofTypeValuesWhere(true, "log_Username", proccessedBy, (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.EQUALITY));
            }

            if (fromDate != "" && toDate != "")
            {
                terms.Add(createItem.ofTypeValuesWhere(true, "log_DateTime", fromDate, (int)OPERATORBOOLEAN.AND, (int)OPERATOR.GREATER_THAN_OR_EQUAL));
                terms.Add(createItem.ofTypeValuesWhere(true, "log_DateTime", toDate, (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.LESS_THAN_OR_EQUAL));
            }

            if (fromDate != "" && toDate == "")
            {
                terms.Add(createItem.ofTypeValuesWhere(true, "log_DateTime", fromDate, (int)OPERATORBOOLEAN.NINGUNO, (int)OPERATOR.GREATER_THAN_OR_EQUAL));
            }

            obj.Datatable = createQuery.toSelectAll("Log", terms);

            for (int i = 0; i < obj.Datatable.Rows.Count; i++)
            {
                DataRow filas = obj.Datatable.Rows[i];
                var items = new log();

                items.log_Username = Convert.ToString(filas["log_Username"]);
                items.log_DateTime = Convert.ToString(filas["log_DateTime"]);
                items.log_Actions = Convert.ToString(filas["log_Actions"]);

                obj.list.Add(items);
            }

            return obj;
        }
    }
}
