using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DoctorCashWpf
{
    class sqlQueryService : constants
    {

        private SqlConnection openConection()
        {
            SqlConnection conexion = new SqlConnection(conectionServer);
            conexion.Open();
            return conexion;
        }

        private void closeConection(SqlConnection conection)
        {
            conection.Close();
        }

        private SqlCommand createCommand(string query, SqlConnection conection)
        {
            SqlCommand command = new SqlCommand(query, conection);
            return command;
        }

        private string getOperator(int Operator)
        {
            string operatorType = "";

            switch (Operator)
            {
                case (int)OPERATOR.EQUALITY:
                    operatorType =  "=";
                    break;

                case (int)OPERATOR.INEGUALITY:
                    operatorType = "!=";
                    break; 

                case (int)OPERATOR.LESS_THAN:
                    operatorType = "<";
                    break;

                case (int)OPERATOR.LESS_THAN_OR_EQUAL:
                    operatorType = "<=";
                    break;

                case (int)OPERATOR.GREATER_THAN:
                    operatorType = ">";
                    break;

                case (int)OPERATOR.GREATER_THAN_OR_EQUAL:
                    operatorType = ">=";
                    break;
            }

            return operatorType;
        }

        private string getOperatorBoolean(int operatorBool)
        {
            string operatorBoolType = "";

            switch (operatorBool)
            {
                case (int)OPERATORBOOLEAN.AND:
                    operatorBoolType = "AND";
                    break;

                case (int)OPERATORBOOLEAN.OR:
                    operatorBoolType = "OR";
                    break;
            }

            return operatorBoolType;
        }

        public void toInsert(string table, List<columnsValues> valuesArray)
        { 
            string columns = "";
            string values = "";

            //Obtenemos todos las columnas en las que queremos insertar datos
            for (int i = 0; i < valuesArray.Count(); i++)
            {
                columns += valuesArray[i].column;
                values += "@" + valuesArray[i].column;

                if(i < valuesArray.Count()-1)
                {
                    columns += ", ";
                    values += ", ";
                }
            }

            //Creamos el query ya con las columnas y valores
            string query = "INSERT INTO " + table + " (" + columns + ") VALUES (" + values + ")";

            //Abrimos una conexion
            var conection = openConection();

            //Creamos el comando ya con el query y la conexion creada
            var insert = createCommand(query, conection);

            //Añadimos todos los valores de las columnas
            for (int i = 0; i < valuesArray.Count(); i++)
            {

                switch (valuesArray[i].typeValue)
                {
                  
                    case (int)DATATYPE.INT:
                        insert.Parameters.AddWithValue("@" + valuesArray[i].column, valuesArray[i].valueInt);
                        break;
                    case (int)DATATYPE.STRING:
                        insert.Parameters.AddWithValue("@" + valuesArray[i].column, valuesArray[i].valueString);
                        break;
                    case (int)DATATYPE.BOOL:
                        insert.Parameters.AddWithValue("@" + valuesArray[i].column, valuesArray[i].valueBool);
                        break;
                    case (int)DATATYPE.FLOAT:
                        insert.Parameters.AddWithValue("@" + valuesArray[i].column, valuesArray[i].valueFloat);
                        break;

                }
            }

            //Insertamos los datos
            insert.ExecuteNonQuery();

            //Cerramos la conexion
            closeConection(conection);

        }

        public DataTable toSelectAll(string table, List<valuesWhere> valuesOfTerms)
        {
            string terms = "";

            //En el caso de tener condiciones las guardamos para despues agregarla a la consulta
            for (int i = 0; i < valuesOfTerms.Count(); i++)
            {
                if (i == 0)
                {
                    terms += " WHERE ";
                }
                if (valuesOfTerms[i].isTypeString) 
                {
                    terms += valuesOfTerms[i].column + " " + getOperator(valuesOfTerms[i].Operator) + " '" + valuesOfTerms[i].value + "' " + getOperatorBoolean(valuesOfTerms[i].operationBool) + " ";
                }
                else
                {
                    terms += valuesOfTerms[i].column + " " + getOperator(valuesOfTerms[i].Operator) + " " + valuesOfTerms[i].value + " " + getOperatorBoolean(valuesOfTerms[i].operationBool) + " ";
                }
            }

            //Creamos la consulta
            string query = "SELECT * FROM " + table + terms;

            //Abrimos una conexion
            var conection = openConection();

            //Creamos el adaptador para guardar los datos
            SqlDataAdapter select = new SqlDataAdapter(query, conectionServer);

            //Pasamos la informacion en un tabla 
            DataSet data = new DataSet();
            DataTable selectInformation = new DataTable();
            select.Fill(data);

            selectInformation = data.Tables[0];

            //Cerramos la conexion
            closeConection(conection);

            return selectInformation;
        }

        //SELECT column, column FROM table WHERE column = value;
        public DataTable toSelect(List<String> columnsArray, string table,  List<valuesWhere> valuesOfTerms)
        {
            string columns = "";
            string terms = "";

            //Añadimos todas las columnas que queremos consultar
            for (int i = 0; i < columnsArray.Count(); i++)
            {
                columns += columnsArray[i];

                if (i < columnsArray.Count() - 1)
                {
                    columns += ", ";
                }
                else
                {
                    columns += "  ";
                }
            }

            //En el caso de tener condiciones las guardamos para despues agregarla a la consulta
            for (int i = 0; i < valuesOfTerms.Count(); i++)
            {
                if (i == 0)     
                {
                    terms += " WHERE ";
                }
                if(valuesOfTerms[i].isTypeString)
                {
                    terms += valuesOfTerms[i].column + " " + getOperator(valuesOfTerms[i].Operator) + " '" + valuesOfTerms[i].value + "' " + getOperatorBoolean(valuesOfTerms[i].operationBool) + " ";
                }
                else
                {
                    terms += valuesOfTerms[i].column + " " + getOperator(valuesOfTerms[i].Operator) + " " + valuesOfTerms[i].value + " " + getOperatorBoolean(valuesOfTerms[i].operationBool) + " ";
                }
            }

            //Creamos la consulta
            string query = "SELECT " + columns + "FROM " + table + terms;

            //Abrimos una conexion
            var conection = openConection();

            //Creamos el adaptador para guardar los datos
            SqlDataAdapter select = new SqlDataAdapter(query, conectionServer);

            //Pasamos la informacion en un tabla 
            DataSet data = new DataSet();
            DataTable selectInformation = new DataTable();
            select.Fill(data);

            selectInformation = data.Tables[0];

            //Cerramos la conexion
            closeConection(conection);

            return selectInformation;
        }

        //SELECT COUNT(column) FROM table WHERE column = value;
        public int toCount(string column, string table, List<valuesWhere> valuesTermsArray)
        {
            int count = 0;
            string terms = "";

            //En el caso de tener condiciones las guardamos para despues agregarla a la consulta
            for (int i = 0; i < valuesTermsArray.Count(); i++)
            {
                if (i == 0)
                {
                    terms += " WHERE ";
                }
                if (valuesTermsArray[i].isTypeString)
                {
                    terms += valuesTermsArray[i].column + " " + getOperator(valuesTermsArray[i].Operator) + " '" + valuesTermsArray[i].value + "' " + getOperatorBoolean(valuesTermsArray[i].operationBool) + " ";
                }
                else
                {
                    terms += valuesTermsArray[i].column + " " + getOperator(valuesTermsArray[i].Operator) + " " + valuesTermsArray[i].value + " " + getOperatorBoolean(valuesTermsArray[i].operationBool) + " ";
                }
            }

            //Creamos la consulta
            string query = "SELECT COUNT(" + column + ")" + "FROM " + table + terms;

            //Abrimos una conexion
            var conection = openConection();

            //Creamos el comando ya con el query
            var selectCount = createCommand(query, conection);

            //Ejecutamos la consulta
            count = selectCount.ExecuteNonQuery();

            return count;
        }

        //UPDATE table SET column= value, column= value  WHERE column = value 
        public void toUpdate(string table, List<columnsValues> columnsValuesArray, List<valuesWhere> valuesTermsArray)
        {
            string columns = "";
            string condictions = "";

            //Añadimos las columnas que queremos consultar
            for (int i = 0; i < columnsValuesArray.Count(); i++)
            {
                columns +=  columnsValuesArray[i].column + " = " + "@" + columnsValuesArray[i].column;

                if (i < columnsValuesArray.Count() - 1)
                {
                    columns += ", ";
                }
                else
                {
                    columns += "  ";
                }
            }

            //Guardamos las condiciones que tendrá la consulta
            for (int i = 0; i < valuesTermsArray.Count(); i++)
            {
                if (i == 0)
                {
                    condictions += " WHERE ";
                }
                if (valuesTermsArray[i].isTypeString)
                {
                    condictions += valuesTermsArray[i].column + " " + getOperator(valuesTermsArray[i].Operator) + " '" + valuesTermsArray[i].value + "' " + getOperatorBoolean(valuesTermsArray[i].operationBool) + " ";
                }
                else
                {
                    condictions += valuesTermsArray[i].column + " " + getOperator(valuesTermsArray[i].Operator) + " " + valuesTermsArray[i].value + " " + getOperatorBoolean(valuesTermsArray[i].operationBool) + " ";
                }
            }

            var conection = openConection();

            //Creamos el query para actualizar
            string query = "UPDATE " + table + " SET " + columns + " " + condictions;

            var update = createCommand(query, conection);

            //Añadimos todos los valores de las columnas
            for (int i = 0; i < columnsValuesArray.Count(); i++)
            {
                switch (columnsValuesArray[i].typeValue)
                {

                    case (int)DATATYPE.INT:
                        update.Parameters.AddWithValue("@" + columnsValuesArray[i].column, columnsValuesArray[i].valueInt);
                        break;
                    case (int)DATATYPE.STRING:
                        update.Parameters.AddWithValue("@" + columnsValuesArray[i].column, columnsValuesArray[i].valueString);
                        break;
                    case (int)DATATYPE.BOOL:
                        update.Parameters.AddWithValue("@" + columnsValuesArray[i].column, columnsValuesArray[i].valueBool);
                        break;
                    case (int)DATATYPE.FLOAT:
                        update.Parameters.AddWithValue("@" + columnsValuesArray[i].column, columnsValuesArray[i].valueFloat);
                        break;
                }
            }

            //Ejecutamos el comando
            update.ExecuteNonQuery();

            closeConection(conection);
        }

        //DELETE table WHERE column = value
        public void forDelete(string table, List<valuesWhere> valuesTermsArray)
        {
            string terms = "";

            //Añadimos las condiciones que tendrá nuestra consulta delete
            for (int i = 0; i < valuesTermsArray.Count(); i++)
            {
                if (i == 0)
                {
                    terms += " WHERE ";
                }
                if (valuesTermsArray[i].isTypeString)
                {
                    terms += valuesTermsArray[i].column + " " + getOperator(valuesTermsArray[i].Operator) + " '" + valuesTermsArray[i].value + "' " + getOperatorBoolean(valuesTermsArray[i].operationBool) + " ";
                }
                else
                {
                    terms += valuesTermsArray[i].column + " " + getOperator(valuesTermsArray[i].Operator) + " " + valuesTermsArray[i].value + " " + getOperatorBoolean(valuesTermsArray[i].operationBool) + " ";
                }
            }

            var conection = openConection();

            //Cremos el query para borrar
            string query = "DELETE FROM " + table + terms;

            var delete = createCommand(query, conection);

            //Ejecutamos el comando
            delete.ExecuteNonQuery();

            closeConection(conection);
        }

    }
}
