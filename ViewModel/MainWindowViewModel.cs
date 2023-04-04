using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Normal__Function.ViewModel
{
    public class MainWindowViewModel
    {
        public MB_RTU_ViewModel MbRtuViewModel { get; set; }=new MB_RTU_ViewModel();
        public MB_TCP_ViewModel MbTcpViewModel { get; set; }=new MB_TCP_ViewModel();
    }
}
