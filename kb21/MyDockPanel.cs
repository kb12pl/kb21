using System.Windows;
using System.Windows.Controls;
using kb_lib;
namespace kb12
{
    class MyDockPanel: DockPanel, MyCtrl
    {
        static void ok(object a) => MyFrame.ok(a);
        public MyDockPanel()
        {
        }
        public bool Add(MyCtrl ctrl, MyArg arg)
        {
            Children.Add((UIElement)ctrl);
            return false;
        }
    }
}
