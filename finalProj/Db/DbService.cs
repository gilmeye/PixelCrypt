using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProj
{
    internal class DbService
    {


        public static DataTable SelectKeys()
        {
            Connect db = new Connect();
            string query = "SELECT * FROM Keys";
            DataTable dt = db.SelectFromDb(query);
            return dt;
        }


        public static DataTable SelectValues(int id) 
        {
            Connect db = new Connect();
            string query = String.Format("SELECT Values.Next FROM [Values] WHERE ID = {0}", id);
            DataTable dt = db.SelectFromDb(query);
            return dt;
        }


        public static void AddKey(string key) 
        {
            Connect db = new Connect();
            string query = String.Format("Insert Into Keys (Prev) Values( '{0}' )", key);
            db.sqlUdi(query);
        }

        public static void AddValue(string key, string value)
        {
            Connect db = new Connect();
            string query = String.Format("Select Keys.ID from Keys where Keys.Prev = '{0}'", key);
            DataTable dt = db.SelectFromDb(query);
            int id = int.Parse(dt.Rows[0][0].ToString());
            query = String.Format("Select Values.Count from [Values] where Values.ID = {0} " +
                "AND Values.Next = '{1}'", id, value);
            dt = db.SelectFromDb(query);
            if (dt.Rows.Count == 0)
            {
                query = string.Format("Insert Into [Values] ([ID], [Next], [Count]) " +
                    "Values ({0}, '{1}', 1)", id, value);
                db.sqlUdi(query);
            }
            else
            {
                int count = int.Parse(dt.Rows[0][0].ToString());
                query = string.Format("Update [Values] Set Values.Count = {0} Where Values.ID = {1} " +
                    "AND Values.Next = '{2}'", count + 1, id, value);
                db.sqlUdi(query);
            }
        }


        public static void InsertNextConnection(string key, string value)
        {
            Connect db = new Connect();
            String query = string.Format("Select * FROM Keys where Keys.Prev = '{0}'", key);
            DataTable dt = db.SelectFromDb(query);
            if(dt.Rows.Count == 0)
            {
                AddKey(key);
            }

            AddValue(key, value);
        }


        public static DataTable SelectDB()
        {
            Connect db = new Connect();
            String query = string.Format("Select Keys.Prev, [Values].Next, [Values].Count FROM Keys " +
                "INNER JOIN [Values] ON Keys.ID = [Values].ID");
            DataTable dt = db.SelectFromDb(query);
            return dt;
        }



        public static int CountLetters()
        {
            Connect db = new Connect();
            string query = "select SUM([Count]) FROM Frequency;";
            DataTable dt = db.SelectFromDb(query);
            return int.Parse(dt.Rows[0][0].ToString());
        }

        public static DataTable SelectFrequency()
        {
            Connect db = new Connect();
            string query = "Select * FROM Frequency";
            DataTable dt = db.SelectFromDb(query);
            return dt;
        }

        public static int SelectLetterFreq(char c)
        {
            Connect db = new Connect();
            string query = string.Format("Select Frequency.[Count] FROM Frequency WHERE " +
                "Frequency.[Letter] = '{0}'", c);
            DataTable dt = db.SelectFromDb(query);
            if (dt.Rows.Count == 0) return 0;    
            return int.Parse(dt.Rows[0][0].ToString());
        }


        public static void UpdateLetterFreq(int count, char c)
        {
            Connect db = new Connect();
            string query = string.Format("Update Frequency Set Frequency.[Count] = {0} " +
                "WHERE Frequency.[Letter] = '{1}'", count, c);
            db.sqlUdi(query);
        }

        public static void InsertLetterFreq(char c, int count)
        {
            Connect db = new Connect();
            string query = string.Format("Insert Into Frequency([Letter], [Count]) Values('{0}', {1})", c, count);
            db.sqlUdi(query);

        }

    }
}
