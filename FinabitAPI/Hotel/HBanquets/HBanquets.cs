using System.Data;
using FinabitAPI.Core.Global.dto;

namespace FinabitAPI.Hotel.HBanquets
{
    public class HBanquets : BaseClass
  {

#region Class_Methods


/// Methods
/// <summary>
/// The ID field in the DB
/// </summary>
public new int ID { get; set; }

/// <summary>
/// The BookedByID field in the DB
/// </summary>
public Nullable<int> BookedByID { get; set; }

/// <summary>
/// The Source field in the DB
/// </summary>
public Nullable<int> Source { get; set; }

/// <summary>
/// The Status field in the DB
/// </summary>
public Nullable<int> Status { get; set; }

/// <summary>
/// The StartDate field in the DB
/// </summary>
public Nullable<DateTime> StartDate { get; set; }

/// <summary>
/// The EndDate field in the DB
/// </summary>
public Nullable<DateTime> EndDate { get; set; }

/// <summary>
/// The StartTime field in the DB
/// </summary>
public TimeSpan StartTime { get; set; }

/// <summary>
/// The EndTime field in the DB
/// </summary>
public TimeSpan EndTime { get; set; }

/// <summary>
/// The Adult field in the DB
/// </summary>
public Nullable<int> Adult { get; set; }

/// <summary>
/// The Child field in the DB
/// </summary>
public Nullable<int> Child { get; set; }

/// <summary>
/// The PaymentType field in the DB
/// </summary>
public Nullable<int> PaymentType { get; set; }

/// <summary>
/// The BanquetRoom field in the DB
/// </summary>
public Nullable<int> BanquetRoom { get; set; }

/// <summary>
/// The Theme field in the DB
/// </summary>
public string? Theme { get; set; }

/// <summary>
/// The Charges field in the DB
/// </summary>
public Nullable<decimal> Charges { get; set; }

/// <summary>
/// The Paid field in the DB
/// </summary>
public Nullable<decimal> Paid { get; set; }

/// <summary>
/// The Notes field in the DB
/// </summary>
public string? Notes { get; set; }

public int ID1 { get; set; }

public string? Name { get; set; }

public string? Mysafiri { get; set; }

public bool AllDayEvent { get; set; }

public bool Repeat { get; set; }

public bool IsParent { get; set; }
public string? SetUp { get; set; }
public string? Menu { get; set; }
public string? Note1 { get; set; }
public string? Note2 { get; set; }
public string? Note3 { get; set; }
public int ParentID { get; set; }
public DataTable? Detajet { get; set; }
public DateTime Data { get; set; }
#endregion 

    }
}