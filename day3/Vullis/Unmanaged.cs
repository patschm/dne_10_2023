
namespace Vullis;

public class Unmanaged : IDisposable
{
    private static bool isOpen = false;
    private FileStream stream;

    public void Open()
    {
        if(!isOpen)
        {
            Console.WriteLine("Open resource...");
            stream = File.Open("bla.txt", FileMode.OpenOrCreate);
            isOpen = true;
        }
        else
        {
            Console.WriteLine("Helaas. Resource is in gebruik");
        }
    }

    public void Close()
    {
        Console.WriteLine("Resource wordt afgesloten");
        isOpen = false;
        
    }

    protected void RuimOp(bool fromFinalizer)
    {
        if (!fromFinalizer)
        {
            stream.Dispose();
        }
        Close();
    }
    public void Dispose()
    {
        RuimOp(false);
        GC.SuppressFinalize(this);
    }

    ~Unmanaged()
    {
        RuimOp(true);
    }
}
