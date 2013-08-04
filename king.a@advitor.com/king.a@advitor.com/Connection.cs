using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kpa.Developer.Controller.Collection
{
    public class Connection
    {
        static string con = string.Empty;
        public static string inputString
        {
            get { return con; }
            set { con = value; }
        }

        public static void String(string input)
        {
            inputString = input;
        }
    }
}
