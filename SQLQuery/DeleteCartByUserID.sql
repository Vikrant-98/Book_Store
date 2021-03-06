USE [Book_Store]
GO
/****** Object:  StoredProcedure [dbo].[spDeleteCartByUserId]    Script Date: 8/10/2020 11:32:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[spDeleteCartByUserId]
@UserID int,
@CartID int

as
begin try
	begin transaction
	if exists(select * from Cart where CartID = @CartID and UserID = @UserID )
	begin
		update Cart
			set IsDelete = 'true',
				IsActive = 'false'
			where CartID = @CartID ;
			commit transaction 
	end
	else
	begin
		rollback transaction
		raiserror('Invalid User',11,1)
	end
end try
begin catch
		rollback transaction
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch