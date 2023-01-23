global using static kb21_tools.KbLog;
using kb21_tools;



LogInit(Console.WriteLine);
int k = 0;
while (k<8)
{    
    xlog("Step",k);
    k++;
    var deep = new KbDeepL();
    if (k % 4 == 0)
    {
        xlog("send");
        deep.Trans();
    }
    
    await Task.Delay(100);


}
