using FinabitAPI.Core.Global.dto;

namespace FinabitAPI.Hotel.HFolios
{
    public class HFolios : BaseClass
  {

#region Class_Methods


/// Methods
/// <summary>
/// The ID field in the DB
/// </summary>
public new int ID { get; set; }

/// <summary>
/// The Folio field in the DB
/// </summary>
public string? Folio { get; set; }

/// <summary>
/// The ReservationId field in the DB
/// </summary>
public int ReservationId { get; set; }

/// <summary>
/// The PartnerId field in the DB
/// </summary>
public int PartnerId { get; set; }

public string? PartnerName { get; set; }

public decimal ChargeValue { get; set; }

public decimal PaidValue { get; set; }

public decimal BilancValue { get; set; }

public bool IsMaster { get; set; }


public decimal VATPercent { get; set; }
#endregion 

    }
}
