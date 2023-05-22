using System.Windows;


namespace kb21_wpf
{
    /// <summary>
    /// 222
    /// </summary>
    class KbDialog:Window
    {
        public readonly KbWindow win;
        //internal bool isError=false;

        public KbDialog(MyArg arg)
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
            win.Integration(Conf("window_on_load_event"));            
        }
    }
}
