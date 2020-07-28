USE [Book_Store]
GO
/****** Object:  StoredProcedure [dbo].[spGetListOfWishListByUserID]    Script Date: 7/28/2020 7:56:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[spGetListOfWishListByUserID]
@UserID int
as
begin try
	begin
		select * from WishList join Books on WishList.BookID = Books.BookID where 
				IsDelete = 'false' and IsMoved = 'false' and UserID = @UserID
	end
end try
begin catch
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch