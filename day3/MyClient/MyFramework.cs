using System.Reflection;

namespace MyClient;

public class MyFramework
{
    public void DoeIets(object imp)
    {
        //var attrs = imp.GetType().GetCustomAttributes(false);
        MyAttribute attr =  imp.GetType().GetCustomAttribute(typeof(MyAttribute)) as MyAttribute;
        if (attr.Age < 65)
            Console.WriteLine("Happy Coding");
        else
            Console.WriteLine("AUB, een Drionpilletje");
    }
}
