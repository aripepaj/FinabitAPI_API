//-- =============================================
//-- Author:		Gazmend Mehmeti
//-- Create date: 05.05.09 6:18:42 PM
//-- Description:	CRUD for Table tblTransactions
//-- =============================================
using System.Data;
using Microsoft.Data.SqlClient;
using FinabitAPI.Finabit.Transaction.dto;
using FinabitAPI.Core.Global;
using FinabitAPI.Core.Utilis;
using FinabitAPI.Utilis;

namespace FinabitAPI.Finabit.Transaction
{
    public class TransactionsRepository
    {
        public int ErrorID = 0;
        public string ErrorDescription = "";

        private readonly DBAccess? _dbAccess;
        private bool _ownsConnection; 

        private SqlConnection? cnnGlobal;
        private SqlTransaction? tranGlobal;

        public TransactionsRepository() { }

        public TransactionsRepository(bool useGlobalTransaction)
        {
            OpenGlobalConnection(); 
        }

        public TransactionsRepository(DBAccess dbAccess, bool useGlobalTransaction = true)
        {
            _dbAccess = dbAccess ?? throw new ArgumentNullException(nameof(dbAccess));
            if (useGlobalTransaction)
            {
                OpenGlobalConnection();
            }
        }

        #region Insert

        public void Insert(Transactions cls)
        {
            //SqlConnection cnn = DALGlobal.GetConnection();
            //SqlCommand cmd = new SqlCommand("spTransactionsInsert", cnn);
            //cmd.CommandType = CommandType.StoredProcedure;

            SqlCommand cmd = SqlCommandForTran_SP("spTransactionsInsert");
            try
            {
                SqlParameter param;
                param = new SqlParameter("@TransactionTypeID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.TransactionTypeID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@TransactionDate", SqlDbType.DateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.TransactionDate;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@InvoiceDate", SqlDbType.SmallDateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.InvoiceDate;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@DueDate", SqlDbType.SmallDateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.DueDate;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@TransactionNo", SqlDbType.NVarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.TransactionNo;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@DUDNo", SqlDbType.NVarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.DUDNo;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@InvoiceNo", SqlDbType.NVarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.InvoiceNo;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@VAT", SqlDbType.Bit);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.VAT;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@InPL", SqlDbType.Bit);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.InPL;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@PartnerID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.PartnerID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@PartnersAddress", SqlDbType.NVarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.PartnersAddress;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@PartnersContactPerson", SqlDbType.NVarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.PartnersContactPerson;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@PartnersPhoneNo", SqlDbType.NVarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.PartnersPhoneNo;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@DepartmentID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.DepartmentID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@DriverID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.DriverID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@PlateNo", SqlDbType.NVarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.PlateNo;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@InternalDepartmentID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.InternalDepartmentID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@EmpID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.EmpID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@CashAccount", SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.CashAccount;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Import", SqlDbType.Bit);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.Import;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Value", SqlDbType.Decimal);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.Value;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@VATValue", SqlDbType.Decimal);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.VATValue;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@AllValue", SqlDbType.Decimal);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.AllValue;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@PaidValue", SqlDbType.Decimal);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.PaidValue;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@VATPercent", SqlDbType.Money);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.VATPercent;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@VATPercentID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.VATPercentID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Memo", SqlDbType.NVarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.Memo;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Reference", SqlDbType.NVarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.Reference;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Links", SqlDbType.NVarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.Links;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Active", SqlDbType.Bit);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.Active;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@JournalStatus", SqlDbType.Bit);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.JournalStatus;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@InsBy", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.InsBy;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@LUB", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.InsBy;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@prmErrorID", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@prmNewID", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Transport", SqlDbType.Money);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.Transport;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Dogana", SqlDbType.Money);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.Dogana;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Akciza", SqlDbType.Money);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.Akciza;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@RABAT", SqlDbType.Money);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.RABAT;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@TableID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.TableID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@POSStatus", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.POSStatus;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@TerminID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.TerminID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Commission1", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.Commission1;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Commission2", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.Commission2;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Commission3", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.Commission3;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@SuficitAccount", SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.SuficitAccount;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@DeficitAccount", SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.DeficitAccount;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@VehicleID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.VehicleID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@ItemID", SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.ItemID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Quantity", SqlDbType.Money);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.Quantity;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@CurrencyID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.CurrencyID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@CurrencyRate", SqlDbType.Money);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.CurrencyRate;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@OverValue", SqlDbType.Money);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.OverValue;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Charges", SqlDbType.Money);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.Charges;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@PartnerItemID", SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.PartnerItemID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@ContractID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.ContractID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@POSPaid", SqlDbType.Bit);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.POSPaid;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@HReservationID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.HReservationID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@PaymentTypeID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.PaymentTypeID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@VisitID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.VisitID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@ReferenceID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.ReferenceID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@CompanyID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.CompanyID;
                cmd.Parameters.Add(param);

                if (cls.PDAInsDate == DateTime.MinValue)
                    cls.PDAInsDate = DateTime.Now;

                param = new SqlParameter("@PDAInsDate", SqlDbType.DateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.PDAInsDate;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@IsNoCustom", SqlDbType.Bit);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.IsNoCustom;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@IncludeTransport", SqlDbType.Bit);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.IncludeTransport;
                cmd.Parameters.Add(param);


                param = new SqlParameter("@HostName", SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.Memo; // perkohesisht luan rolin e HostName
                cmd.Parameters.Add(param);


                param = new SqlParameter("@Longitude", SqlDbType.Decimal);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.Longitude;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Latitude", SqlDbType.Decimal);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.Latitude;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@ServiceType", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.ServiceType;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@AssetID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.AssetID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@BL", SqlDbType.Bit);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.BL;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@IsPriceFromPartner", SqlDbType.Bit);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.IsPriceFromPartner;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@PayValueWithForce", SqlDbType.Bit);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.PayValueWithForce;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@CardID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.CardID;
                cmd.Parameters.Add(param);

            


                //cnn.Open();

                cmd.ExecuteNonQuery();
                cls.ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);
                ErrorID = cls.ErrorID;
                cls.ID = Convert.ToInt32(cmd.Parameters["@prmNewID"].Value);

                //cnn.Close();
            }
            catch (Exception ex)
            {
                cls.ErrorID = -1;
                ErrorID = cls.ErrorID;
                cls.ErrorDescription = ex.Message;
                //cnn.Close();
            }

            ErrorID = cls.ErrorID;
        }

        #endregion

        #region Update

        public void Update(Transactions cls)
        {
            //SqlConnection cnn = DALGlobal.GetConnection();
            //SqlCommand cmd = new SqlCommand("spTransactionsUpdate", cnn);
            //cmd.CommandType = CommandType.StoredProcedure;

            SqlCommand cmd = SqlCommandForTran_SP("spTransactionsUpdate");

            SqlParameter param;
            param = new SqlParameter("@ID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@TransactionTypeID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.TransactionTypeID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@TransactionDate", SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.TransactionDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@InvoiceDate", SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.InvoiceDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DueDate", SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DueDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@TransactionNo", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.TransactionNo;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@InvoiceNo", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.InvoiceNo;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DUDNo", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DUDNo;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@VAT", SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.VAT;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@InPL", SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.InPL;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PartnerID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PartnerID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PartnersAddress", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PartnersAddress;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PartnersContactPerson", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PartnersContactPerson;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PartnersPhoneNo", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PartnersPhoneNo;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DepartmentID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DepartmentID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DriverID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DriverID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PlateNo", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PlateNo;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@InternalDepartmentID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.InternalDepartmentID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@EmpID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.EmpID;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@CashAccount", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.CashAccount;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Import", SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Import;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Value", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@VATValue", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.VATValue;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@AllValue", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AllValue;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PaidValue", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PaidValue;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@VATPercent", SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.VATPercent;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@VATPercentID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.VATPercentID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Memo", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Memo;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Reference", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Reference;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Links", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Links;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Active", SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Active;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@JournalStatus", SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.JournalStatus;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@LUB", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.InsBy;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Transport", SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Transport;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Dogana", SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Dogana;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Akciza", SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Akciza;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RABAT", SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RABAT;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@POSStatus", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.POSStatus;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Commission1", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Commission1;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Commission2", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Commission2;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Commission3", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Commission3;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@SuficitAccount", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.SuficitAccount == null ? "" : cls.SuficitAccount;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DeficitAccount", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DeficitAccount == null ? "" : cls.DeficitAccount;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@VehicleID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.VehicleID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ItemID", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ItemID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Quantity", SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Quantity;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@CurrencyID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.CurrencyID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@CurrencyRate", SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.CurrencyRate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@OverValue", SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.OverValue;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Charges", SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Charges;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PartnerItemID", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PartnerItemID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ContractID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ContractID;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@POSPaid", SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.POSPaid;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@HReservationID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.HReservationID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PaymentTypeID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PaymentTypeID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ReferenceID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ReferenceID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@CompanyID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.CompanyID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@IsNoCustom", SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.IsNoCustom;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@IncludeTransport", SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.IncludeTransport;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@Longitude", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Longitude;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Latitude", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Latitude;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@CardID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.CardID;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@prmErrorID", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            try
            {
                //cnn.Open();

                cmd.ExecuteNonQuery();
                cls.ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);
                ErrorID = cls.ErrorID;
                //cnn.Close();
            }
            catch (Exception ex)
            {
                cls.ErrorID = -1;
                ErrorID = cls.ErrorID;
                cls.ErrorDescription = ex.Message;
                //cnn.Close();
            }

            ErrorID = cls.ErrorID;
        }

        #endregion

        #region Delete

        public void Delete(Transactions cls)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsDelete", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            //SqlCommand cmd = SqlCommandForTran_SP("spTransactionsDelete");

            SqlParameter param;
            param = new SqlParameter("@ID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@UserID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.InsBy;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmErrorID", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);
            try
            {
                cnn.Open();

                cmd.ExecuteNonQuery();
                cls.ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);
                ErrorID = cls.ErrorID;
                cnn.Close();
            }
            catch (Exception ex)
            {
                cls.ErrorID = -1;
                ErrorID = cls.ErrorID;
                cls.ErrorDescription = ex.Message;
                cnn.Close();
            }

            ErrorID = cls.ErrorID;

        }

        #endregion

        #region SelectAll

        public List<Transactions> SelectAll(string FromDate, string ToDate, bool Active, string Type, string DepartmentID)
        {
            List<Transactions> clsList = new List<Transactions>();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;

            param = new SqlParameter("@FromDate", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Active", SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = Active;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Type", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = Type;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DepartmentID", SqlDbType.VarChar);
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
                        Transactions cls = new Transactions();
                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.TransactionTypeID = Convert.ToInt32(dr["TransactionTypeID"]);
                        cls.TransactionDate = Convert.ToDateTime(dr["TransactionDate"]);
                        cls.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]);
                        cls.DueDate = Convert.ToDateTime(dr["DueDate"]);
                        cls.TransactionNo = Convert.ToString(dr["TransactionNo"]);
                        cls.InvoiceNo = Convert.ToString(dr["InvoiceNo"]);
                        cls.VAT = Convert.ToBoolean(dr["VAT"]);
                        cls.InPL = Convert.ToBoolean(dr["InPL"]);
                        cls.PartnerID = Convert.ToInt32(dr["PartnerID"]);
                        cls.DriverID = dr["DriverID"] == DBNull.Value ? -1 : int.Parse(dr["DriverID"].ToString());
                        cls.PlateNo = Convert.ToString(dr["PlateNoDriver"]);
                        cls.PartnersAddress = Convert.ToString(dr["PartnersAddress"]);
                        cls.PartnersContactPerson = Convert.ToString(dr["PartnersContactPerson"]);
                        cls.PartnersPhoneNo = Convert.ToString(dr["PartnersPhoneNo"]);
                        cls.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                        try
                        {
                            cls.InternalDepartmentID = Convert.ToInt32(dr["InternalDepartmentID"]);
                        }
                        catch
                        {
                            cls.InternalDepartmentID = 0;
                        }
                        cls.EmpID = Convert.ToInt32(dr["EmpID"]);
                        cls.CashAccount = Convert.ToString(dr["CashAccount"]);
                        cls.Import = Convert.ToBoolean(dr["Import"]);
                        cls.Value = Convert.ToDecimal(dr["Value"]);
                        cls.VATValue = Convert.ToDecimal(dr["VATValue"]);
                        cls.AllValue = Convert.ToDecimal(dr["AllValue"]);
                        cls.PaidValue = Convert.ToDecimal(dr["PaidValue"]);
                        cls.VATPercent = Convert.ToDecimal(dr["VATPercent"]);
                        cls.Memo = Convert.ToString(dr["Memo"]);
                        cls.Reference = Convert.ToString(dr["Reference"]);
                        cls.Links = Convert.ToString(dr["Links"]);
                        cls.Active = Convert.ToBoolean(dr["Active"]);
                        cls.JournalStatus = Convert.ToBoolean(dr["JournalStatus"]);
                        cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                        cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                        cls.LUB = Convert.ToInt32(dr["LUB"]);
                        cls.LUN = Convert.ToInt32(dr["LUN"]);
                        cls.LUD = Convert.ToDateTime(dr["LUD"]);
                        cls.Transport = Convert.ToDecimal(dr["Transport"]);
                        cls.Dogana = Convert.ToDecimal(dr["Dogana"]);
                        cls.Akciza = Convert.ToDecimal(dr["Akciza"]);
                        cls.RABAT = Convert.ToDecimal(dr["RABAT"]);
                        cls.PartnerName = Convert.ToString(dr["PartnerName"]);
                        cls.DepartmentName = Convert.ToString(dr["DepartmentName"]);
                        cls.EmployeeName = Convert.ToString(dr["FirstName"]) + " " + Convert.ToString(dr["LastName"]);
                        cls.Commission1 = Convert.ToInt32(dr["Commission1"]);
                        cls.Commission2 = Convert.ToInt32(dr["Commission2"]);
                        cls.Commission3 = Convert.ToInt32(dr["Commission3"]);
                        cls.SuficitAccount = Convert.ToString(dr["SuficitAccount"]);
                        cls.DeficitAccount = Convert.ToString(dr["DeficitAccount"]);
                        cls.VehicleID = Convert.ToInt32(dr["VehicleID"]);
                        cls.ItemID = Convert.ToString(dr["ItemID"]);
                        cls.Quantity = Convert.ToDecimal(dr["Quantity"]);

                        int CurrencyID = 0;
                        int.TryParse(dr["CurrencyID"].ToString(), out CurrencyID);
                        cls.CurrencyID = CurrencyID;

                        decimal CurrencyRate = 0;
                        decimal.TryParse(dr["CurrencyRate"].ToString(), out CurrencyRate);
                        cls.CurrencyRate = CurrencyRate;

                        decimal OverValue = 0;
                        decimal.TryParse(dr["OverValue"].ToString(), out OverValue);
                        cls.OverValue = OverValue;

                        decimal Charges = 0;
                        decimal.TryParse(dr["Charges"].ToString(), out Charges);
                        cls.Charges = Charges;

                        cls.POSPaid = dr["POSPaid"] == DBNull.Value ? false : Convert.ToBoolean(dr["POSPaid"]);
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

        #endregion

        #region SelectByID

        public Transactions SelectByID(Transactions cls)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsByID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

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
                        cls = new Transactions();
                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.TransactionTypeID = Convert.ToInt32(dr["TransactionTypeID"]);
                        cls.TransactionDate = Convert.ToDateTime(dr["TransactionDate"]);
                        cls.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]);
                        cls.DueDate = Convert.ToDateTime(dr["DueDate"]);
                        cls.TransactionNo = Convert.ToString(dr["TransactionNo"]);
                        cls.InvoiceNo = Convert.ToString(dr["InvoiceNo"]);
                        cls.DUDNo = Convert.ToString(dr["DUDNo"]);
                        cls.VAT = Convert.ToBoolean(dr["VAT"]);
                        cls.InPL = dr["InPL"] == DBNull.Value ? false : Convert.ToBoolean(dr["InPL"]);
                        cls.PartnerID = Convert.ToInt32(dr["PartnerID"]);
                        cls.PartnersAddress = Convert.ToString(dr["PartnersAddress"]);
                        cls.PartnersContactPerson = Convert.ToString(dr["PartnersContactPerson"]);
                        cls.PartnersPhoneNo = Convert.ToString(dr["PartnersPhoneNo"]);
                        cls.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                        cls.DriverID = dr["DriverID"] == DBNull.Value ? -1 : int.Parse(dr["DriverID"].ToString());
                        cls.PlateNo = Convert.ToString(dr["PlateNoDriver"]);
                        try
                        {
                            cls.InternalDepartmentID = Convert.ToInt32(dr["InternalDepartmentID"]);
                        }
                        catch
                        {
                            cls.InternalDepartmentID = 0;
                        }
                        cls.EmpID = Convert.ToInt32(dr["EmpID"]);
                        cls.CashAccount = dr["CashAccount"] == DBNull.Value ? "" : Convert.ToString(dr["CashAccount"]);
                        cls.Import = Convert.ToBoolean(dr["Import"]);
                        cls.Value = Convert.ToDecimal(dr["Value"]);
                        cls.VATValue = Convert.ToDecimal(dr["VATValue"]);
                        cls.AllValue = Convert.ToDecimal(dr["AllValue"]);
                        cls.PaidValue = Convert.ToDecimal(dr["PaidValue"]);
                        cls.VATPercent = Convert.ToDecimal(dr["VATPercent"]);
                        cls.VATPercentID = Convert.ToInt32(dr["VATPercentID"]);
                        cls.Memo = Convert.ToString(dr["Memo"]);
                        cls.Reference = Convert.ToString(dr["Reference"]);
                        cls.Links = Convert.ToString(dr["Links"]);
                        cls.Active = Convert.ToBoolean(dr["Active"]);
                        cls.JournalStatus = dr["JournalStatus"] == DBNull.Value ? false : Convert.ToBoolean(dr["JournalStatus"]); ;
                        cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                        cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                        cls.LUB = Convert.ToInt32(dr["LUB"]);
                        cls.LUN = Convert.ToInt32(dr["LUN"]);
                        cls.LUD = Convert.ToDateTime(dr["LUD"]);
                        cls.Transport = Convert.ToDecimal(dr["Transport"]);
                        cls.Dogana = Convert.ToDecimal(dr["Dogana"]);
                        cls.Akciza = Convert.ToDecimal(dr["Akciza"]);
                        cls.RABAT = Convert.ToDecimal(dr["RABAT"]);
                        cls.ReferenceID = Convert.ToInt32(dr["ReferenceID"]);
                        cls.Commission1 = dr["Commission1"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Commission1"]);
                        cls.Commission2 = dr["Commission2"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Commission2"]);
                        cls.Commission3 = dr["Commission3"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Commission3"]);
                        cls.SuficitAccount = dr["SuficitAccount"] == DBNull.Value ? "" : Convert.ToString(dr["SuficitAccount"]);
                        cls.DeficitAccount = dr["DeficitAccount"] == DBNull.Value ? "" : Convert.ToString(dr["DeficitAccount"]);
                        cls.VehicleID = dr["VehicleID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["VehicleID"]);
                        cls.ItemID = dr["ItemID"] == DBNull.Value ? "" : Convert.ToString(dr["ItemID"]);
                        cls.Quantity = dr["Quantity"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Quantity"]);
                        cls.PartnerItemID = dr["PartnerItemID"] == DBNull.Value ? "" : dr["PartnerItemID"].ToString();
                        cls.CurrencyID = dr["CurrencyID"] == DBNull.Value ? 0 : int.Parse(dr["CurrencyID"].ToString());
                        cls.CurrencyRate = dr["CurrencyRate"] == DBNull.Value ? 0 : decimal.Parse(dr["CurrencyRate"].ToString());
                        cls.OverValue = dr["OverValue"] == DBNull.Value ? 0 : decimal.Parse(dr["OverValue"].ToString());
                        cls.Charges = dr["Charges"] == DBNull.Value ? 0 : decimal.Parse(dr["Charges"].ToString());
                        cls.ContractID = Convert.ToInt32(dr["ContractID"]);
                        cls.POSPaid = dr["POSPaid"] == DBNull.Value ? false : Convert.ToBoolean(dr["POSPaid"]);
                        cls.HReservationID = dr["HReservationID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["HReservationID"]);
                        cls.PaymentTypeID = dr["PaymentTypeID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["PaymentTypeID"]);
                        cls.PaymentTypeName = dr["PaymentTypeName"] == DBNull.Value ? "" : Convert.ToString(dr["PaymentTypeName"]);
                        cls.CompanyID = Convert.ToInt16(dr["CompanyID"]);
                        cls.Longitude = Convert.ToDecimal(dr["Longitude"]);
                        cls.Latitude = Convert.ToDecimal(dr["Latitude"]);
                        cls.CardID = Convert.ToInt32(dr["CardID"]);
                        cls.CardBarcode = Convert.ToString(dr["CardBarcode"]);

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


        #region SelectByTransactionNo

        public Transactions SelectByTransactionNo(Transactions cls)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsByNo", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;

            param = new SqlParameter("@TransactionNo", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.TransactionNo;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cls = new Transactions();
                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.TransactionTypeID = Convert.ToInt32(dr["TransactionTypeID"]);
                        cls.TransactionDate = Convert.ToDateTime(dr["TransactionDate"]);
                        cls.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]);
                        cls.DueDate = Convert.ToDateTime(dr["DueDate"]);
                        cls.TransactionNo = Convert.ToString(dr["TransactionNo"]);
                        cls.InvoiceNo = Convert.ToString(dr["InvoiceNo"]);
                        cls.DUDNo = Convert.ToString(dr["DUDNo"]);
                        cls.VAT = Convert.ToBoolean(dr["VAT"]);
                        cls.InPL = dr["InPL"] == DBNull.Value ? false : Convert.ToBoolean(dr["InPL"]);
                        cls.PartnerID = Convert.ToInt32(dr["PartnerID"]);
                        cls.PartnersAddress = Convert.ToString(dr["PartnersAddress"]);
                        cls.PartnersContactPerson = Convert.ToString(dr["PartnersContactPerson"]);
                        cls.PartnersPhoneNo = Convert.ToString(dr["PartnersPhoneNo"]);
                        cls.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                        cls.DriverID = dr["DriverID"] == DBNull.Value ? -1 : int.Parse(dr["DriverID"].ToString());
                        cls.PlateNo = Convert.ToString(dr["PlateNoDriver"]);
                        try
                        {
                            cls.InternalDepartmentID = Convert.ToInt32(dr["InternalDepartmentID"]);
                        }
                        catch
                        {
                            cls.InternalDepartmentID = 0;
                        }
                        cls.EmpID = Convert.ToInt32(dr["EmpID"]);
                        cls.CashAccount = dr["CashAccount"] == DBNull.Value ? "" : Convert.ToString(dr["CashAccount"]);
                        cls.Import = Convert.ToBoolean(dr["Import"]);
                        cls.Value = Convert.ToDecimal(dr["Value"]);
                        cls.VATValue = Convert.ToDecimal(dr["VATValue"]);
                        cls.AllValue = Convert.ToDecimal(dr["AllValue"]);
                        cls.PaidValue = Convert.ToDecimal(dr["PaidValue"]);
                        cls.VATPercent = Convert.ToDecimal(dr["VATPercent"]);
                        cls.VATPercentID = Convert.ToInt32(dr["VATPercentID"]);
                        cls.Memo = Convert.ToString(dr["Memo"]);
                        cls.Reference = Convert.ToString(dr["Reference"]);
                        cls.Links = Convert.ToString(dr["Links"]);
                        cls.Active = Convert.ToBoolean(dr["Active"]);
                        cls.JournalStatus = dr["JournalStatus"] == DBNull.Value ? false : Convert.ToBoolean(dr["JournalStatus"]); ;
                        cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                        cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                        cls.LUB = Convert.ToInt32(dr["LUB"]);
                        cls.LUN = Convert.ToInt32(dr["LUN"]);
                        cls.LUD = Convert.ToDateTime(dr["LUD"]);
                        cls.Transport = Convert.ToDecimal(dr["Transport"]);
                        cls.Dogana = Convert.ToDecimal(dr["Dogana"]);
                        cls.Akciza = Convert.ToDecimal(dr["Akciza"]);
                        cls.RABAT = Convert.ToDecimal(dr["RABAT"]);
                        cls.ReferenceID = Convert.ToInt32(dr["ReferenceID"]);
                        cls.Commission1 = dr["Commission1"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Commission1"]);
                        cls.Commission2 = dr["Commission2"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Commission2"]);
                        cls.Commission3 = dr["Commission3"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Commission3"]);
                        cls.SuficitAccount = dr["SuficitAccount"] == DBNull.Value ? "" : Convert.ToString(dr["SuficitAccount"]);
                        cls.DeficitAccount = dr["DeficitAccount"] == DBNull.Value ? "" : Convert.ToString(dr["DeficitAccount"]);
                        cls.VehicleID = dr["VehicleID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["VehicleID"]);
                        cls.ItemID = dr["ItemID"] == DBNull.Value ? "" : Convert.ToString(dr["ItemID"]);
                        cls.Quantity = dr["Quantity"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Quantity"]);
                        cls.PartnerItemID = dr["PartnerItemID"] == DBNull.Value ? "" : dr["PartnerItemID"].ToString();
                        cls.CurrencyID = dr["CurrencyID"] == DBNull.Value ? 0 : int.Parse(dr["CurrencyID"].ToString());
                        cls.CurrencyRate = dr["CurrencyRate"] == DBNull.Value ? 0 : decimal.Parse(dr["CurrencyRate"].ToString());
                        cls.OverValue = dr["OverValue"] == DBNull.Value ? 0 : decimal.Parse(dr["OverValue"].ToString());
                        cls.Charges = dr["Charges"] == DBNull.Value ? 0 : decimal.Parse(dr["Charges"].ToString());
                        cls.ContractID = Convert.ToInt32(dr["ContractID"]);
                        cls.POSPaid = dr["POSPaid"] == DBNull.Value ? false : Convert.ToBoolean(dr["POSPaid"]);
                        cls.HReservationID = dr["HReservationID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["HReservationID"]);
                        cls.PaymentTypeID = dr["PaymentTypeID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["PaymentTypeID"]);
                        cls.PaymentTypeName = dr["PaymentTypeName"] == DBNull.Value ? "" : Convert.ToString(dr["PaymentTypeName"]);
                        cls.CompanyID = Convert.ToInt16(dr["CompanyID"]);
                        cls.Longitude = Convert.ToDecimal(dr["Longitude"]);
                        cls.Latitude = Convert.ToDecimal(dr["Latitude"]);

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

        #region TransactionsByTableID

        public Transactions TransactionsByTableID(int TableID)
        {
            Transactions cls = null;
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsByTableID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;

            param = new SqlParameter("@TableID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TableID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cls = new Transactions();
                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.TransactionTypeID = Convert.ToInt32(dr["TransactionTypeID"]);
                        cls.TransactionDate = Convert.ToDateTime(dr["TransactionDate"]);
                        cls.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]);
                        cls.DueDate = Convert.ToDateTime(dr["DueDate"]);
                        cls.TransactionNo = Convert.ToString(dr["TransactionNo"]);
                        cls.InvoiceNo = Convert.ToString(dr["InvoiceNo"]);
                        cls.DUDNo = Convert.ToString(dr["DUDNo"]);
                        cls.VAT = Convert.ToBoolean(dr["VAT"]);
                        cls.PartnerID = Convert.ToInt32(dr["PartnerID"]);
                        //cls.PartnersAddress = "";// dr["PartnersAddress"] == DBNull.Value ? "" : Convert.ToString(dr["PartnersAddress"]);
                        //cls.PartnersContactPerson = Convert.ToString(dr["PartnersContactPerson"]);
                        //cls.PartnersPhoneNo = Convert.ToString(dr["PartnersPhoneNo"]);
                        cls.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                        cls.EmpID = Convert.ToInt32(dr["EmpID"]);
                        cls.CashAccount = Convert.ToString(dr["CashAccount"]);
                        cls.Import = Convert.ToBoolean(dr["Import"]);
                        cls.Value = Convert.ToDecimal(dr["Value"]);
                        cls.VATValue = Convert.ToDecimal(dr["VATValue"]);
                        cls.AllValue = Convert.ToDecimal(dr["AllValue"]);
                        cls.PaidValue = Convert.ToDecimal(dr["PaidValue"]);
                        cls.VATPercent = Convert.ToDecimal(dr["VATPercent"]);
                        cls.VATPercentID = Convert.ToInt32(dr["VATPercentID"]);
                        cls.Memo = Convert.ToString(dr["Memo"]);
                        cls.Reference = Convert.ToString(dr["Reference"]);
                        cls.Links = Convert.ToString(dr["Links"]);
                        cls.Active = Convert.ToBoolean(dr["Active"]);
                        cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                        cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                        cls.LUB = Convert.ToInt32(dr["LUB"]);
                        cls.LUN = Convert.ToInt32(dr["LUN"]);
                        cls.LUD = Convert.ToDateTime(dr["LUD"]);
                        cls.Transport = Convert.ToDecimal(dr["Transport"]);
                        cls.Dogana = Convert.ToDecimal(dr["Dogana"]);
                        cls.Akciza = Convert.ToDecimal(dr["Akciza"]);
                        cls.RABAT = Convert.ToDecimal(dr["RABAT"]);
                        cls.TableID = Convert.ToInt32(dr["TableID"]);
                        cls.POSStatus = Convert.ToInt32(dr["POSStatus"]);
                        cls.TerminID = Convert.ToInt32(dr["TerminID"]);
                        cls.POSPaid = dr["POSPaid"] == DBNull.Value ? false : Convert.ToBoolean(dr["POSPaid"]);
                        cls.Longitude = Convert.ToDecimal(dr["Longitude"]);
                        cls.Latitude = Convert.ToDecimal(dr["Latitude"]);
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

        #region SelectDataTableByID

        public DataTable SelectDataTableByID(int TranID)
        {
            DataTable data = new DataTable();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsByID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;

            param = new SqlParameter("@ID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TranID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(data);
                return data;

            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return data;
        }

        #endregion

        #region TransactionTypeList

        public DataTable TransactionTypeList()
        {
            DataTable dt = new DataTable();

            SqlConnection conn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetTransactionType", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            catch (Exception)
            {
                string s = "";
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

            return dt;

        }

        #endregion

        #region GetTransaction

        public DataTable GetTransaction(string FromDate, string ToDate, bool Active, string Type, string DepartmentID, int UserID)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;
            param = new SqlParameter("@FromDate", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Active", SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = Active;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Type", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = Type;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DepartmentID", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = DepartmentID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@UserID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = UserID;
            cmd.Parameters.Add(param);

            return GlobalRepository.ListTables(cmd);
        }

        public DataTable GetTransactionForAll(string FromDate, string ToDate, bool Active, string Type, string DepartmentID, int UserID)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsListForAll", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;
            param = new SqlParameter("@FromDate", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Active", SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = Active;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Type", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = Type;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DepartmentID", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = DepartmentID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@UserID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = UserID;
            cmd.Parameters.Add(param);

            return GlobalRepository.ListTables(cmd);
        }

        public DataTable GetOrdersList(string FromDate, string ToDate, string DepartmentID, int UserID)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spOrdersList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;
            param = new SqlParameter("@FromDate", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DepartmentID", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = DepartmentID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@UserID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = UserID;
            cmd.Parameters.Add(param);

            return GlobalRepository.ListTables(cmd);
        }

        #endregion

        #region RegistrationsList

        public DataTable RegistrationsList(string FromDate, string ToDate, bool Active, string DepartmentID)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spRegistrationsList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;
            param = new SqlParameter("@FromDate", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Active", SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = Active;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DepartmentID", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = DepartmentID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@UserID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = GlobalAppData.UserID;
            cmd.Parameters.Add(param);

            return GlobalRepository.ListTables(cmd);
        }

        #endregion

        #region GetTranIntern

        public DataTable GetTranIntern(string FromDate, string ToDate, string DepartmentID)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsInternList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;
            param = new SqlParameter("@FromDate", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DepartmentID", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = DepartmentID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@UserID", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = GlobalAppData.UserID;
            cmd.Parameters.Add(param);

            return GlobalRepository.ListTables(cmd);
        }

        #endregion

        #region GetImport

        public DataTable GetImport(string FromDate, string ToDate, string DepartmentID, bool Active)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spImportsList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;
            param = new SqlParameter("@FromDate", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DepartmentID", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = DepartmentID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Active", SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = Active;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@UserID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = GlobalAppData.UserID;
            cmd.Parameters.Add(param);

            return GlobalRepository.ListTables(cmd);
        }

        #endregion

        #region GetTransactionPayments

        public DataTable GetTransactionPayments(string FromDate, string ToDate, bool Active, string Type)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spPaymentsList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;
            param = new SqlParameter("@FromDate", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Active", SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = Active;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Type", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = Type;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@UserID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = GlobalAppData.UserID;
            cmd.Parameters.Add(param);

            return GlobalRepository.ListTables(cmd);
        }

        #endregion

        #region AutomaticSync

        public int AutomaticSync()
        {
            int RowCount = 0;
            SqlConnection cnn = new SqlConnection();
            cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spFINAutoBit_PULL", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            try
            {
                cnn.Open();
                object ob = cmd.ExecuteScalar();
                RowCount = ob == null ? 0 : int.Parse(ob.ToString());
                cnn.Close();
            }
            catch (Exception ex)
            {
                RowCount = -1;
                cnn.Close();
            }

            return RowCount;
        }

        #endregion



        #region GetTransactionNo

        public string GetTransactionNo(int TransactionType, DateTime Date, int DepartmentID)
        {
            string TransactionNo = "";
            SqlConnection cnn = new SqlConnection();
            cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetTransactionNo", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;
            param = new SqlParameter("@TypeID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionType;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Date", SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = Date;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DepartmentID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = DepartmentID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();
                object ob = cmd.ExecuteScalar();
                TransactionNo = ob == null ? "" : ob.ToString();
                cnn.Close();
            }
            catch (Exception ex)
            {
                TransactionNo = "";
                cnn.Close();
            }

            return TransactionNo;
        }

        #endregion

        #region GetTransactionNo2

        public string GetTransactionNo2(int TransactionType, DateTime Date)
        {
            string TransactionNo = "";
            SqlConnection cnn = new SqlConnection();
            cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetTransactionNo2", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;
            param = new SqlParameter("@TypeID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionType;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Date", SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = Date;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();
                object ob = cmd.ExecuteScalar();
                TransactionNo = ob == null ? "" : ob.ToString();
                cnn.Close();
            }
            catch (Exception ex)
            {
                TransactionNo = "";
                cnn.Close();
            }

            return TransactionNo;
        }

        #endregion

        #region GetTransactionNoForBankJournal

        public string GetTransactionNoForBankJournal(int TransactionType, DateTime Date)
        {
            string TransactionNo = "";
            SqlConnection cnn = new SqlConnection();
            cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetTransactionNoForBankJournal", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;
            param = new SqlParameter("@TypeID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionType;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Date", SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = Date;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();
                object ob = cmd.ExecuteScalar();
                TransactionNo = ob == null ? "" : ob.ToString();
                cnn.Close();
            }
            catch (Exception ex)
            {
                TransactionNo = "";
                cnn.Close();
            }

            return TransactionNo;
        }

        #endregion

        #region spUpdatePayment

        public void UpdatePayment(int TransactionID)
        {
            //SqlConnection cnn = DALGlobal.GetConnection();
            //SqlCommand cmd = new SqlCommand("spUpdatePayment", cnn);
            //cmd.CommandType = CommandType.StoredProcedure;

            SqlCommand cmd = SqlCommandForTran_SP("spUpdatePayment");

            SqlParameter param;
            param = new SqlParameter("@TransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);

            try
            {
                //cnn.Open();
                cmd.ExecuteNonQuery();
                //cnn.Close();
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                ErrorID = -1;
                //cnn.Close();
            }
        }

        #endregion

        #region GetTransactionIDs

        public DataTable GetTransactionIDs(string TranID)
        {
            //SqlConnection cnn = DALGlobal.GetConnection();
            //SqlCommand cmd = new SqlCommand("spGetTransactionIDs", cnn);
            //cmd.CommandType = CommandType.StoredProcedure;

            SqlCommand cmd = SqlCommandForTran_SP("spGetTransactionIDs");

            SqlParameter param;
            param = new SqlParameter("@TranID", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = TranID;
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
                ErrorID = -1;
            }

            return dt;
        }

        #endregion

        #region GetDueReport

        public DataTable GetDueReport(int TranID)
        {
            DataTable dt = new DataTable();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spDueReport", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;
            param = new SqlParameter("@TranType", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TranID;
            cmd.Parameters.Add(param);

            dt = GlobalRepository.ListTables(cmd);
            return dt;
        }

        #endregion

        #region GetDueReportDet

        public DataTable GetDueReportDet(string Type, int PartnerID, short DuePeriod)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spDueReportDet", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;
            param = new SqlParameter("@Type", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = Type;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ParnterID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = PartnerID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DuePeriod", SqlDbType.TinyInt);
            param.Direction = ParameterDirection.Input;
            param.Value = DuePeriod;
            cmd.Parameters.Add(param);

            return GlobalRepository.ListTables(cmd);
        }

        #endregion

        #region UpdateAveragePrice

        public void UpdateAveragePrice(string ItemID, int DepartmentID, int TransactionID, DateTime TransactionDate, int UserID)
        {
            SqlCommand cmd = SqlCommandForTran_SP("[spUpdateAveragePrice]");

            SqlParameter param;
            param = new SqlParameter("@ItemID", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = ItemID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DepartmentID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = DepartmentID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@TransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@TransactionDate", SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@UserID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = UserID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmErrorID", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            try
            {
                cmd.ExecuteNonQuery();
                ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                ErrorID = -1;
            }
        }

        #endregion

        #region UpdateAveragePriceForGroup

        public void UpdateAveragePriceForGroup(DataTable Items, DateTime TransactionDate, int UserID)
        {
            DataTable dt = new DataTable();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spUpdateAveragePriceForGroup", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;
            param = new SqlParameter("@ItemTable", SqlDbType.Structured);
            param.Direction = ParameterDirection.Input;
            param.Value = Items;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@TranDate", SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@UserID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = UserID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmErrorID", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
                ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                ErrorID = -1;
                cnn.Close();
            }
        }

        #endregion

        #region CreateAutomaticRealization

        public void CreateAutomaticRealization(int TransactionID, int Mode, int NewTransactionID, int DepartmentID, int UserID)
        {

            SqlCommand cmd = SqlCommandForTran_SP("[spCreateAutomaticRealization]");

            SqlParameter param;
            param = new SqlParameter("@TransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@NewTransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = NewTransactionID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DepartmentID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = DepartmentID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Mode", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = Mode;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@UserID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = UserID;
            cmd.Parameters.Add(param);



            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                ErrorID = -1;
            }
        }

        #endregion

        #region CreateManualRealization

        public void CreateManualRealization(int TransactionID, int DepartmentID, int Mode, DataTable Serials, int UserID)
        {

            SqlCommand cmd = SqlCommandForTran_SP("[spCreateManualRealization]");

            SqlParameter param;
            param = new SqlParameter("@TransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DepartmentID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = DepartmentID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Mode", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = Mode;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@UserID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = UserID;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@Serials", SqlDbType.Structured);
            param.Direction = ParameterDirection.Input;
            param.Value = Serials;
            cmd.Parameters.Add(param);


            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                ErrorID = -1;
            }
        }

        #endregion

        #region TransactionsByTerminForPOS

        public DataTable TransactionsByTerminForPOS(DateTime FromDate, DateTime ToDate, int TerminID)
        {
            DataTable dt = new DataTable();


            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsListForPOS", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;
            SqlDataAdapter adapt = new SqlDataAdapter(cmd);

            param = new SqlParameter("@TerminID", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = TerminID.ToString();
            cmd.Parameters.Add(param);

            param = new SqlParameter("@FromDate", SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();
                adapt.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                if (ex.Message != null)
                {
                    return null;
                }
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
            return dt;
        }

        #endregion

        #region UpdateAveragePriceForAssembly

        public void UpdateAveragePriceForAssembly(int UserID)
        {

            SqlCommand cmd = SqlCommandForTran_SP("spUpdateAveragePriceForAssembly");

            SqlParameter param;
            param = new SqlParameter("@UserID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = UserID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmErrorID", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            try
            {
                cmd.ExecuteNonQuery();
                ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                ErrorID = -1;
            }
        }

        #endregion

        #region CreateBatchJournals

        public void CreateBatchJournals(int UserID)
        {

            SqlCommand cmd = SqlCommandForTran_SP("spCreateBatchJournals");

            SqlParameter param;
            param = new SqlParameter("@UserID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = UserID;
            cmd.Parameters.Add(param);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                ErrorID = -1;
            }
        }

        #endregion

        #region GetUserTransactions

        public DataTable GetUserTransactions(int UserID, int DepartmentID)
        {
            DataTable dt = new DataTable();


            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetUsedTransactions", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;
            SqlDataAdapter adapt = new SqlDataAdapter(cmd);

            param = new SqlParameter("@UserID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = UserID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DepartmentID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = DepartmentID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();
                adapt.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                if (ex.Message != null)
                {
                    return null;
                }
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
            return dt;
        }

        #endregion

        #region GetAllTransactions

        public DataTable GetAllTransactions(string FromDate, string ToDate)
        {
            DataTable data = new DataTable();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetAllTransactions", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;

            param = new SqlParameter("@FromDate", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(data);
                return data;

            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return data;
        }

        #endregion

        #region GetReservations

        public DataTable GetReservations(DateTime resDate)
        {
            DataTable data = new DataTable();
            data.TableName = "Reservations";
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("sp_m_HInHouseList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;

            param = new SqlParameter("@Date", SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = resDate;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(data);
                return data;

            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return data;
        }

        #endregion

        #region TransferTransactions

        public void TransferTransactions(DataTable dt, Transactions cls)
        {
            //DataTable data = new DataTable();
            //SqlConnection cnn = DALGlobal.GetConnection();
            //SqlCommand cmd = new SqlCommand("spTransferTransactions", cnn);
            //cmd.CommandType = CommandType.StoredProcedure;

            SqlCommand cmd = SqlCommandForTran_SP("spTransferTransactions");

            SqlParameter param;

            param = new SqlParameter("@Transactions", SqlDbType.Structured);
            param.Direction = ParameterDirection.Input;
            param.Value = dt;
            cmd.Parameters.Add(param);


            try
            {

                cmd.ExecuteNonQuery();
                cls.ErrorID = 0;
                ErrorID = 0;
            }
            catch (Exception ex)
            {
                cls.ErrorID = -1;
                ErrorID = -1;
                cls.ErrorDescription = ex.Message;
                //cnn.Close();
            }

            ErrorID = cls.ErrorID;

        }

        #endregion

        #region CheckForTranNo

        public bool CheckForTranNo(string TranNo, int TypeID)
        {
            bool rez = false;

            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spCheckForTranNo", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;

            param = new SqlParameter("@TranNo", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = TranNo;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@TypeID", SqlDbType.Int);
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

        #region GetPaymentCard

        public DataTable GetPaymentCard(int TranID)
        {
            DataTable dt = new DataTable();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetPaymentCard", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;
            param = new SqlParameter("@TransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TranID;
            cmd.Parameters.Add(param);

            dt = GlobalRepository.ListTables(cmd);
            return dt;
        }

        #endregion

        #region UpdateNewCash

        public DataTable UpdateNewCash(int DetailsID, int OldTransactionID, int NewTransactionID, int userID)
        {
            DataTable dt = new DataTable();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spUpdateNewCash", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;
            param = new SqlParameter("@DetailsID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = DetailsID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@OldTransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = OldTransactionID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@NewTransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = NewTransactionID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@userID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = userID;
            cmd.Parameters.Add(param);

            dt = GlobalRepository.ListTables(cmd);
            return dt;
        }

        #endregion

        #region AllowReplication

        public void AllowReplication(DataTable Transactions, bool Value)
        {

            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spAllowReplicationForAll", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;

            param = new SqlParameter("@Transactions", SqlDbType.Structured);
            param.Direction = ParameterDirection.Input;
            param.Value = Transactions;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Value", SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = Value;
            cmd.Parameters.Add(param);

            cnn.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                ErrorID = -1;
            }
            cnn.Close();
        }

        #endregion

        #region RegistrationReport

        public DataTable RegistrationReport(int RegistrationID)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spRegistrationReport", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;

            param = new SqlParameter("@RegistrationID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = RegistrationID;
            cmd.Parameters.Add(param);

            return GlobalRepository.ListTables(cmd);
        }

        #endregion

        #region RegistrationProccess

        public DataTable RegistrationProccess(int RegistrationID)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spRegistrationProccess", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;

            param = new SqlParameter("@RegistrationID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = RegistrationID;
            cmd.Parameters.Add(param);

            return GlobalRepository.ListTables(cmd);
        }

        #endregion

        #region CreateRegistration

        public void CreateRegistration(int ID)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spRegistrationCreate", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;

            param = new SqlParameter("@RegistrationID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = ID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                cmd.ExecuteNonQuery();

                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
        }

        #endregion

        #region GetRegistrations

        public DataTable GetRegistrations(string FromDate, string ToDate, string DepartmentID, int UserID)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spRegistrationList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;
            param = new SqlParameter("@FromDate", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DepartmentID", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = DepartmentID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@UserID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = UserID;
            cmd.Parameters.Add(param);

            return GlobalRepository.ListTables(cmd);
        }

        #endregion

        public static int CashJournalIDByCashAccount(string CashAccount, int TypeID, int CompanyID)
        {
            int CashJournalID = 0;
            SqlConnection cnn = new SqlConnection();
            cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spCashJournalIDByCashAccount", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;
            param = new SqlParameter("@Account", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = CashAccount;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Date", SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = DateTime.Now;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@TypeID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TypeID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@CompanyID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = CompanyID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();
                object ob = cmd.ExecuteScalar();
                CashJournalID = ob == DBNull.Value ? 0 : int.Parse(ob.ToString());
                cnn.Close();
            }
            catch (Exception ex)
            {
                CashJournalID = 0;
                cnn.Close();
            }

            return CashJournalID;
        }

        public void UpdateJournalStatus(Transactions cls)
        {
            SqlCommand cmd = SqlCommandForTran_SP("spTransactionsUpdateJournalStatus");

            SqlParameter param;
            param = new SqlParameter("@ID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@JournalStatus", SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.JournalStatus;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmErrorID", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            try
            {
                //cnn.Open();

                cmd.ExecuteNonQuery();
                cls.ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);
                ErrorID = cls.ErrorID;
                //cnn.Close();
            }
            catch (Exception ex)
            {
                cls.ErrorID = -1;
                ErrorID = cls.ErrorID;
                cls.ErrorDescription = ex.Message;
                //cnn.Close();
            }

            ErrorID = cls.ErrorID;
        }

        #region UpdateDiscountBatch

        public void UpdateDiscountBatch(DataTable Transactions, decimal Discount)
        {

            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spUpdateDiscountBatch", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;

            param = new SqlParameter("@Transactions", SqlDbType.Structured);
            param.Direction = ParameterDirection.Input;
            param.Value = Transactions;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Discount", SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = Discount;
            cmd.Parameters.Add(param);

            cnn.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                ErrorID = -1;
            }
            cnn.Close();
        }

        #endregion

        #region ReRealisation

        public void ReRealisation(DataTable Transactions, DateTime FromDate, DateTime ToDate, int UserID)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spRiRealization", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;

            param = new SqlParameter("@Transactions", SqlDbType.Structured);
            param.Direction = ParameterDirection.Input;
            param.Value = Transactions;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@FromDate", SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@UserID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = UserID;
            cmd.Parameters.Add(param);

            cnn.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                ErrorID = -1;
            }
            cnn.Close();
        }

        #endregion

        #region GetTransatcionsByItemID

        public DataTable GetTransatcionsByItemID(DataTable ItemID)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetTransatcionsByItemID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;
            param = new SqlParameter("@Items", SqlDbType.Structured);
            param.Direction = ParameterDirection.Input;
            param.Value = ItemID;
            cmd.Parameters.Add(param);

            return GlobalRepository.ListTables(cmd);
        }

        #endregion

        public bool CheckIfRegExists(int RegID)
        {
            bool RegExists = false;
            SqlConnection cnn = new SqlConnection();
            cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spCheckIfRegExists", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;
            param = new SqlParameter("@RegID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = RegID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();
                object ob = cmd.ExecuteScalar();
                RegExists = ob == null ? false : bool.Parse(ob.ToString());
                cnn.Close();
            }
            catch (Exception ex)
            {
                RegExists = false;
                cnn.Close();
            }

            return RegExists;
        }

        #region GetOrdersByLocationID

        public DataTable GetOrdersByLocationID(int LocationID)
        {
            DataTable dt = new DataTable();

            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetOrdersByLocationID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;
            SqlDataAdapter adapt = new SqlDataAdapter(cmd);

            param = new SqlParameter("@LocationID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = LocationID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();
                adapt.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                if (ex.Message != null)
                {
                    return null;
                }
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
            return dt;
        }

        #endregion


        #region GetOrdersForUser

        public DataTable GetOrdersForUser(int DepartmentID, DataTable Locations, int Status)
        {
            DataTable dt = new DataTable();

            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetOrdersForUser", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;
            SqlDataAdapter adapt = new SqlDataAdapter(cmd);

            param = new SqlParameter("@DepartmentID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = DepartmentID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Locations", SqlDbType.Structured);
            param.Direction = ParameterDirection.Input;
            param.Value = Locations;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Status", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = Status;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();
                adapt.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                if (ex.Message != null)
                {
                    return null;
                }
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
            return dt;
        }

        #endregion

        #region GetDataTableForFiscal

        public DataTable GetDataTableForFiscal(int TransactionID)
        {
            DataTable dt = new DataTable();

            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetDataTableForFiscal", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;
            SqlDataAdapter adapt = new SqlDataAdapter(cmd);

            param = new SqlParameter("@TransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();
                adapt.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                if (ex.Message != null)
                {
                    return null;
                }
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
            return dt;
        }

        #endregion

        public static DataTable GetHeaderFromDoc(int TransactionID)
        {
            DataTable Header = null;

            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spHeaderFromDoc", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

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
                    Header = new DataTable();
                    Header.Load(dr);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != null)
                {
                    return null;
                }
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
            return Header;
        }

        public static DataTable GetDetailsFromDoc(int TransactionID)
        {
            DataTable Details = null;

            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spDetailsFromDoc", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

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
                    Details = new DataTable();
                    Details.Load(dr);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != null)
                {
                    return null;
                }
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
            return Details;
        }

        #region GetRegistrationsForPeriod

        public DataTable GetRegistrationsForPeriod(DateTime FromDate, DateTime ToDate)
        {
            DataTable data = new DataTable();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetReportBetweenRegistrations", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;

            param = new SqlParameter("@fromdate", SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@todate", SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(data);
                return data;

            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return data;

        }

        #endregion

        #region ChangeTransactionTypeID

        #region ChangeTransactionTypeID

        public void ChangeTransactionTypeID(int ID, int TransactionTypeID)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spChangeTransactionTypeID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@ID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@TransactionTypeID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionTypeID;
            cmd.Parameters.Add(param);

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

        #endregion

        #endregion

        #region CheckForFiscalInvoice

        public bool CheckForFiscalInvoice(int TranID)
        {
            bool rez = false;

            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spCheckForFiscalInvoice", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;

            param = new SqlParameter("@TranID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TranID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();
                object ob = cmd.ExecuteScalar();
                rez = ob == null ? false : Convert.ToBoolean(ob);
                cnn.Close();
            }
            catch (Exception ex)
            {
                rez = false;
                cnn.Close();
            }

            return rez;
        }

        #endregion

        #region UpdatePOSPaid

        public void UpdatePOSPaid(int TransactionID, bool POSPaid)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsUpdatePOSPaid", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            //SqlCommand cmd = DALGlobal.SqlCommandForTran_SP("spTransactionsUpdate");

            SqlParameter param;
            param = new SqlParameter("@ID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@POSPaid", SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = POSPaid;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                cmd.ExecuteNonQuery();
                cnn.Close();
                ErrorID = 0;
            }
            catch (Exception ex)
            {
                cnn.Close();
                ErrorID = -1;
            }
        }

        #endregion

        #region GlobalConnection

        public void OpenGlobalConnection()
        {
            if (cnnGlobal != null && cnnGlobal.State == ConnectionState.Open)
                return; 

            SqlConnection conn = _dbAccess != null
                ? _dbAccess.GetConnection()
                : GlobalRepository.GetConnection();

            _ownsConnection = true;  
            cnnGlobal = conn;

            cnnGlobal.Open();

            tranGlobal = cnnGlobal.BeginTransaction();
        }

        public void CloseGlobalConnection()
        {
            try
            {
                if (cnnGlobal == null) return;

                if (tranGlobal != null)
                {
                    try
                    {
                        if (ErrorID != 0)
                            tranGlobal.Rollback();
                        else
                            tranGlobal.Commit();
                    }
                    catch
                    {
                    }
                    finally
                    {
                        tranGlobal.Dispose();
                        tranGlobal = null;
                    }
                }

                if (cnnGlobal.State != ConnectionState.Closed && _ownsConnection)
                {
                    cnnGlobal.Close();
                }
            }
            finally
            {
                if (_ownsConnection && cnnGlobal != null)
                {
                    cnnGlobal.Dispose();
                }
                cnnGlobal = null;
                _ownsConnection = false;
            }
        }

        public SqlCommand SqlCommandForTran_SP(string cmdText)
        {
            EnsureConnectionAndTransaction();
            var cmd = new SqlCommand(cmdText, cnnGlobal!)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 0,
                Transaction = tranGlobal
            };
            return cmd;
        }

        public SqlCommand SqlCommandForTran_Text(string cmdText)
        {
            EnsureConnectionAndTransaction();
            var cmd = new SqlCommand(cmdText, cnnGlobal!)
            {
                CommandType = CommandType.Text,
                CommandTimeout = 0,
                Transaction = tranGlobal
            };
            return cmd;
        }

        private void EnsureConnectionAndTransaction()
        {
            if (cnnGlobal == null || cnnGlobal.State != ConnectionState.Open)
            {
                OpenGlobalConnection();
            }

            if (tranGlobal == null)
            {
                tranGlobal = cnnGlobal!.BeginTransaction();
            }
        }

        public void Dispose()
        {
            CloseGlobalConnection();
        }

        #endregion

        #region CheckForPayment

        public bool CheckForPayment(int TransactionID)
        {
            bool HasPayment = false;
            SqlConnection cnn = new SqlConnection();
            cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spCheckForPayment", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;
            param = new SqlParameter("@TransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();
                object ob = cmd.ExecuteScalar();
                HasPayment = ob == null ? false : bool.Parse(ob.ToString());
                cnn.Close();
            }
            catch (Exception ex)
            {
                cnn.Close();
            }

            return HasPayment;
        }

        #endregion

        public int GetTarnsactionIDIfExist(string TranNo, int PartnerID, string TranDate)
        {
            int ID = 0;
            SqlConnection cnn = new SqlConnection();
            cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetTransactionID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;
            param = new SqlParameter("@TransactionNo", SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = TranNo;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PartnerId", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = PartnerID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@TransactionDate", SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = TranDate;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();
                object ob = cmd.ExecuteScalar();
                ID = ob == null ? 0 : int.Parse(ob.ToString());
                cnn.Close();
            }
            catch (Exception ex)
            {
                cnn.Close();
            }

            return ID;
        }

        public bool CheckTransactionIfExists(Transactions cls)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("CheckTransactionIfExists", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            bool ReturnValue = false;
            //SqlCommand cmd = SqlCommandForTran_SP("CheckTransactionIfExists");
            try
            {
                SqlParameter param;
                param = new SqlParameter("@TransactionTypeID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.TransactionTypeID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@TransactionDate", SqlDbType.DateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.TransactionDate.Date;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@PDAInsDate", SqlDbType.DateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.PDAInsDate;
                cmd.Parameters.Add(param);


                param = new SqlParameter("@PartnerID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.PartnerID;
                cmd.Parameters.Add(param);



                param = new SqlParameter("@DepartmentID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.DepartmentID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@InternalDepartmentID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.InternalDepartmentID;
                cmd.Parameters.Add(param);


                param = new SqlParameter("@prmExists", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);


                //param = new SqlParameter("@PaymentMethodID", System.Data.SqlDbType.TinyInt);
                //param.Direction = ParameterDirection.Input;
                //param.Value = cls.PaymentMethodID;
                //cmd.Parameters.Add(param);

                cnn.Open();

                cmd.ExecuteNonQuery();
                ReturnValue = Convert.ToBoolean(cmd.Parameters["@prmExists"].Value);


                cnn.Close();
            }
            catch (Exception ex)
            {
                cls.ErrorID = -1;
                ErrorID = cls.ErrorID;
                cls.ErrorDescription = ex.Message;

                cnn.Close();
            }

            ErrorID = cls.ErrorID;
            return ReturnValue;
        }

        public List<XMLTransactionDetails> M_GetSalesArticles(DateTime FromDate, DateTime ToDate, int DepartmentID)
        {
            List<XMLTransactionDetails> detailsList = new List<XMLTransactionDetails>();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("_sp_m_GetSalesArticles", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@FromDate", SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DepartmentID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = DepartmentID;
            cmd.Parameters.Add(param);


            try
            {
                cnn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                XMLTransactionDetails dt;
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dt = new XMLTransactionDetails();
                        dt.ItemID = Convert.ToString(dr["ItemID"]);
                        dt.ItemName = Convert.ToString(dr["ItemName"]);
                        dt.Quantity = Convert.ToDecimal(dr["Quantity"]);
                        dt.Value = Convert.ToDecimal(dr["Value"]);
                        detailsList.Add(dt);
                    }
                }
                cnn.Close();
            }
            catch (Exception ex) { string m = ex.Message; cnn.Close(); }
            return detailsList;
        }


        #region GetInvoiceNo

        public string GetInvoiceNo(int TransactionType, DateTime Date, int DepartmentID)
        {
            string TransactionNo = "";
            SqlConnection cnn = new SqlConnection();
            cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetInvoiceNo", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;
            param = new SqlParameter("@TypeID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionType;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Date", SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = Date;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DepartmentID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = DepartmentID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();
                object ob = cmd.ExecuteScalar();
                TransactionNo = ob == null ? "" : ob.ToString();
                cnn.Close();
            }
            catch (Exception ex)
            {
                TransactionNo = "";
                cnn.Close();
            }

            return TransactionNo;
        }

        #endregion

        #region GetInvoiceNo_NAV

        public string GetInvoiceNo_NAV(int TransactionType, DateTime Date, int DepartmentID)
        {
            string TransactionNo = "";
            SqlConnection cnn = new SqlConnection();
            cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetInvoiceNo_NAV", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;
            param = new SqlParameter("@TypeID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionType;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Date", SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = Date;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DepartmentID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = DepartmentID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();
                object ob = cmd.ExecuteScalar();
                TransactionNo = ob == null ? "" : ob.ToString();
                cnn.Close();
            }
            catch (Exception ex)
            {
                TransactionNo = "";
                cnn.Close();
            }

            return TransactionNo;
        }

        #endregion

        #region GetTransactionNo_M

        public string GetTransactionNo_M(int TransactionType, DateTime Date, int DepartmentID)
        {
            string TransactionNo = "";
            SqlConnection cnn = new SqlConnection();
            cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetTransactionNo_M", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;
            param = new SqlParameter("@TypeID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionType;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Date", SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = Date;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DepartmentID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = DepartmentID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();
                object ob = cmd.ExecuteScalar();
                TransactionNo = ob == null ? "" : ob.ToString();
                cnn.Close();
            }
            catch (Exception ex)
            {
                TransactionNo = "";
                cnn.Close();
            }

            return TransactionNo;
        }

        #endregion

        #region GetMerchTransactionNo

        public string GetMerchTransactionNo()
        {
            string TransactionNo = "";
            SqlConnection cnn = new SqlConnection();
            cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetMerchTransactionNo", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;



            try
            {
                cnn.Open();
                object ob = cmd.ExecuteScalar();
                TransactionNo = ob == null ? "" : ob.ToString();
                cnn.Close();
            }
            catch (Exception ex)
            {
                TransactionNo = "";
                cnn.Close();
            }

            return TransactionNo;
        }

        #endregion

        #region GetTranID

        public int GetTranID(int Type, int PartnerID, string TransactionNo, string tranDate)
        {
            int rez = 0;

            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("sp_M_GetTransactionID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;



            param = new SqlParameter("@Type", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = Type;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PartnerID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = PartnerID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@TransactionNo", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionNo;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@TranDate", SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = Convert.ToDateTime(tranDate).ToString("yyyy-MM-dd");
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();
                object ob = cmd.ExecuteScalar();
                rez = ob == null ? 0 : Convert.ToInt32(ob);
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return rez;
        }

        public int GetTranID_NotaKreditore(int Type, int PartnerID, string TransactionNo, string tranDate)
        {
            int rez = 0;

            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("sp_M_GetTransactionID_Nota", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;



            param = new SqlParameter("@Type", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = Type;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PartnerID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = PartnerID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@TransactionNo", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionNo;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@TranDate", SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = Convert.ToDateTime(tranDate).ToString("yyyy-MM-dd");
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();
                object ob = cmd.ExecuteScalar();
                rez = ob == null ? 0 : Convert.ToInt32(ob);
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

        public List<XMLTransactionDetails> M_GetSalesArticles_F(string FlightNo)
        {
            List<XMLTransactionDetails> detailsList = new List<XMLTransactionDetails>();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("_sp_m_GetSalesArticles_F", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@Number", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = FlightNo;
            cmd.Parameters.Add(param);


            try
            {
                cnn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                XMLTransactionDetails dt;
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dt = new XMLTransactionDetails();
                        dt.ItemID = Convert.ToString(dr["ItemID"]);
                        dt.ItemName = Convert.ToString(dr["ItemName"]);
                        dt.Quantity = Convert.ToDecimal(dr["Quantity"]);
                        dt.Value = Convert.ToDecimal(dr["Value"]);
                        dt.Rabat = Convert.ToDecimal(dr["Shabllon_QUantity"]);// fusha e Rabatit perdoret per Shabllon_Quantity
                        dt.Rabat2 = Convert.ToDecimal(dr["Return_Quantity"]);
                        detailsList.Add(dt);
                    }
                }
                cnn.Close();
            }
            catch (Exception ex) { string m = ex.Message; cnn.Close(); }
            return detailsList;
        }
        public XMLTransactions SelectXMLTransactionByID(int TransactionID)
        {

            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spTransactionsByID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;

            param = new SqlParameter("@ID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);
            XMLTransactions cls = new XMLTransactions();
            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cls = new XMLTransactions();
                        cls.ID = Convert.ToInt32(dr["ID"]);
                        //cls.TransactionTypeID = Convert.ToInt32(dr["TransactionTypeID"]);
                        cls.TransactionDate = Convert.ToDateTime(dr["TransactionDate"]);
                        //cls.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]);
                        // cls.DueDate = Convert.ToDateTime(dr["DueDate"]);
                        cls.TransactionNo = Convert.ToString(dr["TransactionNo"]);
                        cls.InvoiceNo = Convert.ToString(dr["InvoiceNo"]);

                        cls.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);

                        cls.AllValue = Convert.ToDecimal(dr["AllValue"]);
                        cls.PaymentValue = Convert.ToDecimal(dr["PaidValue"]);

                        cls.Memo = Convert.ToString(dr["Memo"]);
                        cls.BL = Convert.ToBoolean(dr["BL"]);
                        cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                        cls.InsDate = Convert.ToDateTime(dr["InsDate"]);

                        cls.Longitude = Convert.ToDecimal(dr["Longitude"]);
                        cls.Latitude = Convert.ToDecimal(dr["Latitude"]);

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




        public void UpdateIsPrintFiscalInvoiceAsTrueByTranID(int TransactionsID)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spUpdateIsPrintFiscalInvoiceAsTrueByTranID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@TransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionsID;
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

        public int OrdersBatchInsert(DataTable dt)
        {
            int result = 0;
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spCreateOrder_API", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;

         

            param = new SqlParameter("@Orders", SqlDbType.Structured);
            param.Direction = ParameterDirection.Input;
            param.Value = dt;
            cmd.Parameters.Add(param);

            cnn.Open();

            SqlDataReader dr = null;

            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    result = Convert.ToInt32(dr["count"]);
                    break;
                }
            }
            cnn.Close();


            return result;
        }

        public bool UpdateDriverInTransaction(int TransactionId,int EmpID)
        {
            bool result = false;
            SqlConnection cnn = new SqlConnection();
            cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("sp_m_TransactionsDriverUpdate", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;
            param = new SqlParameter("@TransactionID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionId;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@EmpID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = EmpID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();
                object ob = cmd.ExecuteScalar();
                result = int.Parse(ob.ToString()) == 1;
                cnn.Close();
            }
            catch (Exception ex)
            {
                result = false;
                cnn.Close();
            }

            return result;
        }

    }
}
