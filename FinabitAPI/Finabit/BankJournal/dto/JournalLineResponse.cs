using FinabitAPI.Finabit.Transaction.dto;

namespace FinabitAPI.BankJournal.dto
{

    public static class JournalLineResponse
    {
        public static object From(Transactions header, TransactionsDetails line, DateTime dateUsed) => new
        {
            ok = true,
            cashJournalId = header.ID,
            transactionTypeId = header.TransactionTypeID,
            dateUsed = dateUsed.ToString("yyyy-MM-dd"),
            departmentId = header.DepartmentID,
            cashAccount = header.CashAccount,
            transactionNo = header.TransactionNo,
            invoiceNo = header.InvoiceNo,
            insertedLines = 1,
            line = new
            {
                detailsType = line.DetailsType,
                signedAmount = line.Value,
                description = line.ItemName,
                linkedPaymentID = line.PaymentID
            }
        };
    }
}