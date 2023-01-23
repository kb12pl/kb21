global using kb21_tools;
global using static kb21_tools.KbConf;
global using static kb21_tools.KbLog;


using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace kb21
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
            
            win.lua.DoString(Conf("frame_init_script"));
        }
        
        

        internal static bool NewPage(KbTabItem page)
        {
            staticTabControl.Items.Add(page);
            staticTabControl.SelectedItem = page;
            page.Focus();
            return false;
        }

        internal static bool Remove(KbTabItem page)
        {
            staticTabControl.Items.Remove(page);
            // staticTabControl.Focus();
            return false;
        }
    }
}
