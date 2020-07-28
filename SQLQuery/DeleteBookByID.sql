USE [Book_Store]
GO
/****** Object:  StoredProcedure [dbo].[spDeleteBookById]    Script Date: 7/28/2020 7:55:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[spDeleteBookById]
@BookID int,
@ModifiedDate datetime
as
begin try

	if exists(select * from Books where BookID = @BookID and IsDeleted = 'false')
	begin
		update Books
			set IsDeleted = 'true',
				BookAvailable = 0
			where BookID = @BookID 
	end
	else
	begin
		raiserror('No Such Book Available To Delete',11,1)
	end
end try
begin catch
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch