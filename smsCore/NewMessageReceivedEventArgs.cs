using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smsCore
{
    public class NewMessageReceivedEventArgs : EventArgs
    {
        public  string Phone
        {
            get { return ReceivedText.Phone; }
        }

        public  string TextMessage
        {
            get { return ReceivedText.TextMessage; }
        }

        public  string SMSC
        { 
            get { return ReceivedText.SMSC; }
        }

        public  DateTime TimeStamp
        { 
            get { return ReceivedText.TimeStamp; }
        }

        public  string TimeStampRFC
        {
            get { return ReceivedText.TimeStampRFC; }
        }

        public  int TimeZone
        {
            get { return ReceivedText.TimeZone; }
        }

        public  int TotalParts
        {
            get { return ReceivedText.TotalParts; }
        }

        public  int ReferenceNumber
        {
            get { return ReceivedText.ReferenceNumber; }
        }

        public  int SequenceNumber
        {
            get { return ReceivedText.SequenceNumber; }
        }
    }
}
