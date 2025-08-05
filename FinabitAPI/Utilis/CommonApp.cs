using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FinabitAPI.Utilis
{
    public class CommonApp
    {
        public static int CheckForInt(string senderText)
        {
            int intS = 0;
            if (int.TryParse(senderText, out intS))
            {
                int intValue = Convert.ToInt32(senderText.Trim());
                intS = intValue;
            }
            return intS;
        }
        public static void WriteLog(string log)
        {
            try
            {
                string Path ="C:\\Temp" + "\\Fina_errorLog_api";
                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                }

                FileInfo f1 = new FileInfo(Path + @"\Log_" + DateTime.Now.ToString("dd.MM.yyyy ") + "_" + ".txt");
                if (!f1.Exists)
                {
                    FileStream fs = new FileStream(Path + @"\Log_" + DateTime.Now.ToString("dd.MM.yyyy ") + "_" +".txt", FileMode.Append);
                    fs.Close();
                }



                File.AppendAllText(Path + @"\Log_" + DateTime.Now.ToString("dd.MM.yyyy ") + "_" + ".txt", log + "*****" + DateTime.Now + Environment.NewLine);
            }
            //    string path = FINA.Properties.Settings.Default.FiscalPrinterPath;
            //    if (!Directory.Exists(path + "\\Finabit_ErrorLogs"))
            //    {
            //        Directory.CreateDirectory(path + "\\Finabit_ErrorLogs");
            //    }

            //    string filePath = path + "\\Finabit_ErrorLogs";
            //    DirectoryInfo dInfo = new DirectoryInfo(filePath);
            //    DirectorySecurity dSecurity = dInfo.GetAccessControl();
            //    dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            //    dInfo.SetAccessControl(dSecurity);
            //    using (StreamWriter sw = new StreamWriter(filePath, true))
            //    {
            //        //StreamWriter sw = new StreamWriter(filePath);
            //        sw.WriteLine(DateTime.Now.ToString() + "**** " + log + "****");
            //        sw.Close();
            //    }
            //}
            catch (Exception ex) { string str = ex.Message; }



        }

        public static decimal CheckForDecimal(string senderText)
        {
            decimal decS = 0;

            if (decimal.TryParse(senderText, out decS))
            {
                decimal decValue = Convert.ToDecimal(senderText.Trim());
                decS = decValue;
            }

            return decS;
        }

    }
}