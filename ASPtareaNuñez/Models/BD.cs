using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ASPtareaNuñez.Models.Entities
{
    public class BD
    {
        public MySqlConnection connection;

        public BD()
        {
            connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=Sena1234;Database=bdtareasnuñez");
        }
        public string ejecutarSQL(string sql)
        {
            string resultado = "";

            connection.Open();

            MySqlCommand cmd = new MySqlCommand(sql, connection);

            int filasResultado = cmd.ExecuteNonQuery();

            if (filasResultado > -1)
            {
                resultado = "Correcto";
            }
            else
            {
                resultado = "Incorrecto";
            }

            connection.Close();


            return resultado;
        }
        public DataTable ejecutarSQL1(string sqlS)
        {
            DataTable mostrarDatos = new DataTable();

            MySqlCommand comando = new MySqlCommand(sqlS, connection);
            try
            {
                connection.Open();

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(comando);

                dataAdapter.Fill(mostrarDatos);
                connection.Close();

                dataAdapter.Dispose();
            }
            catch (Exception)
            {
                return null;
            }
            return mostrarDatos;
        }
    }
}
