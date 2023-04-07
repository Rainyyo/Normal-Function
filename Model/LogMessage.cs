using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Normal__Function.View;
using System.IO;

namespace Normal__Function.Model
{
    public class LogMessage
    {
        private MB_RTU modBusRtuLogmsg = new MB_RTU();
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
            modBusRtuLogmsg.TextBox.Text= Log;
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
            ScrollOffset = modBusRtuLogmsg.Scroll.VerticalOffset;
            if (!modBusRtuLogmsg.Scroll.IsMouseOver && SelectionLength == 0)
            {
                modBusRtuLogmsg.Scroll.ScrollToEnd();
                SelectionStart = 0;
                SelectionLength = 0;
            }
            else
            {
                if (SelectionLength == 0 && SelectionStart == 0)
                { }
                else 
                    modBusRtuLogmsg.TextBox.Select(SelectionStart, SelectionLength);
                modBusRtuLogmsg.Scroll.ScrollToVerticalOffset(ScrollOffset);
            }
        }

        private void TextBox_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            if (modBusRtuLogmsg.TextBox.SelectionStart == 0 && modBusRtuLogmsg.TextBox.SelectionLength == 0)
            {
            }
            else
            {
                SelectionStart = modBusRtuLogmsg.TextBox.SelectionStart;
                SelectionLength = modBusRtuLogmsg.TextBox.SelectionLength;
            }
        }
        #endregion

        #region 保存日志文件
        static string mBarCodeFilePath = @"D:\日志信息\";
        public static string BarCodeFilePath
        {
            get { return mBarCodeFilePath; }
            set
            {
                mBarCodeFilePath = value;
            }
        }

        static object SaveBarCodeLock = new object();
        public static async void SaveLog(string _BarCode)
        {
            Task Task_SaveLog = Task.Run(() =>
            {
                try
                {
                    lock (SaveBarCodeLock)
                    {
                        if (!Directory.Exists(BarCodeFilePath + DateTime.Now.ToString("yyyy-MM")))
                            Directory.CreateDirectory(BarCodeFilePath + DateTime.Now.ToString("yyyy-MM"));
                        File.AppendAllText(BarCodeFilePath + DateTime.Now.ToString("yyyy-MM") + "/" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", _BarCode + Environment.NewLine);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("SaveDebugLog:" + ex.Message);
                }
            });
            await Task_SaveLog;
        }
        #endregion

        #region 创建.CSV
        /////<summary>
        ///// 创建.csv文件
        ///// </summary>
        //static object SAVEPianYiLock = new object();
        //public static async void SaveDataCSV(string DianYa, string DianLiu, int mIndex)
        //{
        //    Task Task_Save = Task.Run(() =>
        //    {
        //        lock (SAVEPianYiLock)
        //        {
        //            System.IO.DirectoryInfo LocalDir = new System.IO.DirectoryInfo("D:\\电流电压记录\\");
        //            try
        //            {
        //                LocalDir.Create();
        //            }
        //            catch
        //            {
        //            }
        //            try
        //            {

        //                string mLocalFile = LocalDir.FullName + DateTime.Today.ToString("yyyyMMdd") + "_" + mIndex + ".csv";
        //                string mLocalData = DateTime.Now.ToString("HH:mm:ss") + "," + DianYa + "," + DianLiu;
        //                if (!System.IO.File.Exists(mLocalFile))
        //                {
        //                    string mHeader = @"时间,电压,电流";
        //                    System.IO.File.WriteAllText(mLocalFile, mHeader, Encoding.Default);
        //                }
        //                System.IO.File.AppendAllText(mLocalFile, Environment.NewLine + mLocalData, Encoding.Default);

        //            }
        //            catch
        //            {
        //            }
        //        }
        //    });
        //    await Task_Save;
        //}
        #endregion
    }
}
