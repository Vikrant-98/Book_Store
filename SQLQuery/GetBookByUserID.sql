USE [Book_Store]
GO
/****** Object:  StoredProcedure [dbo].[spGetBooksByUserId]    Script Date: 7/28/2020 7:56:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[spGetBooksByUserId]
@UserID int
as
begin try
	begin
		select * from Cart join Books on Cart.BookID = Books.BookID where IsDelete = 'false' and IsActive = 'true' and UserID = @UserID
	end
end try
begin catch
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch