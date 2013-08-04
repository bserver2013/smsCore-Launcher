using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using smsLauncher_offline.model;
using smsLauncher_offline.smsCoreApi;

namespace smsLauncher_offline
{
    class Query
    {
        static SMSDataClassesDataContext db;
        static smsCore_api api;

        public static Int64 TotalQueued
        {
            get;
            private set;
        }

        public static bool Check()
        {
            db = new SMSDataClassesDataContext();
            var result = (from i in db.SMS_ReceivedMsgs.Where(i => i.Status == false) select i).Count();
            if (result != 0)
            {
                TotalQueued = result;
                return true;
            }
            return false;
        }

        public static void Do()
        {
            api = new smsCore_api();
            var result = (from i in db.SMS_ReceivedMsgs.Where(i => i.Status == false) select i).ToList();
            if (result != null)
            {
                foreach (var l in result)
                {
                    Thread.Sleep(100);
                    api.sendMethodWithValidation(l.Sender, l.Message);

                    Thread.Sleep(100);
                    delete(l.Id);
                }
            }
        }

        internal static bool delete(long id)
        {
            var x = (from i in db.SMS_ReceivedMsgs.Where(i => i.Id == id) select i).Take(1).FirstOrDefault();
            try
            {
                db.SMS_ReceivedMsgs.DeleteOnSubmit(x);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
