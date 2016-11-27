CREATE TABLE [dbo].[smilies]
(
	[id] INT NOT NULL identity(1,1),
	[unicode] varchar(6) not null, 
    CONSTRAINT [PK_smilies] PRIMARY KEY ([id])
	
)
