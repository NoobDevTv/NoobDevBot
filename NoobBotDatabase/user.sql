CREATE TABLE [dbo].[user]
(
	[id] INT NOT NULL PRIMARY KEY, 
    [group] int not NULL DEFAULT 0, 
    [name] varchar(max) NULL, 
    CONSTRAINT [group] FOREIGN KEY ([group]) REFERENCES [groups]([id])

	
)
	