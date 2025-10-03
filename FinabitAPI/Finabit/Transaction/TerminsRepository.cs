using System.Data;
using Microsoft.Data.SqlClient;
using FinabitAPI.Finabit.Transaction.dto;
using FinabitAPI.Core.Global;
using FinabitAPI.Core.Utilis;

namespace FinabitAPI.Finabit.Transaction
{
    public class TerminsRepository
    {

        #region Insert

        public void Insert(Termins cls)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTerminsInsert", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@EmployeeID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.EmployeeID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@BeginningDate", SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = DateTime.Now;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Status", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Status;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@CashJournalPOSID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.CashJournalPOSID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DepartmentID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DepartmentID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmNewID", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmErrorID", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@JournalInitialState", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.JournalInititalState;
            cmd.Parameters.Add(param);
            try
            {
                cnn.Open();

                cmd.ExecuteNonQuery();
                cls.ID = Convert.ToInt32(cmd.Parameters["@prmNewID"].Value);
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

        public void Update(Termins cls)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTerminsUpdate", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@TerminID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@EndingDate", SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = DateTime.Now;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Status", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Status;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DepartmentID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DepartmentID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@IsSynchronised", SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.IsSynchronised;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmErrorID", SqlDbType.Int);
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

        public void Delete(Termins cls)
        {
            SqlConnection cnn = new SqlConnection();
            cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTerminsDelete", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@TerminID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmErrorID", SqlDbType.Int);
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

        public List<Termins> SelectAll()
        {
            List<Termins> clsList = new List<Termins>();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTerminsList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Termins cls = new Termins();
                        cls.ID = Convert.ToInt32(dr["TerminID"]);
                        cls.EmployeeID = Convert.ToInt32(dr["EmployeeID"]);
                        cls.EmployeeName = dr["EmployeeName"].ToString();
                        cls.BeginningDate = Convert.ToDateTime(dr["BeginningDate"]);
                        cls.EndingDate = dr["EndingDate"] == DBNull.Value ? null : new DateTime?(Convert.ToDateTime(dr["EndingDate"]));
                        cls.StatusName = dr["StatusName"].ToString();
                        cls.Status = Convert.ToInt32(dr["Status"]);
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

        public DataTable SelectAllTable(bool IsCombo)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTerminsList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@IsCombo", SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = IsCombo;
            cmd.Parameters.Add(param);

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

        #region SelectAllTableWithSum

        public DataTable SelectAllTableWithSum(string FromDate, string ToDate, string DepartmentID)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTerminsListWhithSum", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            SqlDataAdapter dadap = new SqlDataAdapter(cmd);

            SqlParameter param;

            param = new SqlParameter("@FromDate", SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DepartmentID", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = DepartmentID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@UserID", SqlDbType.Int);
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

        public Termins SelectByID(Termins cls)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTerminsByID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@TerminID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cls = new Termins();
                        cls.ID = Convert.ToInt32(dr["TerminID"]);
                        cls.EmployeeID = Convert.ToInt32(dr["EmployeeID"]);
                        cls.EmployeeName = dr["EmployeeName"].ToString();
                        cls.TerminName = dr["TerminName"].ToString();
                        cls.BeginningDate = Convert.ToDateTime(dr["BeginningDate"]);
                        cls.EndingDate = dr["EndingDate"] == DBNull.Value ? null : new DateTime?(Convert.ToDateTime(dr["EndingDate"]));
                        cls.StatusName = dr["StatusName"].ToString();
                        cls.Status = Convert.ToInt32(dr["Status"]);
                        cls.CashJournalPOSID = Convert.ToInt32(dr["CashJournalPOSID"]);
                        cls.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                        cls.IsSynchronised = dr["IsSynchronised"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsSynchronised"]);

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

        #region SelectByIDDataTable
        public DataTable SelectByIDDataTable(Termins cls)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTerminsByID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dadap = new SqlDataAdapter(cmd);

            SqlParameter param;

            param = new SqlParameter("@TerminID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ID;
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

        #region OpenedTerminID

        public static string[] OpenedTerminID(int EmpID, int DepartmentID)
        {
            string[] s = { "", "" };

            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("[spGetOpenTerminID]", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@EmployeeID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = EmpID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DepartmentID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = DepartmentID;
            cmd.Parameters.Add(param);
            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        s[0] = Convert.ToString(dr["TerminID"].ToString());
                        s[1] = Convert.ToString(dr["CashJournalPOSID"].ToString());
                    }
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return s;
        }

        #endregion

        #region TerminClosed

        public static void TerminClosed(int TerminID)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTerminClosed", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@TerminID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TerminID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
        }

        #endregion

        #region RealisationsList

        public DataTable RealisationsList(string FromDate, string ToDate, int Mode, string Import)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("[spRealisationsList]", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dadap = new SqlDataAdapter(cmd);

            SqlParameter param;

            param = new SqlParameter("@FromDate", SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Mode", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = Mode;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Import", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = Import;
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

        #region RealisationsListChart

        public DataTable RealisationsListChart(string FromDate, string ToDate, int Mode, string Import)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spRealisationsListChart", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dadap = new SqlDataAdapter(cmd);

            SqlParameter param;

            param = new SqlParameter("@FromDate", SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Mode", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = Mode;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Import", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = Import;
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

        #region TerminReport

        public DataTable TerminReport(int TerminID)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTerminReport", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dadap = new SqlDataAdapter(cmd);

            SqlParameter param;

            param = new SqlParameter("@TerminID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TerminID;
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

        #region TerminReportBatch

        public DataTable TerminReportBatch(DataTable Termins, bool IsReprezentacion)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTerminReportBatch", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dadap = new SqlDataAdapter(cmd);

            SqlParameter param;

            param = new SqlParameter("@Termins", SqlDbType.Structured);
            param.Direction = ParameterDirection.Input;
            param.Value = Termins;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@IsReprezentacion", SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = IsReprezentacion;
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

        #region TerminReportCash

        public DataSet TerminReportCash(DataTable Termins, DateTime Date, string Account)
        {
            DataSet dtList = new DataSet();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("[spTerminCash]", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dadap = new SqlDataAdapter(cmd);

            SqlParameter param;

            param = new SqlParameter("@Termins", SqlDbType.Structured);
            param.Direction = ParameterDirection.Input;
            param.Value = Termins;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Date", SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = Date;
            cmd.Parameters.Add(param);

            //@Account
            param = new SqlParameter("@Account", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = Account;
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

        #region TerminReportList

        public DataTable TerminReportList(int UserID)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTerminReportList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dadap = new SqlDataAdapter(cmd);

            SqlParameter param;

            param = new SqlParameter("@UserID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = UserID;
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

        #region CheckOpenedTermin

        public bool CheckOpenedTermin(int DepartmentID)
        {
            bool rez = false;

            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spCheckOpenTermin", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@DepartmentID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = DepartmentID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    rez = true;
                }
                else
                {
                    rez = false;
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }


            return rez;

        }

        #endregion

        #region TerminReportBatch

        public DataTable TerminReportBatch2(DataTable Termins)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTerminReportBatch_2", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dadap = new SqlDataAdapter(cmd);

            SqlParameter param;

            param = new SqlParameter("@Termins", SqlDbType.Structured);
            param.Direction = ParameterDirection.Input;
            param.Value = Termins;
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

        #region TerminSyncWithTransferDB

        public void TerminSyncWithTransferDB(Termins cls, bool DeletePrevious)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTerminSyncWithTransferDB", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 360;

            SqlParameter param;

            param = new SqlParameter("@TerminID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DeletePrevious", SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = DeletePrevious;
            cmd.Parameters.Add(param);





            try
            {

                cnn.Open();

                cmd.ExecuteNonQuery();


                cnn.Close();

            }
            catch (Exception ex)
            {

                cls.ErrorID = -1;
                //DALGlobal = -1;
                cls.ErrorDescription = ex.Message;
                cnn.Close();
            }


        }

        #endregion

        #region CheckTermins_NotSynced

        public List<Termins> CheckTermins_NotSynced()
        {
            List<Termins> clsList = new List<Termins>();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spCheckTermins_NotSynced", cnn);
            cmd.CommandType = CommandType.StoredProcedure;


            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Termins cls = new Termins();
                        cls.ID = Convert.ToInt32(dr["TerminID"]);

                        cls.BeginningDate = Convert.ToDateTime(dr["BeginningDate"]);

                        cls.Status = Convert.ToInt32(dr["Status"]);
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

        public DataTable TerminsForADay(string Date, int Status)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTerminsForADay", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            SqlDataAdapter dadap = new SqlDataAdapter(cmd);

            SqlParameter param;

            param = new SqlParameter("@Date", SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = Date;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Status", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = Status;
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



    }
}
