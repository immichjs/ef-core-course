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
GO

CREATE TABLE [Customer] (
    [Id] int NOT NULL IDENTITY,
    [Name] VARCHAR(100) NOT NULL,
    [Phone] VARCHAR(11) NOT NULL,
    [CEP] CHAR(8) NOT NULL,
    [UF] CHAR(2) NOT NULL,
    [City] nvarchar(60) NOT NULL,
    CONSTRAINT [PK_Customer] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Products] (
    [Id] int NOT NULL IDENTITY,
    [CodeBar] VARCHAR(14) NOT NULL,
    [Description] VARCHAR(512) NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [ProductType] nvarchar(max) NOT NULL,
    [Status] bit NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Orders] (
    [Id] int NOT NULL IDENTITY,
    [CustomerId] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
    [FinishedAt] datetime2 NOT NULL,
    [FreightType] int NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [Observation] VARCHAR(512) NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Orders_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [OrderItems] (
    [Id] int NOT NULL IDENTITY,
    [OrderId] int NOT NULL,
    [ProductId] int NOT NULL,
    [Quantity] int NOT NULL DEFAULT 1,
    [Price] decimal(18,2) NOT NULL,
    [Discount] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_OrderItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [idx_customer_phone] ON [Customer] ([Phone]);
GO

CREATE INDEX [IX_OrderItems_OrderId] ON [OrderItems] ([OrderId]);
GO

CREATE INDEX [IX_OrderItems_ProductId] ON [OrderItems] ([ProductId]);
GO

CREATE INDEX [IX_Orders_CustomerId] ON [Orders] ([CustomerId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230114045837_PrimeiraMigration', N'7.0.2');
GO

COMMIT;
GO

