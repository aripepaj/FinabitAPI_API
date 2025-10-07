//-- =============================================
//-- Author:		Generated
//-- Create date: 06.10.25 
//-- Description:	CRUD for HRoom
//-- =============================================
using System.Data;
using Microsoft.Data.SqlClient;
using FinabitAPI.Core.Global;
using FinabitAPI.Core.Utilis;
using FinabitAPI.Utilis;

namespace FinabitAPI.Hotel.HRooms
{
    public class HRoomRepository
    {
        public int ErrorID = 0;
        public string ErrorDescription = "";

        private readonly DBAccess _dbAccess;

        public HRoomRepository(DBAccess dbAccess)
        {
            _dbAccess = dbAccess ?? throw new ArgumentNullException(nameof(dbAccess));
        }
        #region Insert

        public void Insert(HRoom cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHRoomInsert", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@RoomName", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RoomName;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RoomTypeID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RoomTypeID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@FloorID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.FloorID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Description", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Description;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Phone", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Phone;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Active", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Active;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@StatusID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.StatusID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RoomPhoto", System.Data.SqlDbType.Image);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RoomPhoto;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RoomPhoto1", System.Data.SqlDbType.Image);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RoomPhoto1;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RoomPhoto2", System.Data.SqlDbType.Image);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RoomPhoto2;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@InsBy", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.InsBy;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@LUB", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.LUB;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@OutOfOrder", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.OutOfOrder;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RoomSize", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RoomSize;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RoomBed", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RoomBed;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RoomView", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RoomView;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@MaxAdult", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.MaxAdult;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@MaxChild", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.MaxChild;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DepartmentID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DepartmentID;
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

