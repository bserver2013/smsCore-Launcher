using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smsLauncher.model;

namespace smsLauncher
{
    public class receivedMessage
    {
        static SMSDataClassesDataContext db;

        public static bool save(string number, string message)
        {
            db = new SMSDataClassesDataContext();
            SMS_Inbox inbox = new SMS_Inbox();
            SMS_ReceivedMsg receivedMsg = new SMS_ReceivedMsg();

            inbox.Sender = number;
            inbox.Message = message;
            inbox.DateReceived = config.receivedDateTime();
            inbox.MonthOf = DateTime.Now.Month;
            inbox.YearOf = DateTime.Now.Year;
            inbox.Status = false;

            receivedMsg.Sender = number;
            receivedMsg.Message = message;
            receivedMsg.DateReceived = config.receivedDateTime();
            receivedMsg.MonthOf = DateTime.Now.Month;
            receivedMsg.YearOf = DateTime.Now.Year;
            receivedMsg.Status = false;

            validatorMessage.save(number, "Sorry, your message cannot be processed at this time, " +
                    "but your message is on queue. and it will process later. thank you!");

            try
            {
                db.SMS_Inboxes.InsertOnSubmit(inbox);
                db.SMS_ReceivedMsgs.InsertOnSubmit(receivedMsg);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                config.log(ex.Message);
                return false;
            }
        }
    }
}
