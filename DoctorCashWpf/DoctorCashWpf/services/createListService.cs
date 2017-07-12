using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorCashWpf
{
    class createListService
    {
        public valuesWhere createListValuesWhere(bool isTypeString, string column, string value, string operationBool)
        {
            var items = new valuesWhere();
            items.isTypeString = isTypeString;
            items.column = column;
            items.value = value;
            items.operationBool = operationBool;

            return items;
        }

        public columnsValues createListOfColumnsValues(string column, int value)
        {
            var items = new columnsValues();
            items.column = column;
            items.valueInt = value;
            items.typeValue = (int)enums.INT;

            return items;
        }

        public columnsValues createListOfColumnsValues(string column, string value)
        {
            var items = new columnsValues();
            items.column = column;
            items.valueString = value;
            items.typeValue = (int)enums.STRING;

            return items;
        }

        public columnsValues createListOfColumnsValues(string column, float value)
        {
            var items = new columnsValues();
            items.column = column;
            items.valueFloat = value;
            items.typeValue = (int)enums.FLOAT;

            return items;
        }

        public columnsValues createListOfColumnsValues(string column, bool value)
        {
            var items = new columnsValues();
            items.column = column;
            items.valueBool = value;
            items.typeValue = (int)enums.BOOL;

            return items;
        }
    }
}
