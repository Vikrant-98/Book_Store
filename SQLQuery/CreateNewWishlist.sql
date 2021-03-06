USE [Book_Store]
GO
/****** Object:  StoredProcedure [dbo].[spCreateNewWishList]    Script Date: 8/10/2020 11:31:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[spCreateNewWishList]
@UserID int,
@BookID int,
@IsDelete bit,
@IsMoved bit,
@CreateDate datetime2(7),
@ModifiedDate datetime2(7)
as
begin try
	begin transaction
			declare @available int,
					@TempQuantity int
			select @available = BookAvailable from Books where BookID = @BookID
			
			if not exists(select WishListID from WishList where 
			BookID = @BookID and UserID = @UserID and IsDelete = 'false' and IsMoved = 'false')
			begin
				INSERT INTO WishList(UserID,BookID,IsDelete,IsMoved,CreateDate,ModifiedDate)
				values(@UserID,@BookID,@IsDelete,@IsMoved,@CreateDate,@ModifiedDate);	
				select * from WishList join Books on WishList.BookID = Books.BookID where 
				IsDelete = 'false' and WishList.BookID=@BookID and UserID = @UserID;
				commit transaction
			end
			else if exists(select WishListID from WishList where 
			BookID = @BookID and UserID = @UserID and IsDelete = 'false' and IsMoved = 'false')
			begin
				select * from WishList join Books on WishList.BookID = Books.BookID where 
				IsDelete = 'false' and WishList.BookID=@BookID and UserID = @UserID;
				commit transaction
			end
			else
			begin
				rollback transaction
				raiserror('No WishList Added',11,1)
			end
end try
begin catch
		rollback transaction
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch