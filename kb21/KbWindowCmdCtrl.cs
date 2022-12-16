using NLua;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using kb_lib;
using static kb_lib.Log;
using static kb_lib.Conf;

namespace kb12
{
    public partial class    KbWindow
    {
        public bool CmdCtrl(LuaTable tab)
        {
            var arg = new MyArg(tab);
            if (!ctrl_list.TryGetValue(arg.Get("id"), out MyCtrl? ctrl))
            {
                xlog("ctrl is null");
                return arg.Error("ctrl is null");
            }

            if (arg.Is("focus"))
            {

                ((Control)ctrl).Focus();
                //Keyboard.Focus(((Control)ctrl));
                return false;
            }

            return ctrl.Cmd(arg);
        }
    }
}