using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;
using smsLauncher.Properties;
using System.Drawing;
using System.Security.Cryptography;
using smsLauncher.model;
using smsCore.Developement.Component;

namespace smsLauncher
{
    public class config
    {
        static SMSDataClassesDataContext db;
        public static string AppName
        {
            get { return "kingpauloaquino@mail.com: "; }
        }

        public static string newLine
        {
            get { return Environment.NewLine; }
        }

        public static DateTime receivedDateTime()
        {
            return convert.DateFormat(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
        }

        public static string subString(string input, int index, int lenght)
        {
            TextBox txt = new TextBox();
            txt.Text = input;
            txt.Select(index, lenght);
            return txt.SelectedText.Trim();
        }

        static string _outBound = string.Empty;
        public static string outBound
        {
            get { return _outBound; }
            set { _outBound = value; }
        }

        static int _type = 0;
        public static int Types
        {
            get { return _type; }
            set { _type = value; }
        }

        static int _network = 0;
        public static int Networks
        {
            get { return _network; }
            set { _network = value; }
        }

        static bool isConnected;
        public static bool IsConnected
        {
            get { return isConnected; }
            set { isConnected = value; }
        }

        public static bool Type(int input)
        {
            bool cases = false;
            switch (input)
            {
                case 2: cases = true; break;
                case 4: cases = true; break;
                case 5: cases = true; break;
                case 7: cases = true; break;
            }
            return cases;
        }

        public static void dropdowns(ComboBox port, ComboBox type, ComboBox outbound, ComboBox network)
        {
            string[] ports = SerialPort.GetPortNames();
            port.Items.Add("COMPORT");
            foreach(string p in ports)
            {
                port.Items.Add(p);
            }
            port.SelectedIndex = 0;
            type.SelectedIndex = 0;
            outbound.SelectedIndex = 0;
            network.SelectedIndex = 0;
        }

        public static string networkName(string input)
        {
            switch (input)
            {
                case "51503":
                    return "SMART";
                case "51502":
                    return "GLOBE";
                case null:
                    return "No Network";
                default:
                    return input;
            }
        }

        public static Bitmap signalStatus(int x)
        {
            Bitmap img = new Bitmap(Resources.no_signal);
            if (x != 0)
            {
                if (x <= 20)
                {
                    img = new Bitmap(Resources._1bar);
                }
                else if (x <= 40)
                {
                    img = new Bitmap(Resources._2bars);
                }
                else if (x <= 60)
                {
                    img = new Bitmap(Resources._3bars);
                }
                else if (x <= 80)
                {
                    img = new Bitmap(Resources._4bars);
                }
                else if (x <= 100)
                {
                    img = new Bitmap(Resources._5bars);
                }
            }
            return img;
        }

        public static void log(TextBox textbox, string input)
        {
            textbox.Text += "sms@launcher: " + input + newLine;
            textbox.Select(textbox.Text.Length, 0);
            textbox.ScrollToCaret();
            textbox.ReadOnly = true;

            System.Threading.Thread.Sleep(10);
            log(input);
        }

        public static void log(string log)
        {
            try
            {
                string filename = "message\\" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + ".log";
                if (!Directory.Exists("message"))
                {
                    Directory.CreateDirectory("message");
                }
                using (StreamWriter writer = new StreamWriter(filename, true))
                {
                    writer.WriteLine(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + ": " + log);
                }
            }
            catch (Exception ex)
            { }
        }

        public enum port
        {
            COM
        }

        public enum outbound
        {
            OUTBOUND
        }

        public enum type
        {
            TYPE,
            SENDER_ONLY,
            RECEIVER_ONLY,
            ALL
        }

        public enum network
        {
            NETWORK,
            SMART_AND_TNT,
            GLOBE_AND_TM,
            SUN_CELLULAR,
            SMART_AND_GLOBE,
            GLOBE_AND_SUN,
            SUN_AND_SMART,
            ALL_NETWORK
        }

        public enum prefixe
        {
            invalid_number = 5,
            invalid_prefix= 4,
            sun = 3,
            globe = 2,
            smart = 1
        }

        public static bool Network(int index, string number)
        {
            int pre = prefixes(number);
            switch (index)
            {
                case (int)network.SMART_AND_TNT:
                    if (pre == 1)
                    {
                        return true;
                    }
                    break;
                case (int)network.GLOBE_AND_TM:
                    if (pre == 2)
                    {
                        return true;
                    }
                    break;
                case (int)network.SUN_CELLULAR:
                    if (pre == 3)
                    {
                        return true;
                    }
                    break;
                case (int)network.SMART_AND_GLOBE:
                    if (pre == 1 || pre == 2)
                    {
                        return true;
                    }
                    break;
                case (int)network.GLOBE_AND_SUN:
                    if (pre == 2|| pre == 3)
                    {
                        return true;
                    }
                    break;
                case (int)network.SUN_AND_SMART:
                    if (pre == 3 || pre == 1)
                    {
                        return true;
                    }
                    break;
                case (int)network.ALL_NETWORK:
                    return true;
            }
            return false;
        }

        public static int prefixes(string number)
        {
            int net = (int)prefixe.invalid_number;
          
            if (number.Length == 11)
            {
                switch (number.Substring(0, 4))
                {
                    case "0907":
                    case "0908":
                    case "0909":
                    case "0910":
                    case "0912":
                    case "0918":
                    case "0919":
                    case "0920":
                    case "0921":
                    case "0928":
                    case "0929":
                    case "0930":
                    case "0938":
                    case "0939":
                    case "0946":
                    case "0947":
                    case "0948":
                    case "0949":
                    case "0989":
                    case "0998":
                    case "0999":
                        net = (int)prefixe.smart;;
                        break;
                    case "0905":
                    case "0906":
                    case "0915":
                    case "0916":
                    case "0917":
                    case "0925":
                    case "0926":
                    case "0927":
                    case "0935":
                    case "0936":
                    case "0937":
                    case "0994":
                    case "0996":
                    case "0997":
                        net = (int)prefixe.globe;;
                        break;
                    case "0922":
                    case "0923":
                    case "0932":
                    case "0933":
                    case "0934":
                    case "0942":
                    case "0943":
                        net = (int)prefixe.sun;;
                        break;
                    default:
                        net = (int)prefixe.invalid_prefix;;
                        break;
                }
            }
            return net;
        }

        public static bool getLastDigit(string number, string outBound)
        {
            if (number.Length == 11)
            {
                string[] bound = outBound.Split(',');
                for (int i = 0; i < bound.Length; i++)
                {
                    if (config.subString(number, 10, 1) == bound[i])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static string format_currency(decimal amount)
        {
            return String.Format("{0:c}", amount).Replace("$", "");
        }

        public static DateTime dateNowWithTimeFormated()
        {
            string timeD = config.receivedDateTime().ToString("yyyy-MM-dd") + " " + DateTime.Now.ToLongTimeString();
            DateTime dc = Convert.ToDateTime(timeD);
            return dc;
        }

        public static bool Runs_SMSOffline()
        {
            var processss = from proc in System.Diagnostics.Process.GetProcesses() 
                            orderby proc.ProcessName 
                            ascending select proc;
            foreach (var item in processss)
            {
                if (item.ProcessName == "smsLauncher_offline")
                {
                    return true;
                }
            }
            return false;
        }

        //public static string format_currency(string amount)
        //{
        //    amount = amount + ".00";
        //    string newAmount = amount;
        //    switch (amount.Length)
        //    {
        //        case 7:
        //            newAmount = config.subString(amount, 0, 1) + "," + config.subString(amount, 1, 6);
        //            break;
        //        case 8:
        //            newAmount = config.subString(amount, 0, 2) + "," + config.subString(amount, 2, 6);
        //            break;
        //        case 9:
        //            newAmount = config.subString(amount, 0, 3) + "," + config.subString(amount, 3, 6);
        //            break;
        //        case 10:
        //            newAmount = config.subString(amount, 0, 1) + "," + config.subString(amount, 1, 3) + "," + config.subString(amount, 4, 6);
        //            break;
        //        case 11:
        //            newAmount = config.subString(amount, 0, 2) + "," + config.subString(amount, 2, 3) + "," + config.subString(amount, 5, 6);
        //            break;
        //        case 12:
        //            newAmount = config.subString(amount, 0, 3) + "," + config.subString(amount, 3, 3) + "," + config.subString(amount, 6, 6);
        //            break;
        //        case 13:
        //            newAmount = config.subString(amount, 0, 1) + "," + config.subString(amount, 1, 3) + "," + config.subString(amount, 4, 3) + "," + config.subString(amount, 7, 6);
        //            break;
        //        case 14:
        //            newAmount = config.subString(amount, 0, 2) + "," + config.subString(amount, 2, 3) + "," + config.subString(amount, 5, 3) + "," + config.subString(amount, 8, 6);
        //            break;
        //        case 15:
        //            newAmount = config.subString(amount, 0, 3) + "," + config.subString(amount, 3, 3) + "," + config.subString(amount, 6, 3) + "," + config.subString(amount, 9, 6);
        //            break;
        //    }
        //    return newAmount;
        //}

        static readonly Random _rng = new Random();
        const string _chars = "123456789987654321123654789987456321QWERTYUPASDFGHJKZXCVBNMMNBVCXZASDFGHJKPUYTREWQ";
        public static string generateReferenceNo(int size)
        {
            char[] buffer = new char[size];
            for (int i = 0; i < size; i++)
            {
                buffer[i] = _chars[_rng.Next(_chars.Length)];
            }
            return new string(buffer);
        }

        static string pass = "ABC21abc";
        public static string encrypt(string input)
        {
            RijndaelManaged AES = new RijndaelManaged();
            MD5CryptoServiceProvider Hash_AES = new MD5CryptoServiceProvider();
            string encrypted = "";
            try
            {
                byte[] hash = new byte[32];
                byte[] temp = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass));
                Array.Copy(temp, 0, hash, 0, 16);
                Array.Copy(temp, 0, hash, 15, 16);
                AES.Key = hash;
                AES.Mode = CipherMode.ECB;
                ICryptoTransform DESEncryptor = AES.CreateEncryptor();
                byte[] buffer = ASCIIEncoding.ASCII.GetBytes(input);
                encrypted = Convert.ToBase64String(DESEncryptor.TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (Exception ex)
            { }
            return encrypted;
        }

        public static string decrypt(string input)
        {
            RijndaelManaged AES = new RijndaelManaged();
            MD5CryptoServiceProvider Hash_AES = new MD5CryptoServiceProvider();
            string decrypted = "";
            try
            {
                byte[] hash = new byte[32];
                byte[] temp = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass));
                Array.Copy(temp, 0, hash, 0, 16);
                Array.Copy(temp, 0, hash, 15, 16);
                AES.Key = hash;
                AES.Mode = CipherMode.ECB;
                ICryptoTransform DESDecryptor = AES.CreateDecryptor();
                byte[] buffer = Convert.FromBase64String(input);
                decrypted = ASCIIEncoding.ASCII.GetString(DESDecryptor.TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (Exception ex)
            { }
            return decrypted;
        }
    }
}
