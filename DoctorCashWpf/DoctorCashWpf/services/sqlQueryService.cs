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

        public void insertData(string table, List<String> columnsArray, List<String> valuesArray)
        { 
            string columns = "";
            string values = "";

            //Obtenemos todos las columnas en las que queremos insertar datos
            for (int i = 0; i < columnsArray.Count(); i++)
            {
                columns += columnsArray[i] + ", ";
                values += "@" + columnsArray[i] + ", ";
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
                insert.Parameters.AddWithValue("@" + columnsArray[i], valuesArray[i]);
            }

            //Insertamos los datos
            insert.ExecuteNonQuery();

            //Cerramos la conexion
            closeConection(conection);

        }

        //SELECT column, column FROM table WHERE column = value;
        public DataTable selectData(List<String> columnsArray, string table,  List<valuesWhere> valuesOfTerms)
        {
            string columns = "";
            string terms = "";

            //Añadimos todas las columnas que queremos consultar
            for (int i = 0; i < columnsArray.Count(); i++)
            {
                columns += columnsArray[i] + " "; 
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
                    terms += valuesOfTerms[i].column + "= '" + valuesOfTerms[i].value + "' " + valuesOfTerms[i].operationBool + " ";
                }
                else
                {
                    terms += valuesOfTerms[i].column + "= " + valuesOfTerms[i].value + " " + valuesOfTerms[i].operationBool + " ";
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
        public int selectCountData(string column, string table, List<valuesWhere> valuesTermsArray)
        {
            int count = 0;
            string condictions = "";

            //En el caso de tener condiciones las guardamos para despues agregarla a la consulta
            for (int i = 0; i < valuesTermsArray.Count(); i++)
            {
                if (i == 0)
                {
                    condictions += " WHERE ";
                }
                if (valuesTermsArray[i].isTypeString)
                {
                    condictions += valuesTermsArray[i].column + "= '" + valuesTermsArray[i].value + "' " + valuesTermsArray[i].operationBool + " ";
                }
                else
                {
                    condictions += valuesTermsArray[i].column + "= " + valuesTermsArray[i].value + " " + valuesTermsArray[i].operationBool + " ";
                }
            }

            //Creamos la consulta
            string query = "SELECT COUNT(" + column + ")" + "FROM " + table + condictions;

            //Abrimos una conexion
            var conection = openConection();

            //Creamos el comando ya con el query
            var selectCount = createCommand(query, conection);

            //Ejecutamos la consulta
            count = selectCount.ExecuteNonQuery();

            return count;
        }

        //UPDATE table SET column= value, column= value  WHERE column = value 
        public void updateData(string table, List<String> columnsArray, List<String> valuesArray, List<valuesWhere> valuesTermsArray)
        {
            string columns = "";
            string condictions = "";

            //Añadimos las columnas que queremos consultar
            for (int i = 0; i < columnsArray.Count(); i++)
            {
                columns += "@" + columnsArray[i] + "= " + columnsArray[i] + ", ";
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
                    condictions += valuesTermsArray[i].column + "= '" + valuesTermsArray[i].value + "' " + valuesTermsArray[i].operationBool + " ";
                }
                else
                {
                    condictions += valuesTermsArray[i].column + "= " + valuesTermsArray[i].value + " " + valuesTermsArray[i].operationBool + " ";
                }
            }


            var conection = openConection();

            //Creamos el query para actualizar
            string query = "UPDATE " + table + "SET " + columns + " " + condictions;

            var update = createCommand(query, conection);

            //Añadimos todos los valores de las columnas
            for (int i = 0; i < valuesArray.Count(); i++)
            {
                update.Parameters.AddWithValue("@" + columnsArray[i], valuesArray[i]);
            }

            //Ejecutamos el comando
            update.ExecuteNonQuery();

            closeConection(conection);
        }

        //DELETE table WHERE column = value
        public void deleteData(string table, List<valuesWhere> valuesTermsArray)
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
                    terms += valuesTermsArray[i].column + "= '" + valuesTermsArray[i].value + "' " + valuesTermsArray[i].operationBool + " ";
                }
                else
                {
                    terms += valuesTermsArray[i].column + "= " + valuesTermsArray[i].value + " " + valuesTermsArray[i].operationBool + " ";
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

        public valuesWhere createListValuesWhere(bool isTypeString, string column, string value, string operationBool)
        {
            var items = new valuesWhere();
            items.isTypeString = isTypeString;
            items.column = column;
            items.value = value;
            items.operationBool = operationBool;

            return items;
        }
    }
}
