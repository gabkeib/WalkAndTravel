CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Surname] NVARCHAR(50) NULL, 
    [Email] NVARCHAR(50) NOT NULL, 
    [Password] NVARCHAR(MAX) NOT NULL, 
    [Username] NVARCHAR(50) NULL, 
    [Age] INT NULL, 
    [Exp] INT NOT NULL DEFAULT 0, 
    [Level] INT NOT NULL
)
