using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorCashWpf
{
    class createListService
    {
        
        public valuesWhere ofTypeValuesWhere(bool isTypeString, string column, string value, string operationBool, int Operator)
        {
            var items = new valuesWhere();
            items.isTypeString = isTypeString;
            items.column = column;
            items.value = value;
            items.operationBool = operationBool;
            items.Operator = Operator;

            return items;
        }

        public columnsValues ofTypeColumnsValues(string column, int value)
        {
            var items = new columnsValues();
            items.column = column;
            items.valueInt = value;
            items.typeValue = (int)DATATYPE.INT;

            return items;
        }

        public columnsValues ofTypeColumnsValues(string column, string value)
        {
            var items = new columnsValues();
            items.column = column;
            items.valueString = value;
            items.typeValue = (int)DATATYPE.STRING;

            return items;
        }

        public columnsValues ofTypeColumnsValues(string column, float value)
        {
            var items = new columnsValues();
            items.column = column;
            items.valueFloat = value;
            items.typeValue = (int)DATATYPE.FLOAT;

            return items;
        }

        public columnsValues ofTypeColumnsValues(string column, bool value)
        {
            var items = new columnsValues();
            items.column = column;
            items.valueBool = value;
            items.typeValue = (int)DATATYPE.BOOL;

            return items;
        }
    }
}
