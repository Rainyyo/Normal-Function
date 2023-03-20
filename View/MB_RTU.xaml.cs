using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Ports;
using DXH.Modbus;

namespace Normal__Function.View
{
    /// <summary>
    /// MB_RTU.xaml 的交互逻辑
    /// </summary>
    public partial class MB_RTU : UserControl
    {
        public MB_RTU()
        {
            InitializeComponent();
        }

        private SerialPort serialPort = new SerialPort();
        private DXH.Modbus.DXHModbusRTU modbus;
        private void Scan_Click(object sender, RoutedEventArgs e)
        {
            string[] Ports = SerialPort.GetPortNames();
            COM.Items.Clear();
            foreach (var port in Ports)
            {
                COM.Items.Add(port);
            }
        }
        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (modbus == null)
                {
                    Global.LoadIni();
                    modbus = new DXHModbusRTU(COM.Text, Global.BaudRate);
                    modbus.StartConnect();
                    modbus.ConnectStateChanged += Modbus_ConnectStateChanged;
                }
               
                if (Connect.Content == "已连接")
                {
                    modbus.Close();
                    modbus=null;
                }
            }
            catch 
            { 
                
            }
        }

        private void Modbus_ConnectStateChanged(object sender, string e)
        {
            Connect.Content = e;
            if (e=="Connected")
            {
                Connect.Content = "已连接";
                modbus.ModbusWrite(1, 16, 0, new[] { 12 });
            }
        }


    }
}
