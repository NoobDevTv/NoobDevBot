CREATE TABLE [dbo].[groups_relation]
(
	[user_id] INT not null,
	[group_id] int not null, 
    CONSTRAINT [user_key] FOREIGN KEY ([user_id]) REFERENCES [user]([id]),
	CONSTRAINT [groups_key] FOREIGN KEY ([group_id]) REFERENCES [groups]([id]), 
    PRIMARY KEY ([user_id], [group_id])
)
