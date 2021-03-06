USE [Book_Store]
GO
/****** Object:  StoredProcedure [dbo].[spMoveBookToCart]    Script Date: 8/10/2020 11:33:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[spMoveBookToCart]
@UserID int,
@WishListID int,
@CreateDate datetime2(7),
@ModifiedDate datetime2(7)
as
begin try

		if exists (select * from WishList where WishListID = @WishListID and IsMoved = 'false' and  IsDelete = 'false')
	begin
		declare @BookID int
				
		select @BookID = BookID from WishList where WishListID = @WishListID
		begin
			exec spBookIntoCart @UserID,@BookID,'false','true',@CreateDate,@ModifiedDate
			update WishList 
			set IsMoved = 'true'
			where WishListID = @WishListID
		end

	end
	else
	begin

		raiserror('WishListId not Found',11, 1)
	end
end try
begin catch

			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch