//-- =============================================
//-- Author:		Gazmend Mehmeti
//-- Create date: 07.05.09 7:32:25 PM
//-- Description:	CRUD for Table tblTransactionsDetails
//-- =============================================
using System.Data;
using Microsoft.Data.SqlClient;
using FinabitAPI.Finabit.Transaction.dto;
using FinabitAPI.Core.Global;
using FinabitAPI.Utilis;

namespace FinabitAPI.Finabit.Transaction
{
    public class TransactionsDetailsRepository
    {

         private readonly DBAccess _db;
        public TransactionsDetailsRepository(DBAccess db)
        {
            _db = db;
        }
        TransactionsRepository GlobalTran = new TransactionsRepository();

        public TransactionsDetailsRepository(TransactionsRepository _GlobalTran)
        {
            GlobalTran = _GlobalTran;
        }

        public TransactionsDetailsRepository() { }

        #region Insert

        public void Insert(TransactionsDetails cls, bool fillItemLotTable)
        {
            //SqlConnection cnn = DALGlobal.GetConnection();
            //SqlCommand cmd = new SqlCommand("spTransactionsDetailsInsert", cnn);
            //cmd.CommandType = CommandType.StoredProcedure;

            SqlCommand cmd = GlobalTran.SqlCommandForTran_SP("spTransactionsDetailsInsert");

            SqlParameter param;
            param = new SqlParameter("@TransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.TransactionID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DetailsType", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DetailsType;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ItemID", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ItemID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ItemName", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ItemName;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Account", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Account;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Contracts", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Contracts;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Quantity", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Quantity;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Price", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Price;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Discount", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Discount;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PriceWithDiscount", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PriceWithDiscount;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Value", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ProjectID", SqlDbType.SmallInt);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ProjectID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PaymentID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PaymentID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PaymentValue", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PaymentValue;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PurchasedValue", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PurchasedValue;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@StockQuantity", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.StockQuantity;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@SalesPrice", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.SalesPrice;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@VATPrice", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.VATPrice;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@InsBy", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.InsBy;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@LUB", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.LUB;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ItemTransferFrom", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ItemTransferFrom;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@CostPrice", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.CostPrice;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmErrorID", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmNewID", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Serials", SqlDbType.Structured);
            param.Direction = ParameterDirection.Input;
            param.Value = null;//cls.Numbers;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@OriginalPrice", SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.OriginalPrice;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@LocationID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.LocationID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@IsPrintFiscalInvoice", SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.IsPrintFiscalInvoice;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@VisitID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.VisitID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@IM7Price", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.IM7Price;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DoganaValue", SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DoganaValue;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@VATValue", SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.VATValue;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@AkcizaValue", SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AkcizaValue;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@SubOrderID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.SubOrderID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Discount2", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Discount2;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PartnerItremID", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PartnerItremID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ItemLots", SqlDbType.Structured);
            param.Direction = ParameterDirection.Input;
            if (!fillItemLotTable)
            {
                param.Value = null;// cls.ItemLots;
            }
            else
            {
                param.Value = GetItemLots(cls.ItemID, cls.Barcode, cls.Quantity);
            }
            cmd.Parameters.Add(param);

            param = new SqlParameter("@LocationID2", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.LocationID2;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Memo", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Memo;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Discount3", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Discount3;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PriceMenuID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PriceMenuID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Barcode", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Barcode;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Points", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Points;
            cmd.Parameters.Add(param);

            try
            {
                //cnn.Open();

                cmd.ExecuteNonQuery();
                cls.ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);
                cls.ID = Convert.ToInt32(cmd.Parameters["@prmNewID"].Value);

                //cnn.Close();
            }
            catch (Exception ex)
            {
                cls.ErrorID = -1;
                cls.ErrorDescription = ex.Message;
                //cnn.Close();
            }

            GlobalTran.ErrorID = cls.ErrorID;
        }

        #endregion

        #region Update

