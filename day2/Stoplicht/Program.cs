namespace Stoplicht;

class Program
{
    static void Main(string[] args)
    {
        var rnd = new Random();
        var stoplicht = new Semaphore(0, 10);

        Parallel.For(0, 50, idx=>{
            Console.WriteLine($"Auto met nrplaat {idx} staat voor de garage");
            stoplicht.WaitOne();
            Console.WriteLine($"Auto met nrplaat {idx} is aan het shoppen");
            Task.Delay(5000 + rnd.Next(1000, 5000)).Wait();
            Console.WriteLine($"Auto met nrplaat {idx} is klaar en vertrekt");
            stoplicht.Release();
            Console.WriteLine($"Auto met nrplaat {idx} rijdt uit de garage");
        });
    }
}
