using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace DecryptSkong;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: dotnet run <input-dir> <output-dir>");
            return;
        }

        string inputDirectory = args[0];
        if (!Directory.Exists(inputDirectory))
        {
            Console.WriteLine($"Input directory '{inputDirectory}' does not exist.'");
            return;
        }
        string outputDirectory = args[1];
        
        foreach (var file in Directory.GetFiles(inputDirectory))
        {
            Console.WriteLine($"Decrypting file '{file}'...");
            string content = File.ReadAllText(file);
            
            string decryptedContent = Decrypt(content);
            decryptedContent = HttpUtility.HtmlDecode(decryptedContent);
            Console.WriteLine($"Decrypted '{Path.GetFileName(file)}'");

            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
                Console.WriteLine($"Created output directory '{outputDirectory}' does not exist.'");
            }
            
            File.WriteAllText(Path.Combine(outputDirectory,Path.GetFileName(file)), decryptedContent);
        }
    }
    
    public static byte[] Encrypt(byte[] decryptedBytes)
    {
        byte[] result;
        using (RijndaelManaged rijndaelManaged = new RijndaelManaged
               {
                   Key = _keyArray,
                   Mode = CipherMode.ECB,
                   Padding = PaddingMode.PKCS7
               })
        {
            result = rijndaelManaged.CreateEncryptor().TransformFinalBlock(decryptedBytes, 0, decryptedBytes.Length);
        }
        return result;
    }

    // Token: 0x06000047 RID: 71 RVA: 0x00002A94 File Offset: 0x00000C94
    public static byte[] Decrypt(byte[] encryptedBytes)
    {
        byte[] result;
        using (RijndaelManaged rijndaelManaged = new RijndaelManaged
               {
                   Key = _keyArray,
                   Mode = CipherMode.ECB,
                   Padding = PaddingMode.PKCS7
               })
        {
            result = rijndaelManaged.CreateDecryptor().TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
        }
        return result;
    }

    // Token: 0x06000048 RID: 72 RVA: 0x00002AF0 File Offset: 0x00000CF0
    public static string Encrypt(string unencryptedString)
    {
        byte[] array = Encrypt(Encoding.UTF8.GetBytes(unencryptedString));
        return Convert.ToBase64String(array, 0, array.Length);
    }

    // Token: 0x06000049 RID: 73 RVA: 0x00002B18 File Offset: 0x00000D18
    public static string Decrypt(string encryptedString)
    {
        byte[] bytes = Decrypt(Convert.FromBase64String(encryptedString));
        return Encoding.UTF8.GetString(bytes);
    }

    // Token: 0x0400001A RID: 26
    private static readonly byte[] _keyArray = Encoding.UTF8.GetBytes("UKu52ePUBwetZ9wNX88o54dnfKRu0T1l");
}