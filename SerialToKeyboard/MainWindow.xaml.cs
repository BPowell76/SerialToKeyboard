using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private bool _errorHappened = false;
        private int _activeBaud = 0;
        
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
                StopListening();
                if (!_errorHappened)
                {
                    SetInterfaceEnable(true); 
                    _isEnabled = false;
                    btnEnable.Content = "Enable";
                    lblStatus.Content = "Deactivated";
                    lblStatus.Foreground = new SolidColorBrush(Colors.Red);    
                }
                
            }
            else
            {
                StartListening();
                if (!_errorHappened)
                {
                    SetInterfaceEnable(false);
                    _isEnabled = true;
                    btnEnable.Content = "Disable";
                    lblStatus.Content = "Activated";
                    lblStatus.Foreground = new SolidColorBrush(Colors.Green);
                }
            }
        }
        
        private void StopListening()
        {
            _transfer.Stop();
            _transfer.Dispose();
        }
        
        private void StartListening()
        {
            if (_transfer != null)
                _transfer.Dispose();
        
            string selectedPort = cmbCOMPorts.SelectedItem.ToString();
            if (radBtn4800.IsChecked == true)
                _activeBaud = 4800;
            else if (radBtn9600.IsChecked == true)
                _activeBaud = 9600;
            else if (radBtn19200.IsChecked == true)
                _activeBaud = 19200;
            else if (radBtn38400.IsChecked == true)
                _activeBaud = 38400;
            else if (radBtn57600.IsChecked == true)
                _activeBaud = 57600;
            else if (radBtn115200.IsChecked == true)
                _activeBaud = 115200;
            else
                _activeBaud = 0;

            if (_activeBaud == 0)
            {
                MessageBox.Show(
                    "A baud rate is required to correctly interface with the device's serial port.",
                    "Missing Baud Rate",
                    MessageBoxButton.OK);
                _errorHappened = true;
                return;
            }

            if (String.IsNullOrEmpty(selectedPort))
            {
                 MessageBox.Show(
                     "A COM port is required to correctly interface with the device's serial port.",
                     "Missing COM Port",
                     MessageBoxButton.OK);

                 _errorHappened = true;
                 cmbCOMPorts.Focus();
                 return;
            }
            
            _transfer = new ComToKey(new SerialPort(selectedPort, _activeBaud, Parity.None, 8, StopBits.One));
            _transfer.Start();
        }
        
        private void SetInterfaceEnable(bool b)
        {
            foreach (RadioButton button in _buttons)
            {
                button.IsEnabled = b;
            }
            cmbCOMPorts.IsEnabled = b;
        }

        private void CmbCOMPorts_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _errorHappened = false;
        }

        private void RadBtn_OnChecked(object sender, RoutedEventArgs e)
        {
            _errorHappened = false;
            RadioButton radioSender = (RadioButton)sender;
            _activeBaud = Convert.ToInt32(radioSender.Content);
        }
    }
}