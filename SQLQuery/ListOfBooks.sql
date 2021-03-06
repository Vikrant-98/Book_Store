USE [Book_Store]
GO
/****** Object:  StoredProcedure [dbo].[spListOfBooks]    Script Date: 8/10/2020 11:33:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[spListOfBooks]
as
begin try
	begin transaction
	begin
		select * from Books where IsDeleted = 'false'
	end
	commit transaction
end try
begin catch
	rollback transaction
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch