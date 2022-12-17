using System.Windows.Controls;
using System.Windows.Input;
using kb21;
using static kb21.Log;

namespace kb21
{
    class MyTextBox:TextBox,MyCtrl
    {
        private readonly KbWindow win;
        string id;
        bool onKey;
        bool onEnter;

        public MyTextBox(MyArg arg, KbWindow _win)
        {
            win=_win;
            id = arg.Get("id");
            onKey = arg.Is("onKey");
            onEnter = arg.Is("onEnter");

            if (onKey)
                KeyUp += MyKeyUp;
            if (onEnter)
                KeyDown += MyKeyDown;
        }

        private void MyKeyDown(object sender, KeyEventArgs e)
        {
            if (onEnter && e.Key == Key.Enter)
                win.Integration("onEnter", id, e.Key.ToString());
        }

        private void MyKeyUp(object sender, KeyEventArgs e)
        {
            if (onEnter && e.Key == Key.Enter)
                return;
            else if (onKey)
                win.Integration("onKey", id, e.Key.ToString());
        }


        public bool Cmd(MyArg arg)
        {
            if (arg.Is("set"))
            {
                Text = arg.Get("set");
                return false;
            }

            if (arg.Is("get"))
                return arg.Set("text", Text);

            if (arg.Is("set"))
            {
                Text = arg.Get("text");
                return false;
            }

            return arg.Error("unknown arg");
        }
    }
}
