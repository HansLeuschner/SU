/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 [UserId]
      ,[LoginName]
      ,[Name]
      ,[Password]
      ,[CanBeGM]
      ,[IsAdmin]
  FROM [SU].[dbo].[Users]
  
  insert into users values ('Hans', 'Hans Leuschner', 'su', 1,1);