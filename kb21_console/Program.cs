global using static kb21_tools.KbLog;
using kb21_tools;
using System.Runtime.InteropServices;




LogInit(Console.WriteLine);
var myInt = new MyProgramIntegration();
int k = 0;
var lua = new KbLua(myInt, "initKb21");

ok(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

while (true)
{
    ok("Step", k);
    k++;
    lua.DoScript("kb21_console");
    await Task.Delay(3000);

}


class MyProgramIntegration
{
    public void Ok(object mess) => ok(mess);
}