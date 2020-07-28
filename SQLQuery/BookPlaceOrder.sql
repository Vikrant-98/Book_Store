USE [Book_Store]
GO
/****** Object:  StoredProcedure [dbo].[spBookPlaceOrder]    Script Date: 7/28/2020 7:54:50 PM ******/
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
	if exists (select * from Cart where CartID = @CartID and UserID = @UserID and IsActive = 'true' and IsDelete = 'false')
	begin
		declare @Available int,
				@BookID int,
				@Price int

		select @BookId = BookID from Cart where CartID = @CartID
		select @Price = Price,@Available = BookAvailable from Books where BookID = @BookId

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
		end
		else
		begin
			raiserror('Book Is out of stock', 11, 1)
		end

	end
	else
	begin
		raiserror('CartId Not Found', 11, 1)
	end
end try
begin catch
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch