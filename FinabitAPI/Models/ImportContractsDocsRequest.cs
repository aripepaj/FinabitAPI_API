// Models/ImportContractsDocsForm.cs
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FinabitAPI.Models
{
    public sealed class ImportContractsDocsForm
    {
        [FromForm] public int DossierId { get; set; }

        [FromForm] public int ScanDocMode { get; set; } = 1;

        [FromForm] public string DocNo { get; set; } = "";
        [FromForm] public DateTime? DocDate { get; set; } = null;
        [FromForm] public string SubjectName { get; set; } = "";

        [FromForm] public int? DocType { get; set; } = null;

        [FromForm] public List<int>? DocTypes { get; set; } = null;

        [FromForm] public List<string>? Descriptions { get; set; } = null;

        [FromForm] public List<IFormFile> Files { get; set; } = new();
    }
}
