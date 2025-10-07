using FinabitAPI.BankJournal.dto;
using FinabitAPI.Core.Global;
using FinabitAPI.Core.Global.dto;
using FinabitAPI.Core.Utilis;
using FinabitAPI.dto;
using FinabitAPI.Finabit.Transaction.dto;
using FinabitAPI.Service;
using FinabitAPI.Utilis;
using Microsoft.AspNetCore.Mvc;

namespace FinabitAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankJournalController : ControllerBase
    {
        private readonly DBAccess _db;

        private TransactionsService transactionsService => new TransactionsService(_db);

        public BankJournalController(DBAccess db)
        {
            _db = db;
        }

        [HttpPost("Line")]
        public IActionResult AddLine([FromBody] JournalLineRequestDto req)
        {
            if (req == null) return BadRequest("Body required.");
            if (req.DepartmentID <= 0) return BadRequest("DepartmentID required.");
            if (string.IsNullOrWhiteSpace(req.CashAccount)) return BadRequest("CashAccount required.");
            if (req.Amount == 0) return BadRequest("Amount cannot be zero.");
            if (req.DetailsType is not (3 or 4 or 5 or 6)) return BadRequest("DetailsType must be 3,4,5,6.");

            var date = (req.Date?.Date) ?? DateTime.Today;

            // company/department
            var dep = _db.SelectDepartmentByID(new Department { ID = req.DepartmentID });
            if (dep == null || dep.ID == 0) return BadRequest("Department not found.");

            const int typeId = 24; // BANK
            var cashAcc = req.CashAccount.Trim();

            // find or create BANK header
            int headerId = _db.CashJournalIDByCashAccount(cashAcc, typeId, date, dep.CompanyID);
            if (headerId == 0)
            {
                var txnNo = !string.IsNullOrWhiteSpace(req.TransactionNo)
                    ? req.TransactionNo!.Trim()
                    : transactionsService.GetTransactionNo(typeId, date, req.DepartmentID);

                var invNo = !string.IsNullOrWhiteSpace(req.InvoiceNo)
                    ? req.InvoiceNo!.Trim()
                    : txnNo;

                var header = BuildJournalHeader(typeId, dep.CompanyID, req.DepartmentID, date, cashAcc, txnNo, invNo);

                var svcIns = new TransactionsService(_db);
                try { header.ErrorID = svcIns.Insert(header, false); }
                finally { svcIns.CloseGlobalConnection(); }

                if (header.ErrorID != 0 || header.ID <= 0)
                    return StatusCode(500, "Failed to create bank header.");

                headerId = header.ID;
            }

            // load header (use the same selector style as in Cash controller for consistency)
            var hdr = _db.SelectTransactionByID(new Transactions { ID = headerId });
            if (hdr == null || hdr.ID == 0) return NotFound("Bank header not found.");

            // build + append detail (now includes AccountCode like in Cash)
            var det = BuildDetail(req, "Lëvizje banke");
            det.TransactionID = hdr.ID;

            hdr.TranDetailsColl = new List<TransactionsDetails> { det };

            var svcUpd = new TransactionsService(_db);
            try
            {
                var err = svcUpd.Update(hdr);
                if (err != 0) return StatusCode(500, $"Update failed. ErrorID={err}");
            }
            finally
            {
                svcUpd.CloseGlobalConnection();
            }

            var latest = _db.SelectTransactionByID(new Transactions { ID = headerId }) ?? hdr;

            return Ok(new
            {
                ok = true,
                cashJournalId = latest.ID,
                transactionTypeId = latest.TransactionTypeID,
                dateUsed = date.ToString("yyyy-MM-dd"),
                departmentId = latest.DepartmentID,
                cashAccount = latest.CashAccount,
                transactionNo = latest.TransactionNo,
                invoiceNo = latest.InvoiceNo,
                insertedLines = 1,
                line = new
                {
                    detailsType = det.DetailsType,
                    signedAmount = det.Value,
                    description = det.ItemName,
                    linkedPaymentID = det.PaymentID,
                    itemId = det.ItemID
                }
            });
        }

        [HttpGet("Header")]
        public IActionResult GetHeader([FromQuery] int departmentId, [FromQuery] string cashAccount, [FromQuery] DateTime date)
        {
            if (departmentId <= 0) return BadRequest("departmentId required.");
            if (string.IsNullOrWhiteSpace(cashAccount)) return BadRequest("cashAccount required.");

            var dep = _db.SelectDepartmentByID(new Department { ID = departmentId });
            if (dep == null || dep.ID == 0) return BadRequest("Department not found.");

            int id = _db.CashJournalIDByCashAccount(cashAccount.Trim(), 24, date.Date, dep.CompanyID);
            if (id == 0) return NotFound("Header not found.");

            var hdr = _db.SelectTransactionByID(new Transactions { ID = id });
            if (hdr == null || hdr.ID == 0) return NotFound("Header not found.");

            return Ok(hdr);
        }

        private Transactions BuildJournalHeader(
            int typeId,
            int companyId,
            int departmentId,
            DateTime date,
            string cashAccount,
            string transactionNo,
            string invoiceNo)
        {
            // Keep same behavior as Cash: always get the system transaction number
            transactionNo = transactionsService.GetTransactionNo(typeId, date, departmentId);

            return new Transactions
            {
                CompanyID = companyId,
                DepartmentID = departmentId,
                TransactionTypeID = typeId,
                TransactionDate = date,
                InvoiceDate = date,
                DueDate = date,
                CashAccount = cashAccount,
                Memo = (typeId == 24 ? "Ditar Banke" : "Ditar Arke"),
                InsBy = GlobalAppData.UserID,
                Active = true,
                TransactionNo = transactionNo,
                InvoiceNo = invoiceNo
            };
        }

        private static string ResolveItemIdFromShifra(
            int detailsType,
            int? partnerId,
            string? partnerCode,
            string? accountCode,
            string? description)
        {
            if (detailsType == 3 || detailsType == 4)
            {
                if (partnerId.HasValue && partnerId.Value > 0) return partnerId.Value.ToString();
                if (!string.IsNullOrWhiteSpace(partnerCode)) return partnerCode.Trim();
            }
            else if (detailsType == 5 || detailsType == 6)
            {
                if (!string.IsNullOrWhiteSpace(accountCode)) return accountCode.Trim();
            }

            return !string.IsNullOrWhiteSpace(description) ? description.Trim() : "Lëvizje";
        }

        private TransactionsDetails BuildDetail(JournalLineRequestDto req, string fallbackDesc)
        {
            var sign = (req.DetailsType == 3 || req.DetailsType == 6) ? -1m : 1m;
            var value = Math.Abs(req.Amount) * sign;

            var desc = string.IsNullOrWhiteSpace(req.Description) ? fallbackDesc : req.Description.Trim();

            return new TransactionsDetails
            {
                DetailsType = req.DetailsType,
                ItemID = ResolveItemIdFromShifra(
                    req.DetailsType, req.PartnerID, req.PartnerCode, req.AccountCode, req.Description),
                ItemName = desc,
                Quantity = 1,
                Price = value,
                Value = value,
                Mode = 1,
                PaymentID = req.PaymentID.GetValueOrDefault(),
                AccountCode = string.IsNullOrWhiteSpace(req.AccountCode) ? "" : req.AccountCode.Trim()
            };
        }

        [HttpPost("Lines")]
        public IActionResult AddLines([FromBody] List<JournalLineRequestDto> lines)
        {
            if (lines == null || lines.Count == 0)
                return BadRequest("At least one line is required.");

            const int typeId = 24; 
            var results = new List<object>();
            int okCount = 0;

            for (int i = 0; i < lines.Count; i++)
            {
                var req = lines[i];

                if (req == null)
                {
                    results.Add(new { index = i, ok = false, error = "Body required." });
                    continue;
                }
                if (req.DepartmentID <= 0)
                {
                    results.Add(new { index = i, ok = false, error = "DepartmentID required." });
                    continue;
                }
                if (string.IsNullOrWhiteSpace(req.CashAccount))
                {
                    results.Add(new { index = i, ok = false, error = "CashAccount required." });
                    continue;
                }
                if (req.Amount == 0)
                {
                    results.Add(new { index = i, ok = false, error = "Amount cannot be zero." });
                    continue;
                }
                if (req.DetailsType is not (3 or 4 or 5 or 6))
                {
                    results.Add(new { index = i, ok = false, error = "DetailsType must be 3,4,5,6." });
                    continue;
                }

                var date = (req.Date?.Date) ?? DateTime.Today;

                try
                {
                    var dep = _db.SelectDepartmentByID(new Department { ID = req.DepartmentID });
                    if (dep == null || dep.ID == 0)
                    {
                        results.Add(new { index = i, ok = false, error = "Department not found." });
                        continue;
                    }

                    var cashAcc = req.CashAccount.Trim();

                    int headerId = _db.CashJournalIDByCashAccount(cashAcc, typeId, date, dep.CompanyID);
                    if (headerId == 0)
                    {
                        var txnNo = !string.IsNullOrWhiteSpace(req.TransactionNo)
                            ? req.TransactionNo!.Trim()
                            : transactionsService.GetTransactionNo(typeId, date, req.DepartmentID);

                        var invNo = !string.IsNullOrWhiteSpace(req.InvoiceNo)
                            ? req.InvoiceNo!.Trim()
                            : txnNo;

                        var header = BuildJournalHeader(typeId, dep.CompanyID, req.DepartmentID, date, cashAcc, txnNo, invNo);

                        var svcIns = new TransactionsService(_db);
                        try { header.ErrorID = svcIns.Insert(header, false); }
                        finally { svcIns.CloseGlobalConnection(); }

                        if (header.ErrorID != 0 || header.ID <= 0)
                        {
                            results.Add(new { index = i, ok = false, error = "Failed to create bank header." });
                            continue;
                        }

                        headerId = header.ID;
                    }

                    var hdr = _db.SelectTransactionByID(new Transactions { ID = headerId });
                    if (hdr == null || hdr.ID == 0)
                    {
                        results.Add(new { index = i, ok = false, error = "Bank header not found." });
                        continue;
                    }

                    var det = BuildDetail(req, "Lëvizje banke");
                    det.TransactionID = hdr.ID;

                    hdr.TranDetailsColl = new List<TransactionsDetails> { det };

                    var svcUpd = new TransactionsService(_db);
                    try
                    {
                        var err = svcUpd.Update(hdr);
                        if (err != 0)
                        {
                            results.Add(new { index = i, ok = false, error = $"Update failed. ErrorID={err}" });
                            continue;
                        }
                    }
                    finally
                    {
                        svcUpd.CloseGlobalConnection();
                    }

                    var latest = _db.SelectTransactionByID(new Transactions { ID = headerId }) ?? hdr;
                    
                    results.Add(new
                    {
                        index = i,
                        ok = true,
                        cashJournalId = latest.ID,
                        transactionTypeId = latest.TransactionTypeID,
                        dateUsed = date.ToString("yyyy-MM-dd"),
                        departmentId = latest.DepartmentID,
                        cashAccount = latest.CashAccount,
                        transactionNo = latest.TransactionNo,
                        invoiceNo = latest.InvoiceNo,
                        insertedLines = 1,
                        line = new
                        {
                            detailsType = det.DetailsType,
                            signedAmount = det.Value,
                            description = det.ItemName,
                            linkedPaymentID = det.PaymentID,
                            itemId = det.ItemID
                        }
                    });

                    okCount++;
                }
                catch (Exception ex)
                {
                    results.Add(new { index = i, ok = false, error = ex.Message });
                }
            }

            return Ok(new
            {
                ok = okCount == results.Count(r => true),
                insertedLines = okCount,
                total = results.Count,
                results
            });
        }

   }
}
