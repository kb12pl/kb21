
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
        public static void xlog(string mess)        
        {
            handler?.Invoke(mess);
        }
        public static void xlog(int mess) => xlog(mess.ToString());       

        public static void ok(string mess) => xlog(mess);
        public static void ok(int mess) => xlog(mess.ToString());
        public static void xlogclear() => handler_clear?.Invoke();

        public static void xlog(object mess, object mess2)
        {
            string m1 = mess.ToString();
            string m2 = mess2.ToString();
            xlog($"( {m1} )  ( {m2} )");
        }

#pragma warning restore IDE1006 // Naming Styles
    }
}
