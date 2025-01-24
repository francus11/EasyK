using configApp.Actions;
using configApp.Controls;
using configApp.Enums;
using configApp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace configApp.ConfigToolbar
{
    /// <summary>
    /// Interaction logic for EncoderAdvancedToolbar.xaml
    /// </summary>

    public enum EncoderInputType
    {
        Left,
        Right,
        Button
    }

    public partial class EncoderAdvancedToolbar : UserControl, IControlToolbar
    {

        public static readonly DependencyProperty EncoderInputTypeProperty =
        DependencyProperty.Register(nameof(EncoderInputType), typeof(EncoderInputType), typeof(EncoderAdvancedToolbar), new PropertyMetadata(EncoderInputType.Left, OnToolbarTypeChanged));

        public EncoderInputType EncoderInputType
        {
            get => (EncoderInputType)GetValue(EncoderInputTypeProperty);
            set => SetValue(EncoderInputTypeProperty, value);
        }

        public IControl NewControl 
        {
            get 
            {
                EncoderControl encoder = new EncoderControl();
                Dictionary<StackPanel, IAction> actionMap = new Dictionary<StackPanel, IAction>
                {
                    { LeftActionsStackPanel, null },
                    { RightActionsStackPanel, null },
                    { ButtonActionsStackPanel, null }
                };

                foreach (var stackPanel in StackPanelMap)
                {
                    if (stackPanel.Value.Children.Count == 1)
                    {
                        actionMap[stackPanel.Value] = ((ActionStackPanelItem)stackPanel.Value.Children[0]).Action;
                    }
                    else if (stackPanel.Value.Children.Count > 1)
                    {
                        List<IAction> actions = new List<IAction>();
                        foreach (ActionStackPanelItem item in stackPanel.Value.Children)
                        {
                            actions.Add(item.Action);
                        }
                        actionMap[stackPanel.Value] = new MacroAction(actions);
                    }
                }

                encoder.ActionLeft = actionMap[LeftActionsStackPanel];
                encoder.ActionRight = actionMap[RightActionsStackPanel];
                encoder.ActionButton = actionMap[ButtonActionsStackPanel];

                return encoder;
            } 
        }

        private IControl _oldControl;

        public IControl OldControl { 
            set 
            {
                _oldControl = value;
                List<(IAction, StackPanel)> actionStackPanelList = new List<(IAction, StackPanel)>()
                {
                    (((EncoderControl)_oldControl).ActionLeft, LeftActionsStackPanel),
                    (((EncoderControl)_oldControl).ActionRight, RightActionsStackPanel),
                    (((EncoderControl)_oldControl).ActionButton, ButtonActionsStackPanel)
                };

                foreach (var (action, stackPanel) in actionStackPanelList)
                {
                    if (action != null)
                    {
                        if (action is MacroAction)
                        {
                            foreach (IAction subAction in ((MacroAction)action).ActionList)
                            {
                                AddStackPanelItem(stackPanel, subAction);
                            }   
                        }
                        else
                        {
                            AddStackPanelItem(stackPanel, action);
                        }
                    }
                }

                //TODO : when uploaded, generate StackPanel items
            }
        }

        private Dictionary<EncoderInputType, ScrollViewer> ScrollViewerMap { get; set; }
        private Dictionary<EncoderInputType, StackPanel> StackPanelMap { get; set; }

        public EncoderAdvancedToolbar()
        {
            InitializeComponent();
            ScrollViewerMap = new Dictionary<EncoderInputType, ScrollViewer>
            {
                { EncoderInputType.Left, LeftScrollViewer },
                { EncoderInputType.Right, RightScrollViewer },
                { EncoderInputType.Button, ButtonScrollViewer }
            };
            StackPanelMap = new Dictionary<EncoderInputType, StackPanel>
            {
                { EncoderInputType.Left, LeftActionsStackPanel },
                { EncoderInputType.Right, RightActionsStackPanel },
                { EncoderInputType.Button, ButtonActionsStackPanel }
            };

        }
        private void AddActionButton_Click(object sender, RoutedEventArgs e)
        {
            MenuPopup.IsOpen = !MenuPopup.IsOpen;
        }

        private void EncoderToolbarButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string toolbarType = button.Tag as string;
                if (toolbarType == "Left")
                    EncoderInputType = EncoderInputType.Left;
                else if (toolbarType == "Right")
                    EncoderInputType = EncoderInputType.Right;
                else if (toolbarType == "Button")
                    EncoderInputType = EncoderInputType.Button;

                foreach (var scrollViewer in ScrollViewerMap.Values)
                {
                    scrollViewer.Visibility = Visibility.Hidden;
                }

                ScrollViewerMap[EncoderInputType].Visibility = Visibility.Visible;

            }
        }

        private static void OnToolbarTypeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is EncoderAdvancedToolbar toolbar)
            {
                toolbar.UpdateVisuals();
            }
            
        }

        private void KeyCaptureButton_Click(object sender, RoutedEventArgs e)
        {
            KeysCapture keysCaptureWindow = new KeysCapture();
            bool? result = keysCaptureWindow.ShowDialog();
            if (result == true)
            {
                AddStackPanelItem(StackPanelMap[EncoderInputType], keysCaptureWindow.CreatedKeyboardAction);
            }
        }

        private void DelayButton_Click(object sender, RoutedEventArgs e)
        {
            DelayWindow delayWindow = new DelayWindow();
            bool? result = delayWindow.ShowDialog();
            if (result == true)
            {
                DelayAction action = new DelayAction(delayWindow.Result.Value);
                AddStackPanelItem(StackPanelMap[EncoderInputType], action);
            }
        }

        private void SystemButton_Click(object sender, RoutedEventArgs e)
        {
            SystemWindow systemWindow = new SystemWindow();
            bool? result = systemWindow.ShowDialog();
            if (result == true)
            {
                AddStackPanelItem(StackPanelMap[EncoderInputType], systemWindow.Action);
            }
        }

        private void EditActionButton_Keyboard_Click(object sender, RoutedEventArgs e)
        {
            if (sender is ActionStackPanelItem item)
            {
                if (item.Action is KeyboardAction)
                {
                    KeysCapture KeysCapture = new KeysCapture();
                    KeysCapture.OnLoadKeyboardAction = ((KeyboardAction)item.Action);
                    bool? result = KeysCapture.ShowDialog();
                    if (result == true)
                    {
                        item.LabelContent = KeysCapture.CreatedKeyboardAction.Label;
                        item.Action = KeysCapture.CreatedKeyboardAction;
                    }
                }
            }
        }

        private void EditActionButton_Delay_Click(object sender, RoutedEventArgs e)
        {
            if (sender is ActionStackPanelItem item)
            {
                if (item.Action is DelayAction)
                {
                    DelayWindow delayWindow = new DelayWindow();
                    delayWindow.Action = ((DelayAction)item.Action);
                    bool? result = delayWindow.ShowDialog();
                    if (result == true)
                    {
                        item.LabelContent = delayWindow.Result.ToString();
                        item.Action = new DelayAction(delayWindow.Result.Value);
                    }
                }
            }
        }

        private void EditActionButton_System_Click(object sender, RoutedEventArgs e)
        {
            if (sender is ActionStackPanelItem item)
            {
                if (item.Action is SystemAction)
                {
                    SystemWindow systemWindow = new SystemWindow();
                    systemWindow.Action = ((SystemAction)item.Action);
                    bool? result = systemWindow.ShowDialog();
                    if (result == true)
                    {
                        item.LabelContent = systemWindow.Action.Label;
                        item.Action = systemWindow.Action;
                    }
                }
            }
        }

        private void AddStackPanelItem(StackPanel stackPanel, IAction action)
        {
            ActionStackPanelItem item = new ActionStackPanelItem
            {
                LabelContent = action.Label,
                Action = action
            };
            item.RemoveClicked += (s, e) => RemoveActionButton_Click(s, e);

            if (action is DelayAction)
            {
                item.EditClicked += (s, e) => EditActionButton_Delay_Click(s, e);
            }
            if (action is KeyboardAction)
            {
                item.EditClicked += (s, e) => EditActionButton_Keyboard_Click(s, e);
            }
            if (action is SystemAction)
            {
                item.EditClicked += (s, e) => EditActionButton_System_Click(s, e);
            }
            stackPanel.Children.Add(item);
        }

        private void UpdateVisuals()
        {

        }

        private void RemoveActionButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is ActionStackPanelItem item)
            {
                StackPanelMap[EncoderInputType].Children.Remove(item);
            }
        }
    }
}
