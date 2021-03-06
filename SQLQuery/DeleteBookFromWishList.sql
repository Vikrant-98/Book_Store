USE [Book_Store]
GO
/****** Object:  StoredProcedure [dbo].[spDeleteBookFromWishList]    Script Date: 8/10/2020 11:32:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[spDeleteBookFromWishList]
@UserID int,
@WishListID int

as
begin try
	if exists(select * from WishList where WishListID = @WishListID and UserID = @UserID )
	begin
		update WishList
			set IsDelete = 'true',
				IsMoved = 'false'
			where WishListID = @WishListID ;

	end
	else
	begin
		raiserror('Invalid User',11,1)
	end
end try
begin catch
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch