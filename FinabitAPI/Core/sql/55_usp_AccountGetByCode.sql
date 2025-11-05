/****** Object:  StoredProcedure [dbo].[spAccountGetByCode_API] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID(N'dbo.[spAccountGetByCode_API]', N'P') IS NULL
    EXEC('CREATE PROCEDURE dbo.[spAccountGetByCode_API] AS RETURN;');
GO

ALTER PROCEDURE dbo.spAccountGetByCode_API
    @AccountCode NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        [Account]            AS AccountCode,
        [AccountDescription] AS AccountDescription,
        [Active]
    FROM [FINA_ONLINE].[dbo].[tblAccount]
    WHERE [Account] = @AccountCode;   
END
GO