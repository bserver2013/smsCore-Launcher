using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using smsLauncher.Properties;
using smsLauncher.model;
using smsLauncher.sms_api;

namespace smsLauncher
{
    public partial class frmMain : Form
    {
        SMSDataClassesDataContext db;
        static smsCore.SMS objSMS = new smsCore.SMS();
        Thread signalThread;
        Thread saveInboxThread;
        Thread processThread;
        Thread sendThread;
        Thread startThread;
        Thread receivedThread;
        bool IsStarted = false;
        static System.Timers.Timer t;

        smsCore_api api = new smsCore_api();
        Settings settings = new Settings();

        public frmMain()
        {
            InitializeComponent();
        }

        public static bool disconnect
        {
            get { config.IsConnected = false; return objSMS.Disconnect(); }
        }

        public bool connectToModem()
        {
            config.log(txtTerminal, "Checking configuration...");
            if (cbPort.Text != "COMPORT")
            {
                objSMS.port = cbPort.Text;
                objSMS.baudrate = "115200";
                objSMS.DataBits = "8";
                objSMS.Parity = 0;
                objSMS.StopBits = 0;
                objSMS.FlowControl = 1;

                if (cbType.SelectedIndex != (int)config.type.SENDER_ONLY)
                {
                    smsCore.SMS.IndecateNewMessageReceived = true;
                }
                
                config.log(txtTerminal, "Connecting...");
                if (objSMS.SetCommParameters())
                {
                    config.IsConnected = true;
                    if (smsCore.SMS.IndecateNewMessageReceived)
                    {
                        objSMS.NewMessageReceived += new smsCore.SMS.NewMessageReceivedEventHandler(objSMS_NewMessageReceived);
                    }
                    lblNumber.Text = objSMS.OwnNumber;
                    config.log(txtTerminal, "Connected!");
                    lblStatus.Text = "Connected at " + cbPort.Text;
                    return true;
                }
            }
            config.log(txtTerminal, "Modem couldn't found!");
            return false;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            txtTerminal.Text = "Offline" + config.newLine;
            config.dropdowns(cbPort, cbType, cbOutbound, cbNetwork);

            smsCore.License.Company = "kingpauloaquino@mail.com";
            smsCore.License.LicenseType = "Pro-Standard";
            smsCore.License.Licensekey = "97YE-YWO4-FYA8-SCNS";

            if (!config.Runs_SMSOffline())
            {
                try
                {
                    System.Diagnostics.Process.Start(@"offline\smsLauncher_offline.exe");
                }
                catch (Exception ex)
                { }
            }

            signalTimer.Tick += delegate { onTimerTick(); };
        }

        bool isStoped = false;
        private void onTimerTick()
        {
            Thread.Sleep(10);
            stopTimer(false, "signal");
            signalStatus();

            if (isStoped)
            {
                Application.DoEvents();
                pbSignal.Image = Resources.no_signal;
                lblNetwork.Text = "Offline";
            }
            else
            {
                stopTimer(true, "signal");
            }
        }

        public void signalStatus()
        {
            pbSignal.Image = config.signalStatus(objSMS.SignalStrength);
            Thread.Sleep(10);
            networkText(config.networkName(objSMS.Network));

            if (IsStarted)
            {
                sendMethod();
            }
        }

        delegate void setNetworkCallBack(string text);
        private void networkText(string text)
        {
            if (this.lblNetwork.InvokeRequired)
            {
                setNetworkCallBack d = new setNetworkCallBack(networkText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                lblNetwork.Text = text;
            }
        }

        private void control(bool input)
        {
            cbPort.Enabled = input;
            cbType.Enabled = input;
            cbNetwork.Enabled = input;
            cbOutbound.Enabled = input;
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbType.SelectedIndex == (int)config.type.RECEIVER_ONLY)
            {
                cbNetwork.Enabled = false;
                cbOutbound.Enabled = false;
            }
            else
            {
                cbPort.Enabled = true;
                cbNetwork.Enabled = true;
                cbOutbound.Enabled = true;
                btnOn.Enabled = true;
                btnStart.Enabled = false;
            }
        }

        public void checkingFirst()
        {
            if (cbPort.SelectedIndex != (int)config.port.COM)
            {
                if (cbType.SelectedIndex != (int)config.type.TYPE)
                {
                    if (cbOutbound.SelectedIndex != (int)config.outbound.OUTBOUND)
                    {
                        if (cbNetwork.SelectedIndex != (int)config.network.NETWORK)
                        {
                            if (connectToModem())
                            {
                                if (cbType.SelectedIndex != (int)config.type.RECEIVER_ONLY)
                                {
                                    btnStart.Enabled = true;
                                    btnOn.Text = "OFF";
                                }
                            }
                        }
                        else
                        {
                            config.log(txtTerminal, "Check network!");
                        }
                    }
                    else
                    {
                        config.log(txtTerminal, "Check outbound!");
                    }
                }
                else
                {
                    config.log(txtTerminal, "Check type!");
                }
            }
            else
            {
                config.log(txtTerminal, "Check comport!");
            }
        }

