using configApp.Actions;
using configApp.Controls;
using configApp.Enums;
using configApp.UI;
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
            // TODO : Add logic to select type of action
            KeysCapture keysCaptureWindow = new KeysCapture();
            bool? result = keysCaptureWindow.ShowDialog();
            if (result == true)
            {
                IAction action = keysCaptureWindow.CreatedKeyboardAction;
                AddStackPanelItem(StackPanelMap[EncoderInputType], action);
            }
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

        private void AddStackPanelItem(StackPanel stackPanel, IAction action)
        {
            ActionStackPanelItem item = new ActionStackPanelItem
            {
                LabelContent = action.Label,
                Action = action
            };
            item.RemoveClicked += (s, e) => RemoveActionButton_Click(s, e);
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
