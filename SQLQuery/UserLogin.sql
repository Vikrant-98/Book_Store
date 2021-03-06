USE [Book_Store]
GO
/****** Object:  StoredProcedure [dbo].[spUserLogin]    Script Date: 8/10/2020 11:34:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[spUserLogin]

(

        @EmailID varchar(50),

        @Password varchar(50),

		@UserCategory varchar(50) = 'User'

)

AS
BEGIN try
	begin transaction
SET NOCOUNT ON
	IF EXISTS(SELECT * FROM Users WHERE [EmailID] = @EmailID AND [Password] = @Password AND [UserCategory] = @UserCategory)
begin
	select * from Users where EmailID = @EmailID 
	commit transaction
end
else
begin
	rollback transaction
	raiserror('INVALID USER', 11 , 1)	
end
END try
begin catch
	rollback transaction
	DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end	catch