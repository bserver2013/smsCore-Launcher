using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smsLauncher.model;


namespace smsLauncher
{
    public class checkMessage
    {
        static SMSDataClassesDataContext db;

        static int _id = 0;
        public static int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        static string _number = string.Empty;
        public static string Number
        {
            get { return _number; }
            set { _number = value; }
        }

        static string _message = string.Empty;
        public static string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public static bool now()
        {
            db = new SMSDataClassesDataContext();
            var que = (from i in db.SMS_QueuedBoxes.Where(i => i.Status == false) select i).Take(1).FirstOrDefault();
            if (que != null)
            {
                if (config.getLastDigit(que.Number, config.outBound))
                {
                    Id = que.ID;
                    Number = que.Number;
                    Message = config.decrypt(que.Message);
                    return true;
                }
            }
            return false;
        }
    }
}
