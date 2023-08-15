using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using kb21;


namespace kb21_wpf
{
    class KbDataGrid:DataGrid,KbCtrl
    {        
        KbWindow win;
        string id;

        public KbDataGrid(MyArg arg, KbWindow _win)
        {

            id = arg.Get("id");
            win = _win;
            IsReadOnly = true;
            SelectionUnit = DataGridSelectionUnit.Cell;
            MouseDoubleClick += MyDoubleClick;
            PreviewKeyDown += MyPreviewKeyDown;
            //Background = Brushes.Black;

            /*
            Style cellStyle = new Style(typeof(DataGridCell));

            // If a cell is editing the border should be red
            Trigger isEditingTrigger = new Trigger();
            isEditingTrigger.Property = DataGridCell.IsSelectedProperty;
            isEditingTrigger.Value = true;
            isEditingTrigger.Setters.Add(new Setter(DataGridCell.BackgroundProperty,
                (Brush)Application.Current.FindResource(SystemColors.HighlightBrushKey)));
            isEditingTrigger.Setters.Add(new Setter(DataGridCell.ForegroundProperty,
                (Brush)Application.Current.FindResource(SystemColors.HighlightTextBrushKey)));
            isEditingTrigger.Setters.Add(new Setter(DataGridCell.BorderBrushProperty,
                (Brush)Application.Current.FindResource(SystemColors.HighlightBrushKey)));

            cellStyle.Triggers.Add(isEditingTrigger);
            
            // Set the cell style for the grid
            CellStyle = cellStyle;
            */
        }


        void MyDoubleClick(object sender, RoutedEventArgs e)
        {
            IList<DataGridCellInfo> cells = SelectedCells;
            if (cells.Count != 1)
                return;
            foreach (DataGridCellInfo cell in cells)
            {
                string header = cell.Column.Header as string;                
                var item = cell.Item as Item;                

                win.Integration("dClick", id, header, item.key);
                
            }
        }

        private void MyPreviewKeyDown(object sender, KeyEventArgs e)
        {
            //var u = e.OriginalSource as UIElement;
            //if (e.Key == Key.Enter && u != null)
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                MyDoubleClick(sender, new RoutedEventArgs());
                //u.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));
            }
        }

        public bool Cmd(MyArg arg)
        {
            if(arg.Is("set"))
            {
                var row = arg.GetI("row",1);
                var col = arg.GetI("col",1);
                for(int i=Items.Count;i<row;i++)
                {
                    Items.Add(new Item(i,""));
                }
                var item = (Item)Items[row-1];
                
                item[col] = arg.Get("set");
                Items.Refresh();
                
                return false;
            }

            if(arg.Is("key"))
            {
                var row = arg.GetI("row", -1);
                if (row < 1)
                    return arg.Error("row<1");
                if (row > Items.Count)
                        return arg.Error("row>count");
                Item item = Items[row - 1] as Item;
                item.key = arg.Get("key");
                return false;
            }

            if(arg.Is("clear"))
            {
                Items.Clear();
                return false;
            }

            if(arg.Is("column"))
            {
                DataGridTextColumn col = new DataGridTextColumn();
                col.Width = new DataGridLength(50,DataGridLengthUnitType.Auto );
                col.Header = arg.Get("column");                
                string tmp = arg.Get("bind");
                if (tmp == "")
                    tmp = "0";
                col.Binding = new Binding("["+tmp+"]");
                Columns.Add(col);                
                return false;
            }

            if (arg.Is("focus_datagrid"))
            {

                if ((Items.Count > 0) &&(Columns.Count > 0))
                {
                    //Select the first column of the first item.
                    CurrentCell = new DataGridCellInfo(Items[0],Columns[0]);
                    SelectedCells.Clear();
                    SelectedCells.Add(CurrentCell);
                    var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
                    timer.Start();
                    timer.Tick += (sender, args) =>
                    {
                        DataGridCell cell = GetDataGridCell(CurrentCell);
                        if (cell != null)
                        {
                            cell.Focus();
                        }
                        timer.Stop();
                    };

                }

                return false;
            }
            if (arg.Is("select"))
            {
                int select = arg.GetI("select");
                SelectedItems.Clear();
                if (Items.Count == 0)
                    return false;
                SelectionUnit = DataGridSelectionUnit.CellOrRowHeader;                
                if (select < Items.Count)
                    select = 0;
                if (select >= Items.Count)
                    select = Items.Count - 1;
                       
                object item = Items[select];                
                SelectedItem = item;                
                ScrollIntoView(select);
                return false;
            }
            return arg.Error("unknown arg");
        }

        private DataGridCell GetDataGridCell(DataGridCellInfo cellInfo)
        {           

            var cellContent = cellInfo.Column.GetCellContent(cellInfo.Item);

            if (cellContent != null)
                return ((DataGridCell)cellContent.Parent);

            return (null);
        }

    }

    class Item
    {        
        internal readonly Dictionary<int, object> data = new();
        internal string key;
        internal Item(int i, string _key)
        {
            key = _key;
            i++;
            this[0] = i.ToString();
        }
        public object this[int i]
        {
            get
            {
                data.TryGetValue(i, out object v);
                return v;
            }
            set
            {
                data[i] = value;
            }
        }
    }
}
