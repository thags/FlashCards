using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashCards.Models;
using System.Data.SqlClient;

namespace FlashCards
{
    class StackController
    {
        public static List<Stack> GetStacks()
        {
            SqlConnection connection = DBManager.OpenSql();

            var stackList = new List<Stack> { };

            SqlCommand command = new SqlCommand("SELECT * FROM Stacks", connection);
            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                int Id = (int)dataReader.GetValue(0);
                string Name = (string)dataReader.GetValue(1);
                Stack newStack = new Stack
                {
                    Id = Id,
                    Name = Name
                };
                stackList.Add(newStack);
            }

            command.Dispose();
            connection.Close();

            return stackList;
        }

        public static List<Stack> GetStacks(int XAmount, string order = "ASC")
        {
            SqlConnection connection = DBManager.OpenSql();

            var stackList = new List<Stack> { };
            string sqlCommand = $"SELECT TOP {XAmount} * FROM Stacks ORDER BY Id {order}";

            SqlCommand command = new SqlCommand(sqlCommand, connection);
            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                int Id = (int)dataReader.GetValue(0);
                string Name = (string)dataReader.GetValue(1);
                Stack newStack = new Stack
                {
                    Id = Id,
                    Name = Name
                };
                stackList.Add(newStack);
            }

            command.Dispose();
            connection.Close();

            return stackList;
        }

        public static void InsertStack(string stackName)
        {
            SqlConnection connection = DBManager.OpenSql();
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

        public static void Delete(int Id)
        {
            SqlConnection connection = DBManager.OpenSql();
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
        public static void Delete(string name)
        {
            SqlConnection connection = DBManager.OpenSql();
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

        public static void UpdateStackName(int Id, string updatedName)
        {
            SqlConnection connection = DBManager.OpenSql();
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
