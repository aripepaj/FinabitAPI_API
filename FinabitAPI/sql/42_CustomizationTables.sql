-- =============================================
-- Customization Tables Migration for SQL Server
-- Created: 2025-10-03
-- Description: Creates tables for user customization preferences
-- =============================================

-- Customization Lists Table
-- Stores saved configurations for different list views (table/pivot/chart)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblAPI_customization_lists]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[tblAPI_customization_lists] (
        [id] INT IDENTITY(1,1) PRIMARY KEY,
        [user] NVARCHAR(255) NOT NULL,
        [name] NVARCHAR(255) NOT NULL,
        [type] NVARCHAR(50) NOT NULL CHECK ([type] IN ('table', 'pivot', 'chart')),
        [config] NVARCHAR(MAX) NOT NULL, -- Using NVARCHAR(MAX) for JSON storage
        [created_at] DATETIME2 DEFAULT GETDATE(),
        [updated_at] DATETIME2 DEFAULT GETDATE(),
        CONSTRAINT [UQ_tblAPI_customization_lists_user_name_type] UNIQUE ([user], [name], [type])
    );
    
    PRINT 'Table tblAPI_customization_lists created successfully.';
END
ELSE
BEGIN
    PRINT 'Table tblAPI_customization_lists already exists.';
END
GO

-- Customization Favorites Table
-- Stores user's bookmarked/favorite items
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblAPI_customization_favorites]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[tblAPI_customization_favorites] (
        [id] INT IDENTITY(1,1) PRIMARY KEY,
        [user] NVARCHAR(255) NOT NULL,
        [item_id] NVARCHAR(255) NOT NULL,
        [item_type] NVARCHAR(100) NOT NULL,
        [metadata] NVARCHAR(MAX), -- Using NVARCHAR(MAX) for JSON storage
        [created_at] DATETIME2 DEFAULT GETDATE(),
        CONSTRAINT [UQ_tblAPI_customization_favorites_user_item] UNIQUE ([user], [item_id], [item_type])
    );
    
    PRINT 'Table tblAPI_customization_favorites created successfully.';
END
ELSE
BEGIN
    PRINT 'Table tblAPI_customization_favorites already exists.';
END
GO

-- Customization Profile Preferences Table
-- Stores user preferences like last selected view names
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblAPI_customization_profile_prefs]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[tblAPI_customization_profile_prefs] (
        [id] INT IDENTITY(1,1) PRIMARY KEY,
        [user] NVARCHAR(255) NOT NULL,
        [pref_key] NVARCHAR(255) NOT NULL,
        [pref_value] NVARCHAR(MAX) NOT NULL,
        [updated_at] DATETIME2 DEFAULT GETDATE(),
        CONSTRAINT [UQ_tblAPI_customization_profile_prefs_user_key] UNIQUE ([user], [pref_key])
    );
    
    PRINT 'Table tblAPI_customization_profile_prefs created successfully.';
END
ELSE
BEGIN
    PRINT 'Table tblAPI_customization_profile_prefs already exists.';
END
GO

-- Create Indexes for Performance
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tblAPI_customization_lists_user' AND object_id = OBJECT_ID('tblAPI_customization_lists'))
BEGIN
    CREATE INDEX [IX_tblAPI_customization_lists_user] ON [dbo].[tblAPI_customization_lists]([user]);
    PRINT 'Index IX_tblAPI_customization_lists_user created.';
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tblAPI_customization_lists_type' AND object_id = OBJECT_ID('tblAPI_customization_lists'))
BEGIN
    CREATE INDEX [IX_tblAPI_customization_lists_type] ON [dbo].[tblAPI_customization_lists]([type]);
    PRINT 'Index IX_tblAPI_customization_lists_type created.';
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tblAPI_customization_favorites_user' AND object_id = OBJECT_ID('tblAPI_customization_favorites'))
BEGIN
    CREATE INDEX [IX_tblAPI_customization_favorites_user] ON [dbo].[tblAPI_customization_favorites]([user]);
    PRINT 'Index IX_tblAPI_customization_favorites_user created.';
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tblAPI_customization_profile_prefs_user' AND object_id = OBJECT_ID('tblAPI_customization_profile_prefs'))
BEGIN
    CREATE INDEX [IX_tblAPI_customization_profile_prefs_user] ON [dbo].[tblAPI_customization_profile_prefs]([user]);
    PRINT 'Index IX_tblAPI_customization_profile_prefs_user created.';
END
GO

-- Create Trigger for updated_at on tblAPI_customization_lists
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE name = 'TR_tblAPI_customization_lists_updated_at')
BEGIN
    EXEC('
    CREATE TRIGGER [dbo].[TR_tblAPI_customization_lists_updated_at]
    ON [dbo].[tblAPI_customization_lists]
    AFTER UPDATE
    AS
    BEGIN
        SET NOCOUNT ON;
        UPDATE [dbo].[tblAPI_customization_lists]
        SET [updated_at] = GETDATE()
        FROM [dbo].[tblAPI_customization_lists] cl
        INNER JOIN inserted i ON cl.id = i.id;
    END
    ');
    PRINT 'Trigger TR_tblAPI_customization_lists_updated_at created.';
END
GO

-- Create Trigger for updated_at on tblAPI_customization_profile_prefs
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE name = 'TR_tblAPI_customization_profile_prefs_updated_at')
BEGIN
    EXEC('
    CREATE TRIGGER [dbo].[TR_tblAPI_customization_profile_prefs_updated_at]
    ON [dbo].[tblAPI_customization_profile_prefs]
    AFTER UPDATE
    AS
    BEGIN
        SET NOCOUNT ON;
        UPDATE [dbo].[tblAPI_customization_profile_prefs]
        SET [updated_at] = GETDATE()
        FROM [dbo].[tblAPI_customization_profile_prefs] cpp
        INNER JOIN inserted i ON cpp.id = i.id;
    END
    ');
    PRINT 'Trigger TR_tblAPI_customization_profile_prefs_updated_at created.';
END
GO

PRINT 'Customization tables migration completed successfully.';
GO
