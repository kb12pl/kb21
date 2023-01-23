using System.Windows;
using System.Windows.Controls;
using kb21;


namespace kb21
{
    class KbStackPanel:StackPanel,KbCtrl
    {         
        public KbStackPanel(MyArg arg)
        {
            Margin = new Thickness(5);

            if (arg.Is("horizontal"))
            {
                //ok(1);
                Orientation = Orientation.Horizontal;
            }
            else
            {
                //ok(2);
                Orientation = Orientation.Vertical;
            }
        }
        public bool Add(KbCtrl ctrl, MyArg arg)
        {           
                
            Children.Add((UIElement)ctrl);

            return false;
        }
    }
}
