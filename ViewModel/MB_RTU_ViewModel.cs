using DXH.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.IO.Ports;
using Normal__Function.View;
using DXH.Modbus;
using System.Runtime.Remoting.Contexts;

namespace Normal__Function.ViewModel
{
    public class MB_RTU_ViewModel:ViewModelBase
    {
        #region 按钮Binding点击事件
      
        public ObservableCollection<string> ItemsCollection { get; set; } = new ObservableCollection<string>();
        /// <summary>
        /// Celect_COMname
        /// </summary>
        private string selecteditem;

        public string SelectedItem
        {
            get { return selecteditem; }
            set
            {
                selecteditem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        private string nowstate="点击连接";

        public string NowState
        {
            get { return nowstate="点击连接"; }
            set
            {
                nowstate = value;
                OnPropertyChanged("NowState");
            }
        }


        RelayCommand _ActionCommand;
        public ICommand ActionCommand
        {
            get
            {
                if (_ActionCommand == null)
                    _ActionCommand = new RelayCommand(param => this.Action(param as string));
                return _ActionCommand;
            }
        }

        public void Action(string param)
        {
            if (param == "Show")
            {
                Scan();
            }
        }
        /// <summary>
        /// 定义一个扫描COM口的方法
        /// </summary>
        public void Scan()
        {
            string[] Ports = SerialPort.GetPortNames();
           ItemsCollection.Clear();
           foreach (var port in Ports)
           {
               ItemsCollection.Add(port);
           }
        }
        #endregion

        private DXH.Modbus.DXHModbusRTU modbus;
        public void State()
        {
            try
            {
                if (modbus == null)
                {
                    Global.LoadIni();
                    modbus = new DXHModbusRTU(SelectedItem, Global.BaudRate);
                    modbus.StartConnect();
                    modbus.ConnectStateChanged += Modbus_ConnectStateChanged; ;
                }

                if (NowState == "已连接")
                {
                    modbus.Close();
                    modbus = null;
                }
            }
            catch
            {

            }
        }

        private void Modbus_ConnectStateChanged(object sender, string e)
        {
            NowState = e;
            if (e == "Connected")
            {
                NowState = "已连接";
                modbus.ModbusWrite(1, 16, 0, new[] { 12 });
            }
        }
    }
}
