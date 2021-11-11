CREATE VIEW [dbo].[FullRoute]
	AS 
	SELECT Name, Length, Longitude, Latitude 
	FROM dbo.Routes AS A JOIN dbo.Markers ON RouteID = A.Id
