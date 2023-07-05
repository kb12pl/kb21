global using static kb21_tools.KbLog;
using kb21_tools;
using NLua;
using System.Runtime.InteropServices;

LogInit(Console.WriteLine);
var lua = new KbLua(new MyProgramIntegration());

//ok(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

while (true)
{
    lua.DoScript("kb21_console");
    var time = KbConf.GetInt("console_loop_time", 60000);
    await Task.Delay(time);
}


class MyProgramIntegration:IKbWindow
{
    public void ok(object mess) => KbLog.ok(mess);
    public string GetConfig(string key) => KbConf.Get(key);
    public void SetConfig(string key, string val)=>KbConf.Set(key, val);
    public Ret Sql(LuaTable tab)
    {
        MyArg arg = new MyArg(tab);
        //var task = Task.Run(async () => await Pg.Select(arg));
        //var ret= task.WaitAndUnwrapException();
        var ret = KbPg.Select(arg);
        return ret;
    }

}