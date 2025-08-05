using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace FinabitAPI.Utilis
{
    public class GlobalAppData
    { 

         public static int UserID { get; set; }
        public static string UserName { get; set; }
        public static string RoleName { get; set; }
        public static string UserCompanyName { get; set; }
        public static int CompanyID { get; set; }
        public static int EmployeeID { get; set; }
        public static int intConnTimeOut { get; set; }
        public static int RoleID { get; set; }
        public static int RoleDefID { get; set; }
        public static string ConnectionString { get; set; }
        public static int VATPrc { get; set; }
        public static int LenBusiNo { get; set; }
        public static int LenPersNo { get; set; }
        public static int ANPRType { get; set; }
        public static string WebServiceIP { get; set; }
        private static byte[] Buffer = new byte[0];
        private static string Key = "1t2e3r4a5b6i7t8";
        public static string Version { get; set; }
        public static int LanguageID { get; set; }
        public static bool HomeCompany { get; set; }
        public static bool ShowLastUpdateInfo { get; set; }
        public static string InfoText { get; set; }
        public static bool HasVAT { get; set; }
        public static bool UserCCView { get; set; }
        public static bool UseCertificates { get; set; }
        public static string Email { get; set; }
        public static string EmailPassword { get; set; }
        public static bool NotificationOpened { get; set; }
        public static string IP { get; set; }
        public static string ChannelId { get; set; }
        public static int ApproveID { get; set; }
        public static bool CanEditVehicle { get; set; }
        public static DateTime? EndDate { get; set; }
        public static bool UseEmployees { get; set; }
        public static int VATGroup { get; set; }
        public static bool GetDataFromQRA { get; set; }
        public static bool UseUnification { get; set; }

        public static int ErrorID { get; set; }
        public static bool ObligatePhotos { get; set; }
        public static bool UseCertificate_2 { get; set; }
        public static string ErrorMessage { get; set; }
        public static string ComPort { get; set; }
        public static int CartecType { get; set; }
        public static int NrTotIKalibrimeve { get; set; }
        public static bool KufizoLimitinEKalibrimit { get; set; }
        public static string POSUserName { get; set; }

        public static string TermnID { get; set; }
        public static int CashJournalPOSID { get; set; }

        public static string DecryptDES(string decPassword)

        {
            try
            {
                TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
                provider.Key = new MD5CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(Key));
                provider.Mode = CipherMode.ECB;
                ICryptoTransform transform = provider.CreateDecryptor();
                Buffer = Convert.FromBase64String(decPassword);
                if (Buffer.Length > 0)
                {
                    return Encoding.ASCII.GetString(transform.TransformFinalBlock(Buffer, 0, Buffer.Length));
                }
                else
                {
                    return "";
                }
            }
            catch { return ""; }
        }

        public static string EncryptDES(string encPassword)
        {
            TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
            provider.Key = new MD5CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(Key));
            provider.Mode = CipherMode.ECB;
            ICryptoTransform transform = provider.CreateEncryptor();
            Buffer = Encoding.ASCII.GetBytes(encPassword);
            return Convert.ToBase64String(transform.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }
    }
}