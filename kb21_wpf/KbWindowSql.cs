using NLua;


namespace kb21_wpf
{
    public partial class KbWindow
    {
        public Ret Sql(LuaTable tab)
        {            
            MyArg arg = new MyArg(tab);
            
            //var task = Task.Run(async () => await Pg.Select(arg));
            //var ret= task.WaitAndUnwrapException();
            var ret = KbPg.Select(arg);

            return ret;
        }
    }
}
