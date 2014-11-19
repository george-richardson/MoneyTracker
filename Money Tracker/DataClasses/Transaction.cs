using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.DataClasses
{
    public class Transaction
    {
        public Transaction() { }
        public Transaction(string inid, string invalue, string indesc, string indate)
        {
            id = inid;
            value = GeorgeMethods.convertToCurrencyString(invalue);
            desc = indesc;
            DateTime dt = Convert.ToDateTime(indate);
            if ((DateTime.Now - dt).Days >= 1)
            {
                var l = new Windows.ApplicationModel.Resources.ResourceLoader();
                date = dt.ToString(l.GetString("DateFormatShort"));
            }
            else
            {
                date = dt.ToString("HH:mm");
            }

        }
        public string value { get; set; }
        public string desc { get; set; }
        public string date { get; set; }
        public string id { get; set; }

    }
}
