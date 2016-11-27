USE [NoobBotDatabase]
GO

/****** Objekt: Table [dbo].[smilies] Skriptdatum: 13.11.2016 20:25:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--DBCC CHECKIDENT ('[smilies]', RESEED, 0);
--GO

insert into smilies(unicode)
values 
--(N'😁');
--(N'🙅'),
--(N'🙆'),
(N'🇩🇪');
--(N'🙈'),
--(N'🙉'),
--(N'🙊'),
--(N'🙋'),
--(N'🙌'),
--(N'🙍'),
--(N'🙎'),
--(N'🙏');









select * from smilies
