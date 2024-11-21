CREATE TABLE [dbo].[Users] (
    [UserId]               INT                IDENTITY (1, 1) NOT NULL,
    [UserName]             NVARCHAR (256)     NOT NULL,
    [NormalizedUserName]   NVARCHAR (256)     NOT NULL,
    [Email]                NVARCHAR (256)     NOT NULL,
    [NormalizedEmail]      NVARCHAR (256)     NOT NULL,
    [EmailConfirmed]       BIT                NOT NULL,
    [PasswordHash]         NVARCHAR (MAX)     NULL,
    [SecurityStamp]        NVARCHAR (MAX)     NULL,
    [ConcurrencyStamp]     NVARCHAR (MAX)     NULL,
    [PhoneNumber]          NVARCHAR (15)      NULL,
    [PhoneNumberConfirmed] BIT                NOT NULL,
    [TwoFactorEnabled]     BIT                NOT NULL,
    [LockoutEnd]           DATETIMEOFFSET (7) NULL,
    [LockoutEnabled]       BIT                NOT NULL,
    [AccessFailedCount]    INT                NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC)
);

