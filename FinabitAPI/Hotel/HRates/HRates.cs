using FinabitAPI.Core.Global.dto;

public class HRates : BaseClass
  {

#region Class_Methods


/// Methods
/// <summary>
/// The ID field in the DB
/// </summary>
public int ID
 { 
       get;
       set;
 } 

/// <summary>
/// The RateName field in the DB
/// </summary>
public string RateName
 { 
       get;
       set;
 } 

/// <summary>
/// /// <summary>
/// The RoomTypeName field in the DB
/// </summary>
public string RoomTypeName
{
    get;
    set;
}

/// <summary>
/// The RoomTypeID field in the DB
/// </summary>
public int RoomTypeID
 { 
       get;
       set;
 } 

/// <summary>
/// The Value field in the DB
/// </summary>
public decimal Value
 { 
       get;
       set;
 } 


#endregion 

  }