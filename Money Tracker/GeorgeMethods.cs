using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;

namespace Money_Tracker
{
    class GeorgeMethods
    {
        // Method that converts a realnumber in a string format to a localized currency string.
        public static string convertToCurrencyString(string s) {
            var l = new Windows.ApplicationModel.Resources.ResourceLoader();
            return l.GetString("CurrencySymbol") +  float.Parse(s).ToString("n2");
        }

        // Method that creates all the databases and what have you.
        public static void createDB()
        {
            using (var connection = new SQLiteConnection("database.db")) {
                using (var statement = connection.Prepare("CREATE TABLE IF NOT EXISTS transactions(value REAL, desc TEXT, time TEXT, tagid INTEGER);")) {
                    statement.Step();
                }
                using (var statement = connection.Prepare("CREATE TABLE IF NOT EXISTS tags(tagid INTEGER PRIMARY KEY AUTOINCREMENT, color TEXT, name TEXT);"))
                {
                    statement.Step();
                }
               

            }
        }

        // Method to delete database and start again. Used when changes are made to the database structure.
        public static void rebuildDB()
        {
            using (var connection = new SQLiteConnection("database.db"))
            {
                using (var statement = connection.Prepare("DROP TABLE IF EXISTS transactions;"))
                {
                    statement.Step();
                }
                using (var statement = connection.Prepare("DROP TABLE IF EXISTS tags;"))
                {
                    statement.Step();
                }
            }
            createDB();
        }
    }
}
