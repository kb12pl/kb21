using NLua;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace kb21_wpf
{
    public partial class KbWindow:IKbWindow
    {
        public readonly KbLua lua;        
        static readonly Dictionary<string, KbDialog> dialog_list = new();
        public static readonly Dictionary<string, object> globals = new();
        readonly ContentControl contentControl;
        private readonly bool dialog;
        private readonly bool page;
        //public static void KbWindowOk(string mes) => ok(mes);
        //public static void KbWindowOkClear() => xlogclear();
        internal KbWindow(KbDialog _dialog, MyArg arg)
        {
            lua = new(this);
            contentControl = _dialog;
            dialog = true;            
        }
        public KbWindow(Window _frame)
        {
            lua = new(this);
            contentControl = _frame;            
        }
        internal KbWindow(KbTabItem _page)
        {
            lua = new(this);
            contentControl = _page;
            page = true;            
        }

        public void Ok(object mess)=>ok(mess);       
        public string GetConfig(string key)=>Conf(key);
        public void SetGlobal(string key, object b) => KbConf.SetGlobal(key, b);
        public object? GetGlobal(string key)=>KbConf.GetGlobal(key);

        public string? Shortcut(string shortcut, string kod,bool shift=false, bool alt=false,bool ctrl=false )
        {

            ModifierKeys mod = 0;
            if (shift)
                mod |= ModifierKeys.Shift;
            if (ctrl)
                mod |= ModifierKeys.Control;
            if (alt)
                mod |= ModifierKeys.Alt;

            
            RoutedCommand newCmd = new(shortcut, typeof(string));
            newCmd.InputGestures.Add(new KeyGesture((Key)Enum.Parse(typeof(Key), kod),mod));
            contentControl.CommandBindings.Add(new CommandBinding(newCmd, DoShortcut));

            return null;
        }

        private void DoShortcut(object sender, ExecutedRoutedEventArgs e)
        {
            Integration("onShort", "",((RoutedCommand)e.Command).Name);
        }

        public void Integration(string event_name, string id="", string col="", string item="", string par="")
        {

            var ret = lua.DoString(string.Format("B12_Integretion_Function('{0}','{1}','{2}','{3}','{4}')",                
                event_name, id, col, item, par));
            if (ret == "close")
            {
                if (dialog)
                {
                    Window w = (Window)contentControl;
                    w.Close();
                }
                if (page)
                {
                    MyFrame.Remove((KbTabItem)contentControl);
                    
                }
                
            }

        }

        public void Integration(string event_name, string id, Ret data)
        {
            var fun = lua.GetPtr().GetFunction("B12_Integretion_Function");
            var ret=fun.Call(event_name, id, data);
            if (ret.ToString() == "close")
            {
                if (dialog)
                {
                    Window w = (Window)contentControl;
                    w.Close();
                }
                if (page)
                {
                    MainWindow.Remove((KbTabItem)contentControl);
                }

            }
        }


        public bool NewWindow(LuaTable tab)
        {
            MyArg arg = new(tab);
            string ret;
            if (arg.Is("page"))
            {
                var page = new KbTabItem(arg);
                KbLua.CopyTable("B12_Integretion_arg", tab, page.win.lua);
                ret = page.win.lua.DoString(Conf("new_window_init_script"));
                if (ret != "")
                    return arg.Error(ret);

                MainWindow.NewPage(page);
                return false;
            }

            var dialog = new KbDialog(arg);
            if(arg.Try("smb",out string smb))
            {
                dialog_list[smb]=dialog;
            }

            KbLua.CopyTable("B12_Integretion_arg", tab, dialog.win.lua);
            dialog.Owner = App.Current.MainWindow;
            
            ret = dialog.win.lua.DoString(Conf("new_window_init_script"));
            
            if (ret != "")
            {
                dialog.Close();
                return arg.Error(ret);
            }


            if (arg.Is("modeless"))
            {
                dialog.Show();
                return false;
            }

            
            dialog.ShowDialog();
            KbLua.CopyTable("B12_Integretion_ret", dialog.win.lua, lua);
            dialog.Close();
            return false;
        }
    }
}
