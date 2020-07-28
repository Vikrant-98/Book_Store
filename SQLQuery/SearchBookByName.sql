USE [Book_Store]
GO
/****** Object:  StoredProcedure [dbo].[spSearchBookByName]    Script Date: 7/28/2020 7:57:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[spSearchBookByName]
@search varchar(255)
as
begin try
	select * from Books
	where BookName  like '%' +@search+ '%' or Description  like '%' +@search+ '%' or AuthorName like '%' +@search+ '%' and IsDeleted = 'false'
end try
begin catch
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch