using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace kb21_wpf
{
    public class KbDatePicker: DatePicker, KbCtrl
    {
        KbWindow win;
        string id;

        public KbDatePicker(MyArg arg, KbWindow _win)
        {
            id = arg.Get("id");
            win = _win;            
            Margin = new Thickness(5);
            SelectedDate=DateTime.Now;
            SelectedDateChanged += KbDatePicker_SelectedDateChanged;
        }

        private void KbDatePicker_SelectedDateChanged(object? sender, SelectionChangedEventArgs e)
        {
            win.Integration("changed", id);
        }
        public bool Cmd(MyArg arg)
        {
            if (arg.Is("set"))
            {
                Text = arg.Get("set");
                return false;
            }

            if (arg.Is("get"))
                return arg.Set("text", Text);

            if (arg.Is("set"))
            {
                Text = arg.Get("text");
                return false;
            }

            return arg.Error("unknown arg");
        }
    }
}
