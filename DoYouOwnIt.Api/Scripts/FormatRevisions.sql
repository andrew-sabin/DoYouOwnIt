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
VALUES (N'20260104063644_InitialCreate', N'10.0.7');

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
VALUES (N'20260105051523_FormatTypes', N'10.0.7');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [Stores] ADD [IsLocked] bit NOT NULL DEFAULT CAST(0 AS bit);

ALTER TABLE [Stores] ADD [LockedByUser] nvarchar(max) NULL;

ALTER TABLE [Stores] ADD [lockedReason] nvarchar(max) NULL;

ALTER TABLE [Products] ADD [LockedByUser] nvarchar(max) NULL;

ALTER TABLE [Products] ADD [lockedReason] nvarchar(max) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260110012439_AddLocksToSoftDeletable', N'10.0.7');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [Categories] ADD [DeletedDate] datetime2 NULL;

ALTER TABLE [Categories] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);

ALTER TABLE [Categories] ADD [IsLocked] bit NOT NULL DEFAULT CAST(0 AS bit);

ALTER TABLE [Categories] ADD [LockedByUser] nvarchar(max) NULL;

ALTER TABLE [Categories] ADD [lockedReason] nvarchar(max) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260110012734_SoftDeletableCategories', N'10.0.7');

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
VALUES (N'20260110015058_removedDuplicateProductLockedByAttribute', N'10.0.7');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [Stores] ADD [LockedDate] datetime2 NULL;

ALTER TABLE [Products] ADD [LockedDate] datetime2 NULL;

ALTER TABLE [Formats] ADD [LockedDate] datetime2 NULL;

ALTER TABLE [Categories] ADD [LockedDate] datetime2 NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260111054510_LockDate', N'10.0.7');

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
VALUES (N'20260206223140_CategoryAttributeTitles', N'10.0.7');

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
VALUES (N'20260220010705_NewsBlogInitiate', N'10.0.7');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [NewsBlogs] ADD [CoverImageUrl] nvarchar(max) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260220012515_NewsBlogCoverImage', N'10.0.7');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [Formats] DROP CONSTRAINT [FK_Formats_Products_ProductId];

ALTER TABLE [Formats] ADD [FormatTypeId] int NULL;

CREATE INDEX [IX_Formats_FormatTypeId] ON [Formats] ([FormatTypeId]);

ALTER TABLE [Formats] ADD CONSTRAINT [FK_Formats_FormatTypes_FormatTypeId] FOREIGN KEY ([FormatTypeId]) REFERENCES [FormatTypes] ([Id]);

ALTER TABLE [Formats] ADD CONSTRAINT [FK_Formats_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260223233800_AddFormatTypeToFormat', N'10.0.7');

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
VALUES (N'20260224004509_MakeFormatTypeIdNonNullable', N'10.0.7');

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
VALUES (N'20260224005414_MakeFormatTypeIdNonNullablePart2', N'10.0.7');

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
VALUES (N'20260224210645_RemoveFormatTypeFromFormats', N'10.0.7');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [Formats] DROP CONSTRAINT [FK_Formats_Products_ProductId];

ALTER TABLE [Formats] ADD [FormatTypeId] int NULL DEFAULT 0;

CREATE INDEX [IX_Formats_FormatTypeId] ON [Formats] ([FormatTypeId]);

ALTER TABLE [Formats] ADD CONSTRAINT [FK_Formats_FormatTypes_FormatTypeId] FOREIGN KEY ([FormatTypeId]) REFERENCES [FormatTypes] ([Id]);

ALTER TABLE [Formats] ADD CONSTRAINT [FK_Formats_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260224214202_ReSetFormatTypeOnFormat', N'10.0.7');

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
VALUES (N'20260224220920_ReMakeFormatTypeIdNonNullable', N'10.0.7');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [FormatTypes] ADD [ImageUrl] nvarchar(max) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260311221033_AddImagesToFormatTypes', N'10.0.7');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [Formats] ADD [IsInPrint] bit NOT NULL DEFAULT CAST(0 AS bit);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260325220657_IsInPrintFormat', N'10.0.7');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [NewsBlogs] ADD [StickToFrontPage] bit NOT NULL DEFAULT CAST(0 AS bit);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260328034944_NewsBlogStickToFrontPage', N'10.0.7');

COMMIT;
GO

BEGIN TRANSACTION;
EXEC sp_rename N'[Products].[CreditsURL]', N'CategoryName', 'COLUMN';

DECLARE @var9 nvarchar(max);
SELECT @var9 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'Name');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT ' + @var9 + ';');
ALTER TABLE [Products] ALTER COLUMN [Name] nvarchar(50) NOT NULL;

DECLARE @var10 nvarchar(max);
SELECT @var10 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'MatureAudienceReason');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT ' + @var10 + ';');
ALTER TABLE [Products] ALTER COLUMN [MatureAudienceReason] nvarchar(150) NULL;

DECLARE @var11 nvarchar(max);
SELECT @var11 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'Creators');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT ' + @var11 + ';');
ALTER TABLE [Products] ALTER COLUMN [Creators] nvarchar(50) NULL;

DECLARE @var12 nvarchar(max);
SELECT @var12 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'AIAssistsWith');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT ' + @var12 + ';');
ALTER TABLE [Products] ALTER COLUMN [AIAssistsWith] nvarchar(150) NULL;

ALTER TABLE [Formats] ADD [ModifierName] nvarchar(max) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260421222528_ProductCategoryNameFormatModifierName', N'10.0.7');

COMMIT;
GO

BEGIN TRANSACTION;
DECLARE @var13 nvarchar(max);
SELECT @var13 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Stores]') AND [c].[name] = N'lockedReason');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Stores] DROP CONSTRAINT ' + @var13 + ';');
ALTER TABLE [Stores] ALTER COLUMN [lockedReason] nvarchar(150) NULL;

ALTER TABLE [Stores] ADD [HasIssue] bit NOT NULL DEFAULT CAST(0 AS bit);

ALTER TABLE [Stores] ADD [Issue] nvarchar(150) NULL;

ALTER TABLE [Stores] ADD [IssueURL] nvarchar(100) NULL;

DECLARE @var14 nvarchar(max);
SELECT @var14 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'lockedReason');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT ' + @var14 + ';');
ALTER TABLE [Products] ALTER COLUMN [lockedReason] nvarchar(150) NULL;

DECLARE @var15 nvarchar(max);
SELECT @var15 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'CategoryName');
IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT ' + @var15 + ';');
UPDATE [Products] SET [CategoryName] = N'' WHERE [CategoryName] IS NULL;
ALTER TABLE [Products] ALTER COLUMN [CategoryName] nvarchar(max) NOT NULL;
ALTER TABLE [Products] ADD DEFAULT N'' FOR [CategoryName];

ALTER TABLE [Products] ADD [DescriptionSource] nvarchar(100) NULL;

ALTER TABLE [Products] ADD [HasIssue] bit NOT NULL DEFAULT CAST(0 AS bit);

ALTER TABLE [Products] ADD [Issue] nvarchar(150) NULL;

ALTER TABLE [Products] ADD [IssueURL] nvarchar(100) NULL;

DECLARE @var16 nvarchar(max);
SELECT @var16 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[NewsBlogs]') AND [c].[name] = N'lockedReason');
IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [NewsBlogs] DROP CONSTRAINT ' + @var16 + ';');
ALTER TABLE [NewsBlogs] ALTER COLUMN [lockedReason] nvarchar(150) NULL;

ALTER TABLE [NewsBlogs] ADD [HasIssue] bit NOT NULL DEFAULT CAST(0 AS bit);

ALTER TABLE [NewsBlogs] ADD [Issue] nvarchar(150) NULL;

ALTER TABLE [NewsBlogs] ADD [IssueURL] nvarchar(100) NULL;

DECLARE @var17 nvarchar(max);
SELECT @var17 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Formats]') AND [c].[name] = N'lockedReason');
IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [Formats] DROP CONSTRAINT ' + @var17 + ';');
ALTER TABLE [Formats] ALTER COLUMN [lockedReason] nvarchar(150) NULL;

ALTER TABLE [Formats] ADD [HasIssue] bit NOT NULL DEFAULT CAST(0 AS bit);

ALTER TABLE [Formats] ADD [Issue] nvarchar(150) NULL;

ALTER TABLE [Formats] ADD [IssueURL] nvarchar(100) NULL;

DECLARE @var18 nvarchar(max);
SELECT @var18 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Categories]') AND [c].[name] = N'lockedReason');
IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [Categories] DROP CONSTRAINT ' + @var18 + ';');
ALTER TABLE [Categories] ALTER COLUMN [lockedReason] nvarchar(150) NULL;

ALTER TABLE [Categories] ADD [HasIssue] bit NOT NULL DEFAULT CAST(0 AS bit);

ALTER TABLE [Categories] ADD [Issue] nvarchar(150) NULL;

ALTER TABLE [Categories] ADD [IssueURL] nvarchar(100) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260502005752_ProdDescriptionSourceIssuesSoftDelete', N'10.0.7');

COMMIT;
GO

BEGIN TRANSACTION;
DECLARE @var19 nvarchar(max);
SELECT @var19 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'LastModified');
IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT ' + @var19 + ';');
ALTER TABLE [Products] DROP COLUMN [LastModified];

DECLARE @var20 nvarchar(max);
SELECT @var20 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Formats]') AND [c].[name] = N'DisplayVideoUrl');
IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [Formats] DROP CONSTRAINT ' + @var20 + ';');
ALTER TABLE [Formats] DROP COLUMN [DisplayVideoUrl];

DECLARE @var21 nvarchar(max);
SELECT @var21 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Formats]') AND [c].[name] = N'LastModified');
IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [Formats] DROP CONSTRAINT ' + @var21 + ';');
ALTER TABLE [Formats] DROP COLUMN [LastModified];

DECLARE @var22 nvarchar(max);
SELECT @var22 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'DescriptionSource');
IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT ' + @var22 + ';');
ALTER TABLE [Products] ALTER COLUMN [DescriptionSource] nvarchar(50) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260502011150_RemovedLastModifiedandVideoURL', N'10.0.7');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [FormatTypes] ADD [HasIssue] bit NOT NULL DEFAULT CAST(0 AS bit);

ALTER TABLE [FormatTypes] ADD [Issue] nvarchar(150) NULL;

ALTER TABLE [FormatTypes] ADD [IssueURL] nvarchar(100) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260502012102_FormatTypeAddIssueWarning', N'10.0.7');

COMMIT;
GO

BEGIN TRANSACTION;
DECLARE @var23 nvarchar(max);
SELECT @var23 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'Name');
IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT ' + @var23 + ';');
ALTER TABLE [Products] ALTER COLUMN [Name] nvarchar(100) NOT NULL;

DECLARE @var24 nvarchar(max);
SELECT @var24 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'DescriptionSource');
IF @var24 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT ' + @var24 + ';');
ALTER TABLE [Products] ALTER COLUMN [DescriptionSource] nvarchar(150) NULL;

DECLARE @var25 nvarchar(max);
SELECT @var25 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'Creators');
IF @var25 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT ' + @var25 + ';');
ALTER TABLE [Products] ALTER COLUMN [Creators] nvarchar(100) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260506033911_UpdateFormatLengths', N'10.0.7');

COMMIT;
GO

BEGIN TRANSACTION;
DECLARE @var26 nvarchar(max);
SELECT @var26 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Formats]') AND [c].[name] = N'AIAssistsWith');
IF @var26 IS NOT NULL EXEC(N'ALTER TABLE [Formats] DROP CONSTRAINT ' + @var26 + ';');
ALTER TABLE [Formats] DROP COLUMN [AIAssistsWith];

DECLARE @var27 nvarchar(max);
SELECT @var27 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Formats]') AND [c].[name] = N'Description');
IF @var27 IS NOT NULL EXEC(N'ALTER TABLE [Formats] DROP CONSTRAINT ' + @var27 + ';');
ALTER TABLE [Formats] DROP COLUMN [Description];

DECLARE @var28 nvarchar(max);
SELECT @var28 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Formats]') AND [c].[name] = N'Edition');
IF @var28 IS NOT NULL EXEC(N'ALTER TABLE [Formats] DROP CONSTRAINT ' + @var28 + ';');
ALTER TABLE [Formats] DROP COLUMN [Edition];

DECLARE @var29 nvarchar(max);
SELECT @var29 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Formats]') AND [c].[name] = N'IsAIAssisted');
IF @var29 IS NOT NULL EXEC(N'ALTER TABLE [Formats] DROP CONSTRAINT ' + @var29 + ';');
ALTER TABLE [Formats] DROP COLUMN [IsAIAssisted];

DECLARE @var30 nvarchar(max);
SELECT @var30 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Formats]') AND [c].[name] = N'IsInPrint');
IF @var30 IS NOT NULL EXEC(N'ALTER TABLE [Formats] DROP CONSTRAINT ' + @var30 + ';');
ALTER TABLE [Formats] DROP COLUMN [IsInPrint];

DECLARE @var31 nvarchar(max);
SELECT @var31 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Formats]') AND [c].[name] = N'ReleaseDate');
IF @var31 IS NOT NULL EXEC(N'ALTER TABLE [Formats] DROP CONSTRAINT ' + @var31 + ';');
ALTER TABLE [Formats] DROP COLUMN [ReleaseDate];

DECLARE @var32 nvarchar(max);
SELECT @var32 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Formats]') AND [c].[name] = N'Type');
IF @var32 IS NOT NULL EXEC(N'ALTER TABLE [Formats] DROP CONSTRAINT ' + @var32 + ';');
ALTER TABLE [Formats] DROP COLUMN [Type];

EXEC sp_rename N'[Formats].[OwnershipLevel]', N'FormatRevisionId', 'COLUMN';

DECLARE @var33 nvarchar(max);
SELECT @var33 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Formats]') AND [c].[name] = N'FormatTypeId');
IF @var33 IS NOT NULL EXEC(N'ALTER TABLE [Formats] DROP CONSTRAINT ' + @var33 + ';');
ALTER TABLE [Formats] ALTER COLUMN [FormatTypeId] int NULL;

CREATE TABLE [FormatRevisions] (
    [Id] int NOT NULL IDENTITY,
    [FormatId] int NOT NULL,
    [Type] nvarchar(max) NULL,
    [FormatTypeId] int NOT NULL,
    [Edition] nvarchar(max) NULL,
    [ReleaseDate] date NULL,
    [IsAiAssisted] bit NOT NULL,
    [AIAssistsWith] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [OwnershipLevel] int NOT NULL,
    [IsInPrint] bit NOT NULL,
    [ModifierName] nvarchar(max) NULL,
    [ModifierId] nvarchar(max) NULL,
    [ContributerIds] nvarchar(max) NOT NULL,
    [PreviousRevisionId] int NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_FormatRevisions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FormatRevisions_FormatRevisions_PreviousRevisionId] FOREIGN KEY ([PreviousRevisionId]) REFERENCES [FormatRevisions] ([Id]),
    CONSTRAINT [FK_FormatRevisions_FormatTypes_FormatTypeId] FOREIGN KEY ([FormatTypeId]) REFERENCES [FormatTypes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_FormatRevisions_Formats_FormatId] FOREIGN KEY ([FormatId]) REFERENCES [Formats] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_FormatRevisions_FormatId] ON [FormatRevisions] ([FormatId]);

CREATE INDEX [IX_FormatRevisions_FormatTypeId] ON [FormatRevisions] ([FormatTypeId]);

CREATE INDEX [IX_FormatRevisions_PreviousRevisionId] ON [FormatRevisions] ([PreviousRevisionId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260512023339_AddFormatRevisions', N'10.0.7');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [FormatRevisions] ADD [EditSummary] nvarchar(max) NULL;

ALTER TABLE [FormatRevisions] ADD [RevisionNumber] int NOT NULL DEFAULT 0;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260512050345_FormatRevisionsEditSummary', N'10.0.7');

COMMIT;
GO

BEGIN TRANSACTION;
DECLARE @var34 nvarchar(max);
SELECT @var34 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Formats]') AND [c].[name] = N'ContributerIds');
IF @var34 IS NOT NULL EXEC(N'ALTER TABLE [Formats] DROP CONSTRAINT ' + @var34 + ';');
ALTER TABLE [Formats] DROP COLUMN [ContributerIds];

ALTER TABLE [Formats] ADD [CreatorName] nvarchar(max) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260515000135_RemoveContributerIdsFromFormat', N'10.0.7');

COMMIT;
GO

BEGIN TRANSACTION;
DECLARE @var35 nvarchar(max);
SELECT @var35 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Formats]') AND [c].[name] = N'FormatRevisionId');
IF @var35 IS NOT NULL EXEC(N'ALTER TABLE [Formats] DROP CONSTRAINT ' + @var35 + ';');
ALTER TABLE [Formats] DROP COLUMN [FormatRevisionId];

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260515003912_RemoveFormatRevisionIdFromFormat', N'10.0.7');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [Formats] ADD [formatrevisionid] int NOT NULL DEFAULT 0;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260515004514_ReAddFormatRevisionIdFromFormat', N'10.0.7');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [FormatRevisions] DROP CONSTRAINT [FK_FormatRevisions_FormatTypes_FormatTypeId];

ALTER TABLE [Formats] DROP CONSTRAINT [FK_Formats_FormatTypes_FormatTypeId];

DROP INDEX [IX_FormatRevisions_FormatTypeId] ON [FormatRevisions];

DECLARE @var36 nvarchar(max);
SELECT @var36 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FormatRevisions]') AND [c].[name] = N'Edition');
IF @var36 IS NOT NULL EXEC(N'ALTER TABLE [FormatRevisions] DROP CONSTRAINT ' + @var36 + ';');
ALTER TABLE [FormatRevisions] DROP COLUMN [Edition];

DECLARE @var37 nvarchar(max);
SELECT @var37 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FormatRevisions]') AND [c].[name] = N'FormatTypeId');
IF @var37 IS NOT NULL EXEC(N'ALTER TABLE [FormatRevisions] DROP CONSTRAINT ' + @var37 + ';');
ALTER TABLE [FormatRevisions] DROP COLUMN [FormatTypeId];

DECLARE @var38 nvarchar(max);
SELECT @var38 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FormatRevisions]') AND [c].[name] = N'ReleaseDate');
IF @var38 IS NOT NULL EXEC(N'ALTER TABLE [FormatRevisions] DROP CONSTRAINT ' + @var38 + ';');
ALTER TABLE [FormatRevisions] DROP COLUMN [ReleaseDate];

DECLARE @var39 nvarchar(max);
SELECT @var39 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FormatRevisions]') AND [c].[name] = N'Type');
IF @var39 IS NOT NULL EXEC(N'ALTER TABLE [FormatRevisions] DROP CONSTRAINT ' + @var39 + ';');
ALTER TABLE [FormatRevisions] DROP COLUMN [Type];

DROP INDEX [IX_Formats_FormatTypeId] ON [Formats];
DECLARE @var40 nvarchar(max);
SELECT @var40 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Formats]') AND [c].[name] = N'FormatTypeId');
IF @var40 IS NOT NULL EXEC(N'ALTER TABLE [Formats] DROP CONSTRAINT ' + @var40 + ';');
UPDATE [Formats] SET [FormatTypeId] = 0 WHERE [FormatTypeId] IS NULL;
ALTER TABLE [Formats] ALTER COLUMN [FormatTypeId] int NOT NULL;
ALTER TABLE [Formats] ADD DEFAULT 0 FOR [FormatTypeId];
CREATE INDEX [IX_Formats_FormatTypeId] ON [Formats] ([FormatTypeId]);

ALTER TABLE [Formats] ADD [Edition] nvarchar(max) NULL;

ALTER TABLE [Formats] ADD [ReleaseDate] date NULL;

ALTER TABLE [Formats] ADD [Type] nvarchar(max) NULL;

ALTER TABLE [Formats] ADD CONSTRAINT [FK_Formats_FormatTypes_FormatTypeId] FOREIGN KEY ([FormatTypeId]) REFERENCES [FormatTypes] ([Id]) ON DELETE CASCADE;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260519003450_ReAddTypeFormatTypeEditionAndReleaseDateToFormatEntities', N'10.0.7');

COMMIT;
GO

BEGIN TRANSACTION;
DECLARE @var41 nvarchar(max);
SELECT @var41 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FormatRevisions]') AND [c].[name] = N'ContributerIds');
IF @var41 IS NOT NULL EXEC(N'ALTER TABLE [FormatRevisions] DROP CONSTRAINT ' + @var41 + ';');
ALTER TABLE [FormatRevisions] DROP COLUMN [ContributerIds];

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260520054015_RemoveContributerIds', N'10.0.7');

COMMIT;
GO

