using System.Windows;

namespace kb21_wpf
{
    interface KbCtrl
    {        
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
