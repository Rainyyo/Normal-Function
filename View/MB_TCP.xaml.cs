using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Normal__Function.ViewModel;

namespace Normal__Function.View
{
    /// <summary>
    /// MB_TCP.xaml 的交互逻辑
    /// </summary>
    public partial class MB_TCP : UserControl
    {
        public MB_TCP()
        {
            InitializeComponent();
            //mbTcpViewModel = new MB_TCP_ViewModel();
            //this.DataContext = mbTcpViewModel;
        }

        //private MB_TCP_ViewModel mbTcpViewModel;
        //private void TextBox_OnSizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //private void TextBox_OnSelectionChanged(object sender, RoutedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
