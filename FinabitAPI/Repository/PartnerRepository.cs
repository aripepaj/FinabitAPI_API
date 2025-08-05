using Finabit_API.Models;
using System.Data;
using Microsoft.Data.SqlClient;

namespace AutoBit_WebInvoices.Models
{
    public class PartnerRepository
    {
        public void Insert(Partner cls)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spPartnerInsert", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;


            param = new SqlParameter("@PartnerName", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PartnerName;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ContactPerson", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ContactPerson;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@PartnerTypeID", System.Data.SqlDbType.SmallInt);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PartnerType.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Account", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Account.AccountCode;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@StateID", System.Data.SqlDbType.SmallInt);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.State.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RegionID", System.Data.SqlDbType.SmallInt);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RegionID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PlaceID", System.Data.SqlDbType.SmallInt);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Place.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Address", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Address;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Tel1", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Tel1;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Tel2", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Tel2;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Email", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Email;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@WebSite", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.WebSite;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@BusinessNo", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.BusinessNo;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RealBusinessNo", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RealBusinessNo;
            cmd.Parameters.Add(param);



            param = new SqlParameter("@BankAccount", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.BankAccount;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@UserID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = GlobalAppData.UserID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DiscountPercent", System.Data.SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DiscountPercent;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PIN", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PIN;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ItemID", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ItemID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PriceMenuID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PriceMenuID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DueDays", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DueDays;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DueValueMaximum", System.Data.SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DueValueMaximum;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ContractNo", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ContractNo;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@VATNO", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.VATNO;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Longitude", System.Data.SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Longitude;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Latitude", System.Data.SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Latitude;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PartnerPicture", System.Data.SqlDbType.Image);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PartnerPicture;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmErrorID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PrmNewID", System.Data.SqlDbType.Int);
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

            param = new SqlParameter("@PartnerID", System.Data.SqlDbType.SmallInt);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PartnerName", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PartnerName;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ContactPerson", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ContactPerson;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PartnerTypeID", System.Data.SqlDbType.SmallInt);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PartnerType.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Account", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Account.AccountCode;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@StateID", System.Data.SqlDbType.SmallInt);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.State.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RegionID", System.Data.SqlDbType.SmallInt);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RegionID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PlaceID", System.Data.SqlDbType.SmallInt);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Place.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Address", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Address;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Tel1", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Tel1;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Tel2", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Tel2;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Email", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Email;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@WebSite", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.WebSite;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@BusinessNo", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.BusinessNo;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@BankAccount", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.BankAccount;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@UserID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.LUB;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DiscountPercent", System.Data.SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DiscountPercent;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PIN", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PIN;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ItemID", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ItemID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PriceMenuID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PriceMenuID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DueDays", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DueDays;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DueValueMaximum", System.Data.SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DueValueMaximum;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ContractNo", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ContractNo;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@VATNO", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.VATNO;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Longitude", System.Data.SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Longitude;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Latitude", System.Data.SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Latitude;
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

        public void Delete(Partner cls)
        {
            SqlConnection cnn = new SqlConnection();
            cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spPartnerDelete", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@PartnerID", System.Data.SqlDbType.Int);
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

            param = new SqlParameter("@Type", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TypeID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Filter", System.Data.SqlDbType.VarChar);
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

            param = new SqlParameter("@PIN", System.Data.SqlDbType.VarChar);
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

            param = new SqlParameter("@PartnerID", System.Data.SqlDbType.Int);
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

            param = new SqlParameter("@Email", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = email;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                object ob = cmd.ExecuteScalar();
                partnerID = ob == null ? 0 :Convert.ToInt32(ob.ToString());
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

            param = new SqlParameter("@PartnerName", System.Data.SqlDbType.NVarChar);
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

            param = new SqlParameter("@ItemID", System.Data.SqlDbType.VarChar);
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
        //    SqlConnection cnn = DALGlobal.GetConnection();
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

            param = new SqlParameter("@Type", System.Data.SqlDbType.Int);
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
                param = new SqlParameter("@PIN", System.Data.SqlDbType.VarChar);
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
                        cls.LUB = dr["LUB"] == DBNull.Value ? 0 : int.Parse(dr["LUB"].ToString());
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

            param = new SqlParameter("@PIN", System.Data.SqlDbType.VarChar);
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

            param = new SqlParameter("@ItemID", System.Data.SqlDbType.VarChar);
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
        //    SqlConnection cnn = DALGlobal.GetConnection();
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

        //    SqlConnection cnn = DALGlobal.GetConnection();
        //    SqlCommand cmd = new SqlCommand("sp_m_GetPartners", cnn);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    SqlParameter param = DALGlobal.GetSqlParameterInput("@prmDepartmentID", DepartmetnID, SqlDbType.Int);
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

        //    SqlConnection cnn = DALGlobal.GetConnection();
        //    SqlCommand cmd = new SqlCommand("sp_m_GetPartnersByDepartment", cnn);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    SqlParameter param = DALGlobal.GetSqlParameterInput("@prmDepartmentID", DepartmetnID, SqlDbType.Int);
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

        //    SqlConnection cnn = DALGlobal.GetConnection();
        //    SqlCommand cmd = new SqlCommand("sp_m_GetPartners", cnn);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    SqlParameter param = DALGlobal.GetSqlParameterInput("@prmDepartmentID", DepartmetnID, SqlDbType.Int);
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

            param = new SqlParameter("@PartnerID", System.Data.SqlDbType.Int);
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

            param = new SqlParameter("@PartnerID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = PartnerID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@TransactionID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@TranValue", System.Data.SqlDbType.Decimal);
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

        //    SqlConnection cnn = DALGlobal.GetConnection();
        //    SqlCommand cmd = new SqlCommand("sp_m_GetPartners_NAV", cnn);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    SqlParameter param = DALGlobal.GetSqlParameterInput("@prmDepartmentID", DepartmetnID, SqlDbType.Int);
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

            param = new SqlParameter("@PartnerID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = PartnerID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@TransactionID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@TranValue", System.Data.SqlDbType.Decimal);
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

    }
}