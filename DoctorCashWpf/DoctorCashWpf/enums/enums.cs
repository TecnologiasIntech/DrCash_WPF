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
        OR,
        NINGUNO
    }

    enum TRANSACTIONTYPE
    {
        IN,
        OUT,
        INITIAL,
        REFOUND
    }

    enum CASHINTYPE
    {
        COPAYMENT,
        SELFPAY,
        DEDUCTIBLE,
        LABS,
        OTHER
    }

    enum SECURIRYLEVEL
    {
        USER,
        SUPERVISOR,
        ADMINISTRATOR
    }
}
