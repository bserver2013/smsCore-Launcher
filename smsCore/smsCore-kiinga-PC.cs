using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Globalization;
using System.Data;
using System.Collections.Generic;
using smsCoreHelper;

namespace smsCore
{
    public class SMS {

        mCore.SMS objSMS = new mCore.SMS();
        smsCoreLib.Properties.Settings mySettings = new smsCoreLib.Properties.Settings();
        
        public bool enableCheckPIN = false;
        public string MyAppName = "smsCore v2.1.1 DEMO";

        public bool Activated = false;
        public string port;
        public string baudrate;
        public string DataBits;
        public int Parity;
        public int StopBits;
        public int FlowControl;

        private void initialize()
        {
            objSMS.NewMessageReceived += new mCore.SMS.NewMessageReceivedEventHandler(this.objSMS_NewMessageReceived);
            objSMS.ReadIntervalTimeout = 100;
            objSMS.DelayAfterPIN = 20000;
            mCore.License objLic = objSMS.License();
            objLic.Company = mySettings.rUbo7SbaeKZQuaCKmF;
            objLic.LicenseType = mySettings.C7sjZ9SZ6BQmFMTuFx;
            objLic.Key = mySettings.fCoBhMKoYAWRCwhoB;
            notifications.PopulateNoti();
            //objSMS.NewDeliveryReport += new mCore.SMS.NewDeliveryReportEventHandler(this.objSMS_NewDeliveryReport);
            //objSMS.NewIncomingCall += new mCore.SMS.NewIncomingCallEventHandler(this.objSMS_NewIncomingCall);
            //objSMS.QueueSMSSent += new mCore.SMS.QueueSMSSentEventHandler(this.objSMS_QueueSMSSent);
            //objSMS.QueueSMSSending += new mCore.SMS.QueueSMSSendingEventHandler(this.objSMS_QueueSMSSending);
            //objSMS.NewUSSDReceived += new mCore.SMS.NewUSSDReceivedEventhandler(this.objSMS_NewUSSDReceived);
        }

        private static bool indecateNewMessageReceived;
        public static bool IndecateNewMessageReceived
        {
            get { return indecateNewMessageReceived; }
            set { indecateNewMessageReceived = value; }
        }

        private string returnMsg = string.Empty;
        public string errorMessages
        {
            get { return returnMsg; }
        }

        private int inboxCounted = 0;
        public int InboxCount
        {
            get { return inboxCounted; }
        }

        private bool isConnectedToPort = false;
        public bool IsConnected
        {
            get { return isConnectedToPort; }
        }

        public string SendDelay
        {
            get { return (objSMS.SendDelay / 1000).ToString(); }
        }

        public string SendRetry
        {
            get { return objSMS.SendRetry.ToString(); }
        }

        public string Timeout
        {
            get { return (objSMS.Timeout / 1000).ToString(); }
        }

        public string SMSC
        {
            get { return objSMS.SMSC; }
        }

        public string Network
        { 
             get { return objSMS.Network; }
        }

        public string OwnNumber
        {
            get 
            {
                string number = string.Empty;
                try
                {
                    number = objSMS.OwnNumber.Replace("++63", "0");
                }
                catch (Exception ex)
                { 
                    number = ""; 
                }

                if (number.Length > 0)
                {
                    if (number.Substring(0, 2) == "63")
                    {
                        number = "[0" + number.Substring(2, 10) + "]";
                    }
                    else if (number.Substring(0, 3) == "+63")
                    {
                        number = "[0" + number.Substring(3, 10) + "]";
                    }
                    else
                    {
                        number = "[" + number.Replace("++63", "0") + "]";
                    }
                }
                else
                {
                    number = "";
                }
                return number;
            }
        }

        public int BaudRate0
        {
            get { return ((int)objSMS.BaudRate); }
        }

        public int DataBits0
        {
            get { return ((int)objSMS.DataBits); }
        }

        public int Parity0
        {
            get { return (int)objSMS.Parity; }
        }

        public int StopBits0
        {
            get { return (int)objSMS.StopBits - 1; }
        }

        public int FlowControl0
        {
            get { return (int)objSMS.FlowControl; }
        }

