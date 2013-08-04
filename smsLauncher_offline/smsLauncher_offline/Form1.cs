using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using smsLauncher_offline.model;

namespace smsLauncher_offline
{
    public partial class Form1 : Form
    {
        SMSDataClassesDataContext db;
        public Form1()
        {
            InitializeComponent();

            NotiMe("The smsLauncher Sync Offline takes care of syncing and notifying you of remote changes.");

            timer1.Interval = 1000;
            timer1.Enabled = true;

            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            Hide();
        }

        public void NotiMe(string message)
        {
            Noti.Text = "smsLauncher Sync Offline 1.0";
            Noti.BalloonTipTitle = "smsLauncher Sync Offline 1.0";
            Noti.BalloonTipText = message;// "The smsLauncher Sync Offline takes care of syncing and notifying you of remote changes";
            Noti.BalloonTipIcon = ToolTipIcon.Info;
            Noti.Icon = smsLauncher_offline.Properties.Resources.Visualpharm_Icons8_Metro_Style_Logos_Maestro;
            Noti.Visible = true;
            Noti.ShowBalloonTip(30000);
        }

        int interval = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (interval == 5)
            {
                Thread.Sleep(10);
                timer1.Enabled = false;
                Thread.Sleep(10);

                if (Query.Check())
                {
                    if (Config.WebService_IsAvailable)
                    {
                        NotiMe(Query.TotalQueued + " message has been found in queue and its being processed.");
                        Query.Do();
                        NotiMe(Query.TotalQueued + " message was successfully updated.");
                    }
                }

                interval = 0;
                timer1.Enabled = true;
            }
            interval++;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
