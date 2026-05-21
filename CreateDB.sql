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

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260521035156_InitialSync', N'8.0.23');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AnalysisResults] (
    [Id] int NOT NULL IDENTITY,
    [ResultType] nvarchar(max) NOT NULL,
    [Confidence] float NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [Notes] nvarchar(max) NULL,
    CONSTRAINT [PK_AnalysisResults] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260521035322_AddAnalysisTable', N'8.0.23');
GO

COMMIT;
GO

