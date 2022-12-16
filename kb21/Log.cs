

namespace kb_lib
{
    public delegate void Del(string mess);
    public delegate void DelClear();
    public class Log
    {
        static Del? handler;
        static DelClear? handler_clear;
        public static void LogInit(Del _handler, DelClear _handler_clear)
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
#pragma warning restore IDE1006 // Naming Styles
    }
}
