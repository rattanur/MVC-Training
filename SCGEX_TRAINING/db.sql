IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190227033623_Initial')
BEGIN
    CREATE TABLE [Categories] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190227033623_Initial')
BEGIN
    CREATE TABLE [Companies] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(100) NOT NULL,
        [ContactName] nvarchar(100) NULL,
        CONSTRAINT [PK_Companies] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190227033623_Initial')
BEGIN
    CREATE TABLE [ProductInfos] (
        [Code] nvarchar(12) NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [Price] decimal(18,2) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CategoryId] int NOT NULL,
        [SupplierId] int NULL,
        CONSTRAINT [PK_ProductInfos] PRIMARY KEY ([Code]),
        CONSTRAINT [FK_ProductInfos_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ProductInfos_Companies_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [Companies] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190227033623_Initial')
BEGIN
    CREATE INDEX [IX_ProductInfos_CategoryId] ON [ProductInfos] ([CategoryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190227033623_Initial')
BEGIN
    CREATE INDEX [IX_ProductInfos_SupplierId] ON [ProductInfos] ([SupplierId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190227033623_Initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190227033623_Initial', N'2.2.2-servicing-10034');
END;

GO

