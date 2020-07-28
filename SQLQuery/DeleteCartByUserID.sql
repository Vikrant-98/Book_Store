USE [Book_Store]
GO
/****** Object:  StoredProcedure [dbo].[spDeleteCartByUserId]    Script Date: 7/28/2020 7:55:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[spDeleteCartByUserId]
@UserID int,
@CartID int

as
begin try
	if exists(select * from Cart where CartID = @CartID and UserID = @UserID )
	begin
		update Cart
			set IsDelete = 'true',
				IsActive = 'false'
			where CartID = @CartID 
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