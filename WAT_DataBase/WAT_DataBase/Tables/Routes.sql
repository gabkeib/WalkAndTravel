CREATE TABLE [dbo].[Routes]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL UNIQUE, 
    [Length] FLOAT NOT NULL, 
    [Type] INT NOT NULL, 
    [Author_Id] INT NULL, 
    CONSTRAINT [RoutesFK] FOREIGN KEY ([Author_Id]) REFERENCES [Users]([Id])
)
