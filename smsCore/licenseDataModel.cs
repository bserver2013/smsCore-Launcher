using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.Services;


namespace smsCore
{
    public class licenseDataModel
    {
        
        public static DataTable GetTable()
        {
            DataTable table = new DataTable("smsCoreModel");
            table.Columns.Add("lid", typeof(int));
            table.Columns.Add("company", typeof(string));
            table.Columns.Add("type", typeof(string));
            table.Columns.Add("key", typeof(string));
            table.Columns.Add("role", typeof(string));
            table.Columns.Add("status", typeof(bool));

            table.Rows.Add(1, "kingpauloaquino@mail.com", "Pro-Standard", "97YE-YWO4-FYA8-SCNS", "Developer", true);
            table.Rows.Add(2, "EFREN.R", "Lite-Standard", "K3F9-WMQA-UTMD-VX9W", "Client", true);
            table.Rows.Add(3, "smsCore™ .NET SMS Library", "Lite-Standard", "IQMN-8D52-LXR2-Z77Y", "Client", true);
            return table;
        }

    }
}
