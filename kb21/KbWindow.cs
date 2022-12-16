using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using NLua;
using kb_lib;
using static kb_lib.Log;
using static kb_lib.Conf;

namespace kb12
{
    public partial class KbWindow
    {


        public readonly kb_lib.Lua lua;        
        static readonly Dictionary<string, MyDialog> dialog_list = new();
        public static readonly Dictionary<string, object> globals = new();
        readonly ContentControl contentControl;
        private readonly bool dialog;
        private readonly bool page;
        public static void KbWindowOk(string mes) => ok(mes);
        public static void KbWindowOkClear() => xlogclear();
        internal KbWindow(MyDialog _dialog, MyArg arg)
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
        internal KbWindow(MyTabItem _page)
        {
            lua = new(this);
            contentControl = _page;
            page = true;            
        }

        public static string GetConfig(string key)=>xconf(key);
        public static void SetGlobal(string key, object b)
        {   
            globals[key] = b;
        }
        public static object? GetGlobal(string key)
        {            
            if (globals.TryGetValue(key,out object? obj))            
            {         
                return obj;
            }
            return null;
        }

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
                    MyFrame.Remove((MyTabItem)contentControl);
                    
                }
                
            }

        }

        public bool NewWindow(LuaTable tab)
        {
            MyArg arg = new(tab);
            string ret;
            if (arg.Is("page"))
            {
                var page = new MyTabItem(arg);
                kb_lib.Lua.CopyTable("B12_Integretion_arg", tab, page.win.lua);
                ret = page.win.lua.DoString(xconf("new_window_init_script"));
                if (ret != "")
                    return arg.Error(ret);

                MyFrame.NewPage(page);
                return false;
            }

            var dialog = new MyDialog(arg);
            if(arg.Try("smb",out string smb))
            {
                dialog_list[smb]=dialog;
            }

            kb_lib.Lua.CopyTable("B12_Integretion_arg", tab, dialog.win.lua);
            dialog.Owner = App.Current.MainWindow;
            
            ret = dialog.win.lua.DoString(xconf("new_window_init_script"));
            
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
            kb_lib.Lua.CopyTable("B12_Integretion_ret", dialog.win.lua, lua);
            dialog.Close();
            return false;
        }
    }
}
