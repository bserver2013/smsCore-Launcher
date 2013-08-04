using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smsLauncher
{
    public class cacheFields
    {
        int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        string number;
        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        string message;
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        bool status;
        public bool Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
