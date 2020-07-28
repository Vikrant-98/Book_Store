USE [Book_Store]
GO
/****** Object:  StoredProcedure [dbo].[spAdminLogin]    Script Date: 7/28/2020 7:54:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[spAdminLogin]

(

        @EmailID varchar(50),

        @Password varchar(50),

		@UserCategory varchar(50) = 'Admin'

)

AS
BEGIN try
	IF EXISTS(SELECT * FROM Users WHERE [EmailID] = @EmailID AND [Password] = @Password AND [UserCategory] = @UserCategory)
begin
	select * from Users where EmailID = @EmailID
end
else
begin
	raiserror('INVALID USER', 11 , 1)	
end
END try
begin catch
	DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end	catch