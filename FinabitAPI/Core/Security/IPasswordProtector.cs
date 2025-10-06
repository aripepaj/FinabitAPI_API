namespace FinabitAPI.Core.Security
{
    public interface IPasswordProtector
    {
        string Protect(string plaintext);
        string Unprotect(string ciphertext);
    }
}
