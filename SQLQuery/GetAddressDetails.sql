USE [Book_Store]
GO
/****** Object:  StoredProcedure [dbo].[spGetAddressDetail]    Script Date: 8/10/2020 11:32:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[spGetAddressDetail]
@UserID int
as
begin try
	begin transaction
		select * from UserAddress where UserID = @UserID
	commit transaction
end try
begin catch
	rollback transaction
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch