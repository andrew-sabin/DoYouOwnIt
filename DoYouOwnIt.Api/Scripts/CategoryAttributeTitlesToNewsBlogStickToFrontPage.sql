IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Categories] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(450) NOT NULL,
    [Slug] varchar(100) NOT NULL,
    [Description] nvarchar(max) NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
);

CREATE TABLE [Stores] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(450) NOT NULL,
    [Slug] varchar(100) NOT NULL,
    [Industry] nvarchar(max) NOT NULL,
    [LogoURL] nvarchar(max) NULL,
    [Online] bit NOT NULL,
    [Street] nvarchar(max) NULL,
    [City] nvarchar(max) NULL,
    [State] nvarchar(max) NULL,
    [PostalCode] nvarchar(max) NULL,
    [Country] nvarchar(max) NULL,
    [Phone] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [WebsiteURL] nvarchar(max) NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedDate] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    [DeletedDate] datetime2 NULL,
    CONSTRAINT [PK_Stores] PRIMARY KEY ([Id])
);

CREATE TABLE [FormatType] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [CategoryId] int NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_FormatType] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FormatType_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Products] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Slug] varchar(100) NOT NULL,
    [CoverImageURL] nvarchar(max) NULL,
    [ProductLaunchDate] date NOT NULL,
    [IsLocked] bit NOT NULL,
    [LockedBy] nvarchar(max) NULL,
    [Creators] nvarchar(max) NULL,
    [CreditsURL] nvarchar(max) NULL,
    [ContentRating] int NOT NULL,
    [IsAIAssisted] bit NOT NULL,
    [AIAssistsWith] nvarchar(max) NULL,
    [ForMatureAudiences] bit NOT NULL,
    [MatureAudienceReason] nvarchar(max) NULL,
    [Description] TEXT NULL,
    [CategoryId] int NOT NULL,
    [CreatorId] nvarchar(max) NULL,
    [ModifierId] nvarchar(max) NULL,
    [LastModified] datetime2 NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedDate] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    [DeletedDate] datetime2 NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Formats] (
    [Id] int NOT NULL IDENTITY,
    [CoverImageUrl] nvarchar(max) NULL,
    [Type] nvarchar(max) NOT NULL,
    [Edition] nvarchar(max) NULL,
    [Slug] varchar(100) NOT NULL,
    [ReleaseDate] date NULL,
    [IsLocked] bit NOT NULL,
    [lockedReason] nvarchar(max) NULL,
    [LockedByUser] nvarchar(max) NULL,
    [IsAIAssisted] bit NOT NULL,
    [AIAssistsWith] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [OwnershipLevel] int NOT NULL,
    [DisplayVideoUrl] nvarchar(max) NOT NULL,
    [ProductId] int NOT NULL,
    [CreatorId] nvarchar(max) NULL,
    [ModifierId] nvarchar(max) NULL,
    [ContributerIds] nvarchar(max) NOT NULL,
    [LastModified] datetime2 NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedDate] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    [DeletedDate] datetime2 NULL,
    CONSTRAINT [PK_Formats] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Formats_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Availabilities] (
    [Id] int NOT NULL IDENTITY,
    [FormatId] int NOT NULL,
    [StoreId] int NOT NULL,
    [URL] nvarchar(max) NOT NULL,
    [CurrencyCode] nvarchar(max) NOT NULL,
    [UnitSold] nvarchar(max) NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [LastCheckedBy] nvarchar(max) NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Availabilities] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Availabilities_Formats_FormatId] FOREIGN KEY ([FormatId]) REFERENCES [Formats] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Availabilities_Stores_StoreId] FOREIGN KEY ([StoreId]) REFERENCES [Stores] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_Availabilities_FormatId] ON [Availabilities] ([FormatId]);

CREATE INDEX [IX_Availabilities_StoreId] ON [Availabilities] ([StoreId]);

CREATE UNIQUE INDEX [IX_Categories_Name] ON [Categories] ([Name]);

CREATE UNIQUE INDEX [IX_Categories_Slug] ON [Categories] ([Slug]);

CREATE INDEX [IX_Formats_ProductId] ON [Formats] ([ProductId]);

CREATE UNIQUE INDEX [IX_Formats_Slug] ON [Formats] ([Slug]);

CREATE INDEX [IX_FormatType_CategoryId] ON [FormatType] ([CategoryId]);

CREATE INDEX [IX_Products_CategoryId] ON [Products] ([CategoryId]);

CREATE UNIQUE INDEX [IX_Products_Slug] ON [Products] ([Slug]);

CREATE UNIQUE INDEX [IX_Stores_Name] ON [Stores] ([Name]);

CREATE UNIQUE INDEX [IX_Stores_Slug] ON [Stores] ([Slug]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260104063644_InitialCreate', N'10.0.5');

COMMIT;
GO

BEGIN TRANSACTION;
DROP TABLE [FormatType];

CREATE TABLE [FormatTypes] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [CategoryId] int NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_FormatTypes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FormatTypes_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_FormatTypes_CategoryId] ON [FormatTypes] ([CategoryId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260105051523_FormatTypes', N'10.0.5');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [Stores] ADD [IsLocked] bit NOT NULL DEFAULT CAST(0 AS bit);

ALTER TABLE [Stores] ADD [LockedByUser] nvarchar(max) NULL;

ALTER TABLE [Stores] ADD [lockedReason] nvarchar(max) NULL;

ALTER TABLE [Products] ADD [LockedByUser] nvarchar(max) NULL;

ALTER TABLE [Products] ADD [lockedReason] nvarchar(max) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260110012439_AddLocksToSoftDeletable', N'10.0.5');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [Categories] ADD [DeletedDate] datetime2 NULL;

ALTER TABLE [Categories] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);

ALTER TABLE [Categories] ADD [IsLocked] bit NOT NULL DEFAULT CAST(0 AS bit);

ALTER TABLE [Categories] ADD [LockedByUser] nvarchar(max) NULL;

ALTER TABLE [Categories] ADD [lockedReason] nvarchar(max) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260110012734_SoftDeletableCategories', N'10.0.5');

COMMIT;
GO

BEGIN TRANSACTION;
DECLARE @var nvarchar(max);
SELECT @var = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'LockedBy');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT ' + @var + ';');
ALTER TABLE [Products] DROP COLUMN [LockedBy];

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260110015058_removedDuplicateProductLockedByAttribute', N'10.0.5');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [Stores] ADD [LockedDate] datetime2 NULL;

ALTER TABLE [Products] ADD [LockedDate] datetime2 NULL;

ALTER TABLE [Formats] ADD [LockedDate] datetime2 NULL;

ALTER TABLE [Categories] ADD [LockedDate] datetime2 NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260111054510_LockDate', N'10.0.5');

COMMIT;
GO

BEGIN TRANSACTION;
DECLARE @var1 nvarchar(max);
SELECT @var1 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FormatTypes]') AND [c].[name] = N'Name');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [FormatTypes] DROP CONSTRAINT ' + @var1 + ';');
ALTER TABLE [FormatTypes] ALTER COLUMN [Name] nvarchar(50) NOT NULL;

DECLARE @var2 nvarchar(max);
SELECT @var2 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FormatTypes]') AND [c].[name] = N'Description');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [FormatTypes] DROP CONSTRAINT ' + @var2 + ';');
ALTER TABLE [FormatTypes] ALTER COLUMN [Description] nvarchar(150) NOT NULL;

DROP INDEX [IX_Categories_Name] ON [Categories];
DECLARE @var3 nvarchar(max);
SELECT @var3 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Categories]') AND [c].[name] = N'Name');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Categories] DROP CONSTRAINT ' + @var3 + ';');
ALTER TABLE [Categories] ALTER COLUMN [Name] nvarchar(50) NOT NULL;
CREATE UNIQUE INDEX [IX_Categories_Name] ON [Categories] ([Name]);

DECLARE @var4 nvarchar(max);
SELECT @var4 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Categories]') AND [c].[name] = N'Description');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Categories] DROP CONSTRAINT ' + @var4 + ';');
ALTER TABLE [Categories] ALTER COLUMN [Description] nvarchar(250) NULL;

ALTER TABLE [Categories] ADD [CreatorsTitle] nvarchar(50) NULL;

ALTER TABLE [Categories] ADD [EditionTitle] nvarchar(50) NULL;

ALTER TABLE [Categories] ADD [FormatsTitle] nvarchar(50) NULL;

ALTER TABLE [Categories] ADD [TypeTitle] nvarchar(50) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260206223140_CategoryAttributeTitles', N'10.0.5');

COMMIT;
GO

BEGIN TRANSACTION;
CREATE TABLE [NewsBlogs] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [ArticleType] nvarchar(max) NOT NULL,
    [Slug] nvarchar(max) NOT NULL,
    [AuthorId] nvarchar(max) NULL,
    [ModifierId] nvarchar(max) NULL,
    [NewsArticle] nvarchar(max) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedDate] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    [DeletedDate] datetime2 NULL,
    [IsLocked] bit NOT NULL,
    [lockedReason] nvarchar(max) NULL,
    [LockedByUser] nvarchar(max) NULL,
    [LockedDate] datetime2 NULL,
    CONSTRAINT [PK_NewsBlogs] PRIMARY KEY ([Id])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260220010705_NewsBlogInitiate', N'10.0.5');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [NewsBlogs] ADD [CoverImageUrl] nvarchar(max) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260220012515_NewsBlogCoverImage', N'10.0.5');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [Formats] DROP CONSTRAINT [FK_Formats_Products_ProductId];

ALTER TABLE [Formats] ADD [FormatTypeId] int NULL;

CREATE INDEX [IX_Formats_FormatTypeId] ON [Formats] ([FormatTypeId]);

ALTER TABLE [Formats] ADD CONSTRAINT [FK_Formats_FormatTypes_FormatTypeId] FOREIGN KEY ([FormatTypeId]) REFERENCES [FormatTypes] ([Id]);

ALTER TABLE [Formats] ADD CONSTRAINT [FK_Formats_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260223233800_AddFormatTypeToFormat', N'10.0.5');

COMMIT;
GO

BEGIN TRANSACTION;
DECLARE @var5 nvarchar(max);
SELECT @var5 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Formats]') AND [c].[name] = N'Type');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Formats] DROP CONSTRAINT ' + @var5 + ';');
ALTER TABLE [Formats] ALTER COLUMN [Type] nvarchar(max) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260224004509_MakeFormatTypeIdNonNullable', N'10.0.5');

COMMIT;
GO

BEGIN TRANSACTION;
BEGIN TRANSACTION;
DECLARE @catId INT;
IF EXISTS (SELECT 1 FROM Categories WHERE Name = 'Unknown' OR Slug = 'unknown')
    SELECT @catId = Id FROM Categories WHERE Name = 'Unknown' OR Slug = 'unknown';
ELSE
BEGIN
    INSERT INTO Categories (Name, Slug, CreatedDate, ModifiedDate)
    VALUES ('Unknown', 'unknown', SYSUTCDATETIME(), SYSUTCDATETIME());
    SET @catId = SCOPE_IDENTITY();
END

DECLARE @ftId INT;
IF EXISTS (SELECT 1 FROM FormatTypes WHERE Name = 'Unknown' AND CategoryId = @catId)
    SELECT @ftId = Id FROM FormatTypes WHERE Name = 'Unknown' AND CategoryId = @catId;
ELSE
BEGIN
    INSERT INTO FormatTypes (Name, Description, CategoryId, CreatedDate, ModifiedDate)
    VALUES ('Unknown', 'Auto-created default FormatType', @catId, SYSUTCDATETIME(), SYSUTCDATETIME());
    SET @ftId = SCOPE_IDENTITY();
END

UPDATE Formats SET FormatTypeId = @ftId WHERE FormatTypeId IS NULL;

COMMIT TRANSACTION;


DROP INDEX [IX_Formats_FormatTypeId] ON [Formats];
DECLARE @var6 nvarchar(max);
SELECT @var6 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Formats]') AND [c].[name] = N'FormatTypeId');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Formats] DROP CONSTRAINT ' + @var6 + ';');
ALTER TABLE [Formats] ALTER COLUMN [FormatTypeId] int NOT NULL;
CREATE INDEX [IX_Formats_FormatTypeId] ON [Formats] ([FormatTypeId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260224005414_MakeFormatTypeIdNonNullablePart2', N'10.0.5');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [Formats] DROP CONSTRAINT [FK_Formats_FormatTypes_FormatTypeId];

ALTER TABLE [Formats] DROP CONSTRAINT [FK_Formats_Products_ProductId];

DROP INDEX [IX_Formats_FormatTypeId] ON [Formats];

DECLARE @var7 nvarchar(max);
SELECT @var7 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Formats]') AND [c].[name] = N'FormatTypeId');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Formats] DROP CONSTRAINT ' + @var7 + ';');
ALTER TABLE [Formats] DROP COLUMN [FormatTypeId];

ALTER TABLE [Formats] ADD CONSTRAINT [FK_Formats_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260224210645_RemoveFormatTypeFromFormats', N'10.0.5');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [Formats] DROP CONSTRAINT [FK_Formats_Products_ProductId];

ALTER TABLE [Formats] ADD [FormatTypeId] int NULL DEFAULT 0;

CREATE INDEX [IX_Formats_FormatTypeId] ON [Formats] ([FormatTypeId]);

ALTER TABLE [Formats] ADD CONSTRAINT [FK_Formats_FormatTypes_FormatTypeId] FOREIGN KEY ([FormatTypeId]) REFERENCES [FormatTypes] ([Id]);

ALTER TABLE [Formats] ADD CONSTRAINT [FK_Formats_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260224214202_ReSetFormatTypeOnFormat', N'10.0.5');

COMMIT;
GO

BEGIN TRANSACTION;
BEGIN TRANSACTION;
DECLARE @catId INT;
IF EXISTS (SELECT 1 FROM Categories WHERE Name = 'Unknown' OR Slug = 'unknown')
    SELECT @catId = Id FROM Categories WHERE Name = 'Unknown' OR Slug = 'unknown';
ELSE
BEGIN
    INSERT INTO Categories (Name, Slug, CreatedDate, ModifiedDate)
    VALUES ('Unknown', 'unknown', SYSUTCDATETIME(), SYSUTCDATETIME());
    SET @catId = SCOPE_IDENTITY();
END

DECLARE @ftId INT;
IF EXISTS (SELECT 1 FROM FormatTypes WHERE Name = 'Unknown' AND CategoryId = @catId)
    SELECT @ftId = Id FROM FormatTypes WHERE Name = 'Unknown' AND CategoryId = @catId;
ELSE
BEGIN
    INSERT INTO FormatTypes (Name, Description, CategoryId, CreatedDate, ModifiedDate)
    VALUES ('Unknown', 'Auto-created default FormatType', @catId, SYSUTCDATETIME(), SYSUTCDATETIME());
    SET @ftId = SCOPE_IDENTITY();
END

UPDATE Formats SET FormatTypeId = @ftId WHERE FormatTypeId IS NULL;

COMMIT TRANSACTION;


DROP INDEX [IX_Formats_FormatTypeId] ON [Formats];
DECLARE @var8 nvarchar(max);
SELECT @var8 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Formats]') AND [c].[name] = N'FormatTypeId');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Formats] DROP CONSTRAINT ' + @var8 + ';');
ALTER TABLE [Formats] ALTER COLUMN [FormatTypeId] int NOT NULL;
CREATE INDEX [IX_Formats_FormatTypeId] ON [Formats] ([FormatTypeId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260224220920_ReMakeFormatTypeIdNonNullable', N'10.0.5');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [FormatTypes] ADD [ImageUrl] nvarchar(max) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260311221033_AddImagesToFormatTypes', N'10.0.5');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [Formats] ADD [IsInPrint] bit NOT NULL DEFAULT CAST(0 AS bit);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260325220657_IsInPrintFormat', N'10.0.5');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [NewsBlogs] ADD [StickToFrontPage] bit NOT NULL DEFAULT CAST(0 AS bit);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260328034944_NewsBlogStickToFrontPage', N'10.0.5');

COMMIT;
GO

