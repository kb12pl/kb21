global using kb21_tools;
global using static kb21_tools.KbConf;
global using static kb21_tools.KbLog;


using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace kb21_wpf
{
    public partial class MainWindow : Window
    {        
        static TabControl ?staticTabControl;
        static KbWindow? win;
        public MainWindow()
        {
            InitializeComponent();
            LogInit((string s)=>System.Windows.MessageBox.Show(s), null);
            win=new KbWindow(this);
            staticTabControl = myTabCtrl;
            Loaded += MyLoaded;
        }
        private void MyLoaded(object sender, RoutedEventArgs e)=>win.lua.DoScript("kb21_frame");
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
