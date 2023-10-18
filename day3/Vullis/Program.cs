namespace Vullis;

class Program
{
    static Unmanaged res1 = new Unmanaged();
    static Unmanaged res2 = new Unmanaged();


    static void Main(string[] args)
    { 
        
        using(res1)
        {
            res1.Open();
        }
        //res1.Dispose();
        res1 = null;

        GC.Collect();
        GC.WaitForPendingFinalizers();

        using (var res3 = new Unmanaged())
        {
            res3.Open();
        }

        try
        {
            res2.Open();
        }
        finally
        {
            res2.Dispose();
        }
    }
}
