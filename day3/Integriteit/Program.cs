
using System.Configuration.Assemblies;
using System.Security.Cryptography;
using System.Text;

namespace Integriteit;

class Program
{
    static void Main(string[] args)
    {
        var doc = "Hello World";
        // byte[] hash = GenerateHash(doc);
        // bool isOk = Validate(doc, hash);

        //byte[] hash = GenerateHMAC(doc);
        //bool isOk = ValodateHMAC(doc, hash);

        (byte[] hash, string pub) data = GenerateAsym(doc);
        bool isOk = ValidateAsym(doc, data);


        Console.WriteLine($"Document is {isOk}");
    }

    private static bool ValidateAsym(string doc, (byte[] hash, string pub) pakket)
    {
        var alg = DSA.Create();
        alg.FromXmlString(pakket.pub);
        var aha = SHA1.Create();
        byte[] data = Encoding.UTF8.GetBytes(doc);    
        byte[] hash = aha.ComputeHash(data);

        return alg.VerifyData(hash, pakket.hash, HashAlgorithmName.SHA1);
    }

    private static (byte[] hash, string pub) GenerateAsym(string doc)
    {
        var alg = DSA.Create();
        var pub = alg.ToXmlString(false);
        var aha = SHA1.Create();
        byte[] data = Encoding.UTF8.GetBytes(doc);    
        byte[] hash = aha.ComputeHash(data);

        byte[] signature = alg.SignData(hash, HashAlgorithmName.SHA1);
        return (signature, pub);
    }

    private static bool ValodateHMAC(string doc, byte[] hash)
    {
        var alg = new HMACSHA1();
        alg.Key = Encoding.UTF32.GetBytes("Pa$$w0rd");
        byte[] data = Encoding.UTF8.GetBytes(doc);      
        byte[] hash2 = alg.ComputeHash(data);
        if (hash.Length != hash2.Length) return false;
        for(int i = 0; i < hash2.Length; i++) 
        {
            if (hash[i] != hash2[i]) return false;
        }
        return true;
    }

    private static byte[] GenerateHMAC(string doc)
    {
        var alg = new HMACSHA1();
        alg.Key = Encoding.UTF32.GetBytes("Pa$$w0rd");
        byte[] data = Encoding.UTF8.GetBytes(doc);      
        byte[] hash = alg.ComputeHash(data);
        Console.WriteLine(Convert.ToBase64String(hash));
        return hash;
    }

    private static bool Validate(string doc, byte[] hash)
    {
        var alg = SHA1.Create();
        byte[] data = Encoding.UTF8.GetBytes(doc);
        
        byte[] hash2 = alg.ComputeHash(data);

        if (hash.Length != hash2.Length) return false;
        for(int i = 0; i < hash2.Length; i++) 
        {
            if (hash[i] != hash2[i]) return false;
        }
        return true;
    }

    private static byte[] GenerateHash(string doc)
    {
        var alg = SHA1.Create();
        byte[] data = Encoding.UTF8.GetBytes(doc);
        
        byte[] hash = alg.ComputeHash(data);
        Console.WriteLine(Convert.ToBase64String(hash));
        return hash;
    }
}
