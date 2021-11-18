using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashCards.xmlManager;
using System.Data.SqlClient;

namespace FlashCards
{
    class DBManager
    {
        private static SqlConnection GetConnection()
        {
            string connectionString = XmlManager.ReadConfig("dbConnectionString");
            var connection = new SqlConnection(connectionString);
            return connection;
        }
        public static SqlConnection OpenSql()
        {
            SqlConnection connection = GetConnection();
            connection.Open();
            return connection;
        }


    }
}
