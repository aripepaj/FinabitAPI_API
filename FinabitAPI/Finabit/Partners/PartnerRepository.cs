using System.Data;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using FinabitAPI.Finabit.Partner.dto;
using FinabitAPI.Core.Global;
using FinabitAPI.Core.Utilis;
using FinabitAPI.Core.Global.dto;
using FinabitAPI.Utilis;

namespace FinabitAPI
{
    public class PartnerRepository
    {
        private readonly DBAccess _dbAccess;
        public PartnerRepository(DBAccess dbAccess) { _dbAccess = dbAccess; }

        public void Insert(Partner cls)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spPartnerInsert", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;


            param = new SqlParameter("@PartnerName", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PartnerName;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ContactPerson", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ContactPerson;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@PartnerTypeID", SqlDbType.SmallInt);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PartnerType.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Account", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Account.AccountCode;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@StateID", SqlDbType.SmallInt);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.State.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RegionID", SqlDbType.SmallInt);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RegionID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PlaceID", SqlDbType.SmallInt);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Place.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Address", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Address;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Tel1", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Tel1;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Tel2", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Tel2;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Email", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Email;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@WebSite", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.WebSite;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@BusinessNo", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.BusinessNo;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RealBusinessNo", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RealBusinessNo;
            cmd.Parameters.Add(param);



            param = new SqlParameter("@BankAccount", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.BankAccount;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@UserID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = GlobalAppData.UserID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DiscountPercent", SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DiscountPercent;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PIN", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PIN;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ItemID", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ItemID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PriceMenuID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PriceMenuID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DueDays", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DueDays;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DueValueMaximum", SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DueValueMaximum;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ContractNo", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ContractNo;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@VATNO", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.VATNO;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Longitude", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Longitude;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Latitude", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Latitude;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PartnerPicture", SqlDbType.Image);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PartnerPicture;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmErrorID", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PrmNewID", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);
            try
            {
                cnn.Open();

                cmd.ExecuteNonQuery();
                cls.ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);
                cls.ID = Convert.ToInt32(cmd.Parameters["@PrmNewID"].Value);
                cnn.Close();
            }
            catch (Exception ex)
            {
                cls.ErrorID = -1;
                cls.ErrorDescription = ex.Message;
                cnn.Close();
            }

        }

        public void Update(Partner cls)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spPartnerUpdate", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@PartnerID", SqlDbType.SmallInt);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PartnerName", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PartnerName;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ContactPerson", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ContactPerson;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PartnerTypeID", SqlDbType.SmallInt);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PartnerType.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Account", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Account.AccountCode;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@StateID", SqlDbType.SmallInt);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.State.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RegionID", SqlDbType.SmallInt);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RegionID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PlaceID", SqlDbType.SmallInt);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Place.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Address", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Address;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Tel1", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Tel1;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Tel2", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Tel2;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Email", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Email;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@WebSite", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.WebSite;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@BusinessNo", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.BusinessNo;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@BankAccount", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.BankAccount;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@UserID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.LUB;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DiscountPercent", SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DiscountPercent;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PIN", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PIN;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ItemID", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ItemID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PriceMenuID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PriceMenuID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DueDays", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DueDays;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DueValueMaximum", SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DueValueMaximum;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ContractNo", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ContractNo;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@VATNO", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.VATNO;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Longitude", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Longitude;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Latitude", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Latitude;
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

        public void Delete(Partner cls)
        {
            SqlConnection cnn = new SqlConnection();
            cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spPartnerDelete", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@PartnerID", SqlDbType.Int);
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

        public List<Partner> SelectAll()
        {
            List<Partner> clsList = new List<Partner>();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spPartnerList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Partner cls = new Partner();
                        cls.ID = Convert.ToInt32(dr["PartnerID"]);
                        cls.PartnerName = Convert.ToString(dr["PartnerName"]);
                        cls.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                        cls.PartnerType.PartnerTypeName = Convert.ToString(dr["PartnerTypeName"]);
                        cls.PartnerType.ID = Convert.ToInt32(dr["PartnerTypeID"]);
                        cls.Account.AccountCode = Convert.ToString(dr["Account"]);
                        cls.State.ID = dr["StateID"] == DBNull.Value ? -1 : int.Parse(dr["StateID"].ToString());
                        cls.RegionID = dr["RegionID"] == DBNull.Value ? -1 : int.Parse(dr["RegionID"].ToString());
                        cls.State.StateName = Convert.ToString(dr["StateName"].ToString());
                        cls.Place.ID = dr["PlaceID"] == DBNull.Value ? 0 : int.Parse(dr["PlaceID"].ToString());
                        cls.Place.PlaceName = Convert.ToString(dr["PlaceName"]);
                        cls.Address = Convert.ToString(dr["Address"]);
                        cls.Tel1 = Convert.ToString(dr["Tel1"]);
                        cls.Tel2 = Convert.ToString(dr["Tel2"]);
                        cls.Fax = Convert.ToString(dr["Fax"]);
                        cls.Email = Convert.ToString(dr["Email"]);
                        cls.WebSite = Convert.ToString(dr["WebSite"]);
                        cls.BusinessNo = Convert.ToString(dr["BusinessNo"]);
                        cls.BankAccount = Convert.ToString(dr["BankAccount"]);
                        cls.PIN = dr["PIN"] == DBNull.Value ? "" : Convert.ToString(dr["PIN"]);
                        cls.ItemID = dr["ItemID"] == DBNull.Value ? "" : Convert.ToString(dr["ItemID"]);
                        cls.LUN = dr["LUN"] == DBNull.Value ? 0 : int.Parse(dr["LUN"].ToString());
                        cls.LUB = dr["LUB"] == DBNull.Value ? 0 : int.Parse(dr["LUB"].ToString());
                        cls.LUD = Convert.ToDateTime(dr["LUD"]);
                        cls.PriceMenuID = dr["PriceMenuID"] == DBNull.Value ? 0 : int.Parse(dr["PriceMenuID"].ToString());
                        cls.DueDays = dr["DueDays"] == DBNull.Value ? 0 : int.Parse(dr["DueDays"].ToString());
                        cls.DueValueMaximum = dr["DueValueMaximum"] == DBNull.Value ? 0 : decimal.Parse(dr["DueValueMaximum"].ToString());
                        cls.ContractNo = Convert.ToString(dr["ContractNo"]);
                        cls.Longitude = Convert.ToDecimal(dr["Longitude"]);
                        cls.Latitude = Convert.ToDecimal(dr["Latitude"]);

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

        public DataTable SelectAllTable()
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spPartnerList", cnn);
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


        public List<Partner> SelectAllByType(int TypeID, string filter)
        {
            List<Partner> clsList = new List<Partner>();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spPartnerByType", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@Type", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TypeID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Filter", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = filter;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Partner cls = new Partner();
                        cls.ID = Convert.ToInt32(dr["PartnerID"]);
                        cls.PartnerName = Convert.ToString(dr["PartnerName"]);

                        cls.PartnerType.PartnerTypeName = Convert.ToString(dr["PartnerTypeName"]);
                        cls.PartnerType.ID = Convert.ToInt32(dr["PartnerTypeID"]);
                        cls.Account.AccountCode = Convert.ToString(dr["Account"]);
                        cls.DiscountPercent = Convert.ToDecimal(dr["DiscountPercent"]);

                        cls.State.StateName = Convert.ToString(dr["StateName"].ToString());

                        cls.Place.PlaceName = dr["PlaceName"] == DBNull.Value ? "" : Convert.ToString(dr["PlaceName"]);

                        cls.BusinessNo = Convert.ToString(dr["BusinessNo"]);

                        cls.PIN = dr["PIN"] == DBNull.Value ? "" : Convert.ToString(dr["PIN"]);

                        cls.PriceMenuID = dr["PriceMenuID"] == DBNull.Value ? 0 : int.Parse(dr["PriceMenuID"].ToString());
                        cls.Longitude = Convert.ToDecimal(dr["Longitude"]);
                        cls.Latitude = Convert.ToDecimal(dr["Latitude"]);
                        cls.ItemID = Convert.ToString(dr["ItemID"]);


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

        public Partner SelectByPIN(string PIN)
        {
            Partner cls = new Partner();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetPartnerByPIN", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@PIN", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = PIN;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        cls.ID = Convert.ToInt32(dr["PartnerID"]);
                        cls.PartnerName = Convert.ToString(dr["PartnerName"]);

                        cls.PartnerType.PartnerTypeName = Convert.ToString(dr["PartnerTypeName"]);
                        cls.PartnerType.ID = Convert.ToInt32(dr["PartnerTypeID"]);
                        cls.Account.AccountCode = Convert.ToString(dr["Account"]);
                        cls.DiscountPercent = Convert.ToDecimal(dr["DiscountPercent"]);

                        cls.State.StateName = Convert.ToString(dr["StateName"].ToString());

                        cls.Place.PlaceName = dr["PlaceName"] == DBNull.Value ? "" : Convert.ToString(dr["PlaceName"]);

                        cls.BusinessNo = Convert.ToString(dr["BusinessNo"]);

                        cls.PIN = dr["PIN"] == DBNull.Value ? "" : Convert.ToString(dr["PIN"]);

                        cls.PriceMenuID = dr["PriceMenuID"] == DBNull.Value ? 0 : int.Parse(dr["PriceMenuID"].ToString());
                        cls.Longitude = Convert.ToDecimal(dr["Longitude"]);
                        cls.Latitude = Convert.ToDecimal(dr["Latitude"]);
                        cls.ItemID = Convert.ToString(dr["ItemID"]);

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



        public static Partner SelectByID(Partner cls)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spPartnerByID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@PartnerID", SqlDbType.Int);
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
                        cls = new Partner();


                        cls.ID = Convert.ToInt32(dr["PartnerID"]);
                        cls.PartnerName = Convert.ToString(dr["PartnerName"]);
                        cls.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                        cls.PartnerType.ID = Convert.ToInt32(dr["PartnerTypeID"]);
                        cls.Account.AccountCode = Convert.ToString(dr["Account"]);
                        cls.State.ID = dr["StateID"] == DBNull.Value ? -1 : int.Parse(dr["StateID"].ToString());
                        cls.RegionID = dr["RegionID"] == DBNull.Value ? -1 : int.Parse(dr["RegionID"].ToString());
                        cls.Place.ID = dr["PlaceID"] == DBNull.Value ? -1 : int.Parse(dr["PlaceID"].ToString());
                        cls.State.StateName = dr["StateName"] == DBNull.Value ? "" : dr["StateName"].ToString();
                        cls.Place.PlaceName = dr["PlaceName"] == DBNull.Value ? "" : dr["PlaceName"].ToString();
                        cls.Address = Convert.ToString(dr["Address"]);
                        cls.Tel1 = Convert.ToString(dr["Tel1"]);
                        cls.Tel2 = Convert.ToString(dr["Tel2"]);
                        cls.Email = Convert.ToString(dr["Email"]);
                        cls.WebSite = Convert.ToString(dr["WebSite"]);
                        cls.BusinessNo = Convert.ToString(dr["BusinessNo"]);
                        cls.BankAccount = Convert.ToString(dr["BankAccount"]);
                        cls.DiscountPercent = dr["DiscountPercent"] == DBNull.Value ? 0 : decimal.Parse(dr["DiscountPercent"].ToString());
                        cls.PIN = dr["PIN"] == DBNull.Value ? "" : Convert.ToString(dr["PIN"]);
                        cls.LUB = dr["LUB"] == DBNull.Value ? 0 : int.Parse(dr["LUB"].ToString());
                        cls.ItemID = dr["ItemID"] == DBNull.Value ? "" : dr["ItemID"].ToString();
                        cls.PriceMenuID = dr["PriceMenuID"] == DBNull.Value ? 0 : int.Parse(dr["PriceMenuID"].ToString());
                        cls.DueDays = dr["DueDays"] == DBNull.Value ? 0 : int.Parse(dr["DueDays"].ToString());
                        cls.DueValueMaximum = dr["DueValueMaximum"] == DBNull.Value ? 0 : decimal.Parse(dr["DueValueMaximum"].ToString());
                        cls.ContractNo = Convert.ToString(dr["ContractNo"]);
                        cls.Longitude = Convert.ToDecimal(dr["Longitude"]);
                        cls.Latitude = Convert.ToDecimal(dr["Latitude"]);
                        cls.HasVAT = Convert.ToBoolean(dr["HasVAT"]);
                        cls.OwnerName = Convert.ToString(dr["OwnerName"]);
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


        public static int SelectByEmail(string email)
        {
            int partnerID = 0;
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetPartnerByEmail", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@Email", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = email;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                object ob = cmd.ExecuteScalar();
                partnerID = ob == null ? 0 : Convert.ToInt32(ob.ToString());
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return partnerID;
        }
        public static int SelectByName(string PartnerName)
        {
            int partnerID = 0;
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetPartnerByPartnerName", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@PartnerName", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = PartnerName;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                object ob = cmd.ExecuteScalar();
                partnerID = ob == null ? 0 : Convert.ToInt32(ob.ToString());
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return partnerID;
        }

        public Partner SelectByItemID(string ItemID)
        {
            Partner cls = new Partner();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("sp_m_GetPartnerByItemID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@ItemID", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = ItemID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cls = new Partner();


                        cls.ID = Convert.ToInt32(dr["PartnerID"]);
                        cls.PartnerName = Convert.ToString(dr["PartnerName"]);
                        cls.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                        cls.PartnerType.ID = Convert.ToInt32(dr["PartnerTypeID"]);
                        cls.Account.AccountCode = Convert.ToString(dr["Account"]);
                        cls.State.ID = dr["StateID"] == DBNull.Value ? -1 : int.Parse(dr["StateID"].ToString());
                        cls.RegionID = dr["RegionID"] == DBNull.Value ? -1 : int.Parse(dr["RegionID"].ToString());
                        cls.Place.ID = dr["PlaceID"] == DBNull.Value ? -1 : int.Parse(dr["PlaceID"].ToString());
                        cls.State.StateName = dr["StateName"] == DBNull.Value ? "" : dr["StateName"].ToString();
                        cls.Place.PlaceName = dr["PlaceName"] == DBNull.Value ? "" : dr["PlaceName"].ToString();
                        cls.Address = Convert.ToString(dr["Address"]);
                        cls.Tel1 = Convert.ToString(dr["Tel1"]);
                        cls.Tel2 = Convert.ToString(dr["Tel2"]);
                        cls.Email = Convert.ToString(dr["Email"]);
                        cls.WebSite = Convert.ToString(dr["WebSite"]);
                        cls.BusinessNo = Convert.ToString(dr["BusinessNo"]);
                        cls.BankAccount = Convert.ToString(dr["BankAccount"]);
                        cls.DiscountPercent = dr["DiscountPercent"] == DBNull.Value ? 0 : decimal.Parse(dr["DiscountPercent"].ToString());
                        cls.PIN = dr["PIN"] == DBNull.Value ? "" : Convert.ToString(dr["PIN"]);
                        cls.LUB = dr["LUB"] == DBNull.Value ? 0 : int.Parse(dr["LUB"].ToString());
                        cls.ItemID = dr["ItemID"] == DBNull.Value ? "" : dr["ItemID"].ToString();
                        cls.PriceMenuID = dr["PriceMenuID"] == DBNull.Value ? 0 : int.Parse(dr["PriceMenuID"].ToString());
                        cls.DueDays = dr["DueDays"] == DBNull.Value ? 0 : int.Parse(dr["DueDays"].ToString());
                        cls.DueValueMaximum = dr["DueValueMaximum"] == DBNull.Value ? 0 : decimal.Parse(dr["DueValueMaximum"].ToString());
                        cls.ContractNo = Convert.ToString(dr["ContractNo"]);
                        cls.Longitude = Convert.ToDecimal(dr["Longitude"]);
                        cls.Latitude = Convert.ToDecimal(dr["Latitude"]);
                        cls.HasVAT = Convert.ToBoolean(dr["HasVAT"]);
                        cls.OwnerName = Convert.ToString(dr["OwnerName"]);
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

        #region GetPartnerByTypeID

        //public List<PartnerLookup> GetPartnerByTypeID(string TypeID)
        //{
        //    List<PartnerLookup> clsList = new List<PartnerLookup>();
        //    SqlConnection cnn = _dbAccess.GetConnection();
        //    SqlCommand cmd = new SqlCommand("spPartnerByType", cnn);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    SqlParameter param;

        //    param = new SqlParameter("@Type", System.Data.SqlDbType.VarChar);
        //    param.Direction = ParameterDirection.Input;
        //    param.Value = TypeID;
        //    cmd.Parameters.Add(param);

        //    try
        //    {
        //        cnn.Open();

        //        SqlDataReader dr = cmd.ExecuteReader();
        //        if (dr.HasRows)
        //        {
        //            while (dr.Read())
        //            {
        //                PartnerLookup cls = new PartnerLookup();
        //                cls.ID = Convert.ToInt32(dr["PartnerID"]);
        //                cls.PartnerName = Convert.ToString(dr["PartnerName"]);
        //                cls.PartnerTypeID = Convert.ToInt32(dr["PartnerTypeID"]);
        //                cls.Account = Convert.ToString(dr["Account"]);
        //                cls.PIN = dr["PIN"] == DBNull.Value ? "" : Convert.ToString(dr["PIN"]);
        //                clsList.Add(cls);

        //            }
        //        }
        //        cnn.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        string exp = ex.Message;
        //        cnn.Close();
        //    }
        //    return clsList;
        //}

        #endregion

        #region GetPartnerTableByTypeID

        public DataTable GetPartnerTableByTypeID(int TypeID)
        {
            DataTable dtList = new DataTable();
            dtList.TableName = "PartnerList";
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spPartnerByType", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@Type", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TypeID;
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

        #region AuthenticatePartner

        public Partner AuthenticatePartner(string PIN)
        {
            Partner cls = null;
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetPartnerByPIN", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            try
            {
                param = new SqlParameter("@PIN", SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = PIN;
                cmd.Parameters.Add(param);

                cnn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cls = new Partner();

                        cls.ID = Convert.ToInt32(dr["PartnerID"]);
                        cls.PartnerName = Convert.ToString(dr["PartnerName"]);
                        cls.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                        cls.PartnerType.ID = Convert.ToInt32(dr["PartnerTypeID"]);
                        cls.Account.AccountCode = Convert.ToString(dr["Account"]);
                        cls.State.ID = dr["StateID"] == DBNull.Value ? -1 : int.Parse(dr["StateID"].ToString());
                        cls.Place.ID = dr["PlaceID"] == DBNull.Value ? -1 : int.Parse(dr["PlaceID"].ToString());
                        cls.Tel1 = Convert.ToString(dr["Tel1"]);
                        cls.Tel2 = Convert.ToString(dr["Tel2"]);
                        cls.Email = Convert.ToString(dr["Email"]);
                        cls.WebSite = Convert.ToString(dr["WebSite"]);
                        cls.BusinessNo = Convert.ToString(dr["BusinessNo"]);
                        cls.BankAccount = Convert.ToString(dr["BankAccount"]);
                        cls.DiscountPercent = dr["DiscountPercent"] == DBNull.Value ? 0 : decimal.Parse(dr["DiscountPercent"].ToString());
                        cls.PIN = dr["PIN"] == DBNull.Value ? "" : Convert.ToString(dr["PIN"]);
                        cls.PriceMenuID = dr["PriceMenuID"] == DBNull.Value ? 0 : int.Parse(dr["PriceMenuID"].ToString());

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

        #region CheckForPartnerPIN

        public bool CheckForPartnerPIN(string PIN)
        {
            bool rez = false;

            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spCheckForPartnerPIN", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@PIN", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = PIN;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        rez = true;
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
            return rez;
        }

        #endregion

        #region CheckForPartnerItemID

        public bool CheckForPartnerItemID(string ItemID)
        {
            bool rez = false;

            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spCheckForPartnerItemID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@ItemID", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = ItemID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        rez = true;
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
            return rez;
        }

        #endregion

        #region GetCustomerWithItemID

        //public List<PartnerLookup> GetCustomerWithItemID()
        //{
        //    List<PartnerLookup> clsList = new List<PartnerLookup>();
        //    SqlConnection cnn = _dbAccess.GetConnection();
        //    SqlCommand cmd = new SqlCommand("spPartnerWithItemIDList", cnn);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    try
        //    {
        //        cnn.Open();

        //        SqlDataReader dr = cmd.ExecuteReader();
        //        if (dr.HasRows)
        //        {
        //            while (dr.Read())
        //            {
        //                PartnerLookup cls = new PartnerLookup();
        //                cls.ID = Convert.ToInt32(dr["PartnerID"]);
        //                cls.PartnerName = Convert.ToString(dr["PartnerName"]);
        //                cls.PartnerTypeID = Convert.ToInt32(dr["PartnerTypeID"]);
        //                cls.Account = Convert.ToString(dr["Account"]);
        //                cls.PIN = dr["PIN"] == DBNull.Value ? "" : Convert.ToString(dr["PIN"]);
        //                cls.ItemID = Convert.ToString(dr["ItemID"]);
        //                clsList.Add(cls);

        //            }
        //        }
        //        cnn.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        string exp = ex.Message;
        //        cnn.Close();
        //    }
        //    return clsList;
        //}

        #endregion

        #region M_GETPartners

        public DataTable M_GETPartners(int DepartmetnID)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("sp_m_GetPartners", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = GlobalRepository.GetSqlParameterInput("@prmDepartmentID", DepartmetnID, SqlDbType.Int);
            cmd.Parameters.Add(param);


            dtList = GlobalRepository.ListTables(cmd);
            dtList.TableName = "Partners";
            return dtList;
        }

        //public List<XMLPartners> M_GETPartnersList(int DepartmetnID)
        //{
        //    List<XMLPartners> clsList = new List<XMLPartners>();

        //    SqlConnection cnn = _dbAccess.GetConnection();
        //    SqlCommand cmd = new SqlCommand("sp_m_GetPartners", cnn);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    SqlParameter param = _dbAccess.GetSqlParameterInput("@prmDepartmentID", DepartmetnID, SqlDbType.Int);
        //    cmd.Parameters.Add(param);

        //    try
        //    {
        //        cnn.Open();

        //        SqlDataReader dr = cmd.ExecuteReader();
        //        if (dr.HasRows)
        //        {
        //            while (dr.Read())
        //            {
        //                XMLPartners cls = new XMLPartners();
        //                cls.PartnerID = Convert.ToInt32(dr["PartnerID"]);
        //                cls.PartnerName = Convert.ToString(dr["PartnerName"]);
        //                cls.PartnerRegion = Convert.ToString(dr["PartnerRegion"]);
        //                cls.DueDays = Convert.ToInt32(dr["DueDays"]);
        //                cls.DueValue = Convert.ToDecimal(dr["DueValue"]);
        //                cls.DueValueMaximum = Convert.ToDecimal(dr["DueValueMaximum"]);
        //                cls.PartnerBarcode = Convert.ToString(dr["PartnerBarcode"]);
        //                cls.Longitude = Convert.ToDecimal(dr["Longitude"]);
        //                cls.Latitude = Convert.ToDecimal(dr["Latitude"]);
        //                cls.FiscalNo = Convert.ToString(dr["FiscalNo"]);
        //                cls.BusinessNo = Convert.ToString(dr["BusinessNo"]);
        //                cls.Address = Convert.ToString(dr["Address"]);
        //                cls.DiscountPercent = Convert.ToDecimal(dr["DiscountPercent"]);
        //                cls.DiscountLevel = Convert.ToInt32(dr["DiscountLevel"]);
        //                cls.PlaceName = Convert.ToString(dr["PlaceName"]);
        //                cls.ItemID = Convert.ToString(dr["ItemID"]);
        //                cls.MinimumValueForDiscount = Convert.ToDecimal(dr["MinimumValueForDiscount"]);
        //                cls.DiscountPercent2 = Convert.ToDecimal(dr["DiscountPercent2"]);
        //                cls.RouteOrderID = Convert.ToInt32(dr["RouteOrderID"]);
        //                cls.Day = Convert.ToInt32(dr["Day"]);
        //                cls.MerchDescription = Convert.ToString(dr["MerchDescription"]);
        //                cls.MerchSecondaryPlacement = Convert.ToString(dr["MerchSecondaryPlacement"]);
        //                cls.DefaultPartner = Convert.ToBoolean(dr["DefaultPartner"]);
        //                cls.VatNo = Convert.ToString(dr["VatNo"]);
        //                cls.OwnerName = Convert.ToString(dr["OwnerName"]);
        //                cls.PricemenuID = Convert.ToInt32(dr["PricemenuID"]);
        //                cls.PartnerTypeID = Convert.ToInt32(dr["PartnerTypeID"]);

        //                clsList.Add(cls);
        //            }
        //        }
        //        cnn.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        string exp = ex.Message;
        //        cnn.Close();
        //    }
        //    return clsList;


        //}
        //public List<XMLPartners> M_GETPartnersListByDepartmentID(int DepartmetnID)
        //{
        //    List<XMLPartners> clsList = new List<XMLPartners>();

        //    SqlConnection cnn = _dbAccess.GetConnection();
        //    SqlCommand cmd = new SqlCommand("sp_m_GetPartnersByDepartment", cnn);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    SqlParameter param = _dbAccess.GetSqlParameterInput("@prmDepartmentID", DepartmetnID, SqlDbType.Int);
        //    cmd.Parameters.Add(param);

        //    try
        //    {
        //        cnn.Open();

        //        SqlDataReader dr = cmd.ExecuteReader();
        //        if (dr.HasRows)
        //        {
        //            while (dr.Read())
        //            {
        //                XMLPartners cls = new XMLPartners();
        //                cls.PartnerID = Convert.ToInt32(dr["PartnerID"]);
        //                cls.PartnerName = Convert.ToString(dr["PartnerName"]);
        //                cls.PartnerRegion = Convert.ToString(dr["PartnerRegion"]);
        //                cls.DueDays = Convert.ToInt32(dr["DueDays"]);
        //                cls.DueValue = Convert.ToDecimal(dr["DueValue"]);
        //                cls.DueValueMaximum = Convert.ToDecimal(dr["DueValueMaximum"]);
        //                cls.PartnerBarcode = Convert.ToString(dr["PartnerBarcode"]);
        //                cls.Longitude = Convert.ToDecimal(dr["Longitude"]);
        //                cls.Latitude = Convert.ToDecimal(dr["Latitude"]);
        //                cls.FiscalNo = Convert.ToString(dr["FiscalNo"]);
        //                cls.BusinessNo = Convert.ToString(dr["BusinessNo"]);
        //                cls.Address = Convert.ToString(dr["Address"]);
        //                cls.DiscountPercent = Convert.ToDecimal(dr["DiscountPercent"]);
        //                cls.DiscountLevel = Convert.ToInt32(dr["DiscountLevel"]);
        //                cls.PlaceName = Convert.ToString(dr["PlaceName"]);
        //                cls.ItemID = Convert.ToString(dr["ItemID"]);
        //                cls.MinimumValueForDiscount = Convert.ToDecimal(dr["MinimumValueForDiscount"]);
        //                cls.DiscountPercent2 = Convert.ToDecimal(dr["DiscountPercent2"]);
        //                cls.RouteOrderID = Convert.ToInt32(dr["RouteOrderID"]);
        //                cls.Day = Convert.ToInt32(dr["Day"]);
        //                cls.MerchDescription = Convert.ToString(dr["MerchDescription"]);
        //                cls.MerchSecondaryPlacement = Convert.ToString(dr["MerchSecondaryPlacement"]);
        //                cls.DefaultPartner = Convert.ToBoolean(dr["DefaultPartner"]);
        //                cls.VatNo = Convert.ToString(dr["VatNo"]);
        //                cls.OwnerName = Convert.ToString(dr["OwnerName"]);
        //                cls.PricemenuID = Convert.ToInt32(dr["PricemenuID"]);
        //                cls.PartnerTypeID = Convert.ToInt32(dr["PartnerTypeID"]);

        //                clsList.Add(cls);
        //            }
        //        }
        //        cnn.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        string exp = ex.Message;
        //        cnn.Close();
        //    }
        //    return clsList;


        //}
        //p.PartnerID,
        //p.PartnerName,		
        //ISNULL(p.ItemID, '') as PartnerBarcode,
        //ISNULL(p.RegionID, 0) as PartnerRegion,
        //ISNULL(p.DueDays , 0) as DueDays,
        //isnull(DueValue,0) as DueValue,
        //p.RegionID,
        //ISNULL(p.DueValueMaximum  , 0) as DueValueMaximum

        //public List<XMLPartners_2> M_GETPartnersList_2(int DepartmetnID)
        //{
        //    List<XMLPartners_2> clsList = new List<XMLPartners_2>();

        //    SqlConnection cnn = _dbAccess.GetConnection();
        //    SqlCommand cmd = new SqlCommand("sp_m_GetPartners", cnn);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    SqlParameter param = _dbAccess.GetSqlParameterInput("@prmDepartmentID", DepartmetnID, SqlDbType.Int);
        //    cmd.Parameters.Add(param);

        //    try
        //    {
        //        cnn.Open();

        //        SqlDataReader dr = cmd.ExecuteReader();
        //        if (dr.HasRows)
        //        {
        //            while (dr.Read())
        //            {
        //                XMLPartners_2 cls = new XMLPartners_2();
        //                cls.PartnerID = Convert.ToInt32(dr["PartnerID"]);
        //                cls.PartnerName = Convert.ToString(dr["PartnerName"]);
        //                clsList.Add(cls);
        //            }
        //        }
        //        cnn.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        string exp = ex.Message;
        //        cnn.Close();
        //    }
        //    return clsList;


        //}

        public DataTable M_GetPartnerCoords(int PartnerID)
        {
            DataTable dt = new DataTable();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("sp_m_GetPartnerCoords", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = GlobalRepository.GetSqlParameterInput("@PartnerID", PartnerID, SqlDbType.Int);
            cmd.Parameters.Add(param);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            try
            {
                cnn.Open();
                adapter.Fill(dt);
                cnn.Close();
            }
            catch { cnn.Close(); }
            return dt;
        }
        public void M_UpdatePartnerCoords(int PartnerID, decimal Lat, decimal Lon)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("sp_m_UpdatePartnersCoords", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = GlobalRepository.GetSqlParameterInput("@PartnerID", PartnerID, SqlDbType.Int);
            cmd.Parameters.Add(param);

            param = GlobalRepository.GetSqlParameterInput("@Lat", Lat, SqlDbType.Decimal);
            cmd.Parameters.Add(param);

            param = GlobalRepository.GetSqlParameterInput("@Lon", Lon, SqlDbType.Decimal);
            cmd.Parameters.Add(param);
            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch { cnn.Close(); }
        }

        #endregion



        public DataTable GenerateItemID_ForPartner(int PartnerID)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spGenerateItemID_ForPartner", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dadap = new SqlDataAdapter(cmd);


            SqlParameter param;

            param = new SqlParameter("@PartnerID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = PartnerID;
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

        public string GetPartnerBalance_ALL(int PartnerID, string ToDate, int TransactionID, decimal TranValue)
        {
            string message = "OK";
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetPartnerBalance_ALL", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dadap = new SqlDataAdapter(cmd);

            SqlParameter param;

            param = new SqlParameter("@PartnerID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = PartnerID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@TransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@TranValue", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = TranValue;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                message = cmd.ExecuteScalar().ToString();
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }

            return message;
        }
        //public List<XMLPartners> M_GETPartnersList_NAV(int DepartmetnID)
        //{
        //    List<XMLPartners> clsList = new List<XMLPartners>();

        //    SqlConnection cnn = _dbAccess.GetConnection();
        //    SqlCommand cmd = new SqlCommand("sp_m_GetPartners_NAV", cnn);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    SqlParameter param = _dbAccess.GetSqlParameterInput("@prmDepartmentID", DepartmetnID, SqlDbType.Int);
        //    cmd.Parameters.Add(param);

        //    try
        //    {
        //        cnn.Open();

        //        SqlDataReader dr = cmd.ExecuteReader();
        //        if (dr.HasRows)
        //        {
        //            while (dr.Read())
        //            {
        //                XMLPartners cls = new XMLPartners();
        //                cls.PartnerID = Convert.ToInt32(dr["PartnerID"]);
        //                cls.PartnerName = Convert.ToString(dr["PartnerName"]);
        //                cls.PartnerRegion = Convert.ToString(dr["PartnerRegion"]);
        //                cls.DueDays = Convert.ToInt32(dr["DueDays"]);
        //                cls.DueValue = Convert.ToDecimal(dr["DueValue"]);
        //                cls.DueValueMaximum = Convert.ToDecimal(dr["DueValueMaximum"]);
        //                cls.PartnerBarcode = Convert.ToString(dr["PartnerBarcode"]);
        //                cls.Longitude = Convert.ToDecimal(dr["Longitude"]);
        //                cls.Latitude = Convert.ToDecimal(dr["Latitude"]);
        //                cls.FiscalNo = Convert.ToString(dr["FiscalNo"]);
        //                cls.BusinessNo = Convert.ToString(dr["BusinessNo"]);
        //                cls.Address = Convert.ToString(dr["Address"]);
        //                cls.DiscountPercent = Convert.ToDecimal(dr["DiscountPercent"]);
        //                cls.DiscountLevel = Convert.ToInt32(dr["DiscountLevel"]);
        //                cls.PlaceName = Convert.ToString(dr["PlaceName"]);
        //                cls.ItemID = Convert.ToString(dr["ItemID"]);
        //                cls.MinimumValueForDiscount = Convert.ToDecimal(dr["MinimumValueForDiscount"]);
        //                cls.DiscountPercent2 = Convert.ToDecimal(dr["DiscountPercent2"]);
        //                cls.RouteOrderID = Convert.ToInt32(dr["RouteOrderID"]);
        //                cls.Day = Convert.ToInt32(dr["Day"]);
        //                cls.MerchDescription = Convert.ToString(dr["MerchDescription"]);
        //                cls.MerchSecondaryPlacement = Convert.ToString(dr["MerchSecondaryPlacement"]);
        //                cls.DefaultPartner = Convert.ToBoolean(dr["DefaultPartner"]);
        //                cls.VatNo = Convert.ToString(dr["VatNo"]);
        //                cls.OwnerName = Convert.ToString(dr["OwnerName"]);
        //                cls.PricemenuID = Convert.ToInt32(dr["PricemenuID"]);
        //                cls.PartnerTypeID = Convert.ToInt32(dr["PartnerTypeID"]);
        //                clsList.Add(cls);
        //            }
        //        }
        //        cnn.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        string exp = ex.Message;
        //        cnn.Close();
        //    }
        //    return clsList;


        //}
        public string GetPartnerBalance_ALL_NAV(int PartnerID, string ToDate, int TransactionID, decimal TranValue)
        {
            string message = "OK";
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetPartnerBalance_ALL_NAV", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dadap = new SqlDataAdapter(cmd);

            SqlParameter param;

            param = new SqlParameter("@PartnerID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = PartnerID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@TransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@TranValue", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = TranValue;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                message = cmd.ExecuteScalar().ToString();
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }

            return message;
        }


        public List<PartnerModel> GetPartners(int partnerTypeID = 2, string partnerName = "%", string partnerGroup = "%", string partnerCategory = "%", string placeName = "%", string stateName = "%")
        {
            var partners = new List<PartnerModel>();
            // Prefer tenant-aware DBAccess; fallback to GlobalRepository for legacy paths
            using var cnn = _dbAccess != null ? _dbAccess.GetConnection() : GlobalRepository.GetConnection();
            using var cmd = new SqlCommand("spGetPartners_API", cnn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.Add(new SqlParameter("@PartnerTypeID", SqlDbType.Int) { Value = partnerTypeID });
            cmd.Parameters.Add(new SqlParameter("@PartnerName", SqlDbType.NVarChar, 200) { Value = partnerName });
            cmd.Parameters.Add(new SqlParameter("@PartnerGroup", SqlDbType.NVarChar, 200) { Value = partnerGroup });
            cmd.Parameters.Add(new SqlParameter("@PartnerCategory", SqlDbType.NVarChar, 200) { Value = partnerCategory });
            cmd.Parameters.Add(new SqlParameter("@PlaceName", SqlDbType.NVarChar, 200) { Value = placeName });
            cmd.Parameters.Add(new SqlParameter("@StateName", SqlDbType.NVarChar, 200) { Value = stateName });
            try
            {
                cnn.Open();
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    partners.Add(new PartnerModel
                    {
                        PartnerID = reader["PartnerID"] == DBNull.Value ? 0 : (int)reader["PartnerID"],
                        PartnerName = reader["PartnerName"]?.ToString(),
                        FiscalNo = reader["FiscalNo"]?.ToString(),
                        BusinessNo = reader["BusinessNo"]?.ToString(),
                        Address = reader["Address"]?.ToString(),
                        Email = reader["Email"]?.ToString(),
                        PlaceName = reader["PlaceName"]?.ToString(),
                        StateName = reader["StateName"]?.ToString(),
                        Group = reader["Group"]?.ToString(),
                        Category = reader["Category"]?.ToString(),
                        DueDays = reader["DueDays"] == DBNull.Value ? 0 : Convert.ToInt32(reader["DueDays"]),
                        DueValueMaximum = reader["DueValueMaximum"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["DueValueMaximum"]),
                        DueValue = reader["DueValue"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["DueValue"])
                    });
                }
            }
            catch (Exception ex)
            {
                // optionally log ex.Message; returning empty list keeps API stable
            }
            return partners;
        }

        public (int inserted, int failed, string error) ImportPartners(List<PartnerBatchItem> partners)
        {
            if (partners == null || partners.Count == 0) return (0, 0, null);
            var dt = new DataTable();
            dt.Columns.Add("PartnerName", typeof(string));  // 1
            dt.Columns.Add("Type", typeof(string));  // 2
            dt.Columns.Add("FiscalNo", typeof(string));  // 3
            dt.Columns.Add("BusinessNo", typeof(string));  // 4
            dt.Columns.Add("VatNo", typeof(string));  // 5
            dt.Columns.Add("Adress", typeof(string));  // 6
            dt.Columns.Add("City", typeof(string));  // 7
            dt.Columns.Add("stateid", typeof(string));  // 8
            dt.Columns.Add("itemid", typeof(string));  // 9
            dt.Columns.Add("discountpercent", typeof(decimal)); // 10
            dt.Columns.Add("PartnerGroup", typeof(string));  // 11
            dt.Columns.Add("Region", typeof(string));  // 12
            dt.Columns.Add("PartnerCategory", typeof(string));  // 13
            dt.Columns.Add("Tel1", typeof(string));  // 14
            dt.Columns.Add("Tel2", typeof(string));  // 15
            dt.Columns.Add("ContactPerson", typeof(string));  // 16
            dt.Columns.Add("Account", typeof(string));  // 17
            dt.Columns.Add("Email", typeof(string));  // 18

            foreach (var p in partners)
            {
                dt.Rows.Add(
     p.PartnerName ?? string.Empty,   // 1
     p.Type ?? string.Empty,          // 2
     p.FiscalNo ?? string.Empty,      // 3
     p.BussinesNo ?? string.Empty,    // 4
     p.VatNo ?? string.Empty,         // 5
     p.Adress ?? string.Empty,        // 6
     p.City ?? string.Empty,          // 7
     p.StateID ?? string.Empty,       // 8
     p.ItemID ?? string.Empty,        // 9
     p.DiscountPercent,               // 10
     p.PartnerGroup ?? string.Empty,  // 11
     p.Region ?? string.Empty,        // 12
     p.PartnerCategory ?? string.Empty,// 13
     p.Tel1 ?? string.Empty,          // 14
     p.Tel2 ?? string.Empty,          // 15
     p.ContactPerson ?? string.Empty, // 16
     p.Account ?? string.Empty,       // 17
     p.Email ?? string.Empty          // 18
 );
            }
            int inserted = 0; string error = null;
            try
            {
                using var cnn = _dbAccess.GetConnection();
                using var cmd = new SqlCommand("spImportPartners", cnn) { CommandType = CommandType.StoredProcedure };
                var tvp = new SqlParameter("@ImportPartners", SqlDbType.Structured)
                {
                    TypeName = "dbo.ImportPartners",
                    Value = dt
                };
                cmd.Parameters.Add(tvp);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
                inserted = partners.Count;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return (inserted, inserted == partners.Count ? 0 : partners.Count - inserted, error);
        }

        public int PartnerExistsAdvanced(
                int? partnerId = null,
                string partnerName = null,
                string email = null,
                string businessNo = null,
                string fiscalNo = null)
        {
            using var cnn = _dbAccess != null ? _dbAccess.GetConnection() : GlobalRepository.GetConnection();
            using var cmd = new SqlCommand("dbo.spPartnersAdvancedExists_API", cnn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.Add(new SqlParameter("@PartnerID", SqlDbType.Int) { Value = (object?)partnerId ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@PartnerName", SqlDbType.NVarChar, 200) { Value = (object?)partnerName ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 200) { Value = (object?)email ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@BusinessNo", SqlDbType.NVarChar, 200) { Value = (object?)businessNo ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@FiscalNo", SqlDbType.NVarChar, 200) { Value = (object?)fiscalNo ?? DBNull.Value });

            try
            {
                cnn.Open();
                var result = cmd.ExecuteScalar();
                return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
            }
            catch
            {
                // TODO: log
                return 0;
            }
            finally
            {
                if (cnn.State != ConnectionState.Closed) cnn.Close();
            }
        }

        public async Task<int> PartnerExistsAdvancedAsync(
            int? partnerId = null,
            string partnerName = null,
            string email = null,
            string businessNo = null,
            string fiscalNo = null,
            CancellationToken cancellationToken = default)
        {
            await using var cnn = _dbAccess != null ? _dbAccess.GetConnection() : GlobalRepository.GetConnection();
            await using var cmd = new SqlCommand("dbo.spPartnersAdvancedExists_API", cnn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.Add(new SqlParameter("@PartnerID", SqlDbType.Int) { Value = (object?)partnerId ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@PartnerName", SqlDbType.NVarChar, 200) { Value = (object?)partnerName ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 200) { Value = (object?)email ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@BusinessNo", SqlDbType.NVarChar, 200) { Value = (object?)businessNo ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@FiscalNo", SqlDbType.NVarChar, 200) { Value = (object?)fiscalNo ?? DBNull.Value });

            try
            {
                await cnn.OpenAsync(cancellationToken);
                var result = await cmd.ExecuteScalarAsync(cancellationToken);
                return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
            }
            catch
            {
                // TODO: log
                return 0;
            }
            finally
            {
                if (cnn.State != ConnectionState.Closed) cnn.Close();
            }
        }







        //LB
         public DataTable CheckFiscalNo(int PartnerID, string FiscalNo)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spCheckFiscalNo", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dadap = new SqlDataAdapter(cmd);

            SqlParameter param;

            param = new SqlParameter("@PartnerID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = PartnerID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@FiscalNo", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = FiscalNo;
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
     

        public DataTable SelectAllCustomers()
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHCustomerList", cnn);
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

        #region GetPartnerByTypeID

        public List<PartnerLookup> GetPartnerByTypeID(string TypeID)
        {
            List<PartnerLookup> clsList = new List<PartnerLookup>();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spPartnerByType", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@Type", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = TypeID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        PartnerLookup cls = new PartnerLookup();
                        cls.ID = Convert.ToInt32(dr["PartnerID"]);
                        cls.PartnerName = Convert.ToString(dr["PartnerName"]);
                        cls.PartnerTypeID = Convert.ToInt32(dr["PartnerTypeID"]);
                        cls.Account = Convert.ToString(dr["Account"]);
                        cls.PIN = dr["PIN"] == DBNull.Value ? "" : Convert.ToString(dr["PIN"]);
                        cls.DiscountPercent = dr["DiscountPercent"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["DiscountPercent"]);
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

    
     
    

   
        #region GetCustomerWithItemID

        public List<PartnerLookup> GetCustomerWithItemID()
        {
            List<PartnerLookup> clsList = new List<PartnerLookup>();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spPartnerWithItemIDList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        PartnerLookup cls = new PartnerLookup();
                        cls.ID = Convert.ToInt32(dr["PartnerID"]);
                        cls.PartnerName = Convert.ToString(dr["PartnerName"]);
                        cls.PartnerTypeID = Convert.ToInt32(dr["PartnerTypeID"]);
                        cls.Account = Convert.ToString(dr["Account"]);
                        cls.PIN = dr["PIN"] == DBNull.Value ? "" : Convert.ToString(dr["PIN"]);
                        cls.ItemID = Convert.ToString(dr["ItemID"]);
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

      
        // public List<PartnerCategories> SelectAllPartnerCategories()
        // {
        //     List<PartnerCategories> clsList = new List<PartnerCategories>();
        //     SqlConnection cnn = _dbAccess.GetConnection();
        //     SqlCommand cmd = new SqlCommand("spPartnerCategoriesList", cnn);
        //     cmd.CommandType = CommandType.StoredProcedure;
        //     try
        //     {
        //         cnn.Open();

        //         SqlDataReader dr = cmd.ExecuteReader();
        //         if (dr.HasRows)
        //         {
        //             while (dr.Read())
        //             {
        //                 PartnerCategories cls = new PartnerCategories();
        //                 cls.ID = Convert.ToInt32(dr["ID"]);
        //                 cls.PartnerCategory = Convert.ToString(dr["PartnerCategory"]);
                        

        //                 clsList.Add(cls);

        //             }
        //         }
        //         cnn.Close();
        //     }
        //     catch (Exception ex)
        //     {
        //         string exp = ex.Message;
        //         cnn.Close();
        //     }
        //     return clsList;
        // }

        #region GetPartnerItemID

        public string GetPartnerItemID()
        {
            string ItemID = "";
            SqlConnection cnn = new SqlConnection();
            cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetPartnerItemID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            try
            {
                cnn.Open();
                object ob = cmd.ExecuteScalar();
                ItemID = ob == null ? "" : ob.ToString();
                cnn.Close();
            }
            catch (Exception ex)
            {
                ItemID = "";
                cnn.Close();
            }

            return ItemID;
        }

        #endregion

        public DataTable GetCustomersBySearch(int TypeID, int departmentid, string PartnerName)
        {
            DataTable dtList = new DataTable();
            dtList.TableName = "PartnerList";
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spCustomerByType_10", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@Type", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TypeID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DepartmentID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = departmentid;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@PartnerName", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = PartnerName;
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


        public List<PartnerLookup> GetPartnersBySearch_ID(string TypeID, int departmentid, string PartnerID)
        {
            List<PartnerLookup> clsList = new List<PartnerLookup>();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spPartnerByType", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@Type", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = TypeID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DEpartmentID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = departmentid;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PartnerID", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = PartnerID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        PartnerLookup cls = new PartnerLookup();
                        try
                        {


                            cls.ID = Convert.ToInt32(dr["PartnerID"]);
                            cls.PartnerName = Convert.ToString(dr["PartnerName"]);
                            cls.PartnerTypeID = Convert.ToInt32(dr["PartnerTypeID"]);
                            cls.Account = Convert.ToString(dr["Account"]);
                            cls.PIN = dr["PIN"] == DBNull.Value ? "" : Convert.ToString(dr["PIN"]);
                            //cls.PartnerTypeName = Convert.ToString(dr["PartnerTypeName"]);
                            //cls.PlaceName = Convert.ToString(dr["PlaceName"]);
                            //cls.PartnerGroupName = Convert.ToString(dr["PartnerGroupName"]);
                            //cls.NIF = dr["BusinessNo"] == DBNull.Value ? "" : Convert.ToString(dr["BusinessNo"]);
                            //cls.Address = Convert.ToString(dr["Address"]);
                            //try { cls.HasVAT = Convert.ToBoolean(dr["HasVAT"]); }
                            //catch { }
                            //cls.Export = dr["Export"] == DBNull.Value ? false : Convert.ToBoolean(dr["Export"]);
                            //cls.HasVAT = dr["HasVAT"] == DBNull.Value ? true : Convert.ToBoolean(dr["HasVAT"]);

                        }
                        catch
                        {
                        }
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




    }
    





    
}
