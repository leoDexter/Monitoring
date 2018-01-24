CREATE TABLE [dbo].[Temperatures] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [CityId]      INT             NOT NULL,
    [Degrees] DECIMAL (10, 2) NOT NULL,
    [Date]        DATETIME        NOT NULL,
    CONSTRAINT [PK_Temperatures] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Temperatures_City] FOREIGN KEY ([CityId]) REFERENCES [dbo].[City] ([Id])
);
GO