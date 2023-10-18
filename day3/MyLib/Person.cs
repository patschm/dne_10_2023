namespace MyLib;

public class Person
{
    private int _age;
    public int Age
    {
        get { return _age; }
        set
        {
            if (value >= 0 && value < 130)
            {
                _age = value;
            }
        }
    }

    public string? Name { get; set; }

    public void Introduce()
    {
        Console.WriteLine($"{Name} ({Age})");
    }
}
