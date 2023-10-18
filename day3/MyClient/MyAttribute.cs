namespace MyClient;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class MyAttribute : Attribute
{
    public int Age { get; set; }

    public void Validate()
    {
        if (Age >65)
        {
            Console.WriteLine("Je bent te oud (obsolete)");
        }
        else
        {
            Console.WriteLine("Hee! Gezellig dat je er ook bent");
        }
    }
}
