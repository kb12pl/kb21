using kb_lib;
using NLua;
using imp_lib;
using kb_ret;
using static kb_lib.Log;
using System.Threading.Tasks;
using Nito.AsyncEx.Synchronous;


namespace kb12
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
