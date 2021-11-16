using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashCards.xmlManager;
using System.Data.SqlClient;

namespace FlashCards.DatabaseManagement
{
    class DBManager
    {
        public static void Connect()
        {
            string connectionString = XmlManager.ReadConfig("dbConnectionString");
            var connection = new SqlConnection(connectionString);
            connection.Open();

            Read(connection);
        }

        private static void Read(SqlConnection connection) 
        {
            SqlCommand command;
            SqlDataReader dataReader;
            string sql, Output = "";

            sql = "SELECT * FROM Stacks";
            command = new SqlCommand(sql, connection);

            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Output = Output + dataReader.GetValue(0) + " - " + dataReader.GetValue(1) + "\n";
            }
            Console.WriteLine(Output);
        }
    }
}
