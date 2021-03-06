﻿using ImageServiceGUI.Model;
using ImageServiceGUI.ViewModel;
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

namespace ImageServiceGUI.View
{
    /// <summary>
    /// Interaction logic for ViewLog.xaml
    /// </summary>
    public partial class ViewLog : UserControl
    {
        LogVM vm;
        public ViewLog()
        {
            InitializeComponent();
            vm = new LogVM(new LogModel());
            this.DataContext = vm;
        }
    }
}
