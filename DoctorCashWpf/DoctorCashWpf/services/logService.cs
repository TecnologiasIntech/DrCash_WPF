using System;
using System.Collections.Generic;
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
    }
}
