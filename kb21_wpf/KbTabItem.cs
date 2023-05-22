using System.Windows.Controls;



namespace kb21_wpf
{
    public class KbTabItem:TabItem
    {
        public readonly KbWindow win;
        public KbTabItem(MyArg arg)
        {
            win = new(this);
            Header = arg.Get("title");
            Loaded += MyLoaded;
        }

        private void MyLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            win.Integration(Conf("window_on_load_event"));            
        }

    }
}
