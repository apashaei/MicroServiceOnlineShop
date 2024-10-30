CREATE TABLE [Categories] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Images] (
    [Id] int NOT NULL IDENTITY,
    [Src] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Images] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Products] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Image] nvarchar(max) NOT NULL,
    [Price] int NOT NULL,
    [CategoryId] uniqueidentifier NOT NULL,
    [SellNumber] int NOT NULL DEFAULT 0,
    [Inventory] int NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE
);
GO


CREATE INDEX [IX_Products_CategoryId] ON [Products] ([CategoryId]);
GO


