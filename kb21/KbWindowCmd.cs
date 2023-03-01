using NLua;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks;

using kb21_tools;

namespace kb21
{
    public partial class KbWindow
    {
        public bool CmdWin(LuaTable tab)
        {
            var arg = new MyArg(tab);
            var error = true;

            if (arg.Is("title"))
            {
                if (dialog)
                {
                    Window w = (Window)contentControl;
                    w.Title = arg.Get("title");
                    error = false;
                }
            }
            if (dialog && arg.Is("maximized"))
            {
                    Window w = (Window)contentControl;
                    w.WindowState = System.Windows.WindowState.Maximized;
                    error = false;             
            }
            
            int par;
            if (dialog && arg.Try("width",out par))
            {
                    Window w = (Window)contentControl;
                    w.Width = par;
                    error = false;
            }

            if (dialog && arg.Try("height", out par))
            {
                Window w = (Window)contentControl;
                w.Height= par;
                error = false;
            }

            if (arg.Is("clear"))
            {
                contentControl.Content = null;
                return false;
            }


            if (arg.Is("serial_read"))
            {
                //Serial s=new();
                //string r=s.Read();
                //return arg.Set("text",r);
                
            }

            if(arg.Try("smb",out string smb))
            {
                KbDialog dialog = dialog_list[smb];
                dialog.Topmost = true;
                if (dialog != null)
                {
                    dialog.win.lua.DoString(arg.Get("script"));
                }
                return false;                
            }
 
            if (error)
                return arg.Error("unknown command");
            
            return false;
        }

    }
}