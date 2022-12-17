using System.Windows;
using kb21;
using static kb21.Conf;

namespace kb21
{
    /// <summary>
    /// 222
    /// </summary>
    class MyDialog:Window
    {
        public readonly KbWindow win;
        //internal bool isError=false;

        public MyDialog(MyArg arg)
        {

            win = new(this,arg);            
            //Width = arg.GetI("width",800);
            //Height = arg.GetI("hieght", 600);
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //SizeToContent = SizeToContent.WidthAndHeight;
            ShowInTaskbar = false;
            Loaded += MyLoaded;
        }

        private void MyLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            win.Integration(xconf("window_on_load_event"));            
        }
    }
}
