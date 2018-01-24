CREATE TABLE [dbo].[City] (
    [Id]   INT             IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (150) NOT NULL,
    CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO