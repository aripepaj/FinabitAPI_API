using FinabitAPI.Core.Global.dto;

public class ExtraCharge : BaseClass
  {

#region Class_Methods


/// Methods
/// <summary>
/// The ExtraChargeID field in the DB
/// </summary>
public int ExtraChargeID { get; set; }

/// <summary>
/// The ExtraChargeName field in the DB
/// </summary>
public string ExtraChargeName { get; set; }

/// <summary>
/// The Rate field in the DB
/// </summary>
public Nullable<decimal> Rate { get; set; }

/// <summary>
/// The Account field in the DB
/// </summary>
public string Account { get; set; }
public int EventActivityID { get; set; }


#endregion 

  }