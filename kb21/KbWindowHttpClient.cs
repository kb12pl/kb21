using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kb21
{
    public partial class KbWindow
    {
        public void HttpClient(LuaTable tab)
        {
            var arg = new MyArg(tab);
            if(arg.Is("deepl"))
            {
                var deepl = new KbDeepL();
                deepl.Trans();
                return;
            }


            KbHttpClient client = new();
            client.Send(arg, this);            
        }
    }
}
