using NLua;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using kb21;
using static kb21.Log;
using static kb21.Conf;

namespace kb21
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