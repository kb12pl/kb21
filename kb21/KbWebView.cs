using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Web.WebView2.Wpf;
using static System.Net.Mime.MediaTypeNames;


namespace kb21
{
    internal class KbWebView:WebView2,KbCtrl
    {
        KbWindow win;
        string id;
        public KbWebView(MyArg arg, KbWindow _win)
        {
            id = arg.Get("id");
            win = _win;
        }

        public bool Cmd(MyArg arg)
        {
            string par;
            if (arg.Is("set"))
            {
                try
                {
                    //CoreWebView2.Navigate(@"C:/karol/text.pdf");                    
                    Source = new Uri(arg.Get("set"));
                    //NavigateToString("asd");
                }
                catch(UriFormatException e)
                {
                    return arg.Error(e.Message);
                }
                return false;
            }
            if (arg.Try("jscript", out par))
            {
                ScriptAsync(par);                
                return false; 
            }
            return arg.Error("unknown arg");
        }
        async void ScriptAsync(string script)
        {
                await ExecuteScriptAsync(script);
        }
    }
}
