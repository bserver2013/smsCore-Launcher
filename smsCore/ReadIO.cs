using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace smsCore
{
    public class ReadIO
    {
        public string textFilePath = string.Empty;
        public List<String> importTextFile()
        {
            List<String> arrayList = new List<String>();
            using (FileStream fStream = File.OpenRead(textFilePath))
            {
                using (TextReader reader = new StreamReader(fStream))
                {
                    string line = string .Empty;
                    while (!String.IsNullOrEmpty(line = reader.ReadLine()))
                    {
                        arrayList.Add(line);
                    }
                }
            }
            return arrayList;
        }
    }
}
