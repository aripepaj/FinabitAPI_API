-- =============================================
-- Customization Tables (FINAL SCHEMA - combined 42 & 43)
-- This script DROPs existing customization tables (if they exist) and recreates with the final schema.
-- Execute ONLY after confirming you want to destroy existing data (or backup first).
-- Date: 2025-10-04
-- =============================================
SET NOCOUNT ON;

/* Safety: Drop triggers first (if any) to avoid dependency errors */
IF OBJECT_ID('dbo.TR_tblAPI_customization_lists_updated_at','TR') IS NOT NULL DROP TRIGGER dbo.TR_tblAPI_customization_lists_updated_at;
IF OBJECT_ID('dbo.TR_tblAPI_customization_profile_prefs_updated_at','TR') IS NOT NULL DROP TRIGGER dbo.TR_tblAPI_customization_profile_prefs_updated_at;

/* Drop tables (in FK dependency order if FKs existed; none here) */
IF OBJECT_ID('dbo.tblAPI_customization_favorites','U') IS NOT NULL DROP TABLE dbo.tblAPI_customization_favorites;
IF OBJECT_ID('dbo.tblAPI_customization_lists','U') IS NOT NULL DROP TABLE dbo.tblAPI_customization_lists;
IF OBJECT_ID('dbo.tblAPI_customization_profile_prefs','U') IS NOT NULL DROP TABLE dbo.tblAPI_customization_profile_prefs;

/* ============= Lists Table ============= */
CREATE TABLE dbo.tblAPI_customization_lists (
    id INT IDENTITY(1,1) PRIMARY KEY,
    [user]        NVARCHAR(255) NOT NULL,
    storage_key   NVARCHAR(255) NULL,
    name          NVARCHAR(255) NOT NULL,
    mode          NVARCHAR(50)  NOT NULL,
    device        NVARCHAR(20)  NULL,
    data          NVARCHAR(MAX) NOT NULL,  -- JSON payload
    created_at    DATETIME2      NOT NULL DEFAULT GETDATE(),
    updated_at    DATETIME2      NOT NULL DEFAULT GETDATE(),
    -- Computed columns for normalized uniqueness (COALESCE semantics)
    storage_key_norm AS ISNULL(storage_key,'') PERSISTED,
    device_norm      AS ISNULL(device,'') PERSISTED,
    CONSTRAINT CK_tblAPI_customization_lists_mode   CHECK (mode IN ('table','pivot','chart')),
    CONSTRAINT CK_tblAPI_customization_lists_device CHECK (device IN ('mobile','desktop') OR device IS NULL)
);

-- Unique composite (user, storage_key, mode, device, name)
CREATE UNIQUE INDEX UX_tblAPI_customization_lists_user_storage_mode_device_name
    ON dbo.tblAPI_customization_lists ([user], storage_key_norm, mode, device_norm, name);

-- Supporting indexes
CREATE INDEX IX_tblAPI_customization_lists_user      ON dbo.tblAPI_customization_lists([user]);
CREATE INDEX IX_tblAPI_customization_lists_mode      ON dbo.tblAPI_customization_lists(mode);
CREATE INDEX IX_tblAPI_customization_lists_updated_at ON dbo.tblAPI_customization_lists(updated_at DESC);

/* ============= Favorites Table ============= */
CREATE TABLE dbo.tblAPI_customization_favorites (
    id INT IDENTITY(1,1) PRIMARY KEY,
    [user]    NVARCHAR(255) NOT NULL,
    item_id   NVARCHAR(255) NOT NULL,
    label     NVARCHAR(255) NOT NULL,
    path      NVARCHAR(500) NOT NULL,
    icon      NVARCHAR(MAX) NULL,      -- JSON (icon metadata)
    metadata  NVARCHAR(MAX) NULL,      -- Legacy / extra JSON
    added_at  DATETIME2      NOT NULL DEFAULT GETDATE(),
    item_type NVARCHAR(100) NULL       -- Legacy (not part of uniqueness)
);

CREATE UNIQUE INDEX UX_tblAPI_customization_favorites_user_item
    ON dbo.tblAPI_customization_favorites([user], item_id);
CREATE INDEX IX_tblAPI_customization_favorites_user     ON dbo.tblAPI_customization_favorites([user]);
CREATE INDEX IX_tblAPI_customization_favorites_added_at ON dbo.tblAPI_customization_favorites(added_at DESC);

/* ============= Profile Preferences Table ============= */
CREATE TABLE dbo.tblAPI_customization_profile_prefs (
    id INT IDENTITY(1,1) PRIMARY KEY,
    [user]         NVARCHAR(255) NOT NULL,
    storage_key    NVARCHAR(255) NULL,
    mode           NVARCHAR(50)  NULL, -- allow NULL for generic prefs
    device         NVARCHAR(20)  NULL,
    last_view_name NVARCHAR(255) NULL,
    pref_key       NVARCHAR(255) NULL, -- legacy
    pref_value     NVARCHAR(MAX) NULL, -- legacy
    updated_at     DATETIME2      NOT NULL DEFAULT GETDATE(),
    storage_key_norm AS ISNULL(storage_key,'') PERSISTED,
    device_norm      AS ISNULL(device,'') PERSISTED,
    CONSTRAINT CK_tblAPI_customization_profile_prefs_mode   CHECK (mode IN ('table','pivot','chart') OR mode IS NULL),
    CONSTRAINT CK_tblAPI_customization_profile_prefs_device CHECK (device IN ('mobile','desktop') OR device IS NULL)
);

-- Composite uniqueness (user, storage_key, mode, device)
CREATE UNIQUE INDEX UX_tblAPI_customization_profile_prefs_user_storage_mode_device
    ON dbo.tblAPI_customization_profile_prefs([user], storage_key_norm, mode, device_norm);

CREATE INDEX IX_tblAPI_customization_profile_prefs_user ON dbo.tblAPI_customization_profile_prefs([user]);
CREATE INDEX IX_tblAPI_customization_profile_prefs_mode ON dbo.tblAPI_customization_profile_prefs(mode);

/* Triggers to update updated_at */
EXEC (
'CREATE TRIGGER dbo.TR_tblAPI_customization_lists_updated_at
   ON dbo.tblAPI_customization_lists
   AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE l SET updated_at = GETDATE()
    FROM dbo.tblAPI_customization_lists l
    INNER JOIN inserted i ON l.id = i.id;
END');

EXEC (
'CREATE TRIGGER dbo.TR_tblAPI_customization_profile_prefs_updated_at
   ON dbo.tblAPI_customization_profile_prefs
   AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE p SET updated_at = GETDATE()
    FROM dbo.tblAPI_customization_profile_prefs p
    INNER JOIN inserted i ON p.id = i.id;
END');

PRINT 'Customization tables (final schema) created successfully.';
GO
