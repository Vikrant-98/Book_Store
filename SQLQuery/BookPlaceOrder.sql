USE [Book_Store]
GO
/****** Object:  StoredProcedure [dbo].[spBookPlaceOrder]    Script Date: 8/10/2020 11:28:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[spBookPlaceOrder]
@UserID int,
@CartID int,
@Quantity int,
@AddressID int,
@CreateDate datetime2(7),
@ModifiedDate datetime2(7)
as
begin try
	begin transaction
	if exists (select * from Cart where CartID = @CartID and UserID = @UserID and IsActive = 'true' and IsDelete = 'false')
	begin
		declare @Available int,
				@BookID int,
				@Price int

		select @BookId = Books.BookID,@Available = BookAvailable,@Price = Price from Cart inner join Books 
		on Cart.BookID = Books.BookID where Cart.CartID = @CartID

		if (@Available >= @Quantity)
		begin
			insert into Orders(CartID, UserID, IsPlaced, TotalPrice,Quantity,AddressID,IsActive, CreateDate, ModifiedDate)
			values(@CartId, @UserId, 'true', @Quantity * @Price,@Quantity,@AddressID,'true', @CreateDate, @ModifiedDate)

			select OrderID, 
				   Cart.CartID, 
				   Users.UserID, 
				   Books.BookID, 
				   Books.BookName, 
				   Books.AuthorName, 
				   Books.Images , 
				   Books.Price,
				   Orders.Quantity,
				   Orders.IsActive,
				   Orders.IsPlaced,
				   Orders.TotalPrice  
					from Cart inner join Orders on Cart.CartID = Orders.CartID inner join Books 
					on Cart.CartId = @CartId and Books.BookId = @BookId 
					inner join Users on Orders.UserID = @UserID and Users.UserID = @UserID

			update Cart 
			set IsActive = 'false'
			where CartID = @CartID

			update Books
			set BookAvailable = BookAvailable - @Quantity
			where BookID = @BookID

			commit transaction

		end
		else
		begin
			rollback transaction
			raiserror('Book Is out of stock', 11, 1)
		end

	end
	else
	begin
		rollback transaction
		raiserror('CartId Not Found', 11, 1)
	end
end try
begin catch
	rollback transaction
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch