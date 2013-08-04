using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smsLauncher.model;

namespace smsLauncher
{
    public class balance
    {
        static SMSDataClassesDataContext db;
        static bool isNotExist = false;

        static long id(string account)
        {
            var acnt = (from i in db.SMS_eMoneys.Where(i => i.Account == account && i.Status == true) select i).Take(1).FirstOrDefault();
            if (acnt != null)
            {
                return acnt.ID;
            }
            return 0;
        }

        static string reply(string status)
        {
            var rep = (from i in db.SMS_Replies.Where(i => i.Tagged_ID == "BAL" && i.Status == status) select i).Take(1).FirstOrDefault();
            if (rep != null)
            {
                return rep.Message;
            }
            return null;
        }

        public static double current_amount(string account)
        {
            db = new SMSDataClassesDataContext();
            var acnt = (from i in db.SMS_eMoneys.Where(i => i.ID == id(account)) select i).Take(1).FirstOrDefault();
            if (acnt != null)
            {
                isNotExist = true;
                return (double)acnt.Amount;
            }
            isNotExist = false; 
            return 0;
        }

        public static double deduct(string account, double amount)
        {
            return current_amount(account) - amount;
        }

        public static double increase(string account, double amount)
        {
            return current_amount(account) + amount;
        }

        public static double IsQualified(string account)
        {
            return current_amount(account);
        }

        public static string check(string account)
        {
            string total = config.format_currency((decimal)current_amount(account));
            if (isNotExist)
            {
                return reply("OK").Replace("[AMOUNT]", total) + " " + DateTime.Now.ToLongDateString();
            }
            else
            {
                return reply("NOK");
            }
        }

        public static bool update(string account, double amount)
        {
            //Connection.String(smsLauncher.Properties.Settings.Default.SMSDatabaseConnectionString);
            //if (SqlClient.Open())
            //{
            //    if (SqlClient.Update("UPDATE SMS_eMoney SET Amount = '" + amount + "' WHERE Account = '" + account + "' AND Status = 'True';"))
            //    {
            //        return true;
            //    }
            //}
            return false;
        }
    }
}
