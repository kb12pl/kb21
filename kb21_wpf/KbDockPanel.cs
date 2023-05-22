using System.Windows;
using System.Windows.Controls;
using kb21;
namespace kb21_wpf
{
    class KbDockPanel: DockPanel, KbCtrl
    {        
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
