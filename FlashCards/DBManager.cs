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
        private static string GetConnectionString()
        {
            string dataSource = XmlManager.ReadConfig("dataSource");
            string initialCatalog = XmlManager.ReadConfig("initialCatalog");
            string integratedSecurity = XmlManager.ReadConfig("integratedSecurity");
            string connectionString = $"Server={dataSource};database={initialCatalog};Integrated Security={integratedSecurity}";
            return connectionString;
        }
        public static SqlConnection OpenSql()
        {
            string connectionString = GetConnectionString();
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }


    }
}
