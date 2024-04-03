using System.Text;
using Konscious.Security.Cryptography;

namespace PersonalFinances.Services;

public class HashingService
{
    public byte[] HashArgon2Id(string input)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(input);
        var saltBytes = "stupidsalt"u8.ToArray();
        var argon2Id = new Argon2id(passwordBytes);
        
        argon2Id.DegreeOfParallelism = 16;
        argon2Id.MemorySize = 8192;
        argon2Id.Iterations = 40;
        argon2Id.Salt = saltBytes;
        argon2Id.AssociatedData = passwordBytes;
        
        return argon2Id.GetBytes(128);
    }
}