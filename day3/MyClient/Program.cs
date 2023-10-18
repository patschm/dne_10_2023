//using MyLib;

using System.Net.NetworkInformation;
using System.Reflection;

namespace MyClient;

class Program
{
    static void Main(string[] args)
    {
        //Person p1 = new Person {Name = "Jane", Age = 45};
        //p1.Introduce();

        //Analysis();
        //DarkSide();

        AttributeDemo();
    }

    private static void AttributeDemo()
    {
        var frame = new MyFramework();
        var itm = new Item();

        frame.DoeIets(itm);
    }

    private static void DarkSide()
    {
        var asm = Assembly.LoadFrom(@"E:\DotnetEssentials\dne_10_2023\day3\dist\MyLib.dll");
        var type = asm.GetType("MyLib.Person");
        var p1 = Activator.CreateInstance(type);
        var pName = type.GetProperty("Name");
        var pAge = type.GetProperty("Age");
        pName.SetValue(p1, "Kees");
        pAge.SetValue(p1, -42);

        var pField = type.GetField("_age", BindingFlags.Instance | BindingFlags.NonPublic);
        pField.SetValue(p1, -42);

        var pIntro = type.GetMethod("Introduce");
        pIntro.Invoke(p1, new object[]{});
    }

    private static void Analysis()
    {
        var asm = Assembly.LoadFrom(@"E:\DotnetEssentials\dne_10_2023\day3\dist\MyLib.dll");
        Console.WriteLine(asm.FullName);
        foreach(var type in asm.GetTypes())
        {
            Console.WriteLine("=== " + type.FullName);
        }

        var type2 = asm.GetType("MyLib.Person");
        foreach(var member in type2.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
            Console.WriteLine($"[{member.MemberType}] {member.Name}");
        }
    }
}