        public int SignalStrength
        {
            get { return objSMS.SignalStrength; }
        }

        public string Manufacturer
        {
            get
            {
                string msg = string.Empty;
                try
                {
                    msg =  objSMS.Manufacturer;
                }
                catch (mCore.GeneralException ex)
                {
                    msg = "N/A";
                }
                return msg;
            }
        }

        public string Model
        {
            get
            {
                string msg = string.Empty;
                try
                {
                    msg = objSMS.Model;
                }
                catch (mCore.GeneralException ex)
                {
                    msg = "N/A";
                }
                return msg;
            }
        }

        public string Revision
        {
            get
            {
                string msg = string.Empty;
                try
                {
                    msg = objSMS.Revision;
                }
                catch (mCore.GeneralException ex)
                {
                    msg = "N/A";
                }
                return msg;
            }
        }

        public string IMEI
        {
            get
            {
                string msg = string.Empty;
                try
                {
                    msg = objSMS.IMEI;
                }
                catch (mCore.GeneralException ex)
                {
                    msg = "N/A";
                }
                return msg;
            }
        }

        public string IMSI
        {
            get
            {
                string msg = string.Empty;
                try
                {
                    msg = objSMS.IMSI;
                }
                catch (mCore.GeneralException ex)
                {
                    msg = "N/A";
                }
                return msg;
            }
        }

        public string BatteryLevel
        {
            get
            {
                int BattLevel = -1;
                string msg = string.Empty;
                try
                {
                    BattLevel = objSMS.BatteryLevel;
                }
                catch (mCore.GeneralException ex)
                {
                    msg = "N/A";
                }
                if (BattLevel > 0)
                {
                    msg = BattLevel.ToString() + "%";
                }
                else
                {
                    msg = "Unknown";
                }
                return msg;
            }
        }

        public bool SetCommParameters()
        {
            initialize();
            isConnectedToPort = false;
            try
            {
                if (!objSMS.IsConnected && port != string.Empty)
                {
                    if (baudrate == string.Empty)
                    { baudrate = "115200"; }
                    if (DataBits == string.Empty)
                    { DataBits = "8"; }

                    objSMS.Port = port;
                    objSMS.BaudRate = (mCore.BaudRate)(Convert.ToInt32(baudrate));
                    objSMS.DataBits = (mCore.DataBits)(Convert.ToInt32(DataBits));
                    objSMS.Parity = (mCore.Parity)Parity;
                    objSMS.StopBits = (mCore.StopBits)(StopBits + 1);
                    objSMS.FlowControl = (mCore.FlowControl)FlowControl;
                    objSMS.DisableCheckPIN = enableCheckPIN;
                }

                if (objSMS.Connect())
                {
                    objSMS.NewMessageIndication = IndecateNewMessageReceived;
                    objSMS.MessageMemory = mCore.MessageMemory.SM;
                    returnMsg = "SUCCESS";
                    isConnectedToPort = true;
                    return true;
                }
                else
                {
                    returnMsg = "ISN'T SUCCESS";
                }
                
            }
            catch (mCore.GeneralException ex)
            {
                returnMsg = ex.Message;
            }
            return false;
        }

        public bool Disconnect()
        {
            bool isDisConnect = false;
            try
            {
                objSMS.Disconnect();
                isDisConnect = true;
                isConnectedToPort = false;
            }
            catch (mCore.GeneralException ex)
            {
                returnMsg = ex.Message;
            }
            catch (Exception ex)
            {
                returnMsg = ex.Message;
            }
            return isDisConnect;
        }

        public bool sendMessage(string phone, string message, bool isAlertText)
        {
            bool isStat = false;
            if (phone.Trim().Length <= 0)
            {
                returnMsg =  "Invalid Mobile Number";
                return isStat;
            }

            try
            {
                objSMS.Validity = "24H";
                returnMsg  = "Message sent!\r\n\r\n[ Message Ref.: " + objSMS.SendSMS(phone.Replace("+63", "0"), message, isAlertText) + " ]";
                isStat = true;
            }
            catch (mCore.SMSSendException ex)
            {
                returnMsg = ex.Message;
            }
            catch (mCore.GeneralException ex)
            {
                returnMsg = ex.Message;
            }
            catch (Exception ex)
            {
                returnMsg = ex.Message;
            }
            return isStat;
        }

