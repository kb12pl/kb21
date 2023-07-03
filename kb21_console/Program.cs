global using static kb21_tools.KbLog;
using kb21_tools;
using NLua;
using System.Runtime.InteropServices;



LogInit(Console.WriteLine);
var myInt = new MyProgramIntegration();
int k = 0;
var lua = new KbLua(myInt);

//ok(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

while (true)
{
    ok("Step", k);
    k++;
    lua.DoScript("kb21_console");
    await Task.Delay(3000);

}


class MyProgramIntegration:IKbWindow
{
    public void ok(object mess) => KbLog.ok(mess);
    public string GetConfig(string key) => KbConf.Conf(key);
    public Ret Sql(LuaTable tab)
    {
        MyArg arg = new MyArg(tab);

        //var task = Task.Run(async () => await Pg.Select(arg));
        //var ret= task.WaitAndUnwrapException();
        var ret = KbPg.Select(arg);

        return ret;
    }

}