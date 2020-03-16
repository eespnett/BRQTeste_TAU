CREATE TABLE [dbo].[Candidato]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Nome] VARCHAR(150) NULL, 
    [Email] VARCHAR(150) NULL, 
    [Idade] INT NULL, 
    [NomePai] VARCHAR(150) NULL, 
    [NomeMae] VARCHAR(150) NULL, 
    [RG] VARCHAR(50) NULL
)
