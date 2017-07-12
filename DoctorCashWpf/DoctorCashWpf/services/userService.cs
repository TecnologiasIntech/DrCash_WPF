using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DoctorCashWpf
{
    class userService
    {

        public bool authentication(string username, string password)
        {
            bool auth = false;

            sqlQueryService querys = new sqlQueryService();
            var listService = new createListService();

            List<string> columnas = new List<string>();
            columnas.Add("usr_Username");

            var listValuesTerms = new List<valuesWhere>();
            listValuesTerms.Add(listService.createListValuesWhere(true, "usr_Username", username, "AND"));
            listValuesTerms.Add(listService.createListValuesWhere(true, "usr_Password", password, ""));

            DataTable dataTable = querys.selectData(columnas, "users", listValuesTerms);


            if (dataTable.Rows.Count == 0)
            {
                auth = true;
            }

            return auth;
        }

    }
}
