using configApp.Actions;
using configApp.Controls;
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
using static System.Collections.Specialized.BitVector32;

namespace configApp.ConfigToolbar
{
    /// <summary>
    /// Interaction logic for KeyAdvancedToolbar.xaml
    /// </summary>
    public partial class KeyAdvancedToolbar : UserControl, IControlToolbar
    {
        public KeyAdvancedToolbar()
        {
            InitializeComponent();
        }

        private IControl _oldControl;
        public IControl OldControl 
        {
            private get
            {
                return _oldControl;
            }
            set 
            {
                _oldControl = value;
                //TODO: when uploaded, generate StackPanel items
                if (((ButtonControl)_oldControl).PressAction != null)
                {
                    if (((ButtonControl)_oldControl).PressAction is MacroAction)
                    {
                        MacroAction macroAction = (MacroAction)((ButtonControl)_oldControl).PressAction;
                        foreach (IAction action in macroAction.ActionList)
                        {
                            AddStackPanelItem(action);
                        }
                    }
                    else
                    {
                        IAction action = ((ButtonControl)_oldControl).PressAction;
                        AddStackPanelItem(action);
                    }
                }   
            } 
        }

        public IControl NewControl
        {
            get
            {
                ButtonControl control = new ButtonControl((ButtonControl)OldControl);
                if (ActionsStackPanel.Children.Count == 0)
                {
                    control.PressAction = null;
                }
                else if (ActionsStackPanel.Children.Count == 1)
                {
                    control.PressAction = ((ActionStackPanelItem)ActionsStackPanel.Children[0]).Action;
                }
                else
                {
                    List<IAction> actions = new List<IAction>();
                    foreach (ActionStackPanelItem item in ActionsStackPanel.Children)
                    {
                        actions.Add(item.Action);
                    }
                    control.PressAction = new MacroAction(actions);
                }


                return control;
            }
        }


        private void AddActionButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO : Add logic to select type of action
            MenuPopup.IsOpen = !MenuPopup.IsOpen;
            
        }

        private void RemoveActionButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is ActionStackPanelItem item)
            {
                ActionsStackPanel.Children.Remove(item);
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

        private void KeyCaptureButton_Click(object sender, RoutedEventArgs e)
        {
            KeysCapture keysCaptureWindow = new KeysCapture();
            bool? result = keysCaptureWindow.ShowDialog();
            if (result == true)
            {
                AddStackPanelItem(keysCaptureWindow.CreatedKeyboardAction);
            }
        }

        private void DelayButton_Click(object sender, RoutedEventArgs e)
        {
            DelayWindow delayWindow = new DelayWindow();
            bool? result = delayWindow.ShowDialog();
            if (result == true)
            {
                DelayAction action = new DelayAction(delayWindow.Result.Value);
                AddStackPanelItem(action);
            }
        }

        private void AddStackPanelItem(IAction action)
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
            ActionsStackPanel.Children.Add(item);
        }
    }
}
