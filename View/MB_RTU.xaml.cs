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
using System.Threading.Tasks;
using Normal__Function.ViewModel;

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
            //this.DataContext = mbRtuViewModel;
        }

        //MB_RTU_ViewModel mbRtuViewModel=new MB_RTU_ViewModel();
        ////private SerialPort serialPort = new SerialPort();
        //private DXH.Modbus.DXHModbusRTU modbus;
        //private void Scan_Click(object sender, RoutedEventArgs e)
        //{
        //    string[] Ports = SerialPort.GetPortNames();
        //    COM.Items.Clear();
        //    foreach (var port in Ports)
        //    {
        //        COM.Items.Add(port);
        //    }
        //}
        //private void Connect_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (modbus == null)
        //        {
        //            Global.LoadIni();
        //            modbus = new DXHModbusRTU(COM.Text, Global.BaudRate);
        //            modbus.StartConnect();
        //            modbus.ConnectStateChanged += Modbus_ConnectStateChanged;
        //        }
               
        //        if (Connect.Content == "已连接")
        //        {
        //            modbus.Close();
        //            modbus=null;
        //        }
        //    }
        //    catch 
        //    { 
                
        //    }
        //}
        //private void Modbus_ConnectStateChanged(object sender, string e)
        //{
        //    Connect.Content = e;
        //    if (e=="Connected")
        //    {
        //        Connect.Content = "已连接";
        //        modbus.ModbusWrite(1, 16, 0, new[] { 12 });
        //    }
        //}

        #region 运行日志
        public string Log { get; set; }
        string LogHeader = " -> ";
        object LogLock = new object();
        int LogLine = 0;
        public async void LdrLog(string strtoappend)
        {
            Task task_Log = Task.Run(() =>
            {
                lock (LogLock)//多线程同时输出时会丢失Log
                {
                    LogLine++;
                    if (LogLine > 500)//最多500行。
                    {
                        Log = "";
                        LogLine = 1;
                    }
                    Log = Log + CurTime() + LogHeader + strtoappend + Environment.NewLine;
                }
            });
            await task_Log;
            TextBox.Text = Log;
        }
        public string CurTime()
        {
            return DateTime.Now.ToString();
        }



        double ScrollOffset = 0;
        int SelectionStart = 0;
        int SelectionLength = 0;
       
        private void TextBox_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ScrollOffset = Scroll.VerticalOffset;
            if (!Scroll.IsMouseOver && SelectionLength == 0)
            {
                Scroll.ScrollToEnd();
                SelectionStart = 0;
                SelectionLength = 0;
            }
            else
            {
                if (SelectionLength == 0 && SelectionStart == 0)
                { }
                else
                    TextBox.Select(SelectionStart, SelectionLength);
                Scroll.ScrollToVerticalOffset(ScrollOffset);
            }
        }

        private void TextBox_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            if (TextBox.SelectionStart == 0 && TextBox.SelectionLength == 0)
            {
            }
            else
            {
                SelectionStart = TextBox.SelectionStart;
                SelectionLength = TextBox.SelectionLength;
            }
        }
        #endregion
    }
}
