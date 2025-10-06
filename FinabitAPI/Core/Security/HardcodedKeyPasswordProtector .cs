using System;
using System.Security.Cryptography;
using System.Text;

namespace FinabitAPI.Core.Security
{
    public sealed class HardcodedKeyPasswordProtector : IPasswordProtector
    {
        private readonly byte[] _key; // 32 bytes for AES-256

        public HardcodedKeyPasswordProtector(string base64Key)
        {
            if (string.IsNullOrWhiteSpace(base64Key)) throw new ArgumentNullException(nameof(base64Key));
            _key = Convert.FromBase64String(base64Key);
            if (_key.Length != 32) throw new ArgumentException("Key must be 32 bytes (Base64 of 32 bytes) for AES-256.");
        }

        public string Protect(string plaintext)
        {
            plaintext ??= string.Empty;
            byte[] pt = Encoding.UTF8.GetBytes(plaintext);

            Span<byte> nonce = stackalloc byte[12]; // 96-bit nonce for AES-GCM
            RandomNumberGenerator.Fill(nonce);

            byte[] ct = new byte[pt.Length];
            byte[] tag = new byte[16];

            using var aes = new AesGcm(_key);
            aes.Encrypt(nonce, pt, ct, tag);

            // payload = 1-byte version | 12b nonce | ciphertext | 16b tag
            byte[] output = new byte[1 + nonce.Length + ct.Length + tag.Length];
            output[0] = 1;
            nonce.CopyTo(output.AsSpan(1));
            Buffer.BlockCopy(ct, 0, output, 1 + nonce.Length, ct.Length);
            Buffer.BlockCopy(tag, 0, output, 1 + nonce.Length + ct.Length, tag.Length);

            return Convert.ToBase64String(output);
        }

        public string Unprotect(string ciphertext)
        {
            var data = Convert.FromBase64String(ciphertext ?? "");
            if (data.Length < 1 + 12 + 16) throw new CryptographicException("Invalid payload.");

            byte ver = data[0];
            if (ver != 1) throw new CryptographicException("Unsupported payload version.");

            var nonce = new ReadOnlySpan<byte>(data, 1, 12);
            int ctLen = data.Length - (1 + 12 + 16);
            var ct = new ReadOnlySpan<byte>(data, 1 + 12, ctLen);
            var tag = new ReadOnlySpan<byte>(data, 1 + 12 + ctLen, 16);

            byte[] pt = new byte[ctLen];
            using var aes = new AesGcm(_key);
            aes.Decrypt(nonce, ct, tag, pt);

            return Encoding.UTF8.GetString(pt);
        }
    }
}
