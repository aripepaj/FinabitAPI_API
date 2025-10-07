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
    public class CashJournalController : ControllerBase
    {
        private readonly DBAccess _db;
        private TransactionsService transactionsService => new TransactionsService(_db);

        public CashJournalController(DBAccess db)
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

            var dep = _db.SelectDepartmentByID(new Department { ID = req.DepartmentID });
            if (dep == null || dep.ID == 0) return BadRequest("Department not found.");

            var typeId = 25;
            int headerId = _db.CashJournalIDByCashAccount(req.CashAccount.Trim(), 25, date, dep.CompanyID);
            if (headerId == 0)
            {
                var txnNo = !string.IsNullOrWhiteSpace(req.TransactionNo)
                ? req.TransactionNo!.Trim()
                : transactionsService.GetTransactionNo(typeId, date, req.DepartmentID);

                var invNo = !string.IsNullOrWhiteSpace(req.InvoiceNo)
                ? req.InvoiceNo!.Trim()
                : txnNo;

                var header = BuildJournalHeader(typeId, dep.CompanyID, req.DepartmentID, date, req.CashAccount, txnNo, invNo);
                var svcIns = new TransactionsService(_db);
                try { header.ErrorID = svcIns.Insert(header, false); }
                finally { svcIns.CloseGlobalConnection(); }

                if (header.ErrorID != 0 || header.ID <= 0)
                    return StatusCode(500, "Failed to create cash header.");

                headerId = header.ID;
            }

            var hdr = _db.SelectTransactionByID(new Transactions { ID = headerId });
            if (hdr == null || hdr.ID == 0) return NotFound("Cash header not found.");

            var det = BuildDetail(req, "Lëvizje arke");
            det.TransactionID = hdr.ID;
            hdr.TranDetailsColl = new List<TransactionsDetails> { det };

            var svcUpd = new TransactionsService(_db);
            try
            {
                var err = svcUpd.Update(hdr);
                if (err != 0) return StatusCode(500, $"Update failed. ErrorID={err}");
            }
            finally { svcUpd.CloseGlobalConnection(); }

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
                    linkedPaymentID = det.PaymentID
                }
            });
        }

        private string ResolveItemIdFromShifra(JournalLineRequestDto req)
        {
            // For 3/4 we prefer PartnerID/PartnerCode; for 5/6 we use AccountCode
            if (req.DetailsType is 3 or 4)
            {
                if (req.PartnerID.HasValue && req.PartnerID.Value > 0)
                    return req.PartnerID.Value.ToString();

                if (!string.IsNullOrWhiteSpace(req.PartnerCode))
                {
                    // If you have a repo method: var id = PartnerRepository.SelectByCode(req.PartnerCode);
                    // if (id > 0) return id.ToString();
                    return req.PartnerCode.Trim(); // fallback: keep code text
                }
            }
            else if (req.DetailsType is 5 or 6)
            {
                if (!string.IsNullOrWhiteSpace(req.AccountCode))
                    return req.AccountCode.Trim();
            }

            // fallback: use description as ItemID
            return !string.IsNullOrWhiteSpace(req.Description) ? req.Description.Trim() : "Lëvizje";
        }

        [HttpGet("Header")]
        public IActionResult GetHeader([FromQuery] int departmentId, [FromQuery] string cashAccount, [FromQuery] DateTime date)
        {
            if (departmentId <= 0) return BadRequest("departmentId required.");
            if (string.IsNullOrWhiteSpace(cashAccount)) return BadRequest("cashAccount required.");

            var dep = _db.SelectDepartmentByID(new Department { ID = departmentId });
            if (dep == null || dep.ID == 0) return BadRequest("Department not found.");

            int id = _db.CashJournalIDByCashAccount(cashAccount.Trim(), 25, date.Date, dep.CompanyID);
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

        private TransactionsDetails BuildDetail(JournalLineRequestDto req, string fallbackDesc)
        {
            var sign = (req.DetailsType == 3 || req.DetailsType == 6) ? -1m : 1m;
            var value = Math.Abs(req.Amount) * sign;

            string desc = string.IsNullOrWhiteSpace(req.Description) ? fallbackDesc : req.Description.Trim();

            return new TransactionsDetails
            {
                DetailsType = req.DetailsType,
                ItemID = ResolveItemIdFromShifra(req.DetailsType, req.PartnerID, req.PartnerCode, req.AccountCode, req.Description),
                ItemName = desc,
                Quantity = 1,
                Price = value,
                Value = value,
                Mode = 1,
                PaymentID = req.PaymentID.GetValueOrDefault(),
                AccountCode = string.IsNullOrWhiteSpace(req.AccountCode) ? "" : req.AccountCode.Trim()
            };
        }

        // [HttpPost("Import")]
        // public IActionResult ImportCash([FromBody] JournalImportRequest req)
        // {
        //     if (req == null) return BadRequest("Body required.");
        //     if (req.DepartmentID <= 0) return BadRequest("DepartmentID required.");
        //     if (req.Lines == null || req.Lines.Count == 0) return BadRequest("Lines required.");

        //     var dep = _db.SelectDepartmentByID(new Department { ID = req.DepartmentID });
        //     if (dep == null || dep.ID == 0) return BadRequest("Department not found.");

        //     var date = (req.Date?.Date) ?? DateTime.Today;
        //     var resp = new JournalImportResponse();

        //     foreach (var grp in req.Lines.GroupBy(l => (l.CashAccount ?? "").Trim()))
        //     {
        //         var gr = new JournalImportGroupResult { Date = date.ToString("yyyy-MM-dd"), CashAccount = grp.Key };

        //         if (string.IsNullOrWhiteSpace(gr.CashAccount))
        //         {
        //             gr.Status = "error"; gr.Error = "CashAccount missing.";
        //             resp.Results.Add(gr); continue;
        //         }

        //         try
        //         {
        //             // 25 for ARKA
        //             int headerId = _db.CashJournalIDByCashAccount(gr.CashAccount, 25, date, dep.CompanyID);
        //             if (headerId == 0)
        //             {
        //                 var header = BuildJournalHeader(25, dep.CompanyID, req.DepartmentID, date, gr.CashAccount);
        //                 var svcIns = new TransactionsService(true, _db);
        //                 try { header.ErrorID = svcIns.Insert(header, false); }
        //                 finally { svcIns.CloseGlobalConnection(); }

        //                 if (header.ErrorID != 0 || header.ID <= 0)
        //                 {
        //                     gr.Status = "error"; gr.Error = "Failed to create header.";
        //                     resp.Results.Add(gr); continue;
        //                 }
        //                 headerId = header.ID;
        //             }

        //             var hdr = _db.SelectTransactionByID(new Transactions { ID = headerId });
        //             if (hdr == null || hdr.ID == 0)
        //             {
        //                 gr.Status = "error"; gr.Error = "Header not found after creation.";
        //                 resp.Results.Add(gr); continue;
        //             }

        //             var details = new List<TransactionsDetails>();
        //             foreach (var l in grp)
        //             {
        //                 if (l.DetailsType is not (3 or 4 or 5 or 6)) continue;

        //                 // OPTIONAL: enforce partner/account codes for each type
        //                 if ((l.DetailsType == 3 || l.DetailsType == 4) &&
        //                     string.IsNullOrWhiteSpace(l.PartnerCode) && !l.PartnerID.HasValue)
        //                     continue;

        //                 if ((l.DetailsType == 5 || l.DetailsType == 6) &&
        //                     string.IsNullOrWhiteSpace(l.AccountCode))
        //                     continue;

        //                 var reqLine = new JournalLineRequestDto
        //                 {
        //                     DepartmentID = req.DepartmentID,
        //                     Date = date,
        //                     CashAccount = gr.CashAccount,
        //                     DetailsType = l.DetailsType,
        //                     Amount = l.Amount,
        //                     Description = l.Description,
        //                     PartnerID = l.PartnerID,
        //                     PartnerCode = l.PartnerCode,
        //                     AccountCode = l.AccountCode,
        //                     PaymentID = l.PaymentID
        //                 };

        //                 var det = BuildDetail(reqLine, "Lëvizje arke"); 
        //                 det.TransactionID = headerId;
        //                 details.Add(det);
        //             }

        //             if (details.Count == 0)
        //             {
        //                 gr.Status = "error"; gr.Error = "No valid lines.";
        //                 resp.Results.Add(gr); continue;
        //             }

        //             hdr.TranDetailsColl = details;
        //             hdr.TransactionTypeID = 25; // defensive

        //             var svcUpd = new TransactionsService(true, _db);
        //             try
        //             {
        //                 var err = svcUpd.Update(hdr);
        //                 if (err != 0)
        //                 {
        //                     gr.Status = "error"; gr.Error = $"Update failed. ErrorID={err}";
        //                     resp.Results.Add(gr); continue;
        //                 }
        //             }
        //             finally { svcUpd.CloseGlobalConnection(); }

        //             gr.HeaderID = headerId;
        //             gr.InsertedLines = details.Count;
        //             resp.Results.Add(gr);
        //         }
        //         catch (Exception ex)
        //         {
        //             gr.Status = "error"; gr.Error = ex.Message;
        //             resp.Results.Add(gr);
        //         }
        //     }

        //     resp.Ok = resp.Results.All(r => r.Status == "ok");
        //     return Ok(resp);
        // }

        [HttpPost("ImportMulti")]
        public IActionResult ImportMulti([FromBody] MixedImportRequest req)
        {
            if (req == null) return BadRequest("Body required.");
            if (req.DepartmentID <= 0) return BadRequest("DepartmentID required.");
            if (req.JournalTypeID is not (24 or 25)) return BadRequest("JournalTypeID must be 24 or 25.");
            if (req.Lines == null || req.Lines.Count == 0) return BadRequest("Lines required.");

            var dep = _db.SelectDepartmentByID(new Department { ID = req.DepartmentID });
            if (dep == null || dep.ID == 0) return BadRequest("Department not found.");

            var date = (req.Date?.Date) ?? DateTime.Today;
            var resp = new JournalImportResponse();

            foreach (var grp in req.Lines.GroupBy(l => (l.CashAccount ?? "").Trim()))
            {
                var gr = new JournalImportGroupResult
                {
                    Date = date.ToString("yyyy-MM-dd"),
                    CashAccount = grp.Key
                };

                if (string.IsNullOrWhiteSpace(gr.CashAccount))
                {
                    gr.Status = "error";
                    gr.Error = "CashAccount missing.";
                    resp.Results.Add(gr);
                    continue;
                }

                try
                {
                    int headerId = _db.CashJournalIDByCashAccount(gr.CashAccount, req.JournalTypeID, date, dep.CompanyID);
                    if (headerId == 0)
                    {
                        var header = new Transactions
                        {
                            ID = 0,
                            CompanyID = dep.CompanyID,
                            DepartmentID = req.DepartmentID,
                            TransactionTypeID = req.JournalTypeID,
                            TransactionDate = date,
                            InvoiceDate = date,
                            DueDate = date,
                            Value = 0,
                            AllValue = 0,
                            PaidValue = 0,
                            Active = true,
                            Reference = "",
                            Links = "",
                            Memo = (req.JournalTypeID == 24 ? "Ditar Banke" : "Ditar Arke"),
                            VATPercentID = 0,
                            InsBy = GlobalAppData.UserID,
                            CashAccount = gr.CashAccount,
                            TransactionNo = DateTime.Now.ToString("ddMMyyyy"),
                            InvoiceNo = DateTime.Now.ToString("ddMMyyyy")
                        };

                        var svcIns = new TransactionsService(_db);
                        try { header.ErrorID = svcIns.Insert(header, false); }
                        finally { svcIns.CloseGlobalConnection(); }

                        if (header.ErrorID != 0 || header.ID <= 0)
                        {
                            gr.Status = "error";
                            gr.Error = "Failed to create header.";
                            resp.Results.Add(gr);
                            continue;
                        }
                        headerId = header.ID;
                    }

                    var hdr = _db.SelectTransactionByID(new Transactions { ID = headerId });
                    if (hdr == null || hdr.ID == 0)
                    {
                        gr.Status = "error";
                        gr.Error = "Header not found after creation.";
                        resp.Results.Add(gr);
                        continue;
                    }

                    var details = new List<TransactionsDetails>();
                    foreach (var l in grp)
                    {
                        if (l.DetailsType is not (3 or 4 or 5 or 6)) continue;
                        var reqLine = new JournalLineRequestDto
                        {
                            DepartmentID = req.DepartmentID,
                            Date = date,
                            CashAccount = gr.CashAccount,
                            DetailsType = l.DetailsType,
                            Amount = l.Amount,
                            Description = l.Description,
                            PartnerID = l.PartnerID,
                            PartnerCode = l.PartnerCode,
                            AccountCode = l.AccountCode,
                            PaymentID = l.PaymentID
                        };
                        var det = (req.JournalTypeID == 24)
                            ? BuildDetail(reqLine, "Lëvizje banke")
                            : BuildDetail(reqLine, "Lëvizje arke");

                        det.TransactionID = headerId;
                        details.Add(det);
                    }

                    if (details.Count == 0)
                    {
                        gr.Status = "error";
                        gr.Error = "No valid lines.";
                        resp.Results.Add(gr);
                        continue;
                    }

                    hdr.TranDetailsColl = details;

                    var svcUpd = new TransactionsService(_db);
                    try
                    {
                        var err = svcUpd.Update(hdr);
                        if (err != 0)
                        {
                            gr.Status = "error";
                            gr.Error = $"Update failed. ErrorID={err}";
                            resp.Results.Add(gr);
                            continue;
                        }
                    }
                    finally { svcUpd.CloseGlobalConnection(); }

                    gr.HeaderID = headerId;
                    gr.InsertedLines = details.Count;
                    resp.Results.Add(gr);
                }
                catch (Exception ex)
                {
                    gr.Status = "error";
                    gr.Error = ex.Message;
                    resp.Results.Add(gr);
                }
            }

            resp.Ok = resp.Results.All(r => r.Status == "ok");
            return Ok(resp);
        }

        public static string ResolveItemIdFromShifra(
           int detailsType,
           int? partnerId,
           string? partnerCode,
           string? accountCode,
           string? description
       )
        {
            if (detailsType == 3 || detailsType == 4)
            {
                if (partnerId.GetValueOrDefault() > 0)
                    return partnerId!.Value.ToString();

                if (!string.IsNullOrWhiteSpace(partnerCode))
                {
                    return partnerCode.Trim();
                }
            }
            else if (detailsType == 5 || detailsType == 6)
            {
                if (!string.IsNullOrWhiteSpace(accountCode))
                {
                    return accountCode.Trim();
                }
            }

            return !string.IsNullOrWhiteSpace(description) ? description.Trim() : "Lëvizje";
        }


        [HttpPost("Lines")]
        public IActionResult AddLines([FromBody] List<JournalLineRequestDto> lines)
        {
            if (lines == null || lines.Count == 0)
                return BadRequest("At least one line is required.");

            const int typeId = 25; 
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
                            results.Add(new { index = i, ok = false, error = "Failed to create cash header." });
                            continue;
                        }

                        headerId = header.ID;
                    }

                    var hdr = _db.SelectTransactionByID(new Transactions { ID = headerId });
                    if (hdr == null || hdr.ID == 0)
                    {
                        results.Add(new { index = i, ok = false, error = "Cash header not found." });
                        continue;
                    }

                    var det = BuildDetail(req, "Lëvizje arke");
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
                            linkedPaymentID = det.PaymentID
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
                ok = okCount == results.Count,
                insertedLines = okCount,
                total = results.Count,
                results
            });
        }

    }
}