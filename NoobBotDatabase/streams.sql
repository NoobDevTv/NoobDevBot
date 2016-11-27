CREATE TABLE [dbo].[streams]
(
	[id]           INT           IDENTITY (1, 1) NOT NULL, 
	[userId]		INT not null,
	[start]  DateTime NOT NULL,
	[title] varchar(max) NOT NULL,
	[url] varchar(max) 
    CONSTRAINT [PK_streams] PRIMARY KEY ([id]) NULL,
	FOREIGN KEY ([userId]) REFERENCES [dbo].[user] ([id])
)
