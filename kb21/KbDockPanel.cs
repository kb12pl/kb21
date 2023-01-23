using System.Windows;
using System.Windows.Controls;
using kb21;
namespace kb21
{
    class KbDockPanel: DockPanel, KbCtrl
    {
        static void ok(object a) => MyFrame.ok(a);
        public KbDockPanel()
        {
        }
        public bool Add(KbCtrl ctrl, MyArg arg)
        {
            Children.Add((UIElement)ctrl);
            return false;
        }
    }
}
