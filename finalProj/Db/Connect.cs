using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace finalProj
{
    internal class Connect
    {

        private string connectionString;
        private OleDbConnection connection;
        static string dbPath = "C:\\Users\\itaym\\OneDrive\\Desktop\\projects\\finale\\finalProj\\finalProj\\Db\\DatabaseFP.accdb";
        

        public Connect()
        {
            connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};";
            connection = new OleDbConnection(connectionString);
        }

        public DataTable SelectFromDb(string sqlStr)
        {
            connection.ConnectionString = connectionString;
            OleDbCommand myCmd = new OleDbCommand(sqlStr, connection);
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = myCmd;
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            return dataTable;
        }

        public void sqlUdi(string sqlStr)
        {

            connection.ConnectionString = connectionString;
            OleDbCommand myCmd = new OleDbCommand(sqlStr, connection);
            connection.Open();
            myCmd.ExecuteNonQuery();
            connection.Close();
        }


    }
}
