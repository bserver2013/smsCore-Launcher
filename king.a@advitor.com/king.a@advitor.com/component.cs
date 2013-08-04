using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Collections;

namespace kpa.Developer.Controller.Collection
{
    public class convert
    {
        public static string intToString(int input)
        {
            return input.ToString();
        }

        public static Int16 stringToInt16(string input)
        {
            return Convert.ToInt16(input);
        }

        public static Int32 stringToInt32(string input)
        {
            return Convert.ToInt32(input);
        }

        public static Int64 stringToInt64(string input)
        {
            return Convert.ToInt64(input);
        }

        public static decimal intToDecimal(int input)
        {
            return Convert.ToDecimal(input);
        }

        public static DateTime stringToDateTime(string input)
        {
            return DateTime.Parse(input);
        }

        public static string DateFormat(string input)
        {
            return Convert.ToDateTime(input).ToString("yyyyMMddHHmmss");
        }

        public static string Encrypt(string input)
        {
            RijndaelManaged AES = new RijndaelManaged();
            MD5CryptoServiceProvider Hash_AES = new MD5CryptoServiceProvider();
            string encrypted = "";
            string pass = "ABC12abc";
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
            {}
            return encrypted;
        }

        public static string Decrypt(string input)
        {
            RijndaelManaged AES = new RijndaelManaged();
            MD5CryptoServiceProvider Hash_AES = new MD5CryptoServiceProvider();
            string decrypted = "";
            string pass = "ABC12abc";
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
            {}
            return decrypted;
        }
    }
}
