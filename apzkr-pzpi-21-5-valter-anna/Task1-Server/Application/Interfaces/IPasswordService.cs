namespace Application.Interfaces;

public interface IPasswordService
{
    string HashPassword(string password, byte[] salt);

    byte[] GenerateSalt();

    bool VerifyPassword(string password, string hash, string salt);
}
