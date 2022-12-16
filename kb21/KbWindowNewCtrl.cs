using NLua;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using kb_lib;
using static kb_lib.Log;
using static kb_lib.Conf;
using System.Windows.Forms;

namespace kb12
{
    public partial class KbWindow
    {
        readonly Dictionary<string, MyCtrl> ctrl_list = new();

        public bool NewCtrl(LuaTable tab)
        {
            MyArg arg = new(tab);            
            MyCtrl? ctrl =null;
            
            var id = arg.Get("id");

            if (arg.Is("isGrid"))
                ctrl = new MyDataGrid(arg, this);
            if (arg.Is("isButton"))
                ctrl = new MyButton(arg, this);
            if (arg.Is("isText"))
                ctrl = new MyTextBox(arg, this);
            if (arg.Is("isStack"))
                 ctrl = new MyStackPanel(arg);
            if (arg.Is("isDock"))
                ctrl = new MyDockPanel();
            if (arg.Is("isCode"))
                ctrl = new MyCode(arg, this);
            if (arg.Is("isList"))
                ctrl = new KbList(arg, this);
            if (arg.Is("isLabel"))
                ctrl = new KbLabel(arg, this);
            if (arg.Is("isWeb"))
                ctrl = new KbWebView(arg, this);
            

            if (ctrl==null)
                return arg.Error("arg is not recognized in NewCtrl");
                       
            ctrl_list[id] = ctrl;
            
            int par;

            var uiel = (UIElement)ctrl;
            if (arg.Try("Dock", out par))
                DockPanel.SetDock(uiel, (Dock)par);


            var frel = (FrameworkElement)ctrl;
            if (arg.Try("height", out par))
                frel.MinHeight = par;
            if (arg.Try("width", out par))
                frel.MinWidth = par;
            if (arg.Try("VerticalAlignment", out par))
                frel.VerticalAlignment = (System.Windows.VerticalAlignment)par;
            if (arg.Try("HorizontalAlignment", out par))
                frel.HorizontalAlignment = (System.Windows.HorizontalAlignment)par;


            if (contentControl.Content==null)
            {
                contentControl.Content = ctrl;
                return false;
            }
            
            var pa = arg.Get("parent") ?? "";

            if (!ctrl_list.TryGetValue(pa, out MyCtrl? parent))
            {
                ok("parent not exists: "+pa);
                return arg.Error("parent not exists");
            }

            parent.Add(ctrl, arg);


            return false;
        }
    }
}

