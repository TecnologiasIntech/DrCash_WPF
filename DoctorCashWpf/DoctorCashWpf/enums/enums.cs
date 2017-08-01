using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorCashWpf
{
    enum DATATYPE
    {
        INT,
        STRING,
        BOOL,
        FLOAT,
        DATETIME
    }

    enum OPERATOR
    {
        EQUALITY,
        INEGUALITY,
        GREATER_THAN,
        GREATER_THAN_OR_EQUAL,
        LESS_THAN,
        LESS_THAN_OR_EQUAL, 
        SUM,
        REMOVE
    }

    enum OPERATORBOOLEAN
    {
        AND,
        OR
    }

    enum TRANSACTIONTYPE
    {
        IN,
        OUT
    }
}
