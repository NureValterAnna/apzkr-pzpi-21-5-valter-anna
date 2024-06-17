using Application.Interfaces;
using Domain.Entities;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services;

public class PasswordService : IPasswordService
{

    const int keySize = 64;
    const int iterations = 350000;

    public byte[] GenerateSalt()
    {
        return RandomNumberGenerator.GetBytes(keySize);
    }

    public string HashPassword(string password, byte[] salt)
    {
        var hashAlgorithm = HashAlgorithmName.SHA512;
        var hash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password),
            salt, iterations, hashAlgorithm, keySize);

        return Convert.ToHexString(hash);
    }

    public bool VerifyPassword(string password, string hash, string salt)
    {
        var hashAlgorithm = HashAlgorithmName.SHA512;
        var passwordSalt = Convert.FromBase64String(salt);
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, passwordSalt, iterations, hashAlgorithm, keySize);

        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
    }

}
