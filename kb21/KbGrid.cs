using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using kb21;


namespace kb21
{    
    class KbGrid:Grid,KbCtrl
    {    
        public KbGrid()
        {
            var border = new Border();
            border.BorderThickness = new Thickness(5, 5, 5, 5);
            Children.Add(border);

            

        }
        public bool Add(KbCtrl ctrl,MyArg arg)
        {            
            Children.Add((UIElement)ctrl);
            return false;
        }

    }
}
