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
            WindowState = WindowState.Maximized;
            Title = "Bite it - Rcp ver-1.0";

            LogInit((string s)=>System.Windows.MessageBox.Show(s), null);                        
            Loaded += MyLoaded;
            
        }
        private void MyLoaded(object sender, RoutedEventArgs e)
        {
            staticTabControl = myTabCtrl;            
            win = new KbWindow(this, MainMenu);        
            win.lua.DoScript("kb21_frame");
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
