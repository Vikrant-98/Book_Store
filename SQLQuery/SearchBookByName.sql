USE [Book_Store]
GO
/****** Object:  StoredProcedure [dbo].[spSearchBookByName]    Script Date: 8/10/2020 11:33:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[spSearchBookByName]
@search varchar(255)
as
begin try
	begin transaction
	select * from Books
	where BookName  like '%' +@search+ '%' or Description  like '%' +@search+ '%' or AuthorName like '%' +@search+ '%' and IsDeleted = 'false'
	commit transaction
end try
begin catch
	rollback transaction
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch