// See https://aka.ms/new-console-template for more information
global using kb12
int k = 0;
while (true)
{
    Console.WriteLine($"Step:{k}");
    k++;
    await Task.Delay(1000);
    

}
