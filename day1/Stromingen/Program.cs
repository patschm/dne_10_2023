
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Bogus;

namespace Stromingen;

class Program
{
    static void Main(string[] args)
    {
        if (!Directory.Exists(@"E:\DotnetEssentials\tmpdata")) Directory.CreateDirectory(@"E:\DotnetEssentials\tmpdata");

        //RauwSchrijven();
        //RauwLezen();
        //XmlSchrijven();
        //XmlLezen();
        //ZipSchrijven();
        //ZipLezen();
        SchrijfObjecten();
        Console.WriteLine("Done");
        Console.ReadLine();
    }

    private static void SchrijfObjecten()
    {
        var people = CreatePeople();
        var fs = File.Create(@"E:\DotnetEssentials\tmpdata\people.xml");
        var writer = XmlWriter.Create(fs);
        //writer.WriteStartElement("people");
        var ser = new XmlSerializer(typeof(List<Person>));
        //foreach(var p in people)
        //Console.SetOut(new StreamWriter(fs));
        {
            //Console.WriteLine(p);
            ser.Serialize(writer, people);
        }
        //writer.WriteEndElement();
        writer.Flush();
        writer.Close();
    }

    private static List<Person> CreatePeople()
    {
        return new Faker<Person>()
            .RuleFor(p=>p.Id, f=>f.UniqueIndex )
            .RuleFor(p=>p.FirstName, f=>f.Name.FirstName())
            .RuleFor(p=>p.LastName, f=>f.Name.LastName())
            .RuleFor(p=>p.Age, f=>f.Random.Int(0, 123))
            .Generate(1000)
            .ToList();
            
    }

    private static void XmlLezen()
    {
        var fs = File.OpenRead(@"E:\DotnetEssentials\tmpdata\file1.xml");
        var zipper = new GZipStream(fs, CompressionMode.Decompress);
        var reader = XmlReader.Create(zipper);
        while(reader.ReadToFollowing("person"))
        {     
            //var rdr = reader.ReadSubtree();
            Console.WriteLine(reader.GetAttribute("id"));
            var fn =reader.ReadToDescendant("full-name");
            //reader.MoveToContent();
            Console.WriteLine(reader.ReadElementContentAsString());
            //Console.WriteLine(reader.ReadOuterXml());
        }
    }

    private static void ZipSchrijven()
    {
        var fs = File.Create(@"E:\DotnetEssentials\tmpdata\file1.zip");
        var zipper = new GZipStream(fs, CompressionMode.Compress);
        
        var writer = XmlWriter.Create(zipper);
       
       // writer.WriteProcessingInstruction("encoding", "utf-8");

        writer.WriteStartElement("people");
        for (int i = 0; i < 1000; i++)
        {
            writer.WriteStartElement("person");
            writer.WriteAttributeString("id", i.ToString());
            writer.WriteStartElement("full-name");
            writer.WriteString("Jan Hendriks " + i);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
        writer.WriteEndElement();
        writer.Flush();
        writer.Close();

    }
    private static void ZipLezen()
    {
        var fs = File.OpenRead(@"E:\DotnetEssentials\tmpdata\file1.xml");
        var reader = XmlReader.Create(fs);
        while(reader.ReadToFollowing("person"))
        {     
            //var rdr = reader.ReadSubtree();
            Console.WriteLine(reader.GetAttribute("id"));
            var fn =reader.ReadToDescendant("full-name");
            //reader.MoveToContent();
            Console.WriteLine(reader.ReadElementContentAsString());
            //Console.WriteLine(reader.ReadOuterXml());
        }
    }

    private static void XmlSchrijven()
    {
        var fs = File.Create(@"E:\DotnetEssentials\tmpdata\file1.xml");
        var writer = XmlWriter.Create(fs);
       // writer.WriteProcessingInstruction("encoding", "utf-8");

        writer.WriteStartElement("people");
        for (int i = 0; i < 1000; i++)
        {
            writer.WriteStartElement("person");
            writer.WriteAttributeString("id", i.ToString());
            writer.WriteStartElement("full-name");
            writer.WriteString("Jan Hendriks " + i);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
        writer.WriteEndElement();
        writer.Flush();
        writer.Close();

    }

    private static void RauwLezen()
    {
        var fi = new FileInfo(@"E:\DotnetEssentials\tmpdata\file1.txt");
        var fs = fi.OpenRead();

        byte[] buffer = new byte[8];
        while (fs.Read(buffer) > 0)
        {
            Console.Write(Encoding.UTF8.GetString(buffer));
            Array.Clear(buffer);
        }
        Console.WriteLine();
    }

    private static void RauwSchrijven()
    {
        var fi = new FileInfo(@"E:\DotnetEssentials\tmpdata\file1.txt");
        if (fi.Exists)
        {
            fi.Delete();
            return;
        }
        Stream fs = fi.Create();
        for (int i = 0; i < 1000; i++)
        {
            byte[] buffer = Encoding.UTF8.GetBytes($"Hello World {i}\r\n");
            fs.Write(buffer);
        }
        fs.Flush();
        fs.Close();
    }
}
