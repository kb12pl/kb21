using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace kb21_tools
{
    public class KbConf
    {        
        static bool isLoad = false;        
        static Dictionary<string, string> dictSecret = new();
        static readonly Dictionary<string, string> dict = new();
        static KbConf()
        {
            dict["test"] = "123";
            dict["B12_Integretion_Object"] = "B12_Integretion_Object";
            dict["secret_config_file"] = "c:/repo/config.txt";
            dict["prefix_file_script"] = "c:/repo/kb21/kb21/lua/";
            dict["frame_init_script"] = "kb.sys_boot()";
            dict["new_window_init_script"] = "win:on_boot()";
            dict["window_on_load_event"] = "on_load";
            
            dict["initLua"] = @"
import('kb21')
function ok(kom)
    KbWindow.KbWindowOk(tostring(kom));
end

dofile(KbWindow.GetConfig('prefix_file_script')..'sys_window.lua')
";
        }
        public static string Secret(string key)
        {
            Load();
            return dictSecret[key];
            
        }
#pragma warning disable IDE1006 // Naming Styles
        public static string xconf(string key)
        {            
            if (dict.TryGetValue(key, out string? obj))
            {
                return obj;   
            }
            return String.Empty;
        }
#pragma warning restore IDE1006 // Naming Styles

        public static string GetScript(string script)
        {
            return File.ReadAllText(xconf("prefix_file_script") + script + ".lua");
        }

        static void Load()
        {
            if (isLoad)
                return;

            Dictionary<string, string>? tmp;
            tmp= JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(xconf("secret_config_file")));
            if (tmp is not null)
                dictSecret = tmp;
            isLoad = true;
        }
    }
}
