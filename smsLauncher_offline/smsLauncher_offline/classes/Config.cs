using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Windows .Forms;
using System.Net.NetworkInformation;
using smsLauncher_offline.Properties;
using System.Net;

namespace smsLauncher_offline
{
    public class Config
    {
        public static bool WebService_IsAvailable
        {
            get
            {
                Settings st = new Settings();
                WebClient client = new WebClient();
                try
                {
                    using (client.OpenRead(st.smsLauncher_offline_smsCoreApi_smsCore_api))
                    {
                        return true;
                    }
                }
                catch (WebException)
                {
                    return false;
                }
            }
        }

       
    }
}
