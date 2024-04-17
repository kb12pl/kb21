using System.Runtime.CompilerServices;
using System.Text;
using NLua;
using static kb21_tools.KbLog;

namespace kb21_tools
{
    public class KbLua
    {
        private static readonly Lua global = new();
        private readonly Lua lua = new();
        public KbLua()
        {            
        }
        public KbLua(IKbWindow o)
        {
            lua.LoadCLRPackage();
            lua.State.Encoding = Encoding.UTF8;
            lua["B12_Integretion_Object"] = o;
            DoString(KbConf.lua_start);
        }

        public NLua.Lua GetPtr() => lua;               
        public string DoScript(string script)=>DoString(KbConf.GetScript(script));

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

                ok(e.Message);
                return "stop";
            }
        }


        public static void CopyTable(string v, KbLua lua1, KbLua lua2)
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

        public static LuaTable CopyTableFromKbLua(string path, LuaTable tb1, KbLua lua2)
        {
            return CopyTable(path, tb1, lua2.lua);
        }

        public static LuaTable CopyTable(string path, LuaTable copyFrom, Lua toLua)
        {
            if (copyFrom == null)
            {
                ok("Lua Table is nil: " + path);
                return null;
            }

            toLua.NewTable(path);
            LuaTable tb2 = toLua[path] as LuaTable;
            foreach (var key in copyFrom.Keys)
            {
                if (copyFrom[key] is LuaTable)
                {
                    tb2[key] = CopyTable(path + '.' + key, copyFrom[key] as LuaTable, toLua);
                }
                else
                {
                    tb2[key] = copyFrom[key];
                }
            }
            return tb2;
        }


        public void SetNil(string v)
        {
            lua[v] = null;
        }

        public void SetGlobal(string key, LuaTable tab)                
        {            
            CopyTable(key, tab, global);           

        }

        public void GetGlobal(string key,string name)
        {
            LuaTable tab= global[key] as LuaTable;
            if (tab == null)
                lua[key]= null;
            else
                CopyTable(name, tab, lua);
        }
        
    }
}