        public void Update(TransactionsDetails cls)
        {
            //SqlConnection cnn = DALGlobal.GetConnection();
            //SqlCommand cmd = new SqlCommand("spTransactionsDetailsUpdate", cnn);
            //cmd.CommandType = CommandType.StoredProcedure;

            SqlCommand cmd = GlobalTran.SqlCommandForTran_SP("spTransactionsDetailsUpdate");

            SqlParameter param;
            param = new SqlParameter("@ID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@TransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.TransactionID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DetailsType", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DetailsType;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ItemID", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ItemID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ItemName", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ItemName;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Account", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Account;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Contracts", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Contracts;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Quantity", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Quantity;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Price", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Price;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Discount", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Discount;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PriceWithDiscount", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PriceWithDiscount;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Value", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ProjectID", SqlDbType.SmallInt);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ProjectID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PaymentID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PaymentID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PaymentValue", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PaymentValue;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PurchasedValue", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PurchasedValue;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@StockQuantity", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.StockQuantity;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@SalesPrice", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.SalesPrice;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@VATPrice", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.VATPrice;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@LUB", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.LUB;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@CostPrice", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.CostPrice;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ItemTransferFrom", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ItemTransferFrom;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmErrorID", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Serials", SqlDbType.Structured);
            param.Direction = ParameterDirection.Input;
            param.Value = null;//cls.Numbers;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@OriginalPrice", SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.OriginalPrice;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@LocationID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.LocationID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@IsPrintFiscalInvoice", SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.IsPrintFiscalInvoice;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@IM7Price", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.IM7Price;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DoganaValue", SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DoganaValue;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@VATValue", SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.VATValue;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@AkcizaValue", SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AkcizaValue;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@SubOrderID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.SubOrderID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PartnerItremID", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PartnerItremID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@LocationID2", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.LocationID2;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Points", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Points;
            cmd.Parameters.Add(param);

            try
            {
                //cnn.Open();

                cmd.ExecuteNonQuery();
                cls.ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);

