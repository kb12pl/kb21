using System.Windows;
using kb_lib;

namespace kb12
{
    interface MyCtrl
    {
        static void ok(object a) => MyFrame.ok(a);
        public bool Cmd(MyArg arg)
        {
            return arg.Error("Cmd not define in control");
        }

        public bool Add(MyCtrl ctrl, MyArg arg)
        {
            ok("Add not implemented in MyCtrl");
            return arg.Error("Add not implemented");            
        }  

    }
}
