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
        private createItemsForListService createItem = new createItemsForListService();
        private dateService date = new dateService();

        public user authentication(string username, string password)
        {
            var user = new user();

            List<string> columnas = new List<string>();

            var listValuesTerms = new List<valuesWhere>();
            listValuesTerms.Add(createItem.ofTypeValuesWhere(true, "usr_Username", username, (int)OPERATORBOOLEAN.AND, (int)OPERATOR.EQUALITY));
            listValuesTerms.Add(createItem.ofTypeValuesWhere(true, "usr_Password", password, -1, (int)OPERATOR.EQUALITY));

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

            list.Add(createItem.ofTypeColumnsValues("usr_Username", user.usr_Username));
            list.Add(createItem.ofTypeColumnsValues("usr_FirstName", user.usr_FirstName));
            list.Add(createItem.ofTypeColumnsValues("usr_LastName", user.usr_LastName));
            list.Add(createItem.ofTypeColumnsValues("usr_Password", user.usr_Password));
            list.Add(createItem.ofTypeColumnsValues("usr_SecurityQuestion", user.usr_SecurityQuestion));
            list.Add(createItem.ofTypeColumnsValues("usr_SecurityAnswer", user.usr_SecurityAnswer));
            list.Add(createItem.ofTypeColumnsValues("usr_Email", user.usr_Email));
            list.Add(createItem.ofTypeColumnsValues("usr_SecurityLevel", user.usr_SecurityLevel));
            list.Add(createItem.ofTypeColumnsValues("usr_ActiveAccount", user.usr_ActiveAccount));
            list.Add(createItem.ofTypeColumnsValues("usr_PasswordReset", user.usr_PasswordReset));
            list.Add(createItem.ofTypeColumnsValues("usr_ModifiedBy", user.usr_ModifiedBy));
            list.Add(createItem.ofTypeColumnsValues("usr_ModificationDate", date.getCurrentDate()));
            list.Add(createItem.ofTypeColumnsValues("usr_CreatedBy", user.usr_CreatedBy));
            list.Add(createItem.ofTypeColumnsValues("usr_CreationDate", date.getCurrentDate()));

            createQuery.toInsert("users", list);
        }

        public void updateBasicInformation(user user)
        {
            List<columnsValues> list = new List<columnsValues>();

            list.Add(createItem.ofTypeColumnsValues("usr_FirstName", user.usr_FirstName));
            list.Add(createItem.ofTypeColumnsValues("usr_LastName", user.usr_LastName));
            list.Add(createItem.ofTypeColumnsValues("usr_SecurityQuestion", user.usr_SecurityQuestion));
            list.Add(createItem.ofTypeColumnsValues("usr_Email", user.usr_Email));
            list.Add(createItem.ofTypeColumnsValues("usr_ModifiedBy", user.usr_ModifiedBy));
            list.Add(createItem.ofTypeColumnsValues("usr_ModificationDate", date.getCurrentDate()));

            if(user.usr_Password != null)
            {
                list.Add(createItem.ofTypeColumnsValues("usr_Password", user.usr_Password));
            }

            List<valuesWhere> listTerms = new List<valuesWhere>();
            listTerms.Add(createItem.ofTypeValuesWhere(false, "usr_ID", userInformation.user.usr_ID.ToString(), -1, (int)OPERATOR.EQUALITY));

            createQuery.toUpdate("users", list, listTerms);
        }

        public void updateUser(user user)
        {
            var valuesList = new List<columnsValues>();

            valuesList.Add(createItem.ofTypeColumnsValues("usr_FirstName", user.usr_FirstName));
            valuesList.Add(createItem.ofTypeColumnsValues("usr_LastName", user.usr_LastName));
            valuesList.Add(createItem.ofTypeColumnsValues("usr_Email", user.usr_Email));
            valuesList.Add(createItem.ofTypeColumnsValues("usr_SecurityLevel", user.usr_SecurityLevel));
            valuesList.Add(createItem.ofTypeColumnsValues("usr_ActiveAccount", user.usr_ActiveAccount));
            valuesList.Add(createItem.ofTypeColumnsValues("usr_PasswordReset", user.usr_PasswordReset));

            var termsList = new List<valuesWhere>();

            termsList.Add(createItem.ofTypeValuesWhere(false, "usr_ID", user.usr_ID.ToString(), -1, (int)OPERATOR.EQUALITY));

            createQuery.toUpdate("users", valuesList, termsList);
        }

        public void deleteByID(int userID)
        {
            List<valuesWhere> list = new List<valuesWhere>();
            list.Add(createItem.ofTypeValuesWhere(false, "usr_ID", userID.ToString(), -1, (int)OPERATOR.EQUALITY));

            createQuery.forDelete("users", list);
        }

        public DataTable getUsers()
        {
            var list = new List<valuesWhere>();
            var columnsList = new List<string>();

            columnsList.Add("usr_ID");
            columnsList.Add("usr_Username");
            columnsList.Add("usr_FirstName");
            columnsList.Add("usr_LastName");
            columnsList.Add("usr_Email");
            columnsList.Add("usr_SecurityLevel");
            columnsList.Add("usr_ActiveAccount");
            columnsList.Add("usr_PasswordReset");

            var data = createQuery.toSelect(columnsList, "users", list);

            return data;
        }

    }
}