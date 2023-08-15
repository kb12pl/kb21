using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace kb21_wpf
{
    public partial class KbWindow
    {
        public string? Shortcut(string shortcut, string kod, bool shift = false, bool alt = false, bool ctrl = false)
        {

            ModifierKeys mod = 0;
            if (shift)
                mod |= ModifierKeys.Shift;
            if (ctrl)
                mod |= ModifierKeys.Control;
            if (alt)
                mod |= ModifierKeys.Alt;


            RoutedCommand newCmd = new(shortcut, typeof(string));
            newCmd.InputGestures.Add(new KeyGesture((Key)Enum.Parse(typeof(Key), kod), mod));
            contentControl.CommandBindings.Add(new CommandBinding(newCmd, DoShortcut));

            return null;
        }

        private void DoShortcut(object sender, ExecutedRoutedEventArgs e)
        {          
            Integration("onShort", "", ((RoutedCommand)e.Command).Name);
        }

        private void DoMenu(object sender, ExecutedRoutedEventArgs e)
        {
            Integration("onMenu", "", ((RoutedCommand)e.Command).Name);
        }

        public bool AppendMenu(LuaTable tab)
        {
         
            var arg = new MyArg(tab);
            if (!frame)
                return arg.Error("it is not frame");

            
            string name = arg.Get("name");            
                
            MenuItem item = new()
            {
                Header = name
            };
            
            RoutedCommand newCmd = new(name, typeof(string));            
            contentControl.CommandBindings.Add(new CommandBinding(newCmd, DoMenu));
            item.Command = newCmd;
            
            menu_list[name] = item;

            string parent = arg.Get("parent");
            
            if (menu_list.TryGetValue(parent, out MenuItem menuparent))
                menuparent.Items.Add(item);
            else
                menubar.Items.Add(item);
            
            return true;
        }
    }
}
