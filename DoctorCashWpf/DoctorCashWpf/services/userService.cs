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

        public user authentication(string username, string password)
        {
            var user = new user();

            List<string> columnas = new List<string>();

            var listValuesTerms = new List<valuesWhere>();
            listValuesTerms.Add(createList.ofTypeValuesWhere(true, "usr_Username", username, (int)OPERATORBOOLEAN.AND, (int)OPERATOR.EQUALITY));
            listValuesTerms.Add(createList.ofTypeValuesWhere(true, "usr_Password", password, -1, (int)OPERATOR.EQUALITY));

            DataTable dataTable = createQuery.toSelectAll("users", listValuesTerms);


            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow filas = dataTable.Rows[i];

                user.usr_ID = Convert.ToInt32(filas["usr_ID"]);
                user.usr_Username = Convert.ToString(filas["usr_Username"]);
                user.usr_FirstName = Convert.ToString(filas["usr_FirstName"]);
                user.usr_LastName = Convert.ToString(filas["usr_LastName"]);
                user.usr_Password = Convert.ToString(filas["usr_Password"]);
                user.usr_SecurityQuestion = Convert.ToString(filas["usr_SecurityQuestion"]);
                user.usr_SecurityAnswer = Convert.ToString(filas["usr_SecurityAnswer"]);
                user.usr_Email = Convert.ToString(filas["usr_Email"]);
                user.usr_SecurityLevel = Convert.ToInt32(filas["usr_SecurityLevel"]);
                user.usr_ActiveAccount = Convert.ToBoolean(filas["usr_ActiveAccount"]);
                user.usr_PasswordReset = Convert.ToBoolean(filas["usr_PasswordReset"]);
            }

            if(dataTable.Rows.Count == 0)
            {
                user = null;
            }

            return user;
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