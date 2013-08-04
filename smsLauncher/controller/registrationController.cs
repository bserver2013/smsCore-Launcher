using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smsLauncher.model;

namespace smsLauncher
{
    public class SMS
    {
        static SMSDataClassesDataContext db;

        static string lastname = string.Empty;
        static string Lastname
        {
            get { return lastname; }
            set { lastname = value; }
        }

        static string firstname = string.Empty;
        static string Firstname
        {
            get { return firstname; }
            set { firstname = value; }
        }

        static string town = string.Empty;
        static string Town
        {
            get { return town; }
            set { town = value; }
        }

        static string sponsor = string.Empty;
        static string Sponsor
        {
            get { return sponsor; }
            set { sponsor = value; }
        }

        static string populate(string input)
        {
            input = config.subString(input, 4, 50);
            string code = string.Empty;
            string[] msg = input.Split('/');
            for (int i = 0; i < msg.Length; i++)
            {
                switch(i)
                {
                    case 0 :
                        Lastname = msg[i];
                        break;
                    case 1:
                       Firstname  = msg[i];
                        break;
                    case 2:
                        Town= msg[i];
                        break;
                    case 3:
                        Sponsor = msg[i];
                        break;
                    case 4:
                        code = msg[i];
                        break;
                }
            }
            return code;
        }

        static bool checkNumber(string number)
        {
            db = new SMSDataClassesDataContext();
            var member = (from i in db.SMS_Members.Where(i => i.CP_Number == number) select i).Take(1).FirstOrDefault();
            var money = (from i in db.SMS_eMoneys.Where(i => i.Account == number) select i).Take(1).FirstOrDefault();
            if (member != null || money != null)
            {
                return true;
            }
            return false;
        }

        static bool checkCode(string input)
        {
            db = new SMSDataClassesDataContext();
            var code = (from i in db.SMS_ActivationCodes.Where(i => i.PINCode == input && i.Status == true) select i).Take(1).FirstOrDefault();
            if (code != null)
            {
                return code.Status;
            }
            return false;
        }

        static bool checkRef(string input)
        {
            db = new SMSDataClassesDataContext();
            var refNo = (from i in db.SMS_Members.Where(i => i.ReferenceNo == input) select i).Take(1).FirstOrDefault();
            if (refNo != null)
            {
                return true;
            }
            return false;
        }

        static string replyMessage(string tag, string status, string optional = "")
        {
            string reply = "Sorry, our system has encountered an error, please try again later. thank you!";
            db = new SMSDataClassesDataContext();
            var rep = (from i in db.SMS_Replies.Where(i => i.Tagged_ID == tag && i.Status == status) select i).Take(1).FirstOrDefault();
            if (rep != null)
            {
                if (status == "OK")
                {
                    reply = rep.Message.Replace("[Firstname]", optional);
                }
                else
                {
                    reply = rep.Message;
                }
            }
            return reply;
        }

        public static string Registration(string number, string input)
        {
            db = new SMSDataClassesDataContext();
            string returnMessage = replyMessage("NREG", "NOK0");

            if (checkNumber(number))
            {
                return "Oop, Your number already exist to our record!Pleas try a new number";
            }

            if (checkCode(populate(input)))
            {
                string refCode = "CIA" + config.generateReferenceNo(4) + "-" + DateTime.Now.Year;
                SMS_Member m = new SMS_Member();
                m.ReferenceNo = refCode;
                m.Group_Name = "BAYANIHAN";
                m.Account_Number = number;
                m.Family_Name = Lastname;
                m.First_Name = Firstname;
                m.Town = Town;
                m.Sponsor_ID = Sponsor;
                m.CP_Number  = number;
                m.DateReg = config.receivedDateTime();
                m.Status = true;
                m.monthOf = DateTime.Now.Month;
                m.yearOf = DateTime.Now.Year;

                SMS_eMoney e = new SMS_eMoney();
                e.Account = number;
                e.Amount = 100;
                e.Status = true;


                if (!checkRef(refCode))
                {
                    try
                    {
                        db.SMS_Members.InsertOnSubmit(m);
                        db.SMS_eMoneys.InsertOnSubmit(e);
                        db.SubmitChanges();
                        returnMessage = replyMessage("NREG", "OK", Firstname);
                    }
                    catch (Exception ex)
                    {
                        return "Sorry, our system has encountered an error, please try again later. thank you!";
                    }
                }
                else
                {
                    returnMessage = replyMessage("NREG", "NOK1");
                }
            }
            return returnMessage;
        }

    }
}
