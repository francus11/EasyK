using configApp.Actions;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace configApp
{
    public partial class DelayWindow : Window
    {
        private int? _result;
        private IAction _action;

        public event PropertyChangedEventHandler PropertyChanged;

        public IAction Action 
        { 
            get
            {
                return _action;
            }
            set
            {
                _action = value;
                _result = ((DelayAction)_action).Delay;
            }
        }

        public int? Result
        {
            get => _result;
            private set
            {
                _result = value;
                OnPropertyChanged(nameof(Result));

                if (_result.HasValue)
                {
                    InputTextBox.Text = _result.ToString();
                }
            }
        }

        public DelayWindow()
        {
            InitializeComponent();
            Result = null;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(InputTextBox.Text, out int value) && value > 0)
            {
                OkButton.IsEnabled = true;
            }
            else
            {
                OkButton.IsEnabled = false;
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(InputTextBox.Text, out int value) && value > 0)
            {
                Result = value; 
                DialogResult = true; 
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
