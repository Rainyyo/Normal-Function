using DXH.Ini;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;

namespace Normal__Function
{
    public class Global
    {
        public static string GlobalFile = AppDomain.CurrentDomain.BaseDirectory + "\\Global.ini";
        private static int baudRate = 9600;

        public static int BaudRate
        {
            get { return baudRate; }
            set
            {
                baudRate = value;
                DXHIni.WritePrivateProfileString("参数", "波特率", baudRate.ToString(), GlobalFile);
            }
        }
          
        public static void LoadIni()
        {
            if (File.Exists(GlobalFile))
            {
                DXHIni.TryToInt(ref baudRate, DXHIni.ContentReader("参数", "波特率", "", GlobalFile));
            }
            else
            {
                BaudRate = baudRate;
            }
        }
    }
}
