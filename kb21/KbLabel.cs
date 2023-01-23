using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using kb21;


namespace kb21
{
    internal class KbLabel : Label, KbCtrl
    {

        KbWindow win;
        string id;

        public KbLabel(MyArg arg, KbWindow _win)
        {
            id = arg.Get("id");
            win = _win;
            Foreground = Brushes.White;
            Content = arg.Get("label");
        }
    }
}
