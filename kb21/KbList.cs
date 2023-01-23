using ICSharpCode.AvalonEdit.Editing;
using kb21;
using Newtonsoft.Json.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static kb21_tools.KbLog;


namespace kb21
{

    internal class KbList : ListBox, KbCtrl
    {
        class KbListItem
        {
            public string Name { get; set; }
            public string Key { get; set; }
        }
        KbWindow win;
        string id;

        public KbList(MyArg arg, KbWindow _win)
        {
            id = arg.Get("id");
            win = _win;
            DisplayMemberPath = "Name";
            MouseDoubleClick += MyDoubleClick;
            /*
            Style itemStyle = new Style(typeof(ListBoxItem));
            MultiTrigger isSelected = new MultiTrigger();

            Condition  cond = new Condition();
            cond.Property = ListBoxItem.IsSelectedProperty;
            cond.Value= true;            
            isSelected.Conditions.Add(cond);
            cond=new Condition();
            cond.Property = ListBoxItem.IsFocusedProperty;
            cond.Value = false;
            isSelected.Conditions.Add(cond);
            isSelected.Setters.Add(new Setter(ListBoxItem.BackgroundProperty ,                
                (Brush)Application.Current.FindResource(SystemColors.HighlightBrushKey)));
            isSelected.Setters.Add(new Setter(ListBoxItem.ForegroundProperty,
                (Brush)Application.Current.FindResource(SystemColors.HighlightTextBrushKey)));
            isSelected.Setters.Add(new Setter(ListBoxItem.BorderBrushProperty,
                (Brush)Application.Current.FindResource(SystemColors.HighlightBrushKey)));
            itemStyle.Triggers.Add(isSelected);

            ItemContainerStyle = itemStyle;
            */
        }
        public bool Cmd(MyArg arg)
        {
            if (arg.Is("add"))
            {
                Items.Add(new KbListItem() { Name = arg.Get("add"),Key=arg.Get("key")});                
                //Items.Refresh();
                return false;
            }
            
            if (arg.Is("get"))            
            {
                KbListItem item =(KbListItem)SelectedItem;                
                arg.Set("text",item?.Name);
                return false;
            }

            if (arg.Is("getKey"))
            {
                KbListItem item = (KbListItem)SelectedItem;
                arg.Set("key", item?.Key);
                return false;
            }
            if (arg.Is("clear"))
            { 
                Items.Clear();            
                return false;
            }
            if (arg.Try("select",out int idx))
            {       
                if(Items.Count==0)
                    return false;

                var a = Items.IndexOf(SelectedItem);

                if (idx!=0)
                {                                  
                    idx = a + idx;
                    if (idx >= Items.Count)
                        idx = Items.Count - 1;
                    if (idx < 0)
                        idx = 0;
                }
                SelectedItem = Items[idx]; 
                return false;
            }

            return arg.Error("unknown arg");
        }

        void MyDoubleClick(object sender, RoutedEventArgs e)
        {            
            win.Integration("onEnter", id);            
        }

    }
}
