﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashCards.xmlManager;
using System.Data.SqlClient;
using FlashCards.Models;

namespace FlashCards.DatabaseManagement
{
    class DBManager
    {
        private static SqlConnection GetConnection()
        {
            string connectionString = XmlManager.ReadConfig("dbConnectionString");
            Console.WriteLine(connectionString);
            var connection = new SqlConnection(connectionString);
            return connection;
        }
        private static SqlConnection OpenSql()
        {
            SqlConnection connection = GetConnection();
            connection.Open();
            return connection;
        }
        

        public static List<Stack> GetStacks()
        {
            SqlConnection connection = OpenSql();

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
            //remove console.writeline in final, this is just for testing
            Console.WriteLine($"{stackList[0].Name} {stackList[0].Id}");
            
            command.Dispose();
            connection.Close();

            return stackList;
        }

        public static void InsertStack(string stackName)
        {
            SqlConnection connection = OpenSql();
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

        public static void DeleteStack(int Id)
        {
            SqlConnection connection = OpenSql();
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
        public static void DeleteStack(string name)
        {
            SqlConnection connection = OpenSql();
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
            SqlConnection connection = OpenSql();
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
