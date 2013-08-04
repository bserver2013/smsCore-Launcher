using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Net;
using smsLauncher.Properties;

namespace smsLauncher
{
    public class WebService
    {
        public static bool IsAvailable
        {
            get {
                Settings st = new Settings();
                WebClient client = new WebClient();
                try
                {
                    using (client.OpenRead(st.smsLauncher_sms_api_smsCore_api))
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
