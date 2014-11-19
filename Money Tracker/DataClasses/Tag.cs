using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.DataClasses
{
    class Tag
    {
        public Tag(string incolor, string inname)
        {
            color = incolor;
            name = inname;
        }
        public string name { get; set; }
        public string color { get; set; }
    }
}
