using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smsLauncher.model;

namespace smsLauncher
{
    public class transfer
    {
        static SMSDataClassesDataContext db;

        static string sender = string.Empty;
        static string Sender
        {
            get { return sender; }
            set { sender = value; }
        }

        static string receiver = string.Empty;
        static string Receiver
        {
            get { return receiver; }
            set { receiver = value; }
        }

        static string refNo = string.Empty;
        static string ReferenceNo
        {
            get { return refNo; }
            set { refNo = value; }
        }

        static string reply(string tag, string status)
        {
            string reply = "Sorry, our system has encountered an error, please try again later. thank you!";
            var rep = (from i in db.SMS_Replies.Where(i => i.Tagged_ID == tag && i.Status == status) select i).Take(1).FirstOrDefault();
            if (rep != null)
            {
                return rep.Message;
            }
            return reply;
        }

        static string Member(string account)
        {
            try
            {
                var acnt = (from i in db.SMS_Members.Where(i => i.CP_Number == account) select i).Take(1).FirstOrDefault();
                if (acnt != null)
                {
                    return acnt.First_Name;
                }
            }
            catch (Exception ex)
            {}
            return "NULL";
        }

        static bool IsSender_Exist(string account)
        {
            try
            {
                var acnt = (from i in db.SMS_eMoneys.Where(i => i.Account == account) select i).Take(1).FirstOrDefault();
                if (acnt != null)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
            }
            
            return false;
        }

        static bool IsRef_Exist(string input)
        {
            db = new SMSDataClassesDataContext();
            var refNo = (from i in db.SMS_TransferFundReports.Where(i => i.ReferenceNo == input) select i).Take(1).FirstOrDefault();
            if (refNo != null)
            {
                return true;
            }
            return false;
        }

        public static string now(string number, string input)
        {
            db = new SMSDataClassesDataContext();
            Sender = number;
            if (IsSender_Exist(Sender))
            {
                string[] split = input.Split(' ');
                Receiver = split[1];

                if (IsSender_Exist(Receiver))
                {
                    double d = Convert.ToDouble(split[2]);
                    ReferenceNo = config.generateReferenceNo(6);

                    if (balance.current_amount(number) >= d)
                    {
                        if (!IsRef_Exist(ReferenceNo))
                        {
                            balance.update(Sender, balance.deduct(Sender, d));
                            if (save("sender", (decimal)d, ReferenceNo))
                            {
                                report("sender", d.ToString());
                            }

                            balance.update(Receiver, balance.increase(Receiver, d));
                            if (save("receiver", (decimal)d, ReferenceNo))
                            {
                                report("receiver", ReferenceNo);
                            }
                        }
                    }
                    else
                    {
                        save("insufficient");
                    }
                }
                else
                {
                    save("receiver_notExist");
                }
            }
            else
            {
                save("Sender_notExist");
            }
            return "OK";
        }

        static bool save(string type, decimal amount = 0, string refCode = "")
        {
            string m = string.Empty;
            bool x = false;
            switch (type)
            {
                case "sender":
                    m = reply("TF", "Sender").Replace("[TAMOUNT]", config.format_currency(amount)).Replace("[MEMBER]", Member(Receiver)).Replace("[ACCOUNT]", Receiver).Replace("[REFNO]", refCode).Replace("[DAMOUNT]", balance.current_amount(Sender).ToString()).Replace("[DATE]", DateTime.Now.ToLongDateString());
                    if (validatorMessage.save(Sender, m)){ x = true; }
                    break;
                case "receiver":
                    m = reply("TF", "Receiver").Replace("[TFAMOUNT]", config.format_currency(amount)).Replace("[MEMBER]", Member(Sender)).Replace("[ACCOUNT]", Sender).Replace("[REFNO]", refCode).Replace("[TAMOUNT]", balance.current_amount(Receiver).ToString()).Replace("[DATE]", DateTime.Now.ToLongDateString());
                    if (validatorMessage.save(Receiver, m)){ x = true; }
                    break;
                case "insufficient":
                    validatorMessage.save(Sender, reply("TF", "Insufficient"));
                    break;
                case "receiver_notExist":
                    validatorMessage.save(Sender, Receiver + " this number isn't exist to our record!");
                    break;
                case "Sender_notExist":
                    validatorMessage.save(Sender, "Your number isn't exist to our record!");
                    break;
                default :
                    break;
            }
            return x;
        }

        static void report(string type, string amount = "")
        {
            if (type == "sender")
            {
                SMS_TransferFundReport tf = new SMS_TransferFundReport();
                tf.ReferenceNo = ReferenceNo;
                tf.Sender = Sender;
                tf.Receiver = "N/A";
                tf.Amount = Convert.ToDecimal(amount);
                tf.DateTransfered = config.receivedDateTime();
                tf.Status = false;
                db.SMS_TransferFundReports.InsertOnSubmit(tf);
            }
            else if (type == "receiver")
            {
                var upt = db.SMS_TransferFundReports.Single(i => i.ReferenceNo == ReferenceNo);
                if (upt != null)
                {
                    upt.Receiver = Receiver;
                    upt.Status = true;
                }
            }

            try
            {
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
