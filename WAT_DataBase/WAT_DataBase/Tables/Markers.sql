CREATE TABLE [dbo].[Markers]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [RouteID] INT NOT NULL, 
    [Latitude] FLOAT NOT NULL, 
    [Longitude] FLOAT NOT NULL, 
    CONSTRAINT [MarkersFK] FOREIGN KEY ([RouteID]) REFERENCES [Routes]([Id])
)