        public string sendMessageLoop(string phone, string message, int count)
        {
            string strSendResult = string.Empty;
            int ctr = 0;
            int ctrfailed = 0;
            try
            {
                for (int i = 1; i <= count; i++)
                {
                    Thread.Sleep(1);
                    Application.DoEvents();
                    objSMS.Validity = "24H";
                    strSendResult = "Message sent!\r\n\r\n[ Message Ref.: " + objSMS.SendSMS(phone.Replace("+63", "0"), message, false) + " ]";
                    ctr++;
                }
            }
            catch (Exception ex)
            {
                ctrfailed++;
            }
            return ctr.ToString() + " Message Sent" + Environment.NewLine + ctrfailed.ToString() + " Messaged Failed";
        }

        public string sendMessageToMany(string importFile, string message, bool isExcelFile)
        {
            string strSendResult = string.Empty;
            int ctr = 0;
            int ctrfailed = 0;
            try
            {
                if (importFile == string.Empty)
                {
                    return "No text file path...";
                }

                ReadIO  readIO = new ReadIO ();
                readIO.textFilePath = importFile;

                if (isExcelFile == false)
                {
                    foreach (string list in readIO.importTextFile())
                    {
                        Thread.Sleep(1);
                        Application.DoEvents();
                        objSMS.Validity = "24H";
                        strSendResult = "Message sent!\r\n\r\n[ Message Ref.: " + objSMS.SendSMS(list.Replace("+63", "0"), message, false) + " ]";
                        ctr++;
                    }
                }
                
            }
            catch (Exception ex)
            {
                ctrfailed++;
            }
            return ctr.ToString() + " Message Sent" + Environment.NewLine + ctrfailed.ToString() + " Messaged Failed";
        }

        public string textForwarding(string[] number, string message)
        {
            string strSendResult = string.Empty;
            int ctr = 0;
            int ctrfailed = 0;
            try
            {
                for (int i = 0; i <= number.Length - 1; i++)
                {
                    Thread.Sleep(100);
                    Application.DoEvents();
                    objSMS.Validity = "24H";
                    strSendResult = "Message sent!\r\n\r\n[ Message Ref.: " + objSMS.SendSMS(number[i].Replace("+63", "0"), message, false) + " ]";
                    ctr++;
                }
            }
            catch (Exception ex)
            {
                ctrfailed++;
            }
            return ctr.ToString() + " Message Sent" + Environment.NewLine + ctrfailed.ToString() + " Messaged Failed";
        }

        public delegate void NewMessageReceivedEventHandler(object sender, NewMessageReceivedEventArgs e);
        public event NewMessageReceivedEventHandler NewMessageReceived;
        private void objSMS_NewMessageReceived(object sender, mCore.NewMessageReceivedEventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            MessageProperties.Phone = e.Phone.Replace("+63", "0");
            string messages = "Trial Version";
            if (Vrification.isLicense() == true)
            {
                messages = e.TextMessage;
            }
            MessageProperties.TextMessage = messages;
            MessageProperties.SMSC = e.SMSC;
            MessageProperties.TimeStamp = e.TimeStamp;
            MessageProperties.TimeStampRFC = e.TimeStampRFC;
            MessageProperties.TimeZone = e.TimeZone;
            MessageProperties.TotalParts = e.TotalParts;
            MessageProperties.ReferenceNumber = e.ReferenceNumber;
            MessageProperties.SequenceNumber = e.SequenceNumber;
            NewMessageReceived(this, new NewMessageReceivedEventArgs());
            Control.CheckForIllegalCrossThreadCalls = true;
        }

        InboxMessagesCollection inboxMessageList;
        public InboxMessagesCollection Inbox()
        {
            RefreshInbox();
            return inboxMessageList;
        }

        public void RefreshInbox()
        {
            int i = 0;
            inboxCounted = 0;
            mCore.Inbox objInbox = objSMS.Inbox();

            try
            {
                objInbox.Refresh();
                inboxMessageList = new InboxMessagesCollection();
                foreach (mCore.Message Msg in objInbox)
                {
                    InboxMessages inboxMsg = new InboxMessages();
                    inboxMsg.Phone = Msg.Phone;
                    string messages = "Trial Version";
                    if (Vrification.isLicense() == true)
                    {
                        messages = Msg.Text;
                    }
                    inboxMsg.TextMessage = messages;
                    inboxMsg.TimeStamp = Msg.TimeStamp;
                    inboxMessageList.Add(inboxMsg);
                    i++;
                }
                inboxCounted = i;
            }
            catch (mCore.SMSReadException ex)
            {
                returnMsg = ex.Message;
            }
            catch (mCore.GeneralException ex)
            {
                returnMsg = ex.Message;
            }
            catch (Exception ex)
            {
                returnMsg = ex.Message;
            }
        }

        public InboxMessagesCollection Message(int index)
        {
            mCore.Inbox objInbox = objSMS.Inbox();
            mCore.Message Msg;
            try
            {
                Msg = objInbox.Message(index);
                inboxMessageList = new InboxMessagesCollection();
                InboxMessages msg = new InboxMessages();
                msg.Phone = Msg.Phone;
                msg.TextMessage = Msg.Text;
                msg.TimeStamp = Msg.TimeStamp;
                inboxMessageList.Add(msg);

                returnMsg = "Read Success";
                return inboxMessageList;
            }
            catch (mCore.SMSReadException ex)
            {
                returnMsg = ex.Message;
            }
            catch (mCore.GeneralException ex)
            {
                returnMsg = ex.Message;
            }
            catch (Exception ex)
            {
                returnMsg = ex.Message;
            }
            return null;
        }

        public bool Delete(int count, bool all)
        {
            bool isDeleted = false;
            try
            {
                if (all)
                {
                    for (int x = 1; x <= count; x++)
                    {
                        Thread.Sleep(2);
                        Application.DoEvents();
                        objSMS.Inbox().Message(x).Delete();
                    }
                }
                else
                {
                    objSMS.Inbox().Message(count).Delete();
                }
                isDeleted = true; 
            }
            catch (mCore.SMSDeleteException ex)
            {
                returnMsg = ex.Message;
            }
            catch (mCore.GeneralException ex)
            {
                returnMsg = ex.Message;
            }
            catch (Exception ex)
            {
                returnMsg = ex.Message;
            }
            return isDeleted;
        }

        public bool EnableConcatenate(bool IsEnable)
        {
            objSMS.Inbox().Concatenate = IsEnable;
            return objSMS.Inbox().Concatenate;
        }

        public bool EnableNewMessageIndication(bool IsEnable)
        {
            if (objSMS.Inbox().Concatenate == IsEnable)
            {
                objSMS.NewMessageIndication = IsEnable;
            }
            return objSMS.NewMessageIndication;
        }

    }

    public class MessageProperties
    {
        public static string Phone = string.Empty;
        public static string TextMessage = string.Empty;
        public static string SMSC = string.Empty;
        public static DateTime TimeStamp;
        public static string TimeStampRFC = string.Empty;
        public static int TimeZone;
        public static int TotalParts;
        public static int ReferenceNumber;
        public static int SequenceNumber;
    }

    public class InboxMessages
    {
        private string phone;
        private string textMessage;
        private string smsc;
        private DateTime timeStamp;
        private string timeZone;
        private string totalParts;
        private string referenceNumber;
        private string sequenceNumber;

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public string TextMessage
        {
            get { return textMessage; }
            set { textMessage = value; }
        }

        public string SMSC
        {
            get { return smsc; }
            set { smsc = value; }
        }

        public DateTime TimeStamp
        {
            get { return timeStamp; }
            set { timeStamp = value; }
        }

        public string TimeZone
        {
            get { return timeZone; }
            set { timeZone = value; }
        }

        public string TotalParts
        {
            get { return totalParts; }
            set { totalParts = value; }
        }

        public string ReferenceNumber
        {
            get { return referenceNumber; }
            set { referenceNumber = value; }
        }

        public string SequenceNumber
        {
            get { return sequenceNumber; }
            set { sequenceNumber = value; }
        }
    }
}