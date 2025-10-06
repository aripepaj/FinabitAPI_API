// FinabitAPI.Core.Security/DataProtectionPasswordProtector.cs
using Microsoft.AspNetCore.DataProtection;

namespace FinabitAPI.Core.Security
{
    public sealed class DataProtectionPasswordProtector : IPasswordProtector
    {
        private readonly IDataProtector _protector;
        private const string Purpose = "FinabitAPI.DbCredentials.v1"; 

        public DataProtectionPasswordProtector(IDataProtectionProvider provider)
            => _protector = provider.CreateProtector(Purpose);

        public string Protect(string plaintext) => _protector.Protect(plaintext ?? string.Empty);
        public string Unprotect(string ciphertext) => _protector.Unprotect(ciphertext ?? string.Empty);
    }
}
