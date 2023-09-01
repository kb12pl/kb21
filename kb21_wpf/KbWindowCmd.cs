using NLua;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks;

using kb21_tools;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace kb21_wpf
{
    public partial class KbWindow
    {
        public bool CmdWin(LuaTable tab)
        {
            var arg = new MyArg(tab);
            var error = true;
            
            
            if (arg.Try("message",out string mess))
            {
                arg.Try("caption", out string caption);
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBoxImage image = MessageBoxImage.Information;
                MessageBoxResult result = MessageBoxResult.OK;
                if (arg.Is("cancel"))
                {
                    buttons = MessageBoxButton.OKCancel;
                    result = MessageBoxResult.Cancel;
                }
                if (arg.Is("yes_no"))
                {
                    buttons = MessageBoxButton.YesNo;
                    image = MessageBoxImage.Question;
                    result= MessageBoxResult.No;
                }
                if (arg.Is("yes_no_cancel"))
                {
                    buttons = MessageBoxButton.YesNoCancel;
                    image = MessageBoxImage.Question;
                    result = MessageBoxResult.Cancel;
                }


                MessageBoxResult res=MessageBox.Show(mess,caption, buttons, image, result);
                if (res == MessageBoxResult.Yes)
                    return arg.Set("yes",1);
                return false;
            }
            
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
            
            
            if (dialog && arg.Try("width",out int par))
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

            if (arg.Is("frame_close"))
            {
                System.Windows.Application.Current.Shutdown();
                return false;
            }


            if (error)
                return arg.Error("unknown command");
            
            return false;
        }

    }
}