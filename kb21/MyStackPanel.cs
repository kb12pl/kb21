﻿using System.Windows;
using System.Windows.Controls;
using kb_lib;
using static kb_lib.Log;

namespace kb12
{
    class MyStackPanel:StackPanel,MyCtrl
    {         
        public MyStackPanel(MyArg arg)
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
        public bool Add(MyCtrl ctrl, MyArg arg)
        {           
                
            Children.Add((UIElement)ctrl);

            return false;
        }
    }
}
