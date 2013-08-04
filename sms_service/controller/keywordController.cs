using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sms_service
{
    public class keyword
    {
        public static string check(string input)
        {
            string returnMsg = "Oops, please check your keyword, then try again! thank you...";
            switch (input)
            {
                case "number":
                case "Number":
                    returnMsg = "Number combination is 7-24! Maraming Salamat. Ugaliing magbigay ng tulong at Donasyong Barya, Sagip Buhay Ng Masa.";
                    break;
                case "project":
                case "Project":
                    returnMsg = "Ang inyong donasyon ngayon ay para sa mga nasalanta sa bagyong Harabas at pgpapatayo ng HOSPITAL NG MASA!";
                    break;
                case "gateway ":
                case "Gateway ":
                    returnMsg = "Gateway: Smart: 091823456789 | Globe: 0926 2345678 | Sun: 09321456789";
                    break;
                case "help":
                case "Help":
                    returnMsg = "1.To register: Reg FamilyName/FirstName/Address/SponsorCode/PinCode Send Gateway " +
                           "2.To donate: Number combination/amount Send gateway 3.To transfer fund: Tf RecepientNumber/amount/Purpose  Send gateway " +
                           "4.To know the winning number, text <Number> 5.To know the project, text <project>.Send Gateway";
                    break;
                case "hotline":
                case "Hotline":
                    returnMsg = "Call or Text 09164290916, 09182988888";
                    break;
                case "reg":
                case "Reg":
                    returnMsg = "Welcome to Bayanihan Damayan! Ibalik natin ang Bayanihang Filipino. U are given 100 points to share. " +
                           "Donasyong Barya, Sagip Buhay Ng Masa. To donate, type number combination/amount Send to gateway. ex. 2-46/2 Send. MAraming Salamat Po.";
                    break;
                case "donate":
                case "Donate":
                    returnMsg = "Natanggap namin ang iyong dalawang pisong tulong, your number is 14-25 ref # 013423.";
                    break;
            }
            return returnMsg;
        }
    }
}
