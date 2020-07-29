USE [Book_Store]
GO
/****** Object:  StoredProcedure [dbo].[spGetAddressDetail]    Script Date: 7/29/2020 3:19:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[spGetAddressDetail]
@UserID int
as
begin try
	
		select * from UserAddress where UserID = @UserID
	
end try
begin catch
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch