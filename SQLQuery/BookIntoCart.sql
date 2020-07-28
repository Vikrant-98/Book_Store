USE [Book_Store]
GO
/****** Object:  StoredProcedure [dbo].[spBookIntoCart]    Script Date: 7/28/2020 7:54:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[spBookIntoCart]
@UserID int,
@BookID int,
@IsDelete bit,
@IsActive bit,
@CreateDate datetime2(7),
@ModifiedDate datetime2(7)
as
begin try
			declare @available int,
					@TempQuantity int
			select @available = BookAvailable from Books where BookID = @BookID
			
			if(@available > 0 )
			begin
			if not exists(select CartID from Cart where BookID = @BookID and UserID = @UserID)
			begin
				INSERT INTO Cart(UserID,BookID,IsDelete,IsActive,CreateDate,ModifiedDate)
				values(@UserID,@BookID,@IsDelete,@IsActive,@CreateDate,@ModifiedDate);		

				select * from Cart join Books on Cart.BookID = Books.BookID where 
				IsDelete = 'false' and IsActive = 'true' and UserID = @UserID and Books.BookID = @BookID

			end
			else if exists(select CartID from Cart where BookID = @BookID)
			begin
				select * from Cart join Books on Cart.BookID = Books.BookID where 
				IsDelete = 'false' and IsActive = 'true' and UserID = @UserID and Books.BookID = @BookID
			end
			else
				raiserror('No Cart Added',11,1)
			end
end try
begin catch
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch