using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace kpa.developers.helper
{
    public  class IO
    {
       public static ArrayList ImportTextFile(string pathFile)
       {
           string line;
           ArrayList number = new ArrayList();
           System.IO.StreamReader file = new System.IO.StreamReader(pathFile);
           while ((line = file.ReadLine()) != null)
           {
               number.Add(line);
           }
           file.Close();
           return number;
       }
    }
}
