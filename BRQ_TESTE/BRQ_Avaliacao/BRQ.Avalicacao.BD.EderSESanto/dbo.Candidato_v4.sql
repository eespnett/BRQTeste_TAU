CREATE TABLE [dbo].[Candidato] (
    [Id]    INT            NOT NULL IDENTITY,
    [Nome]  NVARCHAR (150) NULL,
    [Email] NVARCHAR (150) NULL,
    [Idade] INT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