                //cnn.Close();
            }
            catch (Exception ex)
            {
                cls.ErrorID = -1;
                cls.ErrorDescription = ex.Message;
                //cnn.Close();
            }

            GlobalTran.ErrorID = cls.ErrorID;
        }

        #endregion

        #region Delete

        public void Delete(TransactionsDetails cls)
        {
            //SqlConnection cnn = DALGlobal.GetConnection();
            //SqlCommand cmd = new SqlCommand("spTransactionsDetailsDelete", cnn);
            //cmd.CommandType = CommandType.StoredProcedure;

            SqlCommand cmd = GlobalTran.SqlCommandForTran_SP("spTransactionsDetailsDelete");

            SqlParameter param;
            param = new SqlParameter("@ID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmErrorID", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);
            try
            {
                //cnn.Open();

                cmd.ExecuteNonQuery();
                cls.ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);

                //cnn.Close();
            }
            catch (Exception ex)
            {
                cls.ErrorID = -1;
                cls.ErrorDescription = ex.Message;
                //cnn.Close();
            }

            GlobalTran.ErrorID = cls.ErrorID;
        }

        #endregion

        #region DeleteAll

        public void DeleteAll(TransactionsDetails cls)
        {
            //SqlConnection cnn = DALGlobal.GetConnection();
            //SqlCommand cmd = new SqlCommand("spTransactionsDetailsDeleteALL", cnn);
            //cmd.CommandType = CommandType.StoredProcedure;

            SqlCommand cmd = GlobalTran.SqlCommandForTran_SP("spTransactionsDetailsDeleteALL");

            SqlParameter param;
            param = new SqlParameter("@TransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.TransactionID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmErrorID", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);
            try
            {
                //cnn.Open();

                cmd.ExecuteNonQuery();
                cls.ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);

                //cnn.Close();
            }
            catch (Exception ex)
            {
                cls.ErrorID = -1;
                cls.ErrorDescription = ex.Message;
                //cnn.Close();
            }

            GlobalTran.ErrorID = cls.ErrorID;
        }

        #endregion

        #region SelectAll

        public List<TransactionsDetails> SelectAll()
        {
            List<TransactionsDetails> clsList = new List<TransactionsDetails>();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsDetailsList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        TransactionsDetails cls = new TransactionsDetails();
                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.TransactionID = Convert.ToInt32(dr["TransactionID"]);
                        cls.DetailsType = dr["DetailsType"] == DBNull.Value ? 0 : Convert.ToInt32(dr["DetailsType"]);
                        cls.ItemID = Convert.ToString(dr["ItemID"]);
                        cls.ItemName = Convert.ToString(dr["ItemName"]);
                        cls.Account = Convert.ToString(dr["Account"]);
                        cls.Contracts = Convert.ToString(dr["Contracts"]);
                        cls.Quantity = Convert.ToDecimal(dr["Quantity"]);
                        cls.Price = Convert.ToDecimal(dr["Price"]);
                        cls.CostPrice = Convert.ToDecimal(dr["CostPrice"]);
                        cls.Discount = Convert.ToDecimal(dr["Discount"]);
                        cls.PriceWithDiscount = Convert.ToDecimal(dr["PriceWithDiscount"]);
                        cls.Value = Convert.ToDecimal(dr["Value"]);
                        cls.ProjectID = Convert.ToInt32(dr["ProjectID"]);
                        cls.PaymentID = Convert.ToInt32(dr["PaymentID"]);
                        cls.PaymentValue = Convert.ToDecimal(dr["PaymentValue"]);
                        cls.PurchasedValue = Convert.ToDecimal(dr["PurchasedValue"]);
                        cls.StockQuantity = Convert.ToDecimal(dr["StockQuantity"]);
                        cls.ItemTransferFrom = Convert.ToString(dr["ItemTransferFrom"]);
                        cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                        cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                        cls.LUB = Convert.ToInt32(dr["LUB"]);
                        cls.LUN = Convert.ToInt32(dr["LUN"]);
                        cls.LUD = Convert.ToDateTime(dr["LUD"]);
                        cls.OriginalPrice = Convert.ToDecimal(dr["OriginalPrice"]);
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
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsDetailsList", cnn);
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

        public TransactionsDetails SelectByID(TransactionsDetails cls)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsDetailsByID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@ID", SqlDbType.Int);
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
                        cls = new TransactionsDetails();
                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.TransactionID = Convert.ToInt32(dr["TransactionID"]);
                        cls.DetailsType = Convert.ToInt32(dr["DetailsType"]);
                        cls.ItemID = Convert.ToString(dr["ItemID"]);
                        cls.ItemName = Convert.ToString(dr["ItemName"]);
                        cls.Account = Convert.ToString(dr["Account"]);
                        cls.Contracts = Convert.ToString(dr["Contracts"]);
                        cls.Quantity = Convert.ToDecimal(dr["Quantity"]);
                        cls.Price = Convert.ToDecimal(dr["Price"]);
                        cls.CostPrice = Convert.ToDecimal(dr["CostPrice"]);
                        cls.Discount = Convert.ToDecimal(dr["Discount"]);
                        cls.PriceWithDiscount = Convert.ToDecimal(dr["PriceWithDiscount"]);
                        cls.Value = Convert.ToDecimal(dr["Value"]);
                        cls.ProjectID = Convert.ToInt32(dr["ProjectID"]);
                        cls.PaymentID = Convert.ToInt32(dr["PaymentID"]);
                        cls.PaymentValue = Convert.ToDecimal(dr["PaymentValue"]);
                        cls.PurchasedValue = Convert.ToDecimal(dr["PurchasedValue"]);
                        cls.StockQuantity = Convert.ToDecimal(dr["StockQuantity"]);
                        cls.ItemTransferFrom = Convert.ToString(dr["ItemTransferFrom"]);
                        cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                        cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                        cls.LUB = Convert.ToInt32(dr["LUB"]);
                        cls.LUN = Convert.ToInt32(dr["LUN"]);
                        cls.LUD = Convert.ToDateTime(dr["LUD"]);
                        cls.OriginalPrice = Convert.ToDecimal(dr["OriginalPrice"]);
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

        #region SelectPaymentDetail

        public TransactionsDetails SelectPaymentDetail(int TransactionID, int PaymentID)
        {
            TransactionsDetails cls = null;

            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spPaymentDetail", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@TransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PaymentID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = PaymentID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cls = new TransactionsDetails();
                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.TransactionID = Convert.ToInt32(dr["TransactionID"]);
                        cls.DetailsType = Convert.ToInt32(dr["DetailsType"]);
                        cls.ItemID = Convert.ToString(dr["ItemID"]);
                        cls.ItemName = Convert.ToString(dr["ItemName"]);
                        cls.Account = Convert.ToString(dr["Account"]);
                        cls.Quantity = Convert.ToDecimal(dr["Quantity"]);
                        cls.Price = Convert.ToDecimal(dr["Price"]);
                        cls.CostPrice = Convert.ToDecimal(dr["CostPrice"]);
                        cls.Discount = Convert.ToDecimal(dr["Discount"]);
                        cls.PriceWithDiscount = Convert.ToDecimal(dr["PriceWithDiscount"]);
                        cls.Value = Convert.ToDecimal(dr["Value"]);
                        cls.ProjectID = Convert.ToInt32(dr["ProjectID"]);
                        cls.PaymentID = Convert.ToInt32(dr["PaymentID"]);
                        cls.PaymentValue = Convert.ToDecimal(dr["PaymentValue"]);
                        cls.PurchasedValue = Convert.ToDecimal(dr["PurchasedValue"]);
                        cls.StockQuantity = Convert.ToDecimal(dr["StockQuantity"]);
                        cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                        cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                        cls.LUB = Convert.ToInt32(dr["LUB"]);
                        cls.LUN = Convert.ToInt32(dr["LUN"]);
                        cls.LUD = Convert.ToDateTime(dr["LUD"]);
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

        #region SelectByIDTran

        public TransactionsDetails SelectByIDTran(TransactionsDetails cls)
        {
            //SqlConnection cnn = DALGlobal.GetConnection();
            //SqlCommand cmd = new SqlCommand("spTransactionsDetailsByID", cnn);
            //cmd.CommandType = CommandType.StoredProcedure;

            SqlCommand cmd = GlobalTran.SqlCommandForTran_SP("spTransactionsDetailsByID");

            SqlParameter param;
            param = new SqlParameter("@ID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ID;
            cmd.Parameters.Add(param);

            try
            {
                //cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cls = new TransactionsDetails();
                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.TransactionID = Convert.ToInt32(dr["TransactionID"]);
                        cls.DetailsType = Convert.ToInt32(dr["DetailsType"]);
                        cls.ItemID = Convert.ToString(dr["ItemID"]);
                        cls.ItemName = Convert.ToString(dr["ItemName"]);
                        cls.Account = Convert.ToString(dr["Account"]);
                        cls.Quantity = Convert.ToDecimal(dr["Quantity"]);
                        cls.Price = Convert.ToDecimal(dr["Price"]);
                        cls.CostPrice = Convert.ToDecimal(dr["CostPrice"]);
                        cls.Discount = Convert.ToDecimal(dr["Discount"]);
                        cls.PriceWithDiscount = Convert.ToDecimal(dr["PriceWithDiscount"]);
                        cls.Value = Convert.ToDecimal(dr["Value"]);
                        cls.ProjectID = Convert.ToInt32(dr["ProjectID"]);
                        cls.PaymentID = Convert.ToInt32(dr["PaymentID"]);
                        cls.PaymentValue = Convert.ToDecimal(dr["PaymentValue"]);
                        cls.PurchasedValue = Convert.ToDecimal(dr["PurchasedValue"]);
                        cls.StockQuantity = Convert.ToDecimal(dr["StockQuantity"]);
                        cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                        cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                        cls.LUB = Convert.ToInt32(dr["LUB"]);
                        cls.LUN = Convert.ToInt32(dr["LUN"]);
                        cls.LUD = Convert.ToDateTime(dr["LUD"]);
                        cls.OriginalPrice = Convert.ToDecimal(dr["OriginalPrice"]);
                        break;
                    }
                }
                dr.Close();
                //cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                GlobalTran.ErrorID = -1;

                //cnn.Close();
            }
            return cls;
        }

        #endregion

        #region GetDetailsSchema

        public DataTable GetDetailsSchema(int ID)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsDetailsByID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@ID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = ID;
            cmd.Parameters.Add(param);
            return GlobalRepository.ListTables(cmd);
        }

        #endregion

        #region GetDetailsTranBackSchema

        public DataTable GetDetailsTranBackSchema(int ID)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsDetailsBackByID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@ID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = ID;
            cmd.Parameters.Add(param);
            return GlobalRepository.ListTables(cmd);
        }

        #endregion

        #region GetDetails

        public DataTable GetDetails(int TransactionID)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsDetailsByTranID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@TransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);
            return GlobalRepository.ListTables(cmd);
        }

        #endregion

        #region GetDetailsForFiscal

        public DataTable GetDetailsForFiscal(int TransactionID)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsDetailsForFiscal", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@TransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);
            return GlobalRepository.ListTables(cmd);
        }

        #endregion

        #region GetDetailsForFiscal_Eachorder

        public DataTable GetDetailsForFiscal_Eachorder(int TransactionID)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsDetailsForFiscal_Eachorder", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@TransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);



            return GlobalRepository.ListTables(cmd);
        }

        public DataTable GetDetailsForFiscal_Eachorder_Mobile(int TransactionID, bool isOrder)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsDetailsForFiscal_Eachorder_Mobile", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@TransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@isOrder", SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = isOrder;
            cmd.Parameters.Add(param);

            return GlobalRepository.ListTables(cmd);
        }

        #endregion

        #region UpdateIsPrintFiscalInvoiceAsFalse

        public static void UpdateIsPrintFiscalInvoiceAsFalse(int DetailID)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spUpdateIsPrintFiscalInvoiceAsFalse", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@DetailID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = DetailID;
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



        #region UpdateTranDetFields

        public void UpdateTranDetFields(int TranID)
        {

            SqlCommand cmd = GlobalTran.SqlCommandForTran_SP("spUpdateTranDetFields");

            SqlParameter param;
            param = new SqlParameter("@TranID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TranID;
            cmd.Parameters.Add(param);

            try
            {

                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {

                GlobalTran.ErrorDescription = ex.Message;
                GlobalTran.ErrorID = -1;

            }
        }

        #endregion

        #region GetDetailsByTranColl

        public DataTable GetDetailsByTranColl(DataTable TranColl)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsDetailsByTranColl", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@TranColl", SqlDbType.Structured);
            param.Direction = ParameterDirection.Input;
            param.Value = TranColl;
            cmd.Parameters.Add(param);
            return GlobalRepository.ListTables(cmd);
        }

        #endregion

        #region GetDetailsReport

        public DataTable GetDetailsReport(int TransactionID)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsDetailsByTranIDReport", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@TransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);
            return GlobalRepository.ListTables(cmd);
        }

        #endregion

        #region GetDetailsList

        public List<TransactionsDetails> GetDetailsList(int TransactionID)
        {
            List<TransactionsDetails> clsList = new List<TransactionsDetails>();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsDetailsByTranID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@TransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        TransactionsDetails cls = new TransactionsDetails();
                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.TransactionID = Convert.ToInt32(dr["TransactionID"]);
                        cls.DetailsType = dr["DetailsType"] == DBNull.Value ? 0 : Convert.ToInt32(dr["DetailsType"]);
                        cls.ItemID = Convert.ToString(dr["ItemID"]);
                        cls.ItemName = Convert.ToString(dr["ItemName"]);
                        cls.Account = Convert.ToString(dr["Account"]);
                        cls.Quantity = Convert.ToDecimal(dr["Quantity"]);
                        cls.Price = Convert.ToDecimal(dr["Price"]);
                        cls.VATPrice = dr["VATPrice"] == DBNull.Value ? default : Convert.ToDecimal(dr["VATPrice"]);
                        //cls.CostPrice = Convert.ToDecimal(dr["CostPrice"]);
                        cls.Discount = Convert.ToDecimal(dr["Discount"]);
                        cls.PriceWithDiscount = Convert.ToDecimal(dr["PriceWithDiscount"]);
                        cls.Value = Convert.ToDecimal(dr["Value"]);
                        cls.VATValue = Convert.ToDecimal(dr["VATPercent"]);
                        cls.ProjectID = dr["ProjectID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ProjectID"]);
                        cls.PaymentID = dr["PaymentID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["PaymentID"]);
                        cls.PaymentValue = Convert.ToDecimal(dr["PaymentValue"]);
                        cls.PurchasedValue = Convert.ToDecimal(dr["PurchasedValue"]);
                        cls.StockQuantity = Convert.ToDecimal(dr["StockQuantity"]);
                        cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                        cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                        cls.LUB = Convert.ToInt32(dr["LUB"]);
                        cls.LUN = Convert.ToInt32(dr["LUN"]);
                        cls.LUD = Convert.ToDateTime(dr["LUD"]);
                        cls.PDAItemName = Convert.ToString(dr["PDAItemName"]);
                        cls.SubOrderID = Convert.ToInt32(dr["SubOrderID"]);
                        cls.LocationID = Convert.ToInt32(dr["LocationID"]);
                        cls.LocationID2 = Convert.ToInt32(dr["LocationID2"]);
                        cls.Memo = Convert.ToString(dr["Memo"]);
                        cls.IsPrintFiscalInvoice = Convert.ToBoolean(dr["IsPrintFiscalInvoice"]);
                        cls.Points = Convert.ToDecimal(dr["Points"]);
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

        #region GetDetailsPOS

        public DataTable GetDetailsPOS(List<int> tag)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsDetailsByTranID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@TableID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = tag[0];
            cmd.Parameters.Add(param);

            param = new SqlParameter("@UserID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = tag[1];
            cmd.Parameters.Add(param);

            param = new SqlParameter("@TransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = tag[2];
            cmd.Parameters.Add(param);

            param = new SqlParameter("@POSStatus", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = tag[3];
            cmd.Parameters.Add(param);

            return GlobalRepository.ListTables(cmd);
        }

        #endregion

        #region GetDetailsBack

        public DataTable GetDetailsBack(int TransactionID)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsBackDetailsByTranID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@TransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);
            return GlobalRepository.ListTables(cmd);
        }

        #endregion

        #region GetDetailsForPayments

        public DataTable GetDetailsForPayments(int TransactionID)
        {
            //SqlConnection cnn = DALGlobal.GetConnection();
            //SqlCommand cmd = new SqlCommand("spTransactionsDetailsForPayments", cnn);
            //cmd.CommandType = CommandType.StoredProcedure;

            SqlCommand cmd = GlobalTran.SqlCommandForTran_SP("spTransactionsDetailsForPayments");
            SqlParameter param;
            param = new SqlParameter("@ID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);
            //return DALGlobal.ListTables(cmd);

            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                adapt.Fill(dt);
            }
            catch (Exception ex)
            {
                GlobalTran.ErrorID = -1;
            }

            return dt;

        }

        public DataTable GetDetailsForPayments2(int TransactionID)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsDetailsForPayments", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@ID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);
            //return DALGlobal.ListTables(cmd);

            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                adapt.Fill(dt);
            }
            catch (Exception ex)
            {
                GlobalTran.ErrorID = -1;
            }

            return dt;

        }

        #endregion

        #region GetImportDetailsSchema

        public DataTable GetImportDetailsSchema(int ID)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spImportByID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@ID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = ID;
            cmd.Parameters.Add(param);
            return GlobalRepository.ListTables(cmd);
        }

        #endregion

        #region GetDetailsForImports

        public DataTable GetDetailsForImports(int TransactionID)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsDetailsForImports", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@ID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);
            return GlobalRepository.ListTables(cmd);
        }

        #endregion

        #region GetDetailsForImportsReport

        public DataTable GetDetailsForImportsReport(int TransactionID)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsDetailsForImportsReport", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@ID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);
            return GlobalRepository.ListTables(cmd);
        }

        #endregion

        #region Receipt

        public DataTable Receipt(int DetailID)
        {
            DataTable dtList = new DataTable();

            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsDetailsReceipt", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@TranDetailID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = DetailID;
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

        #region UpdateOrderStatus

        public void UpdateOrderStatus(int DetailsID)
        {
            //DALGlobal.ErrorID = 0;
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spUpdateOrderStatus", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            //SqlCommand cmd = DALGlobal.SqlCommandForTran_SP("spUpdateOrderStatus");

            SqlParameter param;
            param = new SqlParameter("@DetailID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = DetailsID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                cmd.ExecuteNonQuery();

                cnn.Close();
            }
            catch (Exception ex)
            {
                //DALGlobal.ErrorID = -1;
                cnn.Close();
            }

        }

        #endregion

        public DataTable GetDetailsReportSubOrder(int TransactionID, bool PrintAll)
        {
            DataTable dt = new DataTable();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsDetailsByTranIDReport2", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@TransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PrintAll", SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = PrintAll;
            cmd.Parameters.Add(param);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            try
            {
                cnn.Open();
                ad.Fill(dt);
                cnn.Close();

            }
            catch (Exception)
            {

                cnn.Close();
            }
            return dt;
        }


        #region MakeSubOrderReceived

        public void MakeSubOrderreceived(int TransactionID)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spMakeSubOrderReceived", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@TransastionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
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
        public static int GetLastSubOrder(int TransactionID)
        {
            int Nr = 0;
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetLastSubOrder", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@TransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);
            try
            {
                cnn.Open();
                Nr = (int)cmd.ExecuteScalar();
                cnn.Close();
            }
            catch { cnn.Close(); }

            return Nr;
        }

        public DataTable GetUnprintedDetails(int TransactionID)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spgetUnprintedDetails", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@TransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);


            return GlobalRepository.ListTables(cmd);
        }
        public void MakeTransactionPrinted(int TransactionID)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spMakeTransactionPrinted", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@TransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);


            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch { cnn.Close(); }

        }
        public List<XMLTransactionDetails> GetXMLDetailsList(int TransactionID)
        {
            List<XMLTransactionDetails> clsList = new List<XMLTransactionDetails>();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsDetailsByTranID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@TransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        XMLTransactionDetails cls = new XMLTransactionDetails();
                        //cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.TransactionID = Convert.ToInt32(dr["TransactionID"]);
                        //cls.DetailsType = dr["DetailsType"] == DBNull.Value ? 0 : Convert.ToInt32(dr["DetailsType"]);
                        cls.ItemID = Convert.ToString(dr["ItemID"]);
                        cls.ItemName = Convert.ToString(dr["ItemName"]);
                        //cls.Account = Convert.ToString(dr["Account"]);
                        cls.Quantity = Convert.ToDecimal(dr["Quantity"]);
                        cls.Price = Convert.ToDecimal(dr["Price"]);
                        cls.Value = Convert.ToDecimal(dr["Value"]);
                        cls.Rabat = Convert.ToDecimal(dr["Rabat"]);
                        cls.Rabat2 = Convert.ToDecimal(dr["Rabat2"]);
                        cls.SubOrderID = Convert.ToInt32(dr["SubOrderID"]);
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
        private DataTable GetItemLots(string ItemID, string Barcode, decimal quantity)
        {
            DataTable dtItemLots = new DataTable();

            DataColumn col = new DataColumn("ID", typeof(int));
            dtItemLots.Columns.Add(col);

            col = new DataColumn("Barcode", typeof(string));
            dtItemLots.Columns.Add(col);

            col = new DataColumn("ItemID", typeof(string));
            dtItemLots.Columns.Add(col);

            col = new DataColumn("Date", typeof(DateTime));
            dtItemLots.Columns.Add(col);

            col = new DataColumn("ExpireDate", typeof(DateTime));
            dtItemLots.Columns.Add(col);

            col = new DataColumn("Memo", typeof(string));
            dtItemLots.Columns.Add(col);

            col = new DataColumn("Quantity", typeof(decimal));
            dtItemLots.Columns.Add(col);

            col = new DataColumn("DetailsID", typeof(int));
            dtItemLots.Columns.Add(col);

            col = new DataColumn("StockLocationID", typeof(int));
            dtItemLots.Columns.Add(col);

            col = new DataColumn("ProductionDate", typeof(DateTime));
            dtItemLots.Columns.Add(col);

            dtItemLots.AcceptChanges();

            DataRow dr = dtItemLots.NewRow();
            dr["ID"] = 0;
            dr["Barcode"] = Barcode;
            dr["ItemID"] = ItemID;
            dr["Date"] = DBNull.Value;
            dr["ExpireDate"] = DBNull.Value;
            dr["Memo"] = "";
            dr["Quantity"] = quantity;
            dr["DetailsID"] = 0;
            dr["StockLocationID"] = 0;
            dr["ProductionDate"] = DBNull.Value;
            dtItemLots.Rows.Add(dr);

            return dtItemLots;
        }

        public static int GetDetailForPayment(int TransactionID)
        {
            int Nr = 0;
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetDetailForPayment_M", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@TransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);
            try
            {
                cnn.Open();
                Nr = (int)cmd.ExecuteScalar();
                cnn.Close();
            }
            catch { cnn.Close(); }

            return Nr;
        }



        public static void UpdateWebHostNameStatus()
        {

            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spUpdateWebHostNameStatus", cnn);
            cmd.CommandType = CommandType.StoredProcedure;


            try
            {
                cnn.Open();
                cmd.ExecuteScalar();
                cnn.Close();
            }
            catch { cnn.Close(); }


        }
        

         public static List<TransactionDetailDistinctDto> GetLast300DistinctDetails()
        {
            var list = new List<TransactionDetailDistinctDto>();
            using (SqlConnection cnn = GlobalRepository.GetConnection())
            using (SqlCommand cmd = new SqlCommand("spTransactionsDetails_Distinct_API", cnn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    cnn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        int ordID               = r.GetOrdinal("ID");
                        int ordTransactionID    = r.GetOrdinal("TransactionID");
                        int ordDetailsType      = r.GetOrdinal("DetailsType");
                        int ordItemID           = r.GetOrdinal("ItemID");
                        int ordItemName         = r.GetOrdinal("ItemName");
                        int ordAccount          = r.GetOrdinal("Account");
                        int ordAmount           = r.GetOrdinal("Amount");          
                        int ordTransactionNo    = r.GetOrdinal("TransactionNo");
                        int ordTransactionDate  = r.GetOrdinal("TransactionDate");
                        int ordPartnerID        = r.GetOrdinal("PartnerID");
                        int ordMemo             = r.GetOrdinal("Memo");

                        while (r.Read())
                        {
                            var row = new TransactionDetailDistinctDto
                            {
                                ID              = r.GetInt32(ordID),
                                TransactionID   = r.GetInt32(ordTransactionID),
                                DetailsType     = r.GetInt32(ordDetailsType),
                                ItemID          = r.IsDBNull(ordItemID) ? "" : r.GetString(ordItemID),
                                ItemName        = r.IsDBNull(ordItemName) ? "" : r.GetString(ordItemName),
                                Account         = r.IsDBNull(ordAccount) ? "" : r.GetString(ordAccount),
                                Amount          = r.IsDBNull(ordAmount) ? 0m : r.GetDecimal(ordAmount),
                                TransactionNo   = r.IsDBNull(ordTransactionNo) ? "" : r.GetString(ordTransactionNo),
                                TransactionDate = r.GetDateTime(ordTransactionDate),
                                PartnerID       = r.IsDBNull(ordPartnerID) ? (int?)null : r.GetInt32(ordPartnerID),
                                Memo            = r.IsDBNull(ordMemo) ? "" : r.GetString(ordMemo)
                            };
                            list.Add(row);
                        }
                    }
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed) cnn.Close();
                }
            }
            return list;
        }
    }
}