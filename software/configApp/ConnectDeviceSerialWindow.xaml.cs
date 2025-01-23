using System;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace configApp
{
    public partial class ConnectDeviceSerialWindow : Window
    {
        private DispatcherTimer _portCheckTimer;
        private DispatcherTimer _connectionCheckTimer;
        private SerialPort _serialPort;
        private MainWindow _mainWindow;

        private bool _connectivityCheck = false;
        public bool ConnectivityCheck { 
            get
            {
                return _connectivityCheck;
            }
            set
            {

                if (value == _connectivityCheck)
                {
                    return;
                }

                _connectivityCheck = value;

                if (_connectivityCheck)
                {
                    InitializeConnectionCheckTimer();
                }
                else
                {
                    _connectionCheckTimer.Stop();
                }

            }
        }

        //public RoutedEventHandler DataReceived

        public ConnectDeviceSerialWindow()
        {
            InitializeComponent();
            InitializePortCheckTimer();
            InitializeSerialPort();
        }

        private void InitializePortCheckTimer()
        {
            _portCheckTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };

            _portCheckTimer.Tick += PortCheckTimer_Tick;
            _portCheckTimer.Start();
        }
        private void InitializeSerialPort()
        {
            _serialPort = new SerialPort
            {
                BaudRate = 9600,
                DataBits = 8,
                Parity = Parity.None,
                StopBits = StopBits.One,
                Encoding = Encoding.ASCII,
                ReadTimeout = 500,
                WriteTimeout = 500,
                RtsEnable = true,
                DtrEnable = true,
                //NewLine = "\r\n"
            };

            //_serialPort.DataReceived += SerialPort_DataReceived;
        }

        private void InitializeConnectionCheckTimer()
        {
            _connectionCheckTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(2000)
            }; ;
            _connectionCheckTimer.Tick += ConnectionCheckTimer_Elapsed;
            _connectionCheckTimer.Start();
        }

        private void PortCheckTimer_Tick(object? sender, EventArgs e)
        {
            string[] availablePorts = SerialPort.GetPortNames();
            UpdatePortList(availablePorts);
        }

        private void UpdatePortList(string[] availablePorts)
        {
            var currentPorts = PortComboBox.Items.Cast<string>().ToArray();
            if (!availablePorts.SequenceEqual(currentPorts))
            {
                PortComboBox.Items.Clear();
                foreach (string port in availablePorts)
                {
                    PortComboBox.Items.Add(port);
                }

                if (availablePorts.Length > 0)
                {
                    PortComboBox.SelectedIndex = 0;
                }
            }
        }

        

        private void ConnectionCheckTimer_Elapsed(object? sender, EventArgs e)
        {
            if (!_serialPort.IsOpen)
            {
                ConnectivityCheck = false;
                try
                {
                    _serialPort.Open();
                    ConnectivityCheck = true;
                    return;

                }
                catch (Exception)
                {
                    ConnectivityCheck = false;
                    Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show("Connection lost: Cannot reconnect.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    });
                }

                this.Show();

                if (sender is ConnectDeviceSerialWindow window)
                {
                }
                _mainWindow.Close();
                Dispatcher.Invoke(() =>
                {
                    MessageBox.Show("Connection lost: Port is closed.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                });
                return;
            }

            //try
            //{
            //    _serialPort.WriteLine("PING");
            //}
            //catch (Exception)
            //{
            //    ConnectivityCheck = false;
            //    _serialPort.Close();
            //    Dispatcher.Invoke(() =>
            //    {
            //        MessageBox.Show("Connection lost: Unable to communicate with the device.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    });
            //}
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (_serialPort.IsOpen)
            {
                MessageBox.Show("Port is already open.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            string selectedPort = PortComboBox.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedPort))
            {
                MessageBox.Show("Please select a port before connecting.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                _serialPort.PortName = selectedPort;
                _serialPort.Open();
                //MessageBox.Show($"Connected to port: {selectedPort}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to connect: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (_serialPort.IsOpen)
            {
                _mainWindow = new MainWindow(_serialPort, this);
                _mainWindow.Show();
                this.Hide();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _portCheckTimer.Stop();
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }
            this.Close();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_serialPort.IsOpen)
            {
                MessageBox.Show("Please connect to a port before sending data.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string dataToSend = SendTextBox.Text;
            if (string.IsNullOrEmpty(dataToSend))
            {
                MessageBox.Show("Please enter data to send.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                _serialPort.Write(dataToSend);
                //MessageBox.Show("Data sent successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to send data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string receivedData = _serialPort.ReadExisting();
                Dispatcher.Invoke(() =>
                {
                    ReceivedTextBox.AppendText(receivedData);
                    ReceivedTextBox.ScrollToEnd();
                });
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                {
                    MessageBox.Show($"Failed to receive data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                });
            }
        }
    }
}
