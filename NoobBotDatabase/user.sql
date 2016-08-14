CREATE TABLE [dbo].[user]
(
	[id] INT NOT NULL PRIMARY KEY, 
    [streamer] BIT NULL DEFAULT 0, 
    [name] TEXT NULL
)
	