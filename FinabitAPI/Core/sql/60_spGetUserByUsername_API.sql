/****** Object:  StoredProcedure [dbo].[spAccountGetByCode_API] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID(N'dbo.[spGetUserByUsername_API]', N'P') IS NULL
    EXEC('CREATE PROCEDURE dbo.[spGetUserByUsername_API] AS RETURN;');
GO

ALTER PROCEDURE [dbo].[spGetUserByUsername_API]
    @UserName VARCHAR(256)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
          UserID
        , EmpID
        , UserName
        , Password
        , ExpireDate
        , Status
        , LUB
        , LUD
        , LUN
        , InsDate
        , rowguid
        , PIN
        , PDAPIN
        , LanguageID
        , AllowReplication
        , PIN2
        , IsDeleteWithAuthorization
        , PartnerID
        , MenuID
        , IsAuthoriser
        , DisableDateInDocuments
        , authoriserEmail
        , NavUsername
        , NavPassword
        , DisablePay
        , Color
    FROM dbo.tblUsers
    WHERE UserName = @UserName
END
GO
