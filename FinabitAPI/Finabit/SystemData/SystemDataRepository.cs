//-- =============================================
//-- Author:		Generated
//-- Create date: 07.10.25
//-- Description:	Repository for System Data operations
//-- =============================================
using System.Data;
using FinabitAPI.Core.Global;
using FinabitAPI.Finabit.SystemInfo.dto;
using FinabitAPI.Utilis;
using Microsoft.Data.SqlClient;

namespace FinabitAPI.Finabit.SystemData
{
    public class SystemDataRepository
    {
        public int ErrorID = 0;
        public string ErrorDescription = "";

        private readonly DBAccess _dbAccess;

        public SystemDataRepository(DBAccess dbAccess)
        {
            _dbAccess = dbAccess ?? throw new ArgumentNullException(nameof(dbAccess));
        }

        #region ListSystemData

        public SystemData ListSystemData()
        {
            SystemData data = new SystemData();
            SqlConnection connection = _dbAccess.GetConnection();
            SqlCommand command = new SqlCommand("spSystemList", connection);
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data.ID = Convert.ToInt32(reader["ID"]);
                        data.ComName = Convert.ToString(reader["ComName"]);
                        data.ComBusinessNo = Convert.ToString(reader["ComBusinessNo"]);
                        data.ComVATNo = Convert.ToString(reader["ComVATNo"]);
                        data.ComFiscalNo = Convert.ToString(reader["ComFiscalNo"]);
                        data.ComPhone = Convert.ToString(reader["ComPhone"]);
                        data.ComFax = Convert.ToString(reader["ComFax"]);
                        data.ComAddress = Convert.ToString(reader["ComAddress"]);
                        data.ComBankName = Convert.ToString(reader["ComBankName"]);
                        data.ComBankName2 = Convert.ToString(reader["ComBankName2"]);
                        data.ComBankName3 = Convert.ToString(reader["ComBankName3"]);
                        data.ComBankName4 = Convert.ToString(reader["ComBankName4"]);
                        data.ComBankAccount = Convert.ToString(reader["ComBankAccount"]);
                        data.ComBankAccount2 = Convert.ToString(reader["ComBankAccount2"]);
                        data.ComBankAccount3 = Convert.ToString(reader["ComBankAccount3"]);
                        data.ComBankAccount4 = Convert.ToString(reader["ComBankAccount4"]);
                        data.ComEmail = Convert.ToString(reader["ComEmail"]);
                        data.ComWebAddress = Convert.ToString(reader["ComWebAddress"]);
                        data.ComLogo =
                            (reader["ComLogo"] == DBNull.Value)
                                ? null
                                : ((byte[])reader["ComLogo"]);
                        data.VATPrc = Convert.ToDecimal(reader["VATPrc"]);
                        data.AkcizaPartner = Convert.ToString(reader["AkcizaPartner"]);
                        data.VATPartner = Convert.ToString(reader["VATPartner"]);
                        data.DUDPartner = Convert.ToInt32(reader["DUDPartner"]);
                        data.ShowLastUpdateInfo = Convert.ToBoolean(reader["ShowLastUpdateInfo"]);
                        data.InfoText = Convert.ToString(reader["InfoText"]);
                        data.PayablesVATAccount = Convert.ToString(reader["PayablesVATAccount"]);
                        data.ReceivingBank = Convert.ToString(reader["ReceivingBank"]);
                        data.ReceivingCustomerAccount = Convert.ToString(
                            reader["ReceivingCustomerAccount"]
                        );
                        data.ReceivingCustomerName = Convert.ToString(
                            reader["ReceivingCustomerName"]
                        );
                        data.Mod36 = Convert.ToString(reader["Mod36"]);
                        data.EmailPort = Convert.ToInt32(reader["EmailPort"].ToString());
                        data.EmailSMTP = Convert.ToString(reader["EmailSMTP"]);
                        data.EmailPassword = Convert.ToString(reader["EmailPassword"]);
                        data.EmailFrom = Convert.ToString(reader["EmailFrom"]);
                        data.EmailBody = Convert.ToString(reader["EmailBody"]);
                        data.EmailEnableSSL = Convert.ToBoolean(reader["EmailEnableSSL"]);
                        data.DecryptKey = Convert.ToString(reader["DecryptKey"]);

                        break;
                    }
                }
                connection.Close();
            }
            catch (Exception exception)
            {
                ErrorID = -1;
                ErrorDescription = exception.Message;
                connection.Close();
            }
            return data;
        }

        #endregion

        #region ListSystemDataForTran

        public SystemData ListSystemDataForTran()
        {
            SystemData data = new SystemData();
            SqlConnection connection = _dbAccess.GetConnection();
            SqlCommand command = new SqlCommand("spSystemList", connection);
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data.ID = Convert.ToInt32(reader["ID"]);
                        data.ComName = Convert.ToString(reader["ComName"]);
                        data.ComBusinessNo = Convert.ToString(reader["ComBusinessNo"]);
                        data.ComVATNo = Convert.ToString(reader["ComVATNo"]);
                        data.ComFiscalNo = Convert.ToString(reader["ComFiscalNo"]);
                        data.ComPhone = Convert.ToString(reader["ComPhone"]);
                        data.ComFax = Convert.ToString(reader["ComFax"]);
                        data.ComAddress = Convert.ToString(reader["ComAddress"]);
                        data.ComBankName = Convert.ToString(reader["ComBankName"]);
                        data.ComBankName2 = Convert.ToString(reader["ComBankName2"]);
                        data.ComBankName3 = Convert.ToString(reader["ComBankName3"]);
                        data.ComBankName4 = Convert.ToString(reader["ComBankName4"]);
                        data.ComBankAccount = Convert.ToString(reader["ComBankAccount"]);
                        data.ComBankAccount2 = Convert.ToString(reader["ComBankAccount2"]);
                        data.ComBankAccount3 = Convert.ToString(reader["ComBankAccount3"]);
                        data.ComBankAccount4 = Convert.ToString(reader["ComBankAccount4"]);
                        data.ComEmail = Convert.ToString(reader["ComEmail"]);
                        data.ComWebAddress = Convert.ToString(reader["ComWebAddress"]);
                        data.ComLogo =
                            (reader["ComLogo"] == DBNull.Value)
                                ? null
                                : ((byte[])reader["ComLogo"]);
                        data.VATPrc = Convert.ToDecimal(reader["VATPrc"]);
                        data.AkcizaPartner = Convert.ToString(reader["AkcizaPartner"]);
                        data.VATPartner = Convert.ToString(reader["VATPartner"]);
                        data.DUDPartner = Convert.ToInt32(reader["DUDPartner"]);
                        data.ShowLastUpdateInfo = Convert.ToBoolean(reader["ShowLastUpdateInfo"]);
                        data.InfoText = Convert.ToString(reader["InfoText"]);
                        data.PayablesVATAccount = Convert.ToString(reader["PayablesVATAccount"]);
                        data.ReceivingBank = Convert.ToString(reader["ReceivingBank"]);
                        data.ReceivingCustomerAccount = Convert.ToString(
                            reader["ReceivingCustomerAccount"]
                        );
                        data.ReceivingCustomerName = Convert.ToString(
                            reader["ReceivingCustomerName"]
                        );
                        data.Mod36 = Convert.ToString(reader["Mod36"]);

                        break;
                    }
                }
                connection.Close();
            }
            catch (Exception exception)
            {
                ErrorID = -1;
                ErrorDescription = exception.Message;
                connection.Close();
            }
            return data;
        }

        #endregion

        #region ListSystemDataTable

        public DataTable ListSystemDataTable()
        {
            DataTable data = new DataTable();
            SqlConnection connection = _dbAccess.GetConnection();
            SqlCommand command = new SqlCommand("spSystemList", connection);
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(data);

                return data;
            }
            catch (Exception exception)
            {
                ErrorID = -1;
                ErrorDescription = exception.Message;
                connection.Close();
            }
            return data;
        }

        #endregion

        public SystemBaseInfo? GetSystemBasicInfo()
        {
            var data = ListSystemData();

            if (data == null || data.ID == 0)
                return null;

            return new SystemBaseInfo
            {
                ID = data.ID,
                ComName = data.ComName ?? string.Empty,
                ComBusinessNo = data.ComBusinessNo ?? string.Empty,
                ComPhone = data.ComPhone ?? string.Empty,
                ComAddress = data.ComAddress ?? string.Empty,
                ComEmail = data.ComEmail ?? string.Empty,
            };
        }
    }
}
