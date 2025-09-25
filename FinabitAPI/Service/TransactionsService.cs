//-- =============================================
//-- Author:		Gazmend Mehmeti
//-- Create date: 05.05.09 6:18:42 PM
//-- Description:	CRUD for Table tblTransactions
//-- =============================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using Finabit_API.Models;
using FinabitAPI.Utilis;

namespace FinabitAPI.Service
{
    public class TransactionsService
    {
        public TransactionsRepository GlobaTran = new TransactionsRepository();
        private readonly DBAccess _dbAccess;
        public TransactionsService(bool b, DBAccess dbAccess)
        {
            _dbAccess = dbAccess;
            GlobaTran = new TransactionsRepository(_dbAccess, true);
        }
        public TransactionsService()
        {
        }
        public int ErrorID
        {
            get { return GlobaTran.ErrorID; }
            set { GlobaTran.ErrorID = value; }
        }
        #region Insert

        public int Insert(Transactions cls,bool fillItemLotTable)
        {
            //GlobaTran = new DALTransactions(true);
            GlobaTran.Insert(cls);
            if (GlobaTran.ErrorID == 0)
            {
               // Users mUser = BLLUsers.GetLoginUserByPDAPIN("Pas insertit te transaksionit");

                TransactionsDetailsService bllDet = new TransactionsDetailsService(GlobaTran);
                //BLLUsers.GetLoginUserByPDAPIN("Pas insertit te transaksionit 2");
                if (cls.TranDetailsColl != null && cls.TransactionTypeID != 11)
                {
                    foreach (TransactionsDetails details in cls.TranDetailsColl)
                    {
                        details.TransactionID = cls.ID;
                        bllDet.Insert(details,fillItemLotTable);
                        //BLLUsers.GetLoginUserByPDAPIN("Pas insertit te detajit 2");
                        if (GlobaTran.ErrorID != 0)
                        {
                            cls.ErrorDescription = details.ErrorDescription;
                            //BLLUsers.GetLoginUserByPDAPIN("Pas gabimit te detajit" + cls.ErrorDescription);
                            return GlobaTran.ErrorID;
                        }

                        if (details.DetailsType == 1)
                        {
                            if (cls.TransactionTypeID != 36)
                            {
                                GlobaTran.UpdateAveragePrice(details.ItemID, cls.DepartmentID, cls.ID, cls.TransactionDate, cls.InsBy);
                                if (GlobaTran.ErrorID != 0)
                                {
                                    //cls.ErrorDescription = details.ErrorDescription; // RmsString.getString(BLLGlobal.ErrorID.ToStrijng())
                                    return GlobaTran.ErrorID;
                                }
                            }

                            if (cls.TransactionTypeID == 10)
                            {
                                try
                                {
                                    GlobaTran.UpdateAveragePrice(details.ItemID, cls.InternalDepartmentID, cls.ID, cls.TransactionDate, cls.InsBy);
                                }
                                catch { GlobaTran.ErrorID = 0; }// mos te nderpritet sinkronizimi nese ka gabim 
                                GlobaTran.ErrorID = 0;
                            }
                        }

                        if (details.PaymentID.ToString() != "0")
                        {
                            GlobaTran.UpdatePayment(details.PaymentID);
                        }
                    }




                    bllDet.UpdateTranDetFields(cls.ID);



                    if (GlobaTran.ErrorID == -1)
                    {
                        return GlobaTran.ErrorID;
                        //BLLUsers.GetLoginUserByPDAPIN(GlobaTran.ErrorDescription);
                    }
                }

                if (cls.TransactionTypeID == 36)
                {
                    return GlobaTran.ErrorID;
                }

                

                // krijimi automatik i normativave
                if (cls.CreateAutomaticRealization)
                {
                    //GlobaTran.CreateAutomaticRealization(cls.ID, 1, 0, cls.DepartmentID, cls.InsBy);
                    //if (GlobaTran.ErrorID != 0)
                    //{
                    //    cls.ErrorDescription = cls.ErrorDescription;
                    //    return GlobaTran.ErrorID;
                    //}
                }

                if (cls.TransactionTypeID == 11)
                {
                    //GlobaTran.CreateManualRealization(cls.ID, cls.DepartmentID, 1, cls.Numbers, cls.InsBy);
                    //if (GlobaTran.ErrorID != 0)
                    //{
                    //    cls.ErrorDescription = cls.ErrorDescription;
                    //    return GlobaTran.ErrorID;
                    //}
                }

                // azhurimi i cmimeve te asemblimeve
                //GlobaTran.UpdateAveragePriceForAssembly(cls.InsBy);
                //if (GlobaTran.ErrorID != 0)
                //{
                //    cls.ErrorDescription = cls.ErrorDescription;
                //    return GlobaTran.ErrorID;
                //}

                // azhurimi i transaksioneve
                GlobaTran.CreateBatchJournals(cls.InsBy);
                if (GlobaTran.ErrorID != 0)
                {
                    //BLLUsers.GetLoginUserByPDAPIN("Gabimi ne insertim " + cls.ErrorDescription);
                    cls.ErrorDescription = cls.ErrorDescription;
                    return GlobaTran.ErrorID;
                }
            }
            else
            {
                //BLLUsers.GetLoginUserByPDAPIN("Gabimi ne insertim " + cls.ErrorDescription);
            }
            return ErrorID;
        }

        #endregion

        #region Update

        public int Update(Transactions cls)
        {
            if (cls.TransactionTypeID == 26)
            {
                int ErrorID = Delete(cls);
                if (ErrorID == 0)
                {
                    Insert(cls,false);
                }
                return ErrorID;
            }

            //GlobaTran = new DALTransactions(true);
            GlobaTran.Update(cls);
            if (GlobaTran.ErrorID == 0)
            {
                TransactionsDetailsService bllDet = new TransactionsDetailsService(GlobaTran);
                foreach (TransactionsDetails details in cls.TranDetailsColl)
                {
                    if (cls.TransactionTypeID != 11)
                    {
                        switch (details.Mode)
                        {
                            case 1:
                                bllDet.Insert(details,false);
                                break;
                            case 2:
                                bllDet.Update(details);
                                break;
                            case 3:
                                if (details.PaymentID == 0)
                                {
                                    TransactionsDetailsRepository dd = new TransactionsDetailsRepository(GlobaTran);
                                    TransactionsDetails td = dd.SelectByIDTran(details);
                                    details.PaymentID = td.PaymentID;
                                }
                                bllDet.Delete(details);
                                break;
                        }

                        if (GlobaTran.ErrorID != 0)
                        {
                            cls.ErrorDescription = details.ErrorDescription;
                            return GlobaTran.ErrorID;
                        }

                        if (details.DetailsType == 1)
                        {
                            //GlobaTran.UpdateAveragePrice(details.ItemID, cls.DepartmentID, cls.ID, cls.TransactionDate, cls.InsBy);
                            if (GlobaTran.ErrorID != 0)
                            {
                                //cls.ErrorDescription = details.ErrorDescription; // RmsString.getString(BLLGlobal.ErrorID.ToStrijng())
                                return GlobaTran.ErrorID;
                            }
                        }

                        if (details.PaymentID.ToString() != "0")
                        {
                            GlobaTran.UpdatePayment(details.PaymentID);
                        }
                    }
                }

                bllDet.UpdateTranDetFields(cls.ID);

                if (GlobaTran.ErrorID == -1)
                {
                    //BLLUsers.GetLoginUserByPDAPIN(GlobaTran.ErrorDescription);
                }
            }

            // krijimi automatik i normativave
            if (cls.CreateAutomaticRealization)
            {
                //GlobaTran.CreateAutomaticRealization(cls.ID, 2, cls.ReferenceID, cls.DepartmentID, cls.InsBy);
                if (GlobaTran.ErrorID != 0)
                {
                    cls.ErrorDescription = cls.ErrorDescription;
                    return GlobaTran.ErrorID;
                }
            }

            //if (cls.TransactionTypeID == 11)
            //{
            //    GlobaTran.CreateManualRealization(cls.ID, cls.DepartmentID, 1, cls.Numbers, cls.InsBy);
            //    if (GlobaTran.ErrorID != 0)
            //    {
            //        cls.ErrorDescription = cls.ErrorDescription;
            //        return GlobaTran.ErrorID;
            //    }
            //}

            // azhurimi i cmimeve te asemblimeve
            if (cls.TransactionTypeID != 24 && cls.TransactionTypeID != 25)
            {
                //GlobaTran.UpdateAveragePriceForAssembly(cls.InsBy);
                //if (GlobaTran.ErrorID != 0)
                //{
                //    cls.ErrorDescription = cls.ErrorDescription;
                //    return cls.ErrorID;
                //}
            }

            // azhurimi i transaksioneve
            GlobaTran.CreateBatchJournals(cls.InsBy);
            if (GlobaTran.ErrorID != 0)
            {
                cls.ErrorDescription = cls.ErrorDescription;
                return GlobaTran.ErrorID;
            }
            return 0;
        }

