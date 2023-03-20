using System;
using System.Collections.Generic;
using System.IO.Ports;
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

namespace Normal__Function.View
{
    /// <summary>
    /// Port_Test.xaml 的交互逻辑
    /// </summary>
    public partial class Port_Test : UserControl
    {
        public Port_Test()
        {
            InitializeComponent();
        }
        private SerialPort serialPort = new SerialPort();

        //private string logPath = Application.StartupPath + "/Log/Message";//日志路径

        private void Scan_Click(object sender, RoutedEventArgs e)
        {
            COM.Items.Clear();
            COM.Text = "COM1";
            BaudRate.Text = "9600";
            DataBits.Text = "8";
            StopBits.Text = "1";
            Parity.Text = "None";
            string[] Ports = SerialPort.GetPortNames();
            foreach (var port in Ports)
            {
                COM.Items.Add(port);
            }
        }
        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (serialPort.IsOpen == false)
            {
                serialPort.PortName = COM.Text;//串口名
                serialPort.BaudRate = Int32.Parse(BaudRate.Text);//波特率
                serialPort.DataBits = Int32.Parse(DataBits.Text);//数据位
                serialPort.StopBits = System.IO.Ports.StopBits.One;//停止位
                serialPort.Parity = System.IO.Ports.Parity.None; //校验位
                try
                {
                    serialPort.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                Connect.Content = "断开连接";

                serialPort.DataReceived += SerialPort_DataReceived;
            }
            else
            {
                try
                {
                    serialPort.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                Connect.Content = "通讯连接";
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // 从串口读取接收到的数据
            SerialPort serialPort = (SerialPort)sender;
            string data = serialPort.ReadExisting();

            // 在UI线程中更新接收到的数据
            Dispatcher.BeginInvoke(new Action(() =>
            {
                // 处理接收到的数据
                serialPort.Encoding = Encoding.UTF8;// UTF-8 编码

                // 显示接收到的数据
                Logmsg.Text += DateTime.Now + "->接收:" + data + "\r\n";
            }));
        }

        private void Sending_OnClick(object sender, RoutedEventArgs e)
        {
            // 从串口读取接收到的数据
            SerialPort serialPort = (SerialPort)sender;
            string data = serialPort.ReadExisting();

            // 在UI线程中更新接收到的数据
            Dispatcher.BeginInvoke(new Action(() =>
            {
                // 处理接收到的数据
                serialPort.Encoding = Encoding.UTF8;// UTF-8 编码

                // 显示接收到的数据
                Logmsg.Text += DateTime.Now + "->接收:" + data + "\r\n";
            }));
        }

        //#region 日志

        //public string Log { get; set; }
        //string LogHeader = " -> ";
        //object LogLock = new object();
        //int LogLine = 0;
        //public string CurTime()
        //{
        //    return DateTime.Now.ToString();
        //}
        //public async void LdrLog(string strtoappend)
        //{
        //    string logstr = CurTime() + LogHeader + strtoappend + Environment.NewLine;
        //    Task task_log = Task.Run(() =>
        //    {
        //        lock (LogLock)//多线程同时输出时会丢失Log
        //        {
        //            LogLine++;
        //            if (LogLine > 500)//最多500行。
        //            {
        //                Log = "";
        //                LogLine = 1;
        //            }
        //            Log += logstr;
        //        }
        //        this.OnPropertyChanged("Log");
        //    });
        //    await task_log;
        //}

        //#endregion
    }
}
