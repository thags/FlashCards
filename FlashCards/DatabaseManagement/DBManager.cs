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
            Console.WriteLine(connectionString);
            var connection = new SqlConnection(connectionString);
            //connection.Open();

            //Testing items - can delete later.
            //InsertStack(connection, "test");
            //Read(connection);
            //UpdateStackName(connection, 13, "UpdateTest");
            //DeleteStack(connection,4);
            //DeleteStack(connection, "test");
            //Read(connection);
        }
        private static void Read(SqlConnection connection) 
        {
            connection.Open();
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

            command.Dispose();
            connection.Close();
        }

        private static void InsertStack(SqlConnection connection, string stackName)
        {
            connection.Open();
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            string sql;

            sql = $"Insert into Stacks (Name) values ('{stackName}') ";
            command = new SqlCommand(sql, connection);

            adapter.InsertCommand = new SqlCommand(sql, connection);
            adapter.InsertCommand.ExecuteNonQuery();

            command.Dispose();
            connection.Close();
        }

        private static void DeleteStack(SqlConnection connection, int Id)
        {
            connection.Open();
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            string sql;

            sql = $"DELETE FROM Stacks WHERE Id = {Id}";
            command = new SqlCommand(sql, connection);

            adapter.DeleteCommand = new SqlCommand(sql, connection);
            adapter.DeleteCommand.ExecuteNonQuery();

            command.Dispose();
            connection.Close();
        }
        private static void DeleteStack(SqlConnection connection, string name)
        {
            connection.Open();
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            string sql;

            sql = $"DELETE FROM Stacks WHERE Name = '{name}'";
            command = new SqlCommand(sql, connection);

            adapter.DeleteCommand = new SqlCommand(sql, connection);
            adapter.DeleteCommand.ExecuteNonQuery();

            command.Dispose();
            connection.Close();
        }

        private static void UpdateStackName(SqlConnection connection, int Id, string updatedName)
        {
            connection.Open();
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            string sql;

            sql = $"UPDATE Stacks SET Name = '{updatedName}' WHERE Id = {Id}";
            command = new SqlCommand(sql, connection);

            adapter.DeleteCommand = new SqlCommand(sql, connection);
            adapter.DeleteCommand.ExecuteNonQuery();

            command.Dispose();
            connection.Close();
        }
    }
}
