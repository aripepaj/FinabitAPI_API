using FinabitAPI.Core.Global.dto;

public class HRoom : BaseClass
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
/// The RoomName field in the DB
/// </summary>
/// 
public string? ReservationOrigin
{
    get;
    set;
}
public string? RoomName
 { 
       get;
       set;
 } 

/// <summary>
/// /// <summary>
/// The RoomTypeName field in the DB
/// </summary>
public string? RoomTypeName
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
/// The Description field in the DB
/// </summary>
public string? Description
 { 
       get;
       set;
 } 

/// <summary>
/// The Phone field in the DB
/// </summary>
public string? Phone
 { 
       get;
       set;
 } 

/// <summary>
/// The Active field in the DB
/// </summary>
public bool Active
 { 
       get;
       set;
 } 

/// <summary>
/// The StatusID field in the DB
/// </summary>
public int StatusID
 { 
       get;
       set;
 }

public string? Status
{
    get;
    set;
}
/// <summary>
/// The RoomPhoto field in the DB
/// </summary>
public byte[]? RoomPhoto
{
    get;
    set;
}
public byte[]? RoomPhoto1
{
    get;
    set;
}
public byte[]? RoomPhoto2
{
    get;
    set;
}
      /// <summary>
/// <summary>
/// The FloorID field in the DB
/// </summary>
public int FloorID
{
    get;
    set;
}
      /// <summary>
/// <summary>
/// The Floor field in the DB
/// </summary>
public string? Floor
{
    get;
    set;
}

public int PositionX
{
    get;
    set;
}

public int PositionY
{
    get;
    set;
}

public int SizeX
{
    get;
    set;
}
public int SizeY
{
    get;
    set;
}

public string? BackColor
{
    get;
    set;
}

public string? GuestName
{
    get;
    set;
}

public int ReservationID
{  get;    set;}

public int OutOfOrder
{  get;    set;}

public DateTime Date
{ get; set; }

public string? RoomSize
{ get;    set;}

public string? RoomView
{ get;    set;}

public string? RoomBed
{ get;    set;}
public int MaxAdult
{ get; set; }
public int MaxChild
{ get; set; }
/// <summary>

public int DepartmentID { get; set; }
public string? SubBookingID { get; set; }
#endregion 

  }