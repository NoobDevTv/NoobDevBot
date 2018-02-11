CREATE TABLE [dbo].[rights_relation]
(
	[id] int not null identity(0,1),
	[is_group] bit not null default 0,
	[reference] int not null,
	[right_id] varchar(max) not null,
	[power] tinyint not null default 0, 
    CONSTRAINT [PK_rights_relation] PRIMARY KEY ([id])
	

)
