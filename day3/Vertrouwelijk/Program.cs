
using System.Security.Cryptography;
using System.Text;

namespace Vertrouwelijk;

class Program
{
    static void Main(string[] args)
    {
        //Asymmetrisch();
        Symmetrisch();
    }

    private static void Symmetrisch()
    {
        // Zender
        var algZender = Aes.Create();
        var key = algZender.Key;
        var iv = algZender.IV;
        //algZender.Mode = CipherMode.CBC;

        byte[] cipher;
        using (MemoryStream mem = new MemoryStream())
        {
            using(CryptoStream cryp = new CryptoStream(mem, algZender.CreateEncryptor(), CryptoStreamMode.Write))
            {
                using (var writer = new StreamWriter(cryp))
                {
                    writer.WriteLine("Hello World");
                }
            }
            cipher = mem.ToArray();
        }
        Console.WriteLine(Encoding.UTF8.GetString(cipher));

        // Ontvanger
        var algOntvanger = Aes.Create();
        //algOntvanger.Mode = CipherMode.CBC;

        algOntvanger.Key = key;
        algOntvanger.IV = iv;

        using(var mem2 = new MemoryStream(cipher))
        {
            using (var cryp2 = new CryptoStream(mem2, algOntvanger.CreateDecryptor(), CryptoStreamMode.Read))
            {
                using (var reader = new StreamReader(cryp2))
                {
                    var data = reader.ReadToEnd();
                    Console.WriteLine(data);
                }
            }
        }


    }

    private static void Asymmetrisch()
    {
        // Ontvanger heeft pub en priv key
        var rsaOntvanger = RSA.Create();
        string pubKey = rsaOntvanger.ToXmlString(false);

        // Zender
        var rsaZender = RSA.Create();
        rsaZender.FromXmlString(pubKey);
        var cipher = rsaZender.Encrypt(Encoding.UTF8.GetBytes("Hello World"), RSAEncryptionPadding.Pkcs1);
    
        // Ontvanger
        var data = rsaOntvanger.Decrypt(cipher,RSAEncryptionPadding.Pkcs1);
        Console.WriteLine(Encoding.UTF8.GetString(data));
    }
}
