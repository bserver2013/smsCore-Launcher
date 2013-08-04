using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using smsLauncher.model;

namespace smsLauncher
{
    public class validatorMessage
    {
        static SMSDataClassesDataContext db;
        static cacheList cList;

        static long _id = 0;
        public static long Id
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


        public static bool save(string number, string message)
        {
            if (message != "OK")
            {
                db = new SMSDataClassesDataContext();
                SMS_QueuedBox que = new SMS_QueuedBox();
                que.Message = config.encrypt(message);
                que.Number = number;
                que.Network = (short)config.prefixes(number);
                que.Status = false;

                try
                {
                    db.SMS_QueuedBoxes.InsertOnSubmit(que);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    config.log(ex.Message); 
                }
            }
            return false;
        }

        public static bool delete(long id)
        {
            db = new SMSDataClassesDataContext();
            var x = (from i in db.SMS_ReceivedMsgs.Where(i => i.Id == id) select i).Take(1).FirstOrDefault();

            try
            {
                db.SMS_ReceivedMsgs.DeleteOnSubmit(x);
                db.SubmitChanges();
                config.log("Successful deleted in inbox");
                return true;
            }
            catch (Exception ex)
            {
                config.log(ex.Message);
                return false;
            }
        }

        public static bool saveToSentItemThenDelete(long id, bool status)
        {
            db = new SMSDataClassesDataContext();
            SMS_SentItem sentItems = new SMS_SentItem();
            var x = (from i in db.SMS_QueuedBoxes.Where(i => i.ID == id) select i).Take(1).FirstOrDefault();
            if (x != null)
            {
                sentItems.Number = x.Number;
                sentItems.Message = x.Message;
                sentItems.DateTime = config.receivedDateTime();
                sentItems.Status = status;
                sentItems.MonthOf = DateTime.Now.Month;
                sentItems.YearOf = DateTime.Now.Year;

                try
                {
                    db.SMS_SentItems.InsertOnSubmit(sentItems);
                    db.SMS_QueuedBoxes.DeleteOnSubmit(x);
                    db.SubmitChanges();
                    config.log("Successful updated in inbox");
                    return true;
                }
                catch (Exception ex)
                {
                    config.log(ex.Message);
                }
            }
            return false;
        }
    }
}
