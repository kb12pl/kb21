using System.Windows;
using System.Windows.Controls;
using kb21_tools;


namespace kb21_wpf
{
    class KbButton:Button,KbCtrl
    {
        KbWindow win;
        string id;

        public KbButton(MyArg arg, KbWindow _win)
        {
            id = arg.Get("id");
            win = _win;
            Content = arg.Get("label");
            Margin = new Thickness(5);
            Click += MyClick;
        }
        void MyClick(object sender, RoutedEventArgs e)
        {            
            win.Integration("click", id);
        }
    }   
}