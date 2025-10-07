//-- =============================================
//-- Author:		Generated
//-- Create date: 07.10.25 
//-- Description:	CRUD for HGuest
//-- =============================================
using System.Data;
using Microsoft.Data.SqlClient;
using FinabitAPI.Core.Global;
using FinabitAPI.Core.Utilis;
using FinabitAPI.Utilis;

namespace FinabitAPI.Hotel.HGuests
{
    public class HGuestRepository
    {
        public int ErrorID = 0;
        public string ErrorDescription = "";

        private readonly DBAccess _dbAccess;

        public HGuestRepository(DBAccess dbAccess)
        {
            _dbAccess = dbAccess ?? throw new ArgumentNullException(nameof(dbAccess));
        }

        #region Insert

        public void Insert(HGuest cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHGuestInsert", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@IDNo", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.IDNo ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Name ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Surname", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Surname ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Gender", System.Data.SqlDbType.TinyInt);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Gender;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Company", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Company ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Profession", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Profession ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Email", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Email ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Phone1", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Phone1 ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Phone2", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Phone2 ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@GuestTypeID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.GuestTypeID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@CountryID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.CountryID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@CityID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.CityID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Address", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Address ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Description", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Description ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@InsBy", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.InsBy;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@LUB", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.LUB;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Birthday", System.Data.SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Birthday;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmErrorID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            param = GlobalRepository.GetSqlParameterOutput("@prmNewID", SqlDbType.Int);
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                cmd.ExecuteNonQuery();
                cls.ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);
                cls.ID = Convert.ToInt32(cmd.Parameters["@prmNewID"].Value);

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

        public void Update(HGuest cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHGuestUpdate", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@ID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@IDNo", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.IDNo ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Name ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Surname", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Surname ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Gender", System.Data.SqlDbType.TinyInt);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Gender;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Company", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Company ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Profession", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Profession ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Email", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Email ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Phone1", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Phone1 ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Phone2", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Phone2 ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@GuestTypeID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.GuestTypeID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@CountryID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.CountryID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@CityID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.CityID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Address", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Address ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Description", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Description ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@LUB", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.LUB;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Birthday", System.Data.SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Birthday;
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

