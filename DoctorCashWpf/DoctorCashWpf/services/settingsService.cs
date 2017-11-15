using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorCashWpf
{
    class settingsService
    {

        private sqlQueryService createQuery = new sqlQueryService();
        private createItemsForListService createItem = new createItemsForListService();

        public void createRegister(registers register)
        {
            List<columnsValues> columnsList = new List<columnsValues>();

            columnsList.Add(createItem.ofTypeColumnsValues("reg_RegisterNumber", register.reg_RegisterNumber));
            columnsList.Add(createItem.ofTypeColumnsValues("reg_ComputerName", register.reg_ComputerName));
            columnsList.Add(createItem.ofTypeColumnsValues("reg_ActiveRegister", register.reg_ActiveRegister));
            columnsList.Add(createItem.ofTypeColumnsValues("reg_ModifiedBy", register.reg_ModifiedBy));
            columnsList.Add(createItem.ofTypeColumnsValues("reg_ModificationDate", register.reg_ModificationDate));
            columnsList.Add(createItem.ofTypeColumnsValues("reg_CreatedBy", register.reg_CreatedBy));
            columnsList.Add(createItem.ofTypeColumnsValues("reg_CreationDate", register.reg_CreationDate));

            createQuery.toInsert("Registers", columnsList);
        }

        public void updateRegister(registers register)
        {
            List<columnsValues> columnsList = new List<columnsValues>();

            columnsList.Add(createItem.ofTypeColumnsValues("reg_ActiveRegister", register.reg_ActiveRegister));
            columnsList.Add(createItem.ofTypeColumnsValues("reg_ComputerName", register.reg_ComputerName));

            List<valuesWhere> termsList = new List<valuesWhere>();
            termsList.Add(createItem.ofTypeValuesWhere(false, "reg_RegisterNumber", register.reg_RegisterNumber.ToString(), (int)OPERATORBOOLEAN.NONE, (int)OPERATOR.EQUALITY));

            createQuery.toUpdate("Registers", columnsList, termsList);
        }

        public List<registers> getRegisters()
        {
            List<registers> registersList = new List<registers>();
            List<valuesWhere> termsList = new List<valuesWhere>();

            var data = createQuery.toSelectAll("Registers", termsList);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow filas = data.Rows[i];
                var items = new registers();

                items.reg_ActiveRegister = Convert.ToBoolean(filas["reg_ActiveRegister"]);
                items.reg_ComputerName = Convert.ToString(filas["reg_ComputerName"]);
                items.reg_RegisterNumber = Convert.ToInt32(filas["reg_RegisterNumber"]);

                registersList.Add(items);
            }
            return registersList;
        }

        public void updateSettingGeneral(settings settings)
        {
            List<columnsValues> columnsList = new List<columnsValues>();
            List<valuesWhere> termsList = new List<valuesWhere>();

            columnsList.Add(createItem.ofTypeColumnsValues("LockAutomatically", settings.LockAutomatically));
            columnsList.Add(createItem.ofTypeColumnsValues("TimeOutLock", settings.TimeOutLock));
            columnsList.Add(createItem.ofTypeColumnsValues("RefreshSummary", settings.RefreshSummary));
            columnsList.Add(createItem.ofTypeColumnsValues("TimeRefreshSummary", settings.TimeRefreshSummary));
            columnsList.Add(createItem.ofTypeColumnsValues("LeaveMoneyInRegister", settings.LeaveMoneyInRegister));
            columnsList.Add(createItem.ofTypeColumnsValues("DefaultPassword", settings.DefaultPassword));
            columnsList.Add(createItem.ofTypeColumnsValues("DefaultPasswordValue", settings.DefaultPasswordValue));
            columnsList.Add(createItem.ofTypeColumnsValues("Logo", settings.Logo));

            createQuery.toUpdate("Settings", columnsList, termsList);
        }

        public void setSettings(settings settings)
        {
            List<columnsValues> columnsList = new List<columnsValues>();

            columnsList.Add(createItem.ofTypeColumnsValues("LockAutomatically", settings.LockAutomatically));
            columnsList.Add(createItem.ofTypeColumnsValues("TimeOutLock", settings.TimeOutLock));
            columnsList.Add(createItem.ofTypeColumnsValues("RefreshSummary", settings.RefreshSummary));
            columnsList.Add(createItem.ofTypeColumnsValues("TimeRefreshSummary", settings.TimeRefreshSummary));
            columnsList.Add(createItem.ofTypeColumnsValues("LeaveMoneyInRegister", settings.LeaveMoneyInRegister));
            columnsList.Add(createItem.ofTypeColumnsValues("DefaultPassword", settings.DefaultPassword));
            columnsList.Add(createItem.ofTypeColumnsValues("DefaultPasswordValue", settings.DefaultPasswordValue));
            columnsList.Add(createItem.ofTypeColumnsValues("Logo", settings.Logo));

            createQuery.toInsert("Settings", columnsList);
        }

        public settings getSettings()
        {
            settings settings = new settings();
            List<string> columnsList = new List<string>();
            List<valuesWhere> termsList = new List<valuesWhere>();

            DataTable data = createQuery.toSelect(columnsList, "Settings", termsList);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow filas = data.Rows[i];
                var items = new registers();

                settings.LockAutomatically = Convert.ToBoolean(filas["LockAutomatically"]);
                settings.TimeOutLock = Convert.ToInt32(filas["TimeOutLock"]);
                settings.RefreshSummary = Convert.ToBoolean(filas["RefreshSummary"]);
                settings.TimeRefreshSummary = Convert.ToInt32(filas["TimeRefreshSummary"]);
                settings.LeaveMoneyInRegister = Convert.ToBoolean(filas["LeaveMoneyInRegister"]);
                settings.DefaultPassword = Convert.ToBoolean(filas["DefaultPassword"]);
                settings.DefaultPasswordValue = Convert.ToString(filas["DefaultPasswordValue"]);
                settings.Logo = Convert.ToString(filas["Logo"]);
            }
            return settings;
        }

        public string getComputerName()
        {
            return Environment.MachineName;
        }

    }
}
