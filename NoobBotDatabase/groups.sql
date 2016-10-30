CREATE TABLE [dbo].[groups]
(
	[id]           INT           IDENTITY (1, 1) NOT NULL, 
	[name] varchar(max) not null,
	[power] int not null, 
    CONSTRAINT [PK_groups] PRIMARY KEY ([id])
)
