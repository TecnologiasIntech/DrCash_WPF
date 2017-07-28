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

        private sqlQueryService createQuery = new sqlQueryService();
        private createListService createList = new createListService();
        private dateService date = new dateService();

        public bool authentication(string username, string password)
        {
            bool auth = false;

            List<string> columnas = new List<string>();
            columnas.Add("usr_Username");

            var listValuesTerms = new List<valuesWhere>();
            listValuesTerms.Add(createList.ofTypeValuesWhere(true, "usr_Username", username, (int)OPERATORBOOLEAN.AND, (int)OPERATOR.EQUALITY));
            listValuesTerms.Add(createList.ofTypeValuesWhere(true, "usr_Password", password, -1, (int)OPERATOR.EQUALITY));

            DataTable dataTable = createQuery.toSelect(columnas, "users", listValuesTerms);

            if (dataTable.Rows.Count == 0)
            {
                auth = true;
            }
             
            return auth;
        }

        public void set(user user)
        {
            List<columnsValues> list = new List<columnsValues>();

            list.Add(createList.ofTypeColumnsValues("usr_Username", user.usr_Username));
            list.Add(createList.ofTypeColumnsValues("usr_FirstName", user.usr_FirstName));
            list.Add(createList.ofTypeColumnsValues("usr_LastName", user.usr_LastName));
            list.Add(createList.ofTypeColumnsValues("usr_Password", user.usr_Password));
            list.Add(createList.ofTypeColumnsValues("usr_SecurityQuestion", user.usr_SecurityQuestion));
            list.Add(createList.ofTypeColumnsValues("usr_SecurityAnswer", user.usr_SecurityAnswer));
            list.Add(createList.ofTypeColumnsValues("usr_Email", user.usr_Email));
            list.Add(createList.ofTypeColumnsValues("usr_SecurityLevel", user.usr_SecurityLevel));
            list.Add(createList.ofTypeColumnsValues("usr_ActiveAccount", user.usr_ActiveAccount));
            list.Add(createList.ofTypeColumnsValues("usr_PasswordReset", user.usr_PasswordReset));
            list.Add(createList.ofTypeColumnsValues("usr_ModifiedBy", user.usr_ModifiedBy));
            list.Add(createList.ofTypeColumnsValues("usr_ModificationDate", date.getCurrentDate()));
            list.Add(createList.ofTypeColumnsValues("usr_CreatedBy", user.usr_CreatedBy));
            list.Add(createList.ofTypeColumnsValues("usr_CreationDate", date.getCurrentDate()));

            createQuery.toInsert("users", list);
        }

        public void update(user user)
        {
            List<columnsValues> list = new List<columnsValues>();

            list.Add(createList.ofTypeColumnsValues("usr_Username", user.usr_Username));
            list.Add(createList.ofTypeColumnsValues("usr_FirstName", user.usr_FirstName));
            list.Add(createList.ofTypeColumnsValues("usr_LastName", user.usr_LastName));
            list.Add(createList.ofTypeColumnsValues("usr_Password", user.usr_Password));
            list.Add(createList.ofTypeColumnsValues("usr_SecurityQuestion", user.usr_SecurityQuestion));
            list.Add(createList.ofTypeColumnsValues("usr_SecurityAnswer", user.usr_SecurityAnswer));
            list.Add(createList.ofTypeColumnsValues("usr_Email", user.usr_Email));
            list.Add(createList.ofTypeColumnsValues("usr_SecurityLevel", user.usr_SecurityLevel));
            list.Add(createList.ofTypeColumnsValues("usr_ActiveAccount", user.usr_ActiveAccount)); 
            list.Add(createList.ofTypeColumnsValues("usr_PasswordReset", user.usr_PasswordReset));
            list.Add(createList.ofTypeColumnsValues("usr_ModifiedBy", user.usr_ModifiedBy));
            list.Add(createList.ofTypeColumnsValues("usr_ModificationDate", date.getCurrentDate()));

            List<valuesWhere> listTerms = new List<valuesWhere>();
            listTerms.Add(createList.ofTypeValuesWhere(false, "usr_ID", user.usr_ID.ToString(), -1, (int)OPERATOR.EQUALITY));

            createQuery.toUpdate("users", list, listTerms);
        }

        public void deleteByID(int userID)
        {
            List<valuesWhere> list = new List<valuesWhere>();
            list.Add(createList.ofTypeValuesWhere(false, "usr_ID", userID.ToString(), -1, (int)OPERATOR.EQUALITY));

            createQuery.forDelete("users", list);
        }

    }
}