        private void btnOn_Click(object sender, EventArgs e)
        {
            if (btnOn.Text == "ON")
            {
                if (cbType.SelectedIndex != (int)config.type.RECEIVER_ONLY)
                {
                    checkingFirst();
                }
                else
                {
                    connectToModem();
                }

                btnOn.Text = "OFF";
                if (objSMS.IsConnected)
                {
                    Thread.Sleep(10);
                    if (objSMS.InboxCount > 0)
                    {
                        for (int x = 0; x < objSMS.InboxCount; x++)
                        {
                            objSMS.Delete(x, false);
                        }
                    }
                    Thread.Sleep(10);
                    isStoped = false;
                    stopTimer(true, "signal");
                }
            }
            else
            {
                config.log(txtTerminal, "Disconnecting...");
                if (disconnect)
                {
                    Thread.Sleep(10);
                    btnStart.Enabled = false;
                    btnOn.Text = "ON";
                    isStoped = true;
                    config.log(txtTerminal, "Disconnected!");
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "START")
            {
                Thread.Sleep(200);
                control(false);
                btnStart.Text = "STOP";
                config.Types = cbType.SelectedIndex;
                config.outBound = cbOutbound.Text;
                config.Networks = cbNetwork.SelectedIndex;
                if (!IsStarted)
                {
                    IsStarted = true;
                }
            }
            else
            {
                Thread.Sleep(200);
                control(true);
                btnStart.Text = "START";
                if (IsStarted)
                {
                    IsStarted = false;
                }
                
            }
        }

        public void stopTimer(bool input, string stats)
        {
            switch (stats)
            {
                case "signal":
                    signalTimer.Enabled = input;
                    break;
                case "process":
                    processTimer.Enabled = input;
                    break;
                case "all":
                    signalTimer.Enabled = input;
                    processTimer.Enabled = input;
                    break;
            }
        }

        delegate void SetTextCallback(string text);
        private void SetText(string text)
        {
            if (this.txtTerminal.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                txtTerminal.Text += "sms@launcher: " + text + Environment.NewLine;
                txtTerminal.Select(txtTerminal.Text.Length, 0);
                txtTerminal.ScrollToCaret();
                txtTerminal.ReadOnly = true;
            }
        }

        private void objSMS_NewMessageReceived(object sender, smsCore.NewMessageReceivedEventArgs e)
        {
            Console.Write(Environment.NewLine + "New Message " + e.Phone + Environment.NewLine + config.AppName);
            smsCore.smsCoreController.CheckForIllegalCrossThreadCalls = false;

            checkMessage.Number = e.Phone;
            checkMessage.Message = e.TextMessage;
            Thread.Sleep(10);

            receivedThread = new Thread(saveInbox);
            receivedThread.Start();

            smsCore.smsCoreController.CheckForIllegalCrossThreadCalls = true;
        }

        private void saveInbox()
        {
            string tm = DateTime.Now.ToLongTimeString();
            SetText("Received " + tm);
            config.log("Received " + tm + " " + checkMessage.Number);
            Thread.Sleep(10);

            if (WebService.IsAvailable)
            {
                api.sendMethodWithValidation(checkMessage.Number, checkMessage.Message, settings.Launcher_ID);
            }
            else
            {
                receivedMessage.save(checkMessage.Number, checkMessage.Message);
            }
            
            Thread.Sleep(10);
            objSMS.Delete(1, false);
        }

        public void sendMethod()
        {
            smsCore.smsCoreController.CheckForIllegalCrossThreadCalls = false;
            if (!WebService.IsAvailable)
            {
                if (checkMessage.now())
                {
                    bool isOK = false;
                    string m = "Message isn't sent!";
                    if (config.Network(config.Networks, checkMessage.Number ))
                    {
                        if (config.getLastDigit(checkMessage.Number, config.outBound))
                        {
                            SetText("It's being updated...");
                            config.log("It's being updated...");
                            if (objSMS.sendMessage(checkMessage.Number, checkMessage.Message, false))
                            {
                                isOK = true;
                                m = "Message sent!";
                            }
                        }
                    }
                    SetText(m);
                    config.log(m);
                    validatorMessage.saveToSentItemThenDelete(checkMessage.Id, isOK);
                    config.log(objSMS.errorMessages);
                }
                return;
            }
            
            if (api.GetAllMessage() != null)
            {
                bool isOK = false;
                string m = "Message isn't sent!";
                foreach (var msg in api.GetAllMessage())
                {
                   if (config.Network(config.Networks, msg.Number))
                   {
                       if (config.getLastDigit(msg.Number, config.outBound))
                        {
                            SetText("It's being updated...");
                            config.log("It's being updated...");
                            if (objSMS.sendMessage(msg.Number, msg.Message, false))
                            {
                                isOK = true;
                                m = "Message sent!";
                            }
                        }
                   }
                   SetText(m);
                   config.log(m);
                   api.SentItems(msg.Id, isOK);
                   config.log(objSMS.errorMessages);
                }
            }
            smsCore.smsCoreController.CheckForIllegalCrossThreadCalls = true;
        }
    }
}
