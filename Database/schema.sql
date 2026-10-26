CREATE TABLE [dbo].[Users] (
    [UserID] UNIQUEIDENTIFIER NOT NULL,
    [Username] NVARCHAR(50) NOT NULL,
    [Email] NVARCHAR(100) NOT NULL,
    [PasswordHash] NVARCHAR(256) NOT NULL,
    [FirstName] NVARCHAR(50) NULL,
    [LastName] NVARCHAR(50) NULL,
    [CreatedAt] DATETIME NULL,
    [LastLogin] DATETIME NULL,
    [IsEmailVerified] BIT NULL,
    [VerificationToken] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserID])
);
GO

CREATE TABLE [dbo].[PasswordResetTokens] (
    [TokenId] UNIQUEIDENTIFIER NOT NULL,
    [UserEmail] NVARCHAR(255) NOT NULL,
    [ExpirationDate] DATETIME NOT NULL,
    [IsUsed] BIT NOT NULL,
    CONSTRAINT [PK__Password__658FEEEAEF2EF3C6] PRIMARY KEY ([TokenId])
);
GO


