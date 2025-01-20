using System;
using System.IO.Ports;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace configApp
{
    public partial class ConnectDeviceSerialWindow : Window
    {
        private DispatcherTimer _portCheckTimer;

        public ConnectDeviceSerialWindow()
        {
            InitializeComponent();
            InitializePortCheckTimer();
        }

        private void InitializePortCheckTimer()
        {
            // Tworzymy timer z interwałem 500 ms
            _portCheckTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };

            _portCheckTimer.Tick += PortCheckTimer_Tick;
            _portCheckTimer.Start();
        }

        private void PortCheckTimer_Tick(object? sender, EventArgs e)
        {
            // Pobieramy dostępne porty
            string[] availablePorts = SerialPort.GetPortNames();

            // Aktualizujemy ComboBox, jeśli porty się zmieniły
            UpdatePortList(availablePorts);
        }

        private void UpdatePortList(string[] availablePorts)
        {
            // Sprawdzamy, czy lista portów w ComboBoxie jest zgodna z dostępnymi portami
            var currentPorts = PortComboBox.Items.Cast<string>().ToArray();
            if (!availablePorts.SequenceEqual(currentPorts))
            {
                PortComboBox.Items.Clear();
                foreach (string port in availablePorts)
                {
                    PortComboBox.Items.Add(port);
                }

                // Jeśli dostępne są porty, wybierz pierwszy na liście
                if (availablePorts.Length > 0)
                {
                    PortComboBox.SelectedIndex = 0;
                }
            }
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedPort = PortComboBox.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedPort))
            {
                MessageBox.Show("Please select a port before connecting.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Tutaj możesz dodać logikę łączenia z urządzeniem
            MessageBox.Show($"Connecting to port: {selectedPort}");
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _portCheckTimer.Stop();
            this.Close();
        }
    }
}
