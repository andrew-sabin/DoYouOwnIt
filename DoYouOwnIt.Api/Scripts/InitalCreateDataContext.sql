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
VALUES (N'20260104063644_InitialCreate', N'10.0.1');

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
VALUES (N'20260105051523_FormatTypes', N'10.0.1');

COMMIT;
GO

