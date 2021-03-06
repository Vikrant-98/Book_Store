USE [Book_Store]
GO
/****** Object:  StoredProcedure [dbo].[spDeleteBookById]    Script Date: 8/10/2020 11:32:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[spDeleteBookById]
@BookID int,
@ModifiedDate datetime
as
begin try
	begin transaction
	if exists(select * from Books where BookID = @BookID and IsDeleted = 'false')
	begin
		update Books
			set IsDeleted = 'true',
				ModifiedDate = @ModifiedDate,
				BookAvailable = 0
			where BookID = @BookID 
			commit transaction
	end
	else
	begin
		rollback transaction
		raiserror('No Such Book Available To Delete',11,1)
	end
end try
begin catch
	rollback transaction
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch