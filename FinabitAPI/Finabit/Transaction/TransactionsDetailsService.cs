//-- =============================================
//-- Author:		Gazmend Mehmeti
//-- Create date: 07.05.09 7:32:25 PM
//-- Description:	CRUD for Table tblTransactionsDetails
//-- =============================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using FinabitAPI.Finabit.Transaction.dto;

namespace FinabitAPI.Finabit.Transaction
{
    public class TransactionsDetailsService
    {
        TransactionsRepository GlobalTran = new TransactionsRepository();
        public TransactionsDetailsService(TransactionsRepository _GlobalTran)
        {
            GlobalTran = _GlobalTran;
        }

        public TransactionsDetailsService()
        {
        }

        #region Insert

        public void Insert(TransactionsDetails cls,bool fillItemLotTable)
        {
            TransactionsDetailsRepository dal = new TransactionsDetailsRepository(GlobalTran);
            dal.Insert(cls, fillItemLotTable);
        }

        #endregion

        #region Update

        public void Update(TransactionsDetails cls)
        {
            TransactionsDetailsRepository dal = new TransactionsDetailsRepository(GlobalTran);
            dal.Update(cls);
        }

        #endregion

        #region Delete

        public void Delete(TransactionsDetails cls)
        {
            TransactionsDetailsRepository dal = new TransactionsDetailsRepository(GlobalTran);
            dal.Delete(cls);
        }

        #endregion

        #region Delete

        public void DeleteALL(TransactionsDetails cls)
        {
            TransactionsDetailsRepository dal = new TransactionsDetailsRepository(GlobalTran);
            dal.DeleteAll(cls);
        }

        #endregion

        #region SelectAll

        public static List<TransactionsDetails> SelectAll()
        {
            TransactionsDetailsRepository dal = new TransactionsDetailsRepository();
            return dal.SelectAll();
        }

        #endregion

        #region SelectAllTable

        public static DataTable SelectAllTable()
        {
            TransactionsDetailsRepository dal = new TransactionsDetailsRepository();
            return dal.SelectAllTable();
        }

        #endregion

        #region SelectByID

        public static TransactionsDetails SelectByID(TransactionsDetails cls)
        {
            TransactionsDetailsRepository dal = new TransactionsDetailsRepository();
            return dal.SelectByID(cls);
        }

        #endregion

        #region SelectPaymentDetail

        public static TransactionsDetails SelectPaymentDetail(int TransactionID, int PaymentID)
        {
            TransactionsDetailsRepository dal = new TransactionsDetailsRepository();
            return dal.SelectPaymentDetail(TransactionID, PaymentID);
        }

        #endregion

        #region GetDetailsSchema

        public static DataTable GetDetailsSchema(int ID)
        {
            return new TransactionsDetailsRepository().GetDetailsSchema(ID);
        }

        #endregion

        #region GetDetailsTranBackSchema

        public static DataTable GetDetailsTranBackSchema(int ID)
        {
            return new TransactionsDetailsRepository().GetDetailsTranBackSchema(ID);
        }

        #endregion

        #region GetDetails

        public static DataTable GetDetails(int TransactionID)
        {
            return new TransactionsDetailsRepository().GetDetails(TransactionID);
        }

        #endregion

        #region GetDetailsForFiscal

        public static DataTable GetDetailsForFiscal(int TransactionID)
        {
            return new TransactionsDetailsRepository().GetDetailsForFiscal(TransactionID);
        }

        #endregion

        #region GetDetailsForFiscal_Eachorder

        public static DataTable GetDetailsForFiscal_Eachorder(int TransactionID)
        {
            return new TransactionsDetailsRepository().GetDetailsForFiscal_Eachorder(TransactionID);
        }

        #endregion

        #region UpdateIsPrintFiscalInvoiceAsFalse(DetailID)

        public static void UpdateIsPrintFiscalInvoiceAsFalse(int DetailID)
        {
             TransactionsDetailsRepository.UpdateIsPrintFiscalInvoiceAsFalse(DetailID);
        }

        #endregion

        #region UpdateTranDetFields(TranID)

        public  void UpdateTranDetFields(int TranID)
        {
            TransactionsDetailsRepository dal = new TransactionsDetailsRepository(GlobalTran);
            dal.UpdateTranDetFields(TranID);
        }

        #endregion

        #region GetDetailsByTranColl

        public static DataTable GetDetailsByTranColl(DataTable TranColl)
        {
            return new TransactionsDetailsRepository().GetDetailsByTranColl(TranColl);
        }

        #endregion

        #region GetDetailsReport

        public static DataTable GetDetailsReport(int TransactionID)
        {
            return new TransactionsDetailsRepository().GetDetailsReport(TransactionID);
        }

        #endregion

        #region GetDetailsList

        public static List<TransactionsDetails> GetDetailsList(int TransactionID)
        {
            return new TransactionsDetailsRepository().GetDetailsList(TransactionID);
        }

        #endregion
        
        #region GetDetailsPOS

        public static DataTable GetDetailsPOS(List<int> tag)
        {
            return new TransactionsDetailsRepository().GetDetailsPOS(tag);
        }

        #endregion

        #region GetDetailsBack

        public static DataTable GetDetailsBack(int TransactionID)
        {
            return new TransactionsDetailsRepository().GetDetailsBack(TransactionID);
        }

        #endregion

        #region GetDetailsForPayments

        public DataTable GetDetailsForPayments(int TransactionID)
        {
            return new TransactionsDetailsRepository(GlobalTran).GetDetailsForPayments(TransactionID);
        }

        public static DataTable GetDetailsForPayments2(int TransactionID)
        {
            return new TransactionsDetailsRepository().GetDetailsForPayments2(TransactionID);
        }

        #endregion

        public static DataTable GetImportDetailsSchema(int ID)
        {
            return new TransactionsDetailsRepository().GetImportDetailsSchema(ID);
        }

        #region GetDetailsForImports

        public static DataTable GetDetailsForImports(int TransactionID)
        {
            return new TransactionsDetailsRepository().GetDetailsForImports(TransactionID);
        }

        #endregion

        #region GetDetailsForImportsReport

        public static DataTable GetDetailsForImportsReport(int TransactionID)
        {
            return new TransactionsDetailsRepository().GetDetailsForImportsReport(TransactionID);
        }

        #endregion

        #region Receipt

        public static DataTable Receipt(int DetailID)
        {
            TransactionsDetailsRepository dal = new TransactionsDetailsRepository();
            return dal.Receipt(DetailID);
        }

        #endregion

        #region UpdateOrderStatus

        public void UpdateOrderStatus(int DetailsID)
        {
            TransactionsDetailsRepository dal = new TransactionsDetailsRepository();
            dal.UpdateOrderStatus(DetailsID);
        }

        #endregion

        #region GetDetailsReportSubOrder

        public static DataTable GetDetailsReportSubOrder(int TransactionID, bool PrintAll)
        {
            return new TransactionsDetailsRepository().GetDetailsReportSubOrder(TransactionID, PrintAll);
        }

        #endregion

        #region GetDetailsReportSubOrder

        public static void MakeSubOrderReceieved(int TransactionID)
        {
            new TransactionsDetailsRepository().MakeSubOrderreceived(TransactionID);
        }

        #endregion

        public static int GetLastSubOrder(int TransactionID)
        {
            return TransactionsDetailsRepository.GetLastSubOrder(TransactionID);
        }
        public static int GetDetailForPayment(int TransactionID)
        {
            return TransactionsDetailsRepository.GetDetailForPayment(TransactionID);
        }
        public static DataTable GetUnprintedDetails(int TransactionID)
        {
            return new TransactionsDetailsRepository().GetUnprintedDetails(TransactionID);
        }
        public static void MakeTransactionPrinted(int TransactionID)
        {
            TransactionsDetailsRepository dal = new TransactionsDetailsRepository();
            dal.MakeTransactionPrinted(TransactionID);
        }


       
        public static void UpdateWebHostNameStatus()
        {

            TransactionsDetailsRepository.UpdateWebHostNameStatus();


        }
        #region GetDetailsForFiscal_Eachorder_Mobile

        public static DataTable GetDetailsForFiscal_Eachorder_Mobile(int TransactionID, bool isOrder)
        {
            return new TransactionsDetailsRepository().GetDetailsForFiscal_Eachorder_Mobile(TransactionID, isOrder);
        }

        #endregion
    }
}