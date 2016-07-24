CREATE TABLE [dbo].[user]
(
	[id] INT NOT NULL PRIMARY KEY, 
    [insertStreamAllowed] BIT NULL DEFAULT 0, 
    [name] TEXT NULL
)
	