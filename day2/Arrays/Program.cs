using System.Diagnostics;
using System.Text;

namespace Arrays;

class Program
{
    static void Main(string[] args)
    {
        //string s = "";
        var s = new StringBuilder();
        var sw = new Stopwatch();
        sw.Start();
        for(int i = 0; i< 100000; i++)
        {
            //s += i.ToString();
            s.Append(i.ToString());
        }
        sw.Stop();
        Console.WriteLine(sw.Elapsed);
    }
}
