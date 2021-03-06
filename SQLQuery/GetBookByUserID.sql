USE [Book_Store]
GO
/****** Object:  StoredProcedure [dbo].[spGetBooksByUserId]    Script Date: 8/10/2020 11:33:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[spGetBooksByUserId]
@UserID int
as
begin try
	begin transaction
	begin
		select * from Cart join Books on Cart.BookID = Books.BookID where IsDelete = 'false' and IsActive = 'true' and UserID = @UserID
	end
	commit transaction
end try
begin catch
	rollback transaction
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch