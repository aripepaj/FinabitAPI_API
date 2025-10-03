// Models/ImportContractsDocsForm.cs
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FinabitAPI.Core.Global.dto
{
    public sealed class ImportContractsDocsForm
    {
        [FromForm] public int DossierId { get; set; }

        /// <summary>1 = Replace existing; 0 = Append</summary>
        [FromForm] public int ScanDocMode { get; set; } = 1;

        [FromForm] public string DocNo { get; set; } = "";
        [FromForm] public DateTime? DocDate { get; set; } = null;
        [FromForm] public string SubjectName { get; set; } = "";

        /// <summary>Default DocType for all files (used when DocTypes is null)</summary>
        [FromForm] public int? DocType { get; set; } = null;

        /// <summary>Optional per-file doc types (must match Files count if provided)</summary>
        [FromForm] public List<int>? DocTypes { get; set; } = null;

        /// <summary>Optional per-file descriptions (must match Files count if provided)</summary>
        [FromForm] public List<string>? Descriptions { get; set; } = null;

        /// <summary>The uploaded files</summary>
        [FromForm] public List<IFormFile> Files { get; set; } = new();
    }
}