        public void Update(HRoom cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHRoomUpdate", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@ID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RoomName", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RoomName;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RoomTypeID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RoomTypeID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@FloorID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.FloorID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Description", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Description;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Phone", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Phone;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Active", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Active;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@StatusID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.StatusID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RoomPhoto", System.Data.SqlDbType.Image);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RoomPhoto;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RoomPhoto1", System.Data.SqlDbType.Image);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RoomPhoto1;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RoomPhoto2", System.Data.SqlDbType.Image);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RoomPhoto2;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@LUB", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.LUB;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PositionX", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PositionX;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PositionY", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PositionY;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@OutOfOrder", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.OutOfOrder;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RoomSize", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RoomSize;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RoomBed", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RoomBed;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RoomView", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RoomView;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@MaxAdult", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.MaxAdult;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@MaxChild", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.MaxChild;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DepartmentID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DepartmentID;
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

        public void Delete(HRoom cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHRoomDelete", cnn);
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

        public List<HRoom> SelectAll(DateTime Date, int DepartmentId)
        {
            List<HRoom> clsList = new List<HRoom>();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHRoomList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@Date", System.Data.SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = Date;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DepartmentID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = DepartmentId;
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
                        HRoom cls = new HRoom();

                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.RoomName = Convert.ToString(dr["RoomName"]) ?? string.Empty;
                        cls.RoomTypeID = Convert.ToInt32(dr["RoomTypeID"]);
                        cls.RoomTypeName = Convert.ToString(dr["RoomTypeName"]) ?? string.Empty;
                        cls.FloorID = Convert.ToInt32(dr["FloorID"]);
                        cls.Floor = Convert.ToString(dr["Floor"]) ?? string.Empty;
                        cls.Description = Convert.ToString(dr["Description"]) ?? string.Empty;
                        cls.Phone = Convert.ToString(dr["Phone"]) ?? string.Empty;
                        cls.Active = Convert.ToBoolean(dr["Active"]);
                        cls.StatusID = Convert.ToInt32(dr["StatusID"]);
                        cls.Status = Convert.ToString(dr["Status"]) ?? string.Empty;
                        //if (dr["RoomPhoto"].ToString() != "")
                        //{
                        //    cls.RoomPhoto = (byte[])dr["RoomPhoto"];
                        //}
                        //else
                        //{
                        //    cls.RoomPhoto = null;
                        //}
                        cls.RoomPhoto = (dr["RoomPhoto"] == DBNull.Value || dr["RoomPhoto"] == null) ? null : ((byte[])dr["RoomPhoto"]);
                        cls.RoomPhoto1 = (dr["RoomPhoto1"] == DBNull.Value || dr["RoomPhoto1"] == null) ? null : ((byte[])dr["RoomPhoto1"]);
                        cls.RoomPhoto2 = (dr["RoomPhoto2"] == DBNull.Value || dr["RoomPhoto2"] == null) ? null : ((byte[])dr["RoomPhoto2"]);
                        cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                        cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                        cls.LUB = Convert.ToInt32(dr["LUB"]);
                        cls.LUN = Convert.ToInt32(dr["LUN"]);
                        cls.LUD = Convert.ToDateTime(dr["LUD"]);
                        cls.PositionX = (dr["PositionX"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PositionX"]);
                        cls.PositionY = (dr["PositionY"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PositionY"]);
                        cls.SizeX = (dr["SizeX"] == DBNull.Value) ? 98 : Convert.ToInt32(dr["SizeX"]);
                        cls.SizeY = (dr["SizeY"] == DBNull.Value) ? 45 : Convert.ToInt32(dr["SizeY"]);
                        cls.BackColor = Convert.ToString(dr["BackColor"]) ?? string.Empty;
                        cls.GuestName = Convert.ToString(dr["GuestName"]) ?? string.Empty;
                        cls.ReservationID = Convert.ToInt32(dr["ReservationID"]);
                        cls.OutOfOrder = Convert.ToInt32(dr["OutOfOrder"]);
                        cls.RoomSize = Convert.ToString(dr["RoomSize"]) ?? string.Empty;
                        cls.RoomBed = Convert.ToString(dr["RoomBed"]) ?? string.Empty;
                        cls.RoomView = Convert.ToString(dr["RoomView"]) ?? string.Empty;
                        cls.MaxAdult = Convert.ToInt32(dr["MaxAdult"]);
                        cls.MaxChild = Convert.ToInt32(dr["MaxChild"]);
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
          #region SelectAll_2

        public List<HRoom> SelectAll_3(DateTime Date)
        {
            List<HRoom> clsList = new List<HRoom>();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHRoomList_3", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@Date", System.Data.SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = Date;
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
                        HRoom cls = new HRoom();

                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.RoomName = Convert.ToString(dr["RoomName"]);
                        cls.RoomTypeID = Convert.ToInt32(dr["RoomTypeID"]);
                        
                        //cls.RoomTypeName = Convert.ToString(dr["RoomTypeName"]);
                        //cls.FloorID = Convert.ToInt32(dr["FloorID"]);
                        //cls.Floor = Convert.ToString(dr["Floor"]);
                        cls.Description = Convert.ToString(dr["Description"]);
                        cls.Phone = Convert.ToString(dr["Phone"]);
                        cls.Active = Convert.ToBoolean(dr["Active"]);
                        cls.StatusID = Convert.ToInt32(dr["StatusID"]);
                        cls.Status = Convert.ToString(dr["Status"]);
                        //if (dr["RoomPhoto"].ToString() != "")
                        //{
                        //    cls.RoomPhoto = (byte[])dr["RoomPhoto"];
                        //}
                        //else
                        //{
                        //    cls.RoomPhoto = null;
                        //}
                        //cls.RoomPhoto = (dr["RoomPhoto"] == DBNull.Value || dr["RoomPhoto"] == null) ? null : ((byte[])dr["RoomPhoto"]);
                        //cls.RoomPhoto1 = (dr["RoomPhoto1"] == DBNull.Value || dr["RoomPhoto1"] == null) ? null : ((byte[])dr["RoomPhoto1"]);
                        //cls.RoomPhoto2 = (dr["RoomPhoto2"] == DBNull.Value || dr["RoomPhoto2"] == null) ? null : ((byte[])dr["RoomPhoto2"]);
                        //cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                        //cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                        //cls.LUB = Convert.ToInt32(dr["LUB"]);
                        //cls.LUN = Convert.ToInt32(dr["LUN"]);
                        //cls.LUD = Convert.ToDateTime(dr["LUD"]);
                        //cls.PositionX = (dr["PositionX"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PositionX"]);
                        //cls.PositionY = (dr["PositionY"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PositionY"]);
                        //cls.SizeX = (dr["SizeX"] == DBNull.Value) ? 98 : Convert.ToInt32(dr["SizeX"]);
                        //cls.SizeY = (dr["SizeY"] == DBNull.Value) ? 45 : Convert.ToInt32(dr["SizeY"]);
                        //cls.BackColor = Convert.ToString(dr["BackColor"]);
                        cls.GuestName = Convert.ToString(dr["GuestName"]);
                        cls.ReservationID = Convert.ToInt32(dr["ReservationID"]);
                        cls.OutOfOrder = Convert.ToInt32(dr["OutOfOrder"]);

                        cls.SubBookingID = Convert.ToString(dr["SubBookingID"]);
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

        #region SelectByName_Date

        public HRoom SelectByName_Date(string RoomName, DateTime Date)
        {
            List<HRoom> clsList = new List<HRoom>();
            HRoom cls = new HRoom();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHRoomByName_Date", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("Date", System.Data.SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = Date;
            cmd.Parameters.Add(param);

            param = new SqlParameter("RoomName", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = RoomName;
            cmd.Parameters.Add(param);


            try
            {
                cnn.Open();
                

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        

                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.RoomName = Convert.ToString(dr["RoomName"]);
                        cls.RoomTypeID = Convert.ToInt32(dr["RoomTypeID"]);

                        //cls.RoomTypeName = Convert.ToString(dr["RoomTypeName"]);
                        //cls.FloorID = Convert.ToInt32(dr["FloorID"]);
                        //cls.Floor = Convert.ToString(dr["Floor"]);
                        cls.Description = Convert.ToString(dr["Description"]);
                        cls.Phone = Convert.ToString(dr["Phone"]);
                        cls.Active = Convert.ToBoolean(dr["Active"]);
                        cls.StatusID = Convert.ToInt32(dr["StatusID"]);
                        cls.Status = Convert.ToString(dr["Status"]);
                        //if (dr["RoomPhoto"].ToString() != "")
                        //{
                        //    cls.RoomPhoto = (byte[])dr["RoomPhoto"];
                        //}
                        //else
                        //{
                        //    cls.RoomPhoto = null;
                        //}
                        //cls.RoomPhoto = (dr["RoomPhoto"] == DBNull.Value || dr["RoomPhoto"] == null) ? null : ((byte[])dr["RoomPhoto"]);
                        //cls.RoomPhoto1 = (dr["RoomPhoto1"] == DBNull.Value || dr["RoomPhoto1"] == null) ? null : ((byte[])dr["RoomPhoto1"]);
                        //cls.RoomPhoto2 = (dr["RoomPhoto2"] == DBNull.Value || dr["RoomPhoto2"] == null) ? null : ((byte[])dr["RoomPhoto2"]);
                        //cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                        //cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                        //cls.LUB = Convert.ToInt32(dr["LUB"]);
                        //cls.LUN = Convert.ToInt32(dr["LUN"]);
                        //cls.LUD = Convert.ToDateTime(dr["LUD"]);
                        //cls.PositionX = (dr["PositionX"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PositionX"]);
                        //cls.PositionY = (dr["PositionY"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PositionY"]);
                        //cls.SizeX = (dr["SizeX"] == DBNull.Value) ? 98 : Convert.ToInt32(dr["SizeX"]);
                        //cls.SizeY = (dr["SizeY"] == DBNull.Value) ? 45 : Convert.ToInt32(dr["SizeY"]);
                        //cls.BackColor = Convert.ToString(dr["BackColor"]);
                        cls.GuestName = Convert.ToString(dr["GuestName"]);
                        cls.ReservationID = Convert.ToInt32(dr["ReservationID"]);
                        cls.OutOfOrder = Convert.ToInt32(dr["OutOfOrder"]);
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
            return cls;
        }

        #endregion

        #region SelectAllTable

        public DataTable SelectAllTable(DateTime Date)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHRoomList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dadap = new SqlDataAdapter(cmd);

            SqlParameter param;

            param = new SqlParameter("@Date", System.Data.SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = Date;
            cmd.Parameters.Add(param);

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

        #region SelectAllTableHistory

        public DataTable SelectAllTableHistory(int RoomID)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHRoomHistory", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dadap = new SqlDataAdapter(cmd);


            SqlParameter param;
        
            param = new SqlParameter("@RoomID", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = RoomID;
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

        public HRoom SelectByID(HRoom cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHRoomByID", cnn);
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
                        cls = new HRoom();
                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.RoomName = Convert.ToString(dr["RoomName"]);
                        cls.RoomTypeID = Convert.ToInt32(dr["RoomTypeID"]);
                        cls.FloorID = Convert.ToInt32(dr["FloorID"]);
                        cls.Description = Convert.ToString(dr["Description"]);
                        cls.Phone = Convert.ToString(dr["Phone"]);
                        cls.Active = Convert.ToBoolean(dr["Active"]);
                        cls.StatusID = Convert.ToInt32(dr["StatusID"]);

                        if (dr["RoomPhoto"].ToString() != "")
                        {
                            cls.RoomPhoto = (byte[])dr["RoomPhoto"];
                        }
                        else
                        {
                            cls.RoomPhoto = null;
                        }
                        if (dr["RoomPhoto1"].ToString() != "")
                        {
                            cls.RoomPhoto1 = (byte[])dr["RoomPhoto1"];
                        }
                        else
                        {
                            cls.RoomPhoto1 = null;
                        }
                        if (dr["RoomPhoto2"].ToString() != "")
                        {
                            cls.RoomPhoto2 = (byte[])dr["RoomPhoto2"];
                        }
                        else
                        {
                            cls.RoomPhoto2 = null;
                        }
                        cls.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                        //cls.RoomPhoto = (dr["RoomPhoto"] == DBNull.Value) ? null : ((byte[])dr["RoomPhoto"]);
                        cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                        cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                        cls.LUB = Convert.ToInt32(dr["LUB"]);
                        cls.LUN = Convert.ToInt32(dr["LUN"]);
                        cls.LUD = Convert.ToDateTime(dr["LUD"]);
                        cls.OutOfOrder = Convert.ToInt32(dr["OutOfOrder"]);
                        cls.PositionX = dr["PositionX"] == null ? 0 : Convert.ToInt16(dr["PositionX"]);
                        cls.PositionY = dr["PositionX"] == null ? 0 : Convert.ToInt16(dr["PositionY"]);
                        cls.RoomSize = Convert.ToString(dr["RoomSize"]);
                        cls.RoomBed = Convert.ToString(dr["RoomBed"]);
                        cls.RoomView = Convert.ToString(dr["RoomView"]);
                        cls.MaxAdult = Convert.ToInt32(dr["MaxAdult"]);
                        cls.MaxChild = Convert.ToInt32(dr["MaxChild"]);
                        
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


        #region UpdatePosition

        public void UpdatePosition(int RoomID, int PositionX, int PositionY)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHRoomUpdatePosition", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@RoomID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = RoomID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PositionX", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = PositionX;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PositionY", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = PositionY;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmErrorID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);


            int ErrorID;

            try
            {
                cnn.Open();

                cmd.ExecuteNonQuery();
                ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);

                cnn.Close();
            }
            catch (Exception ex)
            {
                ErrorID = -1;

                cnn.Close();
            }
        }

        #endregion

        public int GetReservationForRoom(int RoomID, DateTime Date)
        {
            int ReservationID;
            ReservationID = -1;
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetReservationForRoom", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@RoomID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = RoomID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Date", System.Data.SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = Date;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ReservationID = Convert.ToInt32(dr["ReservationID"]);
                    }
                }
            }
            catch { }

            return ReservationID;
        }

        public void SetOutOfOrder(int RoomID,int OutOfOrder)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spSetOutOfOrder", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@RoomID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = RoomID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@OutOfOrder", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = OutOfOrder;
            cmd.Parameters.Add(param);


            int ErrorID;

            try
            {
                cnn.Open();

                cmd.ExecuteNonQuery();

                cnn.Close();
            }
            catch (Exception ex)
            {
                ErrorID = -1;

                cnn.Close();
            }


        }

        public DataTable SelectAllInformationInvoice(DateTime FromDate,DateTime ToDate )
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHInformationInvoice", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dadap = new SqlDataAdapter(cmd);

            SqlParameter param;

            param = new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
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

        public DataTable GetFreeRoomsBySelectedType(DateTime FromDate, DateTime ToDate, int Type)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetFreeRoomsBySelectedType", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dadap = new SqlDataAdapter(cmd);

            SqlParameter param;

            param = new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Type", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = Type;
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
        public HRoom GetFreeRoomByType(string RoomTypeCode,DateTime CheckInDate,DateTime CheckOutDate)
        {
            HRoom cls = new HRoom();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetFreeRoomByType", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@RoomTypeCode", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = RoomTypeCode;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@CheckInDate", System.Data.SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = CheckInDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@CheckOutDate", System.Data.SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = CheckOutDate;
            cmd.Parameters.Add(param);

           


            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cls = new HRoom();
                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.RoomName = Convert.ToString(dr["RoomName"]);
                     

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
