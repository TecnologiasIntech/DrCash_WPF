using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorCashWpf.services
{
    class reportService
    {
        private sqlQueryService createQuery = new sqlQueryService();
        private createItemsForListService createItem = new createItemsForListService();
        private formatService createFormat = new formatService();

        /*public List<closeTransaction> getCloseTransactionByRange(string fromDate, string toDate)
        {
            var termsList = new List<valuesWhere>();
            termsList.Add(createItem.ofTypeValuesWhere(true, "clt_Datetime"

            var data = createQuery.toSelectAll("ClosedTransactios", termsList);
        }*/

    }
}
