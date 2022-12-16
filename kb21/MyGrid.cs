using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using kb_lib;


namespace kb12
{    
    class MyGrid:Grid,MyCtrl
    {    
        public MyGrid()
        {
            var border = new Border();
            border.BorderThickness = new Thickness(5, 5, 5, 5);
            Children.Add(border);

            

        }
        public bool Add(MyCtrl ctrl,MyArg arg)
        {            
            Children.Add((UIElement)ctrl);
            return false;
        }

    }
}
