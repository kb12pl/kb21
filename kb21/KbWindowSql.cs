using kb21;
using NLua;
using kb21;
using kb_ret;
using static kb21.Log;
//using System.Threading.Tasks;
//using Nito.AsyncEx.Synchronous;


namespace kb21
{
    partial class KbWindow
    {
        public Ret Sql(LuaTable tab)
        {
            MyArg arg = new MyArg(tab);

            //var task = Task.Run(async () => await Pg.Select(arg));
            //var ret= task.WaitAndUnwrapException();
            var ret = Pg.Select(arg);

            return ret;
        }
    }
}
