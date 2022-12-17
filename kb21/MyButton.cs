using System.Windows;
using System.Windows.Controls;
using kb21;


namespace kb21
{
    class MyButton:Button,MyCtrl
    {
        KbWindow win;
        string id;

        public MyButton(MyArg arg, KbWindow _win)
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