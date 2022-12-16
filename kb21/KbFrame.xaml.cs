using System.Windows;
using System.Windows.Controls;
using static kb_lib.Conf;
using static kb_lib.Log;
namespace kb12
{
    public partial class MyFrame : Window
    { 
        private readonly KbWindow win;
        static TabControl staticTabControl;        
        static public void ok(object o) => System.Windows.MessageBox.Show(o.ToString());
        
        public MyFrame()
        {
            InitializeComponent();
            LogInit(ok,null);            
            win = new(this);
            Loaded += MyLoaded;
        }

        private void MyLoaded(object sender, RoutedEventArgs e)        
        {
            staticTabControl = myTabCtrl;            
            win.lua.DoString(xconf("frame_init_script"));
        }
        
        

        internal static bool NewPage(MyTabItem page)
        {
            staticTabControl.Items.Add(page);
            staticTabControl.SelectedItem = page;
            page.Focus();
            return false;
        }

        internal static bool Remove(MyTabItem page)
        {
            staticTabControl.Items.Remove(page);
            // staticTabControl.Focus();
            return false;
        }
    }
}
