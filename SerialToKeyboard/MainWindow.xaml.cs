using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media;
using SerialToKeyboard.Control;

namespace SerialToKeyboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private List<string> _serialPorts = new List<string>();
        private List<RadioButton> _buttons = new List<RadioButton>();
        private ComToKey _transfer;
        private bool _isEnabled = false;
        
        public MainWindow()
        {
            InitializeComponent();
            foreach (string port in SerialPort.GetPortNames())
            {
                _serialPorts.Add(port);
            }
            _serialPorts.Sort();

            cmbCOMPorts.ItemsSource = _serialPorts;
            if (cmbCOMPorts.Items.Count > 0)
                cmbCOMPorts.SelectedIndex = 0;
            
            _buttons.Add(radBtn4800);
            _buttons.Add(radBtn9600);
            _buttons.Add(radBtn19200);
            _buttons.Add(radBtn38400);
            _buttons.Add(radBtn57600);
            _buttons.Add(radBtn115200);
            radBtn115200.IsChecked = true;
        }

        private void BtnEnable_OnClick(object sender, RoutedEventArgs e)
        {
            if (_isEnabled)
            {
                _isEnabled = false;
                btnEnable.Content = "Enable";
                lblStatus.Content = "Deactivated";
                lblStatus.Foreground = new SolidColorBrush(Colors.Red);
                StopListening();
            }
            else
            {
                _isEnabled = true;
                btnEnable.Content = "Disable";
                lblStatus.Content = "Activated";
                lblStatus.Foreground = new SolidColorBrush(Colors.Green);
                StartListening();
            }
        }
        
        private void StopListening()
        {
            //_transfer.Stop();
            //_transfer.Dispose();
            SetInterfaceEnable(true); 
        }
        
        private void StartListening()
        {
            if (_transfer != null)
                _transfer.Dispose();
        
            SetInterfaceEnable(false);
            //var pName = cmbCOMPorts.SelectedItem.ToString();
            int pBaud;
            //int.TryParse(cmbBaud.SelectedItem.ToString(), out pBaud);
            //_transfer = new ComToKey(new SerialPort(pName, pBaud, Parity.None, 8, StopBits.One));
            //_transfer.Start();
        }
        
        private void SetInterfaceEnable(bool b)
        {
            foreach (RadioButton button in _buttons)
            {
                button.IsEnabled = b;
            }
            cmbCOMPorts.IsEnabled = b;
        }
    }
}