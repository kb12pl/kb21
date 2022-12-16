using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kb_ret
{ 
    public class Ret
    {
        public Dictionary<int, object> dic = new();
        public Dictionary<int,string>labels = new();
        public bool isTable = false;
        public bool isText = false;
        public bool isInt = false;
        public bool isError = false;
        public int cols;
        public int rows;
        public int number;
        public string? text;         
        public string? error;
        
        public void SetError(string _error)
        {
            isError = true;
            error = _error;
        }
        public void SetCols(int _cols)
        {
            isTable=  true;
            cols = _cols;
        }
        public void SetRows(int _rows) => rows = _rows;        
        public void Set(int key,object val)
        {
            dic[key] = val;
            isTable = true;
        }
        public void Set(int row,int col, object val)=>Set(row * cols + col,val);
        public void Set(string val)
        {
            text = val;
            isText = true;
        }
        public void Set(int val)
        {
            number = val;
            isInt = true;
        }
        public string Get(int row, int col)
        {
            return dic[row * cols + col].ToString();
        }
        public string GetLabel(int col)
        {
            return labels[col];
        }
    }
}
