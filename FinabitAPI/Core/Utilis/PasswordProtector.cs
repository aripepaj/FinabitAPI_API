using Microsoft.AspNetCore.DataProtection;
using System.Text;

namespace FinabitAPI.Core.Utilis
{
    public interface IPasswordProtector
    {
        string Protect(string plaintext);
        string Unprotect(string protectedBase64);
    }

    public sealed class PasswordProtector : IPasswordProtector
    {
        private readonly IDataProtector _protector;
        public PasswordProtector(IDataProtectionProvider provider) =>
            _protector = provider.CreateProtector("FinabitAPI.SqlCredentials.v1");

        public string Protect(string plaintext)
        {
            var bytes = Encoding.UTF8.GetBytes(plaintext);
            var protectedBytes = _protector.Protect(bytes);
            return Convert.ToBase64String(protectedBytes);
        }

        public string Unprotect(string protectedBase64)
        {
            var protectedBytes = Convert.FromBase64String(protectedBase64);
            var bytes = _protector.Unprotect(protectedBytes);
            return Encoding.UTF8.GetString(bytes);
        }
    }

}
