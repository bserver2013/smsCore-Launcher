using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using smsCoreLib.Properties;
using smsCoreLib.smslicense;

namespace smsCore
{
    public class License
    {
        static string company = string.Empty;
        public static string Company
        {
            get { return company; }
            set { company = value; }
        }
        static string licenseType = string.Empty;
        public static string LicenseType
        {
            get { return licenseType; }
            set { licenseType = value; }
        }

        static string licensekey = string.Empty;
        public static string Licensekey
        {
            get { return licensekey; }
            set { licensekey = value; }
        }

        static bool isActivated = false;
        public static bool IsActivated
        {
            get { return isActivated; }
            set { isActivated = value; }
        }
    }

    public class Verification
    {
        static license li;
        static Settings st = new Settings();
        static string companyMsg = string.Empty;
        public static string CompanyMsg
        {
            get { return companyMsg; }
            set { companyMsg = value; }
        }

        static string typeMsg = string.Empty;
        public static string TypeMsg
        {
            get { return typeMsg; }
            set { typeMsg = value; }
        }

        static string keyMsg = string.Empty;
        public static string KeyMsg
        {
            get { return keyMsg; }
            set { keyMsg = value; }
        }

        static string role = string.Empty;
        public static string Role
        {
            get { return role; }
            set { role = value; }
        }

        public static bool license()
        {
            if (st.IsActivated)
            {
                if (st.lCompany == License.Company)
                {
                    if (st.lType == License.LicenseType)
                    {
                        if (st.lKey == License.Licensekey)
                        {
                            return true;
                        }
                    }
                }
            }

            if (checkOffline())
            {
                st.IsActivated = false;
                st.Save();
                return true;
            }
            else
            {
                st.IsActivated = false;
                st.Save();
                return checkOnline(License.Company, License.LicenseType, License.Licensekey);
            }
        }

        public static bool checkOnline(string company, string type, string key)
        {
            li = new license();
            try
            {
                return li.smsCore(company, type, key);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool checkOffline()
        {
            DataRow[] result = licenseDataModel.GetTable().Select("status = 'true' AND company = '" + License.Company + "'");
            foreach (DataRow row in result)
            {
                if (row["company"].ToString() == License.Company)
                {
                    if (row["type"].ToString() == License.LicenseType)
                    {
                        if (row["key"].ToString() == License.Licensekey)
                        {
                            KeyMsg = row["company"].ToString();
                            TypeMsg = row["type"].ToString();
                            CompanyMsg = row["key"].ToString();
                            Role = row["role"].ToString();
                            return (bool)row["status"];
                        }
                        else
                        {
                            KeyMsg = "Please check your license key.";
                        }
                    }
                    else
                    {
                        TypeMsg = "Please check your license type.";
                    }
                }
                else
                {
                    CompanyMsg = "Please check your license name.";
                }
            }
            return false;
        }
    }

    public static class notifications
    {
        static NotifyIcon Noti = new NotifyIcon();
        static smsCore.SMS objSMS = new smsCore.SMS();
        static string messageBox = string.Empty;
        static Settings st = new Settings();
        //public static string isLicense = "Trial Version";

        private static string status()
        {
            License.IsActivated = false;
            messageBox = "License Type: Trial Version" +
                                Environment.NewLine +
                                "Copyright @ " + DateTime .Now.Year;
            if (Verification.license())
            {
                messageBox = "Registered to: " + License.Company +
                            Environment.NewLine +
                            "License Type: " + License.LicenseType +
                                Environment.NewLine +
                                "Copyright @ " + DateTime.Now.Year;
                License.IsActivated = true;
                st.lCompany = License.Company;
                st.lType = License.LicenseType;
                st.lKey = License.Licensekey;
                st.IsActivated = true;
                st.Save();
            }
            return messageBox;
        }

        public static void PopulateNoti()
        {
            string result = status();
            string appTitle = objSMS.MyAppName + " [ .NET SMS Library ]";
            if (!License.IsActivated)
            {
                appTitle = objSMS.MyAppName + " [ Trial ]";
            }
            Noti.Text = objSMS.MyAppName;
            Noti.BalloonTipTitle = appTitle;
            Noti.BalloonTipText = result;
            Noti.BalloonTipIcon = ToolTipIcon.Info;
            Noti.Icon = smsCoreLib.Properties.Resources.smsCore;
            Noti.Visible = true ;
            Noti.ShowBalloonTip(30000);
            Noti.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(notifyIcon1_MouseDoubleClick);
        }

        private static void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            messageBox = objSMS.MyAppName + " [ Trial ]" +
                                Environment.NewLine +
                                "License Type: Trial Version" +
                                Environment.NewLine +
                                "Copyright @ " + DateTime.Now.Year;
            if (Verification.license())
            {
                messageBox = objSMS.MyAppName + " [ .NET SMS Library ]" +
                                Environment.NewLine +
                            "Registered to: " + License.Company +
                            Environment.NewLine +
                            "License Type: " + License.LicenseType +
                                Environment.NewLine +
                                "Copyright @ " + DateTime.Now.Year;
            }
            MessageBox.Show(messageBox,
               "smsCore™ Informations",
               MessageBoxButtons.OK,
               MessageBoxIcon.Information,
               MessageBoxDefaultButton.Button1);
        }
    }
}
