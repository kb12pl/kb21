using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using kb21;
using static kb21.Conf;

namespace kb21
{
    public class MyTabItem:TabItem
    {
        public readonly KbWindow win;
        public MyTabItem(MyArg arg)
        {
            win = new(this);
            Header = arg.Get("title");
            Loaded += MyLoaded;
        }

        private void MyLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            win.Integration(xconf("window_on_load_event"));            
        }

    }
}
