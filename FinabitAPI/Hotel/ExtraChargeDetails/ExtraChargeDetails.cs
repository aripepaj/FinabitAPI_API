using FinabitAPI.Core.Global.dto;

namespace FinabitAPI.Hotel.ExtraChargeDetails
{
    public class ExtraChargeDetails : BaseClass
  {

#region Class_Methods


/// Methods
/// <summary>
/// The ExtraChargeDetailsID field in the DB
/// </summary>
public int ExtraChargeDetailsID { get; set; }

/// <summary>
/// The ExtraChargeID field in the DB
/// </summary>
public Nullable<int> ExtraChargeID { get; set; }

/// <summary>
/// The ReservationID field in the DB
/// </summary>
public Nullable<int> ReservationID { get; set; }

/// <summary>
/// The Quantity field in the DB
/// </summary>
public Nullable<decimal> Quantity { get; set; }

/// <summary>
/// The Rate field in the DB
/// </summary>
public string? Rate { get; set; }

public DateTime Date { get; set; }

public new string? Description { get; set; }

public int DepartmentID { get; set; }

public int HFolioID { get; set; }

public string? DepartmentName { get; set; }

public string? ExtraChargeName { get; set; }

public decimal Kredi { get; set; }

public decimal Bilanci { get; set; }

public bool IsMaster { get; set; }

public decimal DebiValue { get; set; }

public int ReferenceID { get; set; }

#endregion 

    }
}