        #endregion

        #region Delete

        public int Delete(Transactions cls)
        {
            TransactionsRepository dal = new TransactionsRepository();
            TransactionsDetailsRepository dd = new TransactionsDetailsRepository(dal);
            DataTable bd = dd.GetDetailsForPayments(cls.ID);


            dal.Delete(cls);
            if (dal.ErrorID == 0)
            {
                TransactionsDetailsService bllDet = new TransactionsDetailsService(dal);
                if (cls.TranDetailsColl != null && cls.TranDetailsColl.Count > 0 && cls.TransactionTypeID != 11)
                {
                    bllDet.DeleteALL(cls.TranDetailsColl[0]);
                    if (dal.ErrorID != 0)
                    {
                        cls.ErrorDescription = cls.TranDetailsColl[0].ErrorDescription;
                        return dal.ErrorID;
                    }

                }

                foreach (DataRow dr in bd.Rows)
                {
                    if (dr["PaymentID"].ToString() != string.Empty)
                    {
                        dal.UpdatePayment(int.Parse(dr["PaymentID"].ToString()));
                    }

                    if (int.Parse(dr["DetailsType"].ToString()) == 1)
                    {
                        dal.UpdateAveragePrice(dr["ItemID"].ToString(), cls.DepartmentID, cls.ID, cls.TransactionDate, cls.InsBy);
                        if (dal.ErrorID != 0)
                        {
                            //cls.ErrorDescription = details.ErrorDescription; // RmsString.getString(BLLGlobal.ErrorID.ToStrijng())
                            return dal.ErrorID;
                        }
                    }
                }

                if (cls.TransactionTypeID == 26)
                {
                    DataTable dtTranIDs = dal.GetTransactionIDs(cls.ID.ToString());
                    foreach (DataRow row in dtTranIDs.Rows)
                    {
                        cls.ID = int.Parse(row["ID"].ToString());
                        cls.TranDetailsColl[0].TransactionID = cls.ID;
                        Delete(cls);
                        if (cls.ErrorID != 0)
                        {
                            return dal.ErrorID;
                        }
                    }
                }


                // krijimi automatik i normativave
                if (cls.CreateAutomaticRealization)
                {
                    dal.CreateAutomaticRealization(cls.ID, 3, cls.ReferenceID, cls.DepartmentID, cls.InsBy);
                    if (dal.ErrorID != 0)
                    {
                        cls.ErrorDescription = cls.ErrorDescription;
                        return dal.ErrorID;
                    }
                }



                // azhurimi i cmimeve te asemblimeve
                dal.UpdateAveragePriceForAssembly(cls.InsBy);
                if (dal.ErrorID != 0)
                {
                    cls.ErrorDescription = cls.ErrorDescription;
                    return dal.ErrorID;
                }

                // azhurimi i transaksioneve
                dal.CreateBatchJournals(cls.InsBy);
                if (dal.ErrorID != 0)
                {
                    cls.ErrorDescription = cls.ErrorDescription;
                    return dal.ErrorID;
                }
            }
            return dal.ErrorID;
        }

        #endregion

        #region SelectAll

        public static List<Transactions> SelectAll(string FromDate, string ToDate, bool Active, string Type, string DepartmentID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.SelectAll(FromDate, ToDate, Active, Type, DepartmentID);
        }

        #endregion

        #region SelectByID

        public static Transactions SelectByID(Transactions cls)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.SelectByID(cls);
        }

        public static Transactions SelectByTransactionNo(Transactions cls)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.SelectByTransactionNo(cls);
        }

        public static DataTable SelectDataTableByID(int TranID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.SelectDataTableByID(TranID);
        }

        #endregion

        #region TransactionsByTableID

        public static Transactions TransactionsByTableID(int TableID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.TransactionsByTableID(TableID);
        }

        #endregion

        #region TransactionTypeList

        public static DataTable TransactionTypeList()
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.TransactionTypeList();
        }

        #endregion

        #region SelectAllTogether

        public static List<Transactions> SelectAllTogether(string FromDate, string ToDate, bool Active, string Type, string DepartmentID)
        {
            List<Transactions> tParent = new TransactionsRepository().SelectAll(FromDate, ToDate, Active, Type, DepartmentID);
            //List<TransactionsDetails> tChild = BLLTransactionsDetails.SelectAll();

            //foreach (Transactions tran in tParent)
            //{
            //    tran.TranDetailsColl = tChild.FindAll(d => d.TransactionID == tran.ID);
            //}

            return tParent;
        }

        #endregion

        #region GetTransaction

        public static DataTable GetTransaction(string FromDate, string ToDate, bool Active, string Type, string DepartmentID,int UserID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            DataTable dt;

            if (Type == "Blerje" || Type == "Shitje" || Type == "Profature Sh" || Type == "Profature B" || Type == "KthimB" || Type == "KthimS")
            {
                dt = dal.GetTransactionForAll(FromDate, ToDate, Active, Type, DepartmentID, UserID);
            }
            else
            {
                dt = dal.GetTransaction(FromDate, ToDate, Active, Type, DepartmentID, UserID);
            }
         


            return dt;
        }
        public static DataTable GetOrdersList(string FromDate, string ToDate, string DepartmentID, int UserID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            DataTable dt;
            dt = dal.GetOrdersList(FromDate, ToDate,  DepartmentID, UserID);

            return dt;
        }
        #endregion

        #region RegistrationsList

        public static DataTable RegistrationsList(string FromDate, string ToDate, bool Active, string DepartmentID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.RegistrationsList(FromDate, ToDate, Active, DepartmentID);
        }

        #endregion

        #region GetRegistrations

        public static DataTable GetRegistrations(string FromDate, string ToDate, string DepartmentID,int UserID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.GetRegistrations(FromDate, ToDate, DepartmentID,UserID);
        }

        #endregion

        #region GetTranIntern

        public static DataTable GetTranIntern(string FromDate, string ToDate, string DepartmentID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.GetTranIntern(FromDate, ToDate, DepartmentID);
        }

        #endregion

        #region GetImport

        public static DataTable GetImport(string FromDate, string ToDate, string DepartmentID, bool Active)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.GetImport(FromDate, ToDate,DepartmentID, Active);
        }

        #endregion

        #region GetTransactionPayments

        public static DataTable GetTransactionPayments(string FromDate, string ToDate, bool Active, string Type)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.GetTransactionPayments(FromDate, ToDate, Active, Type);
        }

        #endregion

        #region AutomaticSync

        public static int AutomaticSync()
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.AutomaticSync();
        }

        #endregion

        

        #region GetTransactionNo

        public static string GetTransactionNo(int TransactionType, DateTime Date, int DepartmentID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.GetTransactionNo(TransactionType, Date, DepartmentID);
        }

        #endregion

        #region GetTransactionNo2

        public static string GetTransactionNo2(int TransactionType, DateTime Date)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.GetTransactionNo2(TransactionType, Date);
        }

        #endregion

        #region GetTransactionNoForBankJournal

        public static string GetTransactionNoForBankJournal(int TransactionType, DateTime Date)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.GetTransactionNoForBankJournal(TransactionType, Date);
        }

        #endregion

        #region UpdatePayment

        public void UpdatePayment(int TransactionID)
        {
            //GlobaTran = new DALTransactions(true);
            GlobaTran.UpdatePayment(TransactionID);
        }

        #endregion

        #region GetTransactionIDs

        public static DataTable GetTransactionIDs(string TranID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.GetTransactionIDs(TranID);
        }

        #endregion

        #region GetDueReport

        public static DataTable GetDueReport(int TranID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.GetDueReport(TranID);
        }

        #endregion

        #region GetDueReportDet

        public static DataTable GetDueReportDet(int TransType, int PartnerID, short DueReport)
        {
            string strTransType = "Shitje";
            switch (TransType)
            {
                case 1:
                    strTransType = "Blerje";
                    break;
                case 2:
                    strTransType = "Shitje";
                    break;
            }
            TransactionsRepository dal = new TransactionsRepository();
            return dal.GetDueReportDet(strTransType, PartnerID, DueReport);
        }

        #endregion

        #region UpdateAveragePrice

        //public static void UpdateAveragePrice(string ItemID, int DepartmentID, int TransactionID, DateTime TransactionDate,int UserID)
        //{
        //    GlobaTran = new DALTransactions(true);
        //    GlobaTran.UpdateAveragePrice(ItemID, DepartmentID, TransactionID, TransactionDate, UserID);
        //}

        #endregion

        #region UpdateAveragePriceForGroup

        public static void UpdateAveragePriceForGroup(DataTable Items, DateTime TransactionDate, int UserID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            dal.UpdateAveragePriceForGroup(Items, TransactionDate,UserID);
        }

        #endregion

        #region TransactionsByTerminForPOS

        public static DataTable TransactionsByTerminForPOS(DateTime FromDate, DateTime ToDate, int TerminID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.TransactionsByTerminForPOS(FromDate,ToDate,TerminID);
        }

        #endregion

        #region GetUserTransactions

        public static DataTable GetUserTransactions(int UserID, int DepartmentID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.GetUserTransactions(UserID, DepartmentID);
        }

        #endregion

        #region GetAllTransactions

        public static DataTable GetAllTransactions(string FromDate, string ToDate)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.GetAllTransactions(FromDate, ToDate);
        }

        #endregion

        #region TransferTransactions

        public void TransferTransactions(DataTable dt, Transactions cls)
        {
            //GlobaTran = new DALTransactions(true);
            GlobaTran.TransferTransactions(dt, cls);
        }

        #endregion

        #region TranNo

        public static bool CheckForTranNo(string TranNo, int TypeID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.CheckForTranNo(TranNo, TypeID);
        }

        #endregion

        #region GetPaymentCard

        public static DataTable GetPaymentCard(int TranID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.GetPaymentCard(TranID);
        }

        #endregion

        #region UpdateNewCash

        public static DataTable UpdateNewCash(int DetailsID, int OldTransactionID, int NewTransactionID, int userID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.UpdateNewCash(DetailsID,OldTransactionID, NewTransactionID, userID);
        }

        #endregion

        #region AllowReplication

        public static void AllowReplication(DataTable Transactions, bool Value)
        {
            TransactionsRepository dal = new TransactionsRepository();
            dal.AllowReplication(Transactions, Value);
        }

        #endregion

        #region RegistrationReport

        public static DataTable RegistrationReport(int RegistrationID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.RegistrationReport(RegistrationID);
        }

        #endregion

        #region RegistrationProccess

        public static DataTable RegistrationProccess(int RegistrationID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.RegistrationProccess(RegistrationID);
        }

        #endregion

        #region CreateRegistration

        public static void CreateRegistration(int ID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            dal.CreateRegistration(ID);
        }

        #endregion

        public static int CashJournalIDByCashAccount(string CashAccount, int TypeID, int CompanyID)
        {
            return TransactionsRepository.CashJournalIDByCashAccount(CashAccount, TypeID, CompanyID);
        }

        public void UpdateJournalStatus(Transactions cls)
        {
            //GlobaTran = new DALTransactions(true);
            GlobaTran.UpdateJournalStatus(cls);
        }

        #region UpdateDiscountBatch

        public static void UpdateDiscountBatch(DataTable Transactions, decimal Discount)
        {
            TransactionsRepository dal = new TransactionsRepository();
            dal.UpdateDiscountBatch(Transactions, Discount);
        }

        #endregion

        #region ReRealisation

        public static void ReRealisation(DataTable Transactions, DateTime FromDate, DateTime ToDate,int UserID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            dal.ReRealisation(Transactions, FromDate, ToDate,UserID);
        }
        
        #endregion

        #region GetTransatcionsByItemID

        public static DataTable GetTransatcionsByItemID(DataTable ItemID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.GetTransatcionsByItemID(ItemID);
        }

        #endregion

        public static bool CheckIfRegExists(int RegID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.CheckIfRegExists(RegID);
        }

        public static DataTable GetOrdersByLocationID(int LocationID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.GetOrdersByLocationID(LocationID);
        }

        public static DataTable GetOrdersForUser(int DepartmentID, DataTable Locations, int Status)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.GetOrdersForUser(DepartmentID, Locations, Status);
        }

        public static DataTable GetDataTableForFiscal(int TransactionID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.GetDataTableForFiscal(TransactionID);
        }

        public static DataTable GetHeaderFromDoc(int TransactionID)
        {
            return TransactionsRepository.GetHeaderFromDoc(TransactionID);
        }

        public static DataTable GetDetailsFromDoc(int TransactionID)
        {
            return TransactionsRepository.GetDetailsFromDoc(TransactionID);

        }
        #region GetRegistrationsForPeriod

        public static DataTable GetRegistrationsForPeriod(DateTime FromDate, DateTime ToDate)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.GetRegistrationsForPeriod(FromDate, ToDate);
        }
        #endregion

        #region ChangeTransactionTypeID

        public static void ChangeTransactionTypeID(int ID, int TransactionTypeID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            dal.ChangeTransactionTypeID(ID, TransactionTypeID);
        }
        #endregion

        #region CheckForFiscalInvoice

        public static bool CheckForFiscalInvoice(int TranID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.CheckForFiscalInvoice(TranID);
        }

        #endregion

        #region UpdatePOSPaid

        public static void UpdatePOSPaid(int TransactionID, bool POSPaid)
        {
            TransactionsRepository dal = new TransactionsRepository();
            dal.UpdatePOSPaid(TransactionID, POSPaid);
        }

        #endregion

        public void CloseGlobalConnection()
        {
            GlobaTran.CloseGlobalConnection();
        }

        #region CheckForPayment

        public static bool CheckForPayment(int TransactionID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.CheckForPayment(TransactionID);
        }

        #endregion

        #region GetReservations

        public static DataTable GetReservations(DateTime resDate)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.GetReservations(resDate);
        }

        #endregion

        public static int GetTransactionIDIfExist(string TranNo, int partnerID, string TranDate)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.GetTarnsactionIDIfExist(TranNo, partnerID, TranDate);
        }
        #region CheckTransactionIfExists

        public static bool CheckTransactionIfExists(Transactions cls)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.CheckTransactionIfExists(cls);

        }
        public static List<XMLTransactionDetails> M_GetSalesArticles(DateTime FromDate, DateTime ToDate, int DepartmentID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.M_GetSalesArticles(FromDate, ToDate, DepartmentID);
        }
        #endregion
        
        #region GetInvoiceNo

        public static string GetInvoiceNo(int TransactionType, DateTime Date,int DepartmentID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.GetInvoiceNo(TransactionType, Date,DepartmentID);
        }

        #endregion

        public static string GetTransactionNo_M(int TransactionType, DateTime Date, int DepartmentID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.GetTransactionNo_M(TransactionType, Date, DepartmentID);
        }

        public static string GetMerchTransactionNo()
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.GetMerchTransactionNo();
        }

        #region TranID

        public static int GetTranID(int Type, int PartnerID, string TransactionNo, string tranDate)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.GetTranID(Type, PartnerID,TransactionNo,tranDate);
        }

        public static int GetTranID_NotaKreditore(int Type, int PartnerID, string TransactionNo, string tranDate)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.GetTranID_NotaKreditore(Type, PartnerID, TransactionNo, tranDate);
        }

        #endregion
        public static List<XMLTransactionDetails> M_GetSalesArticles_F(string FlightNo)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.M_GetSalesArticles_F(FlightNo);
        }
        public static XMLTransactions M_GetXMLTransactionByID(int TransactionID)
        { 
            TransactionsRepository dal=new TransactionsRepository();
            return dal.SelectXMLTransactionByID(TransactionID);
        }
        public static List<XMLTransactionDetails> M_GetDetailsByTranID(int TransactionID)
        {
            TransactionsDetailsRepository dal = new TransactionsDetailsRepository();
            return dal.GetXMLDetailsList(TransactionID);
        }


       
        public static void UpdateIsPrintFiscalInvoiceAsTrueByTranID(int TransactionsID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            dal.UpdateIsPrintFiscalInvoiceAsTrueByTranID(TransactionsID);
        }

        #region GetInvoiceNo_NAV

        public static string GetInvoiceNo_NAV(int TransactionType, DateTime Date, int DepartmentID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.GetInvoiceNo_NAV(TransactionType, Date, DepartmentID);
        }

        #endregion


        public static int OrdersBatchInsert(DataTable Orders)
        {
            TransactionsRepository dal = new TransactionsRepository();
             return dal.OrdersBatchInsert(Orders);
        }
    }  
}