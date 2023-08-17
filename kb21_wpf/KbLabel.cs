using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using kb21;


namespace kb21_wpf
{
    internal class KbLabel : Label, KbCtrl
    {

        KbWindow win;
        string id;

        public KbLabel(MyArg arg, KbWindow _win)
        {
            id = arg.Get("id");
            win = _win;            
            Content = arg.Get("label");          
        }
    }
}
