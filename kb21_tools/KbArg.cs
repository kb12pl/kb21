using System;
using System.Collections.Generic;
using NLua;

namespace kb21_tools
{
    public class MyArg
    {        
        readonly LuaTable tab;
        public MyArg(LuaTable tab) 
        {
            this.tab = tab;            
        }
        public bool Is(string key)
        {
            return tab[key]!=null  ;
        }

        public bool Set(string key, object ?val)
        {
            if(val!=null)
                tab[key] = val;            
            return false;
        }

        public bool Set(int key, object val)
        {
            tab[key] = val;
            return false;
        }


        public string Get(string key)
        {
            return Convert.ToString(tab[key]);
        }

        public int GetI(string key,int def=0)
        {
            if (Int32.TryParse(Get(key), out int j))
            {
                return j;
            }
            else
            {
                return def;
            }            
        }

        public bool Try(string key, out string val)
        {
            val = Get(key);
            return Is(key); 
        }

        public bool Try(string key,out int val)
        {
            if (Int32.TryParse(Get(key), out val))
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public bool Error(string error)
        {            
            tab["error"] = error;
            return true;
        }
    }
}
