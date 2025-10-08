using FinabitAPI.Core.Utilis;
using FinabitAPI.Finabit.Account.dto;
using FinabitAPI.Utilis;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FinabitAPI.Repository
{
    public interface IAccountRepository
    {
        Task<IReadOnlyList<AccountMatchDto>> SearchAccountsAsync(
            string? accountId, string? accountName, CancellationToken ct = default);

        Task<IReadOnlyList<AccountListItemDto>> GetAllAccountsAsync(
            CancellationToken ct = default);

        Task<IReadOnlyList<AccountListItemDto>> GetAccountsBySubGroupAsync(
        int subGroup, CancellationToken ct = default);
    }

    public sealed class AccountRepository : IAccountRepository
    {
        private readonly DBAccess _db;

        private const string ProcSearch = "dbo.spGeAccountMatches_API";
        private const string ProcListAll = "dbo.spAccountList_API";
        private const string ProcPicker = "dbo.spAccountLookup_API";
        private const int DefaultTimeout = 60;

        public AccountRepository(DBAccess db) => _db = db;

        public async Task<IReadOnlyList<AccountMatchDto>> SearchAccountsAsync(
            string? accountId, string? accountName, CancellationToken ct = default)
        {
            var list = new List<AccountMatchDto>();

            await using var conn = _db.GetConnection();
            await using var cmd = new SqlCommand(ProcSearch, conn)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = DefaultTimeout
            };

            cmd.Parameters.Add(new SqlParameter("@AccountId", SqlDbType.NVarChar, 200)
            { Value = (object?)accountId ?? string.Empty });

            cmd.Parameters.Add(new SqlParameter("@AccountName", SqlDbType.NVarChar, 200)
            { Value = (object?)accountName ?? string.Empty });

            await conn.OpenAsync(ct).ConfigureAwait(false);
            await using var dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection, ct).ConfigureAwait(false);

            if (!dr.HasRows) return list;

            int oDetailsType = dr.GetOrdinal("DetailsType");
            int oItemID = dr.GetOrdinal("ItemID");
            int oDesc = dr.GetOrdinal("Description");
            int oItemName = dr.GetOrdinal("ItemName");

            while (await dr.ReadAsync(ct).ConfigureAwait(false))
            {
                list.Add(new AccountMatchDto
                {
                    DetailsType = dr.IsDBNull(oDetailsType) ? 0 : dr.GetInt32(oDetailsType),
                    ItemID = dr.IsDBNull(oItemID) ? string.Empty : dr.GetString(oItemID),
                    Description = dr.IsDBNull(oDesc) ? string.Empty : dr.GetString(oDesc),
                    ItemName = dr.IsDBNull(oItemName) ? string.Empty : dr.GetString(oItemName)
                });
            }

            return list;
        }

        public async Task<IReadOnlyList<AccountListItemDto>> GetAllAccountsAsync(
            CancellationToken ct = default)
        {
            var list = new List<AccountListItemDto>();

            await using var conn = _db.GetConnection();
            await using var cmd = new SqlCommand(ProcListAll, conn)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = DefaultTimeout
            };

            await conn.OpenAsync(ct).ConfigureAwait(false);
            await using var dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection, ct)
                                          .ConfigureAwait(false);

            if (!dr.HasRows) return list;

            int oAccount = dr.GetOrdinal("Account");
            int oDesc = dr.GetOrdinal("AccountDescription");
            int oDisplay = dr.GetOrdinal("AccountDisplay");
            int oSgId = dr.GetOrdinal("AccountSubGroupID");
            int oSgName = dr.GetOrdinal("AccountSubGroupName");

            while (await dr.ReadAsync(ct).ConfigureAwait(false))
            {
                list.Add(new AccountListItemDto
                {
                    Account = dr.IsDBNull(oAccount) ? "" : dr.GetString(oAccount),
                    AccountDescription = dr.IsDBNull(oDesc) ? "" : dr.GetString(oDesc),
                    AccountDisplay = dr.IsDBNull(oDisplay) ? "" : dr.GetString(oDisplay),
                    AccountSubGroupID = dr.IsDBNull(oSgId) ? 0 : dr.GetInt32(oSgId),
                    AccountSubGroupName = dr.IsDBNull(oSgName) ? "" : dr.GetString(oSgName)
                });
            }

            return list;
        }

        public async Task<IReadOnlyList<AccountListItemDto>> GetAccountsBySubGroupAsync(
      int subGroup, CancellationToken ct = default)
        {
            var list = new List<AccountListItemDto>();

            await using var conn = _db.GetConnection();
            await using var cmd = new SqlCommand(ProcPicker, conn)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = DefaultTimeout
            };

            cmd.Parameters.Add(new SqlParameter("@SubGroup", SqlDbType.Int) { Value = subGroup });

            await conn.OpenAsync(ct).ConfigureAwait(false);
            await using var dr = await cmd.ExecuteReaderAsync(
                CommandBehavior.CloseConnection, ct).ConfigureAwait(false);

            if (!dr.HasRows) return list;

            int oAccount = dr.GetOrdinal("Account");
            int oDesc = dr.GetOrdinal("AccountDescription");
            int oDisplay = dr.GetOrdinal("AccountDisplay");
            int oSgId = dr.GetOrdinal("AccountSubGroupID");
            int oSgName = dr.GetOrdinal("AccountSubGroupName");

            while (await dr.ReadAsync(ct).ConfigureAwait(false))
            {
                list.Add(new AccountListItemDto
                {
                    Account = dr.IsDBNull(oAccount) ? "" : dr.GetString(oAccount),
                    AccountDescription = dr.IsDBNull(oDesc) ? "" : dr.GetString(oDesc),
                    AccountDisplay = dr.IsDBNull(oDisplay) ? "" : dr.GetString(oDisplay),
                    AccountSubGroupID = dr.IsDBNull(oSgId) ? 0 : dr.GetInt32(oSgId),
                    AccountSubGroupName = dr.IsDBNull(oSgName) ? "" : dr.GetString(oSgName)
                });
            }

            return list;
        }




        //LB
         #region Insert
         
        public void Insert(Account cls)
        {
            SqlConnection cnn = _db.GetConnection();
            SqlCommand cmd = new SqlCommand("spAccountInsert", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@prmAccountCode", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AccountCode;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmAccountParent", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AccountParent;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmAccountStatus", System.Data.SqlDbType.Char);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AccountStatus;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmAccountLevel", System.Data.SqlDbType.TinyInt);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AccountLevel;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmAccountGroupID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AccountGroup.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmAccountSubGroupID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AccountSubGroup.ID;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@prmAccountCFID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AccountCF.ID;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@prmAccountDescription", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AccountDescription;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmBankID", System.Data.SqlDbType.SmallInt);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Bank.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmBankAccount", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Bank.BankAccount;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@prmUserID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = GlobalAppData.UserID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Users", System.Data.SqlDbType.Structured);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Users;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmErrorID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);
            try
            {
                cnn.Open();

                cmd.ExecuteNonQuery();
                cls.ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);

                cnn.Close();
            }
            catch (Exception ex)
            {
                cls.ErrorID = -1;
                cls.ErrorDescription = ex.Message;
                cnn.Close();
            }

        }

        #endregion

        #region Update

        public void Update(Account cls)
        {
            SqlConnection cnn = _db.GetConnection();
            SqlCommand cmd = new SqlCommand("spAccountUpdate", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@prmAccountCode", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AccountCode;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmAccountParent", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AccountParent;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmAccountStatus", System.Data.SqlDbType.Char);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AccountStatus;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmAccountLevel", System.Data.SqlDbType.TinyInt);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AccountLevel;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmAccountGroupID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AccountGroup.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmAccountSubGroupID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AccountSubGroup.ID;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@prmAccountCFID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AccountCF.ID;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@prmAccountDescription", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AccountDescription;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmBankID", System.Data.SqlDbType.SmallInt);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Bank.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmBankAccount", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Bank.BankAccount;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmLUN", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.LUN;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmUserID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = GlobalAppData.UserID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Users", System.Data.SqlDbType.Structured);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Users;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmErrorID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);
            try
            {
                cnn.Open();

                cmd.ExecuteNonQuery();
                cls.ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);

                cnn.Close();
            }
            catch (Exception ex)
            {
                cls.ErrorID = -1;
                cls.ErrorDescription = ex.Message;
                cnn.Close();
            }

        }

        #endregion

        #region Delete

        public void Delete(Account cls)
        {
            SqlConnection cnn = new SqlConnection();
            cnn = _db.GetConnection();
            SqlCommand cmd = new SqlCommand("spAccountDelete", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@prmAccountCode", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AccountCode;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmAccountStatus", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AccountStatus;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmLUN", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.LUN;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmUserID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = GlobalAppData.UserID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmErrorID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);
            try
            {
                cnn.Open();

                cmd.ExecuteNonQuery();
                cls.ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);

                cnn.Close();
            }
            catch (Exception ex)
            {
                cls.ErrorID = -1;
                cls.ErrorDescription = ex.Message;
                cnn.Close();
            }

        }

        #endregion

        #region SelectAll

        public List<Account> SelectAll()
        {
            List<Account> clsList = new List<Account>();
            SqlConnection cnn = _db.GetConnection();
            SqlCommand cmd = new SqlCommand("spAccountList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Account cls = new Account();
                      
                        cls.AccountCode = Convert.ToString(dr["Account"]);
                        cls.AccountParent = Convert.ToString(dr["AccountParent"]);
                        cls.AccountLevel = Convert.ToInt32(dr["AccountLevel"]);
                        cls.AccountStatus = Convert.ToString(dr["AccountStatus"]);
                        cls.AccountDescription = Convert.ToString(dr["AccountDescription"]);
                        cls.AccountSubGroup.ID = Convert.ToInt32(dr["AccountSubGroupID"]);
                        cls.AccountCF.ID = Convert.ToInt32(dr["AccountCFID"]);
                        cls.AccountGroup.ID = Convert.ToInt32(dr["AccountGroupID"]);
                        cls.Bank.ID = dr["BankID"] == DBNull.Value ? -1 : int.Parse(dr["BankID"].ToString());
                        cls.Bank.BankAccount = dr["BankAccount"].ToString();
                        //cls.LUN = Convert.ToInt32(dr["LUN"]);
                        //cls.LUB = Convert.ToInt32(dr["LUB"]);
                        //cls.LUD = Convert.ToDateTime(dr["LUD"]);
                        //cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                        //cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                        clsList.Add(cls);
                    }
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return clsList;
        }

        #endregion

        #region SelectAllTable

        public DataTable SelectAllTable()
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _db.GetConnection();
            SqlCommand cmd = new SqlCommand("[spAccountList]", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dadap = new SqlDataAdapter(cmd);
            try
            {
                cnn.Open();

                dadap.Fill(dtList);
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return dtList;
        }

        #endregion
        #region CashAndBankAccounts

        public DataTable CashAndBankAccounts()
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _db.GetConnection();
            SqlCommand cmd = new SqlCommand("[spCashAndBankAccounts]", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dadap = new SqlDataAdapter(cmd);

            SqlParameter param;

            param = new SqlParameter("@UserID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = GlobalAppData.UserID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                dadap.Fill(dtList);
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return dtList;
        }

        #endregion


        #region SelectByID

        public Account SelectByID(Account cls)
        {
            SqlConnection cnn = _db.GetConnection();
            SqlCommand cmd = new SqlCommand("spAccountByID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@Account", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AccountCode;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cls = new Account();
                        cls.AccountCode = Convert.ToString(dr["Account"]);
                        cls.AccountParent = Convert.ToString(dr["AccountParent"]);
                        cls.AccountLevel = Convert.ToInt32(dr["AccountLevel"]);
                        cls.AccountStatus = Convert.ToString(dr["AccountStatus"]);
                        cls.AccountDescription = Convert.ToString(dr["AccountDescription"]);
                        cls.AccountSubGroupID = Convert.ToInt32(dr["AccountSubGroupID"]);
                        cls.AccountCFCategoryID = Convert.ToInt32(dr["AccountCFID"]);
                        cls.Active = dr["Active"]==DBNull.Value? true : Convert.ToBoolean(dr["Active"]);
                        //cls.Bank.ID = dr["BankID"] == DBNull.Value ? -1 : int.Parse(dr["BankID"].ToString());
                        cls.AccountDescription_2 = dr["AccountDescription_2"] == DBNull.Value ? "" : Convert.ToString(dr["AccountDescription_2"].ToString());
                        //cls.Bank.BankAccount = dr["BankAccount"].ToString();
                        //cls.LUN = Convert.ToInt32(dr["LUN"]);
                        //cls.LUB = Convert.ToInt32(dr["LUB"]);
                        //cls.LUD = Convert.ToDateTime(dr["LUD"]);
                        //cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                        //cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                        break;
                    }
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return cls;
        }

        #endregion

        #region AccountLookup

        public List<Account> AccountLookup(string SubGroup)
        {
            List<Account> clsList = new List<Account>();
            SqlConnection cnn = _db.GetConnection();
            SqlCommand cmd = new SqlCommand("spAccountLookup", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@SubGroup", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = SubGroup;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@UserID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = GlobalAppData.UserID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Account cls = new Account();

                        cls.AccountCode = Convert.ToString(dr["Account"]);
                        cls.AccountParent = Convert.ToString(dr["AccountParent"]);
                        cls.AccountLevel = Convert.ToInt32(dr["AccountLevel"]);
                        cls.AccountStatus = Convert.ToString(dr["AccountStatus"]);
                        cls.AccountDescription = Convert.ToString(dr["AccountDescription"]);
                        cls.AccountDescription_2 = Convert.ToString(dr["AccountDescription_2"]);
                        cls.AccountSubGroup.ID = Convert.ToInt32(dr["AccountSubGroupID"]);
                        cls.AccountCF.ID = Convert.ToInt32(dr["AccountCFID"]);
                        cls.AccountGroup.ID = Convert.ToInt32(dr["AccountGroupID"]);
                        cls.Bank.ID = dr["BankID"] == DBNull.Value ? -1 : int.Parse(dr["BankID"].ToString());
                        cls.Bank.BankAccount = dr["BankAccount"].ToString();
                        //cls.LUN = Convert.ToInt32(dr["LUN"]);
                        //cls.LUB = Convert.ToInt32(dr["LUB"]);
                        //cls.LUD = Convert.ToDateTime(dr["LUD"]);
                        //cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                        //cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                        clsList.Add(cls);
                    }
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return clsList;
        }



        #endregion

        #region AccountLookup

        public List<Account> AccountARAP()
        {
            List<Account> clsList = new List<Account>();
            SqlConnection cnn = _db.GetConnection();
            SqlCommand cmd = new SqlCommand("spAccountARAP", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Account cls = new Account();
                        cls.AccountCode = Convert.ToString(dr["Account"]);  
                        cls.AccountDescription = Convert.ToString(dr["AccountDescription"]);
                        clsList.Add(cls);
                    }
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return clsList;
        }

        #endregion

        #region GetAccountBalance

        public decimal GetAccountBalance(DateTime Date, string Account)
        {
            decimal Balanc = 0;
            SqlConnection cnn = _db.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetAccountBalance", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@Date", System.Data.SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = Date.Date;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Account", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = Account;
            cmd.Parameters.Add(param);
            try
            {
                cnn.Open();
                object ob = cmd.ExecuteScalar();
                Balanc = ob == null ? 0 : decimal.Parse(ob.ToString());
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return Balanc;
        }



        #endregion

        #region GetInitialForCash

        public decimal GetInitialForCash(DateTime FromDate, int Type, string Account)
        {
            decimal Balanc = 0;
            SqlConnection cnn = _db.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetInitialForCash", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@FromDate", System.Data.SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate.Date;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Type", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = Type;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@UserID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = GlobalAppData.UserID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Account", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = Account;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();
                object ob = cmd.ExecuteScalar();
                Balanc = ob == null ? 0 : decimal.Parse(ob.ToString());
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return Balanc;
        }



        #endregion

        #region ChangeAccount

        public void ChangeAccount(string OldAccount, string NewAccount)
        {
            SqlConnection cnn = _db.GetConnection();
            SqlCommand cmd = new SqlCommand("spChangeAccount", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@OldAccount", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = OldAccount;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@NewAccount", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = NewAccount;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                cmd.ExecuteNonQuery();

                cnn.Close();
            }
            catch (Exception ex)
            {
                cnn.Close();
            }
        }



        #endregion


        public DataTable SelectPaymentsTypes()
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _db.GetConnection();
            SqlCommand cmd = new SqlCommand("[spHGetPaymentTypes]", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dadap = new SqlDataAdapter(cmd);
            try
            {
                cnn.Open();

                dadap.Fill(dtList);
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return dtList;
        }




        public List<Account> AccountLookup(string SubGroup, int UserID)
        {
            List<Account> clsList = new List<Account>();
            SqlConnection cnn = _db.GetConnection();
            SqlCommand cmd = new SqlCommand("spAccountLookup", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@SubGroup", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = SubGroup;
            cmd.Parameters.Add(param);

            if (UserID == 0)
                UserID = GlobalAppData.UserID;

            param = new SqlParameter("@UserID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = UserID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Account cls = new Account();

                        cls.AccountCode = Convert.ToString(dr["Account"]);
                        cls.AccountParent = Convert.ToString(dr["AccountParent"]);
                        cls.AccountLevel = Convert.ToInt32(dr["AccountLevel"]);
                        cls.AccountStatus = Convert.ToString(dr["AccountStatus"]);
                        cls.AccountDescription = Convert.ToString(dr["AccountDescription"]);
                        cls.AccountDescription_2 = Convert.ToString(dr["AccountDescription_2"]);
                        cls.AccountSubGroup.ID = Convert.ToInt32(dr["AccountSubGroupID"]);
                        cls.AccountCF.ID = Convert.ToInt32(dr["AccountCFID"]);
                        cls.AccountGroup.ID = Convert.ToInt32(dr["AccountGroupID"]);
                        cls.Bank.ID = dr["BankID"] == DBNull.Value ? -1 : int.Parse(dr["BankID"].ToString());
                        cls.Bank.BankAccount = dr["BankAccount"].ToString();
                        cls.LlogariKalimtare = Convert.ToBoolean(dr["LlogariKalimtare"]);
                        cls.CurrencyID = Convert.ToInt32(dr["CurrencyID"]);
                        cls.CurrencyRate = Convert.ToDecimal(dr["CurrencyRate"]);
                        cls.HomeCurrency = Convert.ToBoolean(dr["HomeCurrency"]);
                        //cls.LUN = Convert.ToInt32(dr["LUN"]);
                        //cls.LUB = Convert.ToInt32(dr["LUB"]);
                        //cls.LUD = Convert.ToDateTime(dr["LUD"]);
                        //cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                        //cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                        clsList.Add(cls);
                    }
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return clsList;
        }


        #region CashAccountForUsers

        public List<Account> CashAccountForUsers(int UserID, string Account)
        {
            List<Account> clsList = new List<Account>();
            SqlConnection cnn = _db.GetConnection();
            SqlCommand cmd = new SqlCommand("spgetCashAccountForUsers", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@userID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = UserID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Account", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = Account;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Account cls = new Account();
                        cls.AccountCode = Convert.ToString(dr["Account"]);
                        cls.AccountDescription = Convert.ToString(dr["AccountDescription"]);
                        cls.AccountDescription_2 = Convert.ToString(dr["Type"]);
                        clsList.Add(cls);
                    }
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return clsList;
        }
        #endregion
    }
}
