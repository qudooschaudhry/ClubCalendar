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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220321234703_InitialSchema')
BEGIN
    CREATE TABLE [Clubs] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(512) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Clubs] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220321234703_InitialSchema')
BEGIN
    CREATE TABLE [Events] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [StartDate] datetimeoffset NOT NULL,
        [EndDate] datetimeoffset NOT NULL,
        [Capacity] int NOT NULL,
        [ClubId] int NULL,
        CONSTRAINT [PK_Events] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Events_Clubs_ClubId] FOREIGN KEY ([ClubId]) REFERENCES [Clubs] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220321234703_InitialSchema')
BEGIN
    CREATE TABLE [Registrations] (
        [Id] int NOT NULL IDENTITY,
        [Email] nvarchar(max) NOT NULL,
        [FirstName] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [EventId] int NULL,
        CONSTRAINT [PK_Registrations] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Registrations_Events_EventId] FOREIGN KEY ([EventId]) REFERENCES [Events] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220321234703_InitialSchema')
BEGIN
    CREATE INDEX [IX_Events_ClubId] ON [Events] ([ClubId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220321234703_InitialSchema')
BEGIN
    CREATE INDEX [IX_Registrations_EventId] ON [Registrations] ([EventId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220321234703_InitialSchema')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220321234703_InitialSchema', N'6.0.3');
END;
GO

COMMIT;
GO