        public void Delete(HGuest cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHGuestDelete", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@ID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ID;
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

        public List<HGuest> SelectAll()
        {
            List<HGuest> clsList = new List<HGuest>();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHGuestList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        HGuest cls = new HGuest();
                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.IDNo = Convert.ToString(dr["IDNo"]) ?? string.Empty;
                        cls.Name = Convert.ToString(dr["Name"]) ?? string.Empty;
                        cls.Guest = Convert.ToString(dr["Guest"]) ?? string.Empty;
                        cls.Surname = Convert.ToString(dr["StateName"]) ?? string.Empty;
                        cls.Gender = Convert.ToInt16(dr["Gender"]);
                        cls.Company = Convert.ToString(dr["Company"]) ?? string.Empty;
                        cls.Profession = Convert.ToString(dr["Profession"]) ?? string.Empty;
                        cls.Email = Convert.ToString(dr["Email"]) ?? string.Empty;
                        cls.Phone1 = Convert.ToString(dr["Phone1"]) ?? string.Empty;
                        cls.Phone2 = Convert.ToString(dr["Phone2"]) ?? string.Empty;
                        cls.GuestTypeID = Convert.ToInt32(dr["GuestTypeID"]);
                        cls.GuestType = Convert.ToString(dr["GuestType"]) ?? string.Empty;
                        //cls.CountryID = Convert.ToInt32(dr["CountryID"]);
                        //cls.CityID = Convert.ToInt32(dr["CityID"]);
                        cls.Address = Convert.ToString(dr["Address"]) ?? string.Empty;
                        cls.Description = Convert.ToString(dr["Description"]) ?? string.Empty;
                        cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                        cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                        cls.LUB = Convert.ToInt32(dr["LUB"]);
                        cls.LUN = Convert.ToInt32(dr["LUN"]);
                        cls.LUD = Convert.ToDateTime(dr["LUD"]);
                        cls.Birthday = Convert.ToDateTime(dr["Birthday"]);



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
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHGuestList", cnn);
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
          #region SelectByID

        public HGuest SelectByID(HGuest cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHGuestByID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@ID", System.Data.SqlDbType.Int);
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
                        cls = new HGuest();
                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.IDNo = Convert.ToString(dr["IDNo"]);
                        cls.Name = Convert.ToString(dr["Name"]);
                        cls.Surname = Convert.ToString(dr["Surname"]);
                        cls.Gender = Convert.ToInt16(dr["Gender"]);
                        cls.Company = Convert.ToString(dr["Company"]);
                        cls.Profession = Convert.ToString(dr["Profession"]);
                        cls.Email = Convert.ToString(dr["Email"]);
                        cls.Phone1 = Convert.ToString(dr["Phone1"]);
                        cls.Phone2 = Convert.ToString(dr["Phone2"]);
                        cls.GuestTypeID = Convert.ToInt32(dr["GuestTypeID"]);
                        cls.CountryID = Convert.ToInt32(dr["CountryID"]);
                        cls.CityID = Convert.ToInt32(dr["CityID"]);
                        cls.Address = Convert.ToString(dr["Address"]);
                        cls.Description = Convert.ToString(dr["Description"]);
                        cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                        cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                        cls.LUB = Convert.ToInt32(dr["LUB"]);
                        cls.LUN = Convert.ToInt32(dr["LUN"]);
                        cls.LUD = Convert.ToDateTime(dr["LUD"]);
                        cls.Birthday = Convert.ToDateTime(dr["Birthday"]);
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

        #region InHouseList

        public DataTable HInHouseList(DateTime Date, bool Include)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHInHouseList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@Date", System.Data.SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = Date;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@IncludeCheckOut", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = Include;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@UserID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = GlobalAppData.UserID;
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

        public DataTable HArrivalList(DateTime FromDate, DateTime ToDate,string Origin, string RoomType,bool Include)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("[spHArrivaleListByDate]", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Origin", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = Origin;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RoomType", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = RoomType;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@IncludeCheckOut", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = Include;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@UserID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = GlobalAppData.UserID;
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


        public DataTable HDepartureList(DateTime FromDate, DateTime ToDate, string roomType, string origin,bool Include)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("[spHDepartureListByDate]", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RoomType", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = roomType;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Origin", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = origin;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@IncludeCheckOut", SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = Include;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@UserID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = GlobalAppData.UserID;
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

        public DataTable HReservationList(DateTime FromDate, DateTime ToDate,string OriginId)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("[spHReservationListByDate]", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@OriginId", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = OriginId;
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

        public DataTable HClientRealisationList(DateTime FromDate, DateTime ToDate, string OriginId)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("[spClientRealisationList]", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@OriginId", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = OriginId;
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


        public DataTable HClientRealisationList_Group(DateTime FromDate, DateTime ToDate, string OriginId)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("[spClientRealisationList_Group]", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@OriginId", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = OriginId;
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

        public DataTable ExtraChargeListByDate(DateTime FromDate, DateTime ToDate, string Origin, string RoomType)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("[spExtraChargeDetailsListByDate]", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Origin", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = Origin;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RoomType", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = RoomType;
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



        public DataTable ClientBalancByDate(string FromDate)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("[spClientBalanceList]", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@FromDate", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
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

        public DataTable MonthRoomStatuses(string FromDate, string ToDate)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("[spHGetStatusesBatch]", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@FromDate", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
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

        public DataTable RoomTypeStatuses(DateTime FromDate, DateTime ToDate)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("[spHGetStatusesBatch_2]", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@FromDate", System.Data.SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", System.Data.SqlDbType.SmallDateTime );
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@UserID", SqlDbType.Int );
            param.Direction = ParameterDirection.Input;
            param.Value = GlobalAppData.UserID;
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

        public DataTable RoomTypeStatuses_PerPeriod(DateTime FromDate, DateTime ToDate)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("[spHGetStatusesBatch_perPeriod]", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@FromDate", System.Data.SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", System.Data.SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@UserID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = GlobalAppData.UserID;
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

        public DataTable GetFreeRooms_PerPeriod(DateTime FromDate, DateTime ToDate)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("[spHGetFreeRooms_perPeriod]", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@FromDate", System.Data.SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", System.Data.SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@UserID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = GlobalAppData.UserID;
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


        #region SelectAllTableBySearch

        public DataTable SelectAllTableBySearch(string Name)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHGuestListBySearch", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("@Name", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = Name;
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


        public HGuest SelectByFullName(string FullName)
        {
            HGuest cls = new HGuest();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHGuestListByFullName", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@FullName", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = FullName;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cls = new HGuest();
                        cls.ID = Convert.ToInt32(dr["ID"]);
                       
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
    }
}
