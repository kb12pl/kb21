using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using kb_lib;


namespace kb12
{
    internal class KbLabel : Label, MyCtrl
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
