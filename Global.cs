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
        /// <summary>
        /// 串口波特率（int型）
        /// </summary>
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
        /// <summary>
        /// TCP  IP地址（string类型）
        /// </summary>
        private static string mb_ipAddress="127.0.0.1";

        public static string MB_IPAddress
        {
            get { return mb_ipAddress; }
            set
            {
                mb_ipAddress = value;
                DXHIni.WritePrivateProfileString("参数", "IP", mb_ipAddress, GlobalFile);
            }
        }

        public static string GlobalFile = AppDomain.CurrentDomain.BaseDirectory + "\\Global.ini";
        public static void LoadIni()
        {
            if (File.Exists(GlobalFile))
            {
                DXHIni.TryToInt(ref baudRate, DXHIni.ContentReader("参数", "波特率", "", GlobalFile));
                mb_ipAddress = DXHIni.ContentReader("参数", "IP", "", GlobalFile);
            }
            else
            {
                BaudRate = baudRate;
                MB_IPAddress = mb_ipAddress;
            }
        }
    }
}
