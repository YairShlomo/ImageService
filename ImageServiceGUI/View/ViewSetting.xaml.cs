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
    /// Interaction logic for ViewSetting.xaml
    /// </summary>
    public partial class ViewSetting : UserControl
    {
        SettingVM vm;
        public ViewSetting()
        {
            
            InitializeComponent();
            vm = new SettingVM(new SettingModel());
            this.DataContext = vm;
        }
        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (lbUsers.SelectedItem != null)
            {                 
                vm.CloseHandler(lbUsers.SelectedItem as string);
            }
        }
    }
}
