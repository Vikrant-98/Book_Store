USE [Book_Store]
GO
/****** Object:  StoredProcedure [dbo].[spGetListOfWishListByUserID]    Script Date: 8/10/2020 11:33:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[spGetListOfWishListByUserID]
@UserID int
as
begin try
	begin transaction
	begin
		select * from WishList join Books on WishList.BookID = Books.BookID where 
				IsDelete = 'false' and IsMoved = 'false' and UserID = @UserID
	end
	commit transaction
end try
begin catch
	rollback transaction
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch