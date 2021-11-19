using System.Collections.Generic;
using FlashCards.Models;
using System.Data.SqlClient;

namespace FlashCards
{
    class StackController
    {
        public static int GetIdFromName(string name)
        {
            SqlConnection connection = DBManager.OpenSql();

            string sqlCommand = $"SELECT TOP 1 * FROM Stacks WHERE Name = '{name}'";
            int answer = -1;

            SqlCommand command = new SqlCommand(sqlCommand, connection);
            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                answer = (int)dataReader.GetValue(0);
            }

            command.Dispose();
            connection.Close();

            return answer;
        }
        public static string GetNameFromId(int Id)
        {
            SqlConnection connection = DBManager.OpenSql();

            string sqlCommand = $"SELECT TOP 1 * FROM Stacks WHERE Id = {Id}";
            string answer = "null";

            SqlCommand command = new SqlCommand(sqlCommand, connection);
            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                answer = (string)dataReader.GetValue(1);
            }

            command.Dispose();
            connection.Close();

            return answer;
        }
        public static bool CheckStackExists(string name)
        {
            var result = GetStacks(name);
            if (result.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
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
        public static List<Stack> GetStacks(int Id)
        {
            SqlConnection connection = DBManager.OpenSql();

            var stackList = new List<Stack> { };
            string sqlCommand = $"SELECT TOP 1 * FROM Stacks WHERE Id = {Id}";

            SqlCommand command = new SqlCommand(sqlCommand, connection);
            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                int stackId = (int)dataReader.GetValue(0);
                string Name = (string)dataReader.GetValue(1);
                Stack newStack = new Stack
                {
                    Id = stackId,
                    Name = Name
                };
                stackList.Add(newStack);
            }

            command.Dispose();
            connection.Close();

            return stackList;
        }
        public static List<Stack> GetStacks(string name)
        {
            SqlConnection connection = DBManager.OpenSql();

            var stackList = new List<Stack> { };
            string sqlCommand = $"SELECT TOP 1 * FROM Stacks WHERE Name = '{name}'";

            SqlCommand command = new SqlCommand(sqlCommand, connection);
            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                int stackId = (int)dataReader.GetValue(0);
                string Name = (string)dataReader.GetValue(1);
                Stack newStack = new Stack
                {
                    Id = stackId,
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
        public static void UpdateStackName(string stackName, string updatedName)
        {
            SqlConnection connection = DBManager.OpenSql();
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            string sql;

            sql = $"UPDATE Stacks SET Name = '{updatedName}' WHERE Name = '{stackName}'";
            command = new SqlCommand(sql, connection);

            adapter.DeleteCommand = new SqlCommand(sql, connection);
            adapter.DeleteCommand.ExecuteNonQuery();

            command.Dispose();
            connection.Close();
        }
    }
}
