
namespace kb21_tools
{
    public delegate void Del(string mess);
    public delegate void DelClear();
    public class KbLog
    {
        static Del? handler;
        static DelClear? handler_clear;
        public static void LogInit(Del _handler, DelClear? _handler_clear=null)
        {
            handler = _handler;
            handler_clear = _handler_clear;
        }
#pragma warning disable IDE1006 // Naming Styles
        public static void ok(object mess)        
        {
            handler?.Invoke(mess.ToString());
        }
        public static void ok(object mess, object mess2)
        {
            string m1 = mess?.ToString();
            string m2 = mess2.ToString();
            ok($"( {m1} )  ( {m2} )");
        }

#pragma warning restore IDE1006 // Naming Styles
    }
}
