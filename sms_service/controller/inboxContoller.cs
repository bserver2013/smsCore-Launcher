using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sms_service.model;

using sms_service.sender;

namespace sms_service
{
    public class inbox
    {
        static SMSDataClassesDataContext db;

        public static DateTime dateTime()
        {
            return Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
        }

        public static string save(string input)
        {
            db = new SMSDataClassesDataContext();
            SMS_Inbox inbox = new SMS_Inbox();

            string number = string.Empty;
            string message = string.Empty;
            string[] data = input.Split(';');

            for (int x = 0; x < data.Length; x++)
            {
                switch (x)
                {
                    case 0:
                        number = data[x];
                        break;
                    case 1:
                        message = data[x];
                        break;
                }
            }

            //queuedBox.save(number, keyword.check(message));

            inbox.Sender = number;
            inbox.Message = message;
            inbox.DateReceived = dateTime();
            inbox.MonthOf = DateTime.Now.Month;
            inbox.YearOf = DateTime.Now.Year;
            inbox.Status = false;

            try
            {
                db.SMS_Inboxes.InsertOnSubmit(inbox);
                db.SubmitChanges();
                return input;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return "NOK";
            }
        }
    }
}
