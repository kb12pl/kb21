using System.Windows;

namespace kb21
{
    interface KbCtrl
    {
        static void ok(object a) => MyFrame.ok(a);
        public bool Cmd(MyArg arg)
        {
            return arg.Error("Cmd not define in control");
        }

        public bool Add(KbCtrl ctrl, MyArg arg)
        {
            ok("Add not implemented in MyCtrl");
            return arg.Error("Add not implemented");            
        }  

    }
}
