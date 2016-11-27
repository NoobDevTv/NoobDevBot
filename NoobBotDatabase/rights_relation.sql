CREATE TABLE [dbo].[rights_relation]
(
	[is_group] bit not null default 0,
	[reference] int not null,
	[right_id] varchar(max) not null,
	[power] tinyint not null default 0
	

)
