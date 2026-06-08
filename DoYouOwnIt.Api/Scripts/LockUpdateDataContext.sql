BEGIN TRANSACTION;
ALTER TABLE [Stores] ADD [IsLocked] bit NOT NULL DEFAULT CAST(0 AS bit);

ALTER TABLE [Stores] ADD [LockedByUser] nvarchar(max) NULL;

ALTER TABLE [Stores] ADD [lockedReason] nvarchar(max) NULL;

ALTER TABLE [Products] ADD [LockedByUser] nvarchar(max) NULL;

ALTER TABLE [Products] ADD [lockedReason] nvarchar(max) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260110012439_AddLocksToSoftDeletable', N'10.0.1');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [Categories] ADD [DeletedDate] datetime2 NULL;

ALTER TABLE [Categories] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);

ALTER TABLE [Categories] ADD [IsLocked] bit NOT NULL DEFAULT CAST(0 AS bit);

ALTER TABLE [Categories] ADD [LockedByUser] nvarchar(max) NULL;

ALTER TABLE [Categories] ADD [lockedReason] nvarchar(max) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260110012734_SoftDeletableCategories', N'10.0.1');

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
VALUES (N'20260110015058_removedDuplicateProductLockedByAttribute', N'10.0.1');

COMMIT;
GO

