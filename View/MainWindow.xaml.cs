﻿using System;
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
using System.Windows.Shapes;
using Normal__Function.ViewModel;

namespace Normal__Function.View
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Global.LoadIni();
            mainWindowViewModel= new MainWindowViewModel();
            this.DataContext = mainWindowViewModel;
        }

        private MainWindowViewModel mainWindowViewModel;
        //private MB_TCP mbTcp=new MB_TCP();
    }
}
