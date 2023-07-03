using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static kb21_tools.KbLog;

namespace kb21_tools
{
    public class KbConf
    {        
        static bool isLoad = false;        
        static Dictionary<string, string> dictSecret = new();
        public static readonly Dictionary<string, object> globals = new();
        static readonly Dictionary<string, string> dict = new();        
        public static readonly string lua_start= @"
function ok(kom)    
    B12_Integretion_Object:ok(tostring(kom));
end
dofile(B12_Integretion_Object:GetConfig('prefix_file_script')..'sys_window.lua')";
        static KbConf()
        {
            dict["test"] = "123";
            dict["B12_Integretion_Object"] = "B12_Integretion_Object";
            dict["secret_config_file"] = "c:/repo1/config.txt";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                dict["prefix_file_script"] = "c:/repo/kb21/kb21_tools/lua/";
                dict["secret_config_file"] = "c:/repo/config.txt";
            }
            else
            {
                dict["prefix_file_script"] = "../kb21_lua/";
                dict["secret_config_file"] = "../../config.txt";
            }

            dict["frame_init_script"] = "kb.sys_boot()";
            dict["new_window_init_script"] = "win:on_boot()";
            dict["window_on_load_event"] = "on_load";
        }
        public static string Secret(string key)
        {
            Load();
            return dictSecret[key];
            
        }

        public static string Conf(string key)
        {            
            if (dict.TryGetValue(key, out string? obj))
            {
                return obj;   
            }            
            return String.Empty;
        }

        public static string GetScript(string script)
        {
            return File.ReadAllText(Conf("prefix_file_script") + script + ".lua");
        }

        static void Load()
        {
            if (isLoad)
                return;
            try
            {
                Dictionary<string, string>? tmp;
                tmp = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(Conf("secret_config_file")));
                if (tmp is not null)
                    dictSecret = tmp;
            }
            catch(Exception e)
            {
                ok("error in load secret: " + e.Message);
            }
            isLoad = true;
        }
        public static void SetGlobal(string key, object b)
        {
            globals[key] = b;
        }
        public static object? GetGlobal(string key)
        {
            if (globals.TryGetValue(key, out object? obj))
            {
                return obj;
            }
            return null;
        }
    }
}
