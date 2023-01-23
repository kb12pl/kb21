using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kb21_tools
{ 
    public class Ret
    {
        public Dictionary<int, object> dic = new();
        public Dictionary<int,string>labels = new();
        public Dictionary<string, object> key_val = new();
        public bool isError = false;
        public int cols;
        public int rows;
        public string? error;
        
        public void SetError(string _error)
        {
            isError = true;
            error = _error;
        }

        public void SetCols(int _cols)=>cols = _cols;
        public void SetRows(int _rows) => rows = _rows;        
        
        public void Set(int key,object val)=>dic[key] = val;
        public void Set(int row, int col, object val) => Set(row * cols + col, val);
        public void Set(string key, object val)=>key_val[key] = val;
        
        
        public string Get(int row, int col)=>dic[row * cols + col].ToString();        
        public string Get(string key)=> key_val[key].ToString();

        public string GetLabel(int col)=>labels[col];
    }
}
