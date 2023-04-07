using DXH.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DXH.Modbus;
using Normal__Function.View;

namespace Normal__Function.ViewModel
{
    public class MB_TCP_ViewModel:ViewModelBase
    {
        
        private DXH.Modbus.DXHModbusTCP mbModbusTcp;

        private string ipAddress=Global.MB_IPAddress;
        public string IPAddress
        {
            get { return ipAddress; }
            set
            {
                ipAddress = value;
                OnPropertyChanged("IPAddress");
            }
        }
        private string nowstate = "点击连接";

        public string NowState
        {
            get { return nowstate; }
            set
            {
                nowstate = value;
                OnPropertyChanged("NowState");
            }
        }
        private bool tcpState=false;

        public bool TCPState
        {
            get { return tcpState; }
            set
            {
                tcpState = value;
                OnPropertyChanged("TCPState");
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
            if (param=="Connect")
            {
                Connect();
            }

            if (param=="ShoutDown")
            {
                ShoutDown();
            }
        }

        private void ShoutDown()
        {
            if (NowState == "已连接")
            {
                mbModbusTcp.Close();
                mbModbusTcp = null;
            }
        }

        private void Connect()
        {
            try
            {
                if (mbModbusTcp == null)
                {
                    mbModbusTcp = new DXHModbusTCP(Global.MB_IPAddress);
                    mbModbusTcp.StartConnect();
                    mbModbusTcp.ConnectStateChanged += Modbus_ConnectStateChanged;
                    mbModbusTcp.ModbusStateChanged += MbModbusTcp_ModbusStateChanged;
                }
            }
            catch
            {

            }
        }

        private void MbModbusTcp_ModbusStateChanged(object sender, bool e)
        {
            TCPState = e;
        }

        private void Modbus_ConnectStateChanged(object sender, string e)
        {
            NowState = e;
            if (e == "Connected")
            {
                NowState = "已连接";
                mbModbusTcp.ModbusWrite(1, 16, 0, new[] { 12 });
            }
        }
    }
}
