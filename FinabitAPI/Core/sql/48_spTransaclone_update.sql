/****** Object:  StoredProcedure [dbo].[usp_CloneTransactionExact_API]    Script Date: 10/29/2025 8:40:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[usp_CloneTransactionExact_API]
    @SourceID              INT,         
    @NewDate               DATE,       
    @IncludePOSDetails     BIT  = 1,   
    @ResetPaymentIDToZero  BIT  = 0,   
    @Debug                 BIT  = 0 ,
    @NewTransactionNo      NVARCHAR(50) = NULL  
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @NewID INT;

    BEGIN TRY
        BEGIN TRAN;

        -- Object names
        DECLARE @hdr    NVARCHAR(512) = N'[dbo].[tblTransactions]';
        DECLARE @det    NVARCHAR(512) = N'[dbo].[tblTransactionsDetails]';
        DECLARE @detPOS NVARCHAR(512) = N'[dbo].[tblTransactionsDetailsPOS]';

        IF OBJECT_ID(@hdr) IS NULL RAISERROR('Header table %s not found.',16,1,@hdr);
        IF OBJECT_ID(@det) IS NULL RAISERROR('Detail table %s not found.',16,1,@det);

        -- Header identity column
        DECLARE @HdrIdCol SYSNAME;
        SELECT TOP (1) @HdrIdCol = c.name
        FROM sys.columns c
        WHERE c.object_id = OBJECT_ID(@hdr)
          AND COLUMNPROPERTY(c.object_id, c.name, 'IsIdentity') = 1;
        IF @HdrIdCol IS NULL RAISERROR('No identity column found on %s.',16,1,@hdr);

        DECLARE @HdrIdQ NVARCHAR(260) = QUOTENAME(@HdrIdCol);

        -- Verify source exists
        DECLARE @srcExists INT;
        DECLARE @chkSrcSql NVARCHAR(MAX) =
            N'SELECT @out = COUNT(1) FROM ' + @hdr + N' WHERE ' + @HdrIdQ + N' = @id;';
        EXEC sp_executesql @chkSrcSql, N'@out INT OUTPUT, @id INT', @out=@srcExists OUTPUT, @id=@SourceID;
        IF @srcExists = 0 RAISERROR('Source transaction %d not found in %s.',16,1,@SourceID,@hdr);

        DECLARE @cols NVARCHAR(MAX) = N'';
        DECLARE @sel  NVARCHAR(MAX) = N'';
        DECLARE @name SYSNAME;

        DECLARE cur CURSOR FAST_FORWARD FOR
        SELECT c.name
        FROM sys.columns c
        WHERE c.object_id = OBJECT_ID(@hdr)
          AND c.is_computed = 0
          AND COLUMNPROPERTY(c.object_id, c.name, 'IsIdentity') = 0
          AND c.system_type_id <> 189   -- rowversion/timestamp
          AND c.is_rowguidcol = 0;      -- ROWGUIDCOL
        OPEN cur;
        FETCH NEXT FROM cur INTO @name;
        WHILE @@FETCH_STATUS = 0
        BEGIN
            SET @cols = @cols + CASE WHEN LEN(@cols)>0 THEN N',' ELSE N'' END + QUOTENAME(@name);
            SET @sel  = @sel  + CASE WHEN LEN(@sel)>0 THEN N',' ELSE N'' END +
                CASE 
                    WHEN @name IN ('TransactionDate','InvoiceDate','DueDate')
                        THEN '@NewDate'
                    WHEN @name = 'TransactionNo'
                        THEN 'ISNULL(@NewTransactionNo, src.' + QUOTENAME(@name) + ')'  -- <-- NEW
                    ELSE 'src.' + QUOTENAME(@name) 
                END;
            FETCH NEXT FROM cur INTO @name;
        END
        CLOSE cur; DEALLOCATE cur;

        IF (@cols = N'' OR @sel = N'')
            RAISERROR('No insertable columns found on %s.',16,1,@hdr);

        DECLARE @sqlHdr NVARCHAR(MAX) =
N'DECLARE @t TABLE(ID INT);
INSERT INTO ' + @hdr + N'(' + @cols + N')
OUTPUT inserted.' + @HdrIdQ + N' INTO @t(ID)
SELECT ' + @sel + N'
FROM ' + @hdr + N' AS src
WHERE src.' + @HdrIdQ + N' = @SourceID;
SELECT @OutNewID = MAX(ID) FROM @t;';

        IF @Debug = 1 PRINT @sqlHdr;

        EXEC sp_executesql
             @sqlHdr,
             N'@SourceID INT, @NewDate DATE, @NewTransactionNo NVARCHAR(50), @OutNewID INT OUTPUT',
             @SourceID = @SourceID, 
             @NewDate = @NewDate,
             @NewTransactionNo = @NewTransactionNo,  
             @OutNewID = @NewID OUTPUT;

        IF @NewID IS NULL OR @NewID = 0
            RAISERROR('Failed to obtain new header ID.',16,1);


		DECLARE @updSet NVARCHAR(MAX) = N'';

        DECLARE @Resets TABLE (Col SYSNAME, Expr NVARCHAR(100));
        INSERT INTO @Resets(Col, Expr) VALUES
            (N'PaidValue',        N'0'),       -- numeric
            (N'POSPaid',          N'0'),       -- bit
            (N'JournalStatus',    N'NULL'),
            (N'POSStatus',        N'NULL'),
            (N'InvoiceID',        N'NULL'),
            (N'FiscalizationID',  N'NULL'),
            (N'StornoID',         N'NULL'),
            (N'SynchroniseID',    N'NULL'),
            (N'SynchronizeDate',  N'NULL'),
            (N'Emailed',          N'0'),
            (N'Prints',           N'0');

        SELECT @updSet = STRING_AGG(QUOTENAME(r.Col) + N' = ' + r.Expr, N', ')
        FROM @Resets r
        JOIN sys.columns c
          ON c.object_id = OBJECT_ID(@hdr)
         AND c.name = r.Col;

        IF @updSet IS NOT NULL AND LEN(@updSet) > 0
        BEGIN
            DECLARE @sqlReset NVARCHAR(MAX) =
                N'UPDATE ' + @hdr + N'
                  SET ' + @updSet + N'
                WHERE ' + @HdrIdQ + N' = @NewID;';

            IF @Debug = 1 PRINT @sqlReset;

            EXEC sp_executesql
                @sqlReset,
                N'@NewID INT',
                @NewID = @NewID;
        END

        IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(@det) AND name = 'TransactionID')
            RAISERROR('Column TransactionID not found on %s.',16,1,@det);

        DECLARE @colsDet NVARCHAR(MAX) = N'';
        DECLARE @selDet  NVARCHAR(MAX) = N'';
        DECLARE @dname SYSNAME;

        DECLARE @PaymentIdResetExpr NVARCHAR(10) = CASE WHEN @ResetPaymentIDToZero = 1 THEN N'0' ELSE N'NULL' END;

        DECLARE curd CURSOR FAST_FORWARD FOR
        SELECT c.name
        FROM sys.columns c
        WHERE c.object_id = OBJECT_ID(@det)
          AND c.is_computed = 0
          AND COLUMNPROPERTY(c.object_id, c.name, 'IsIdentity') = 0
          AND c.system_type_id <> 189
          AND c.is_rowguidcol = 0;
        OPEN curd;
        FETCH NEXT FROM curd INTO @dname;
        WHILE @@FETCH_STATUS = 0
        BEGIN
            SET @colsDet = @colsDet + CASE WHEN LEN(@colsDet)>0 THEN N',' ELSE N'' END + QUOTENAME(@dname);
            SET @selDet  = @selDet  + CASE WHEN LEN(@selDet)>0 THEN N',' ELSE N'' END +
                           CASE 
                                WHEN @dname = 'TransactionID' THEN '@NewID'
                                WHEN @dname = 'PaymentID'     THEN @PaymentIdResetExpr
                                ELSE 'd.' + QUOTENAME(@dname) 
                           END;
            FETCH NEXT FROM curd INTO @dname;
        END
        CLOSE curd; DEALLOCATE curd;

        IF (@colsDet = N'' OR @selDet = N'')
            RAISERROR('No insertable columns found on %s.',16,1,@det);

        DECLARE @sqlDet NVARCHAR(MAX) =
N'INSERT INTO ' + @det + N'(' + @colsDet + N')
SELECT ' + @selDet + N'
FROM ' + @det + N' AS d
WHERE d.[TransactionID] = @SourceID;';

        IF @Debug = 1 PRINT @sqlDet;

        EXEC sp_executesql
             @sqlDet,
             N'@SourceID INT, @NewID INT',
             @SourceID = @SourceID, @NewID = @NewID;

        IF @IncludePOSDetails = 1 AND OBJECT_ID(@detPOS) IS NOT NULL
           AND EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(@detPOS) AND name = 'TransactionID')
        BEGIN
            DECLARE @colsDetPOS NVARCHAR(MAX) = N'';
            DECLARE @selDetPOS  NVARCHAR(MAX) = N'';
            DECLARE @pname SYSNAME;

            DECLARE curp CURSOR FAST_FORWARD FOR
            SELECT c.name
            FROM sys.columns c
            WHERE c.object_id = OBJECT_ID(@detPOS)
              AND c.is_computed = 0
              AND COLUMNPROPERTY(c.object_id, c.name, 'IsIdentity') = 0
              AND c.system_type_id <> 189
              AND c.is_rowguidcol = 0;
            OPEN curp;
            FETCH NEXT FROM curp INTO @pname;
            WHILE @@FETCH_STATUS = 0
            BEGIN
                SET @colsDetPOS = @colsDetPOS + CASE WHEN LEN(@colsDetPOS)>0 THEN N',' ELSE N'' END + QUOTENAME(@pname);
                SET @selDetPOS  = @selDetPOS  + CASE WHEN LEN(@selDetPOS)>0 THEN N',' ELSE N'' END +
                                  CASE 
                                       WHEN @pname = 'TransactionID' THEN '@NewID'
                                       WHEN @pname = 'PaymentID'     THEN @PaymentIdResetExpr
                                       ELSE 'p.' + QUOTENAME(@pname) 
                                  END;
                FETCH NEXT FROM curp INTO @pname;
            END
            CLOSE curp; DEALLOCATE curp;

            IF (@colsDetPOS <> N'' AND @selDetPOS <> N'')
            BEGIN
                DECLARE @sqlDetPOS NVARCHAR(MAX) =
N'INSERT INTO ' + @detPOS + N'(' + @colsDetPOS + N')
SELECT ' + @selDetPOS + N'
FROM ' + @detPOS + N' AS p
WHERE p.[TransactionID] = @SourceID;';

                IF @Debug = 1 PRINT @sqlDetPOS;

                EXEC sp_executesql
                     @sqlDetPOS,
                     N'@SourceID INT, @NewID INT',
                     @SourceID = @SourceID, @NewID = @NewID;
            END
        END

       COMMIT;
        SELECT @NewID AS NewTransactionID;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK;
        DECLARE @msg NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @line INT = ERROR_LINE();
        RAISERROR('usp_CloneTransactionExact failed at line %d: %s',16,1,@line,@msg);
        RETURN;
    END CATCH
END