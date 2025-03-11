using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Windows;

namespace SerialToKeyboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private List<string> _serialPorts;
        
        public MainWindow()
        {
            InitializeComponent();
            _serialPorts = new List<string>();
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                _serialPorts.Add(port);
            }
            _serialPorts.Sort();

            cmbCOMPorts.ItemsSource = _serialPorts;
            if (cmbCOMPorts.Items.Count > 0)
                cmbCOMPorts.SelectedIndex = 0;
            
            radBtn115200.IsChecked = true;
        }

        private void BtnEnable_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}