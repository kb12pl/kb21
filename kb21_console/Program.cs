global using static kb21_tools.KbLog;
using kb21_tools;





LogInit(Console.WriteLine);
var myInt= new MyProgramIntegration();
int k = 0;
var lua = new KbLua(myInt,"initProgram");
while (true)
{    

    ok("Step",k);
    k++;
    lua.DoScript("program");    
    await Task.Delay(3000);

}


class MyProgramIntegration
{
    public void Ok(object mess) => ok(mess);
}