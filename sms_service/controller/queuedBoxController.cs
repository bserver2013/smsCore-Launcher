using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sms_service.model;

namespace sms_service
{
    public class queuedBox
    {
        static SMSDataClassesDataContext db;

        private enum prefixes_checker
        {
            invalid_number = 5,
            invalid_prefix = 4,
            sun = 3,
            globe = 2,
            smart = 1
        }

        static int prefixes(string number)
        {
            int net = (int)prefixes_checker.invalid_number;

            if (number.Length == 11)
            {
                switch (number.Substring(0, 4))
                {
                    case "0907":
                    case "0908":
                    case "0909":
                    case "0910":
                    case "0912":
                    case "0918":
                    case "0919":
                    case "0920":
                    case "0921":
                    case "0928":
                    case "0929":
                    case "0930":
                    case "0938":
                    case "0939":
                    case "0946":
                    case "0947":
                    case "0948":
                    case "0949":
                    case "0989":
                    case "0998":
                    case "0999":
                        net = (int)prefixes_checker.smart; ;
                        break;
                    case "0905":
                    case "0906":
                    case "0915":
                    case "0916":
                    case "0917":
                    case "0925":
                    case "0926":
                    case "0927":
                    case "0935":
                    case "0936":
                    case "0937":
                    case "0994":
                    case "0996":
                    case "0997":
                        net = (int)prefixes_checker.globe; ;
                        break;
                    case "0922":
                    case "0923":
                    case "0932":
                    case "0933":
                    case "0934":
                    case "0942":
                    case "0943":
                        net = (int)prefixes_checker.sun; ;
                        break;
                    default:
                        net = (int)prefixes_checker.invalid_prefix; ;
                        break;
                }
            }
            return net;
        }

        public static bool save(string number, string message)
        {
            db = new SMSDataClassesDataContext();
            SMS_QueuedBox que = new SMS_QueuedBox();
            que.Message = message;
            que.Number = number;
            que.Network = (short)prefixes(number);
            que.Status = false;

            try
            {
                db.SMS_QueuedBoxes.InsertOnSubmit(que);
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
