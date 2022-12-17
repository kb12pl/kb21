using System.Text;
using NLua;
using static kb21.Log;
using static kb21.Conf;

namespace kb21
{
    public class Lua
    {
        private readonly NLua.Lua lua = new();
        public Lua(object o)
        {
            lua.LoadCLRPackage();
            lua.State.Encoding = Encoding.UTF8;
            lua["B12_Integretion_Object"] = o;
            DoString(xconf("initLua"));
        }

        public NLua.Lua GetPtr() => lua;               
        public string DoScript(string script)=>DoString(Conf.GetScript(script));

        public string DoString(string script)
        {        
            
            //xlog(script);
            try
            {
                lua.DoString(script);
                return "";
            }
            catch (NLua.Exceptions.LuaException e)
            {
                if (e.Message == "stop")
                    return "stop";
                if (e.Message == "close")
                    return "close";

                xlog(e.Message);
                return "stop";
            }
        }


        public static void CopyTable(string v, Lua lua1, Lua lua2)
        {
            LuaTable tb1 = lua1.lua[v] as LuaTable;
            lua2.lua.NewTable(v);

            if (tb1 == null)
                return;


            LuaTable tb2 = lua2.lua[v] as LuaTable;
            foreach (var key in tb1.Keys)
            {
                tb2[key] = tb1[key];
            }
        }

        public static LuaTable CopyTable(string path, LuaTable tb1, Lua lua2)
        {
            if (tb1 == null)
            {
                xlog("Lua Table is nil: " + path);
                return null;
            }

            lua2.lua.NewTable(path);
            LuaTable tb2 = lua2.lua[path] as LuaTable;
            foreach (var key in tb1.Keys)
            {
                if (tb1[key] is LuaTable)
                {
                    tb2[key] = CopyTable(path + '.' + key, tb1[key] as LuaTable, lua2);
                }
                else
                {
                    tb2[key] = tb1[key];
                }
            }
            return tb2;
        }
        public void SetNil(string v)
        {
            lua[v] = null;
        }
        
    }
}


