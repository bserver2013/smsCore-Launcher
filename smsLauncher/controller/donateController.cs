using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smsLauncher.model;
using System.Globalization;

namespace smsLauncher
{
    public class donate
    {
        static SMSDataClassesDataContext db;

        static string number_combination = string.Empty;
        static string Combination
        {
            get { return number_combination; }
            set { number_combination = value; }
        }

        static double donation_amount = 0;
        static double Donation
        {
            get { return donation_amount; }
            set { donation_amount = value; }
        }

        static bool IsSender_Exist(string account)
        {
            try
            {
                var acnt = (from i in db.SMS_Members.Where(i => i.CP_Number == account) select i).Take(1).FirstOrDefault();
                if (acnt != null)
                {
                    return true;
                }
            }
            catch (Exception ex)
            { }
            return false;
        }

        static string check_If_open(DateTime value)
        {
            db = new SMSDataClassesDataContext();
            try
            {
                var x = (from i in db.SMS_BayanihanSums.Where(i => i.open_started >= value && value <= i.closing_time)
                         select i).Take(1).FirstOrDefault();
                if (x != null)
                {
                    return x.bayanihan_ref;
                }
                return "Closed";
            }
            catch (Exception ex)
            {}
            return "not available at this time!";
        }

        public static string now(string number, string message)
        {
            string bayanihan = check_If_open(config.dateNowWithTimeFormated());

            if (IsSender_Exist(number))
            {
                if (bayanihan == "Closed" || bayanihan == "NULL")
                {
                    return "The system is " + bayanihan;
                }

                string[] x = message.Split(' ');
                string[] y = x[1].Split('/');
                Combination = y[0];
                Donation = Convert.ToDouble(y[1]);

                if (balance.current_amount(number) >= Donation)
                {

                }
                return Combination + " " + Donation;
            }

            return "";
        }

    }
}
