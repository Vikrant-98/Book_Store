USE [Book_Store]
GO
/****** Object:  StoredProcedure [dbo].[spAddUserDetail]    Script Date: 7/28/2020 7:54:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[spAddUserDetail]
@FirstName varchar(255),
@LastName varchar(255),
@EmailID varchar(255),
@Password varchar(255),
@UserCategory varchar(255),
@CreateDate datetime2(7),
@ModifiedDate datetime2(7)
as
begin try
if not exists (select EmailID from Users where EmailID = @EmailID)
begin
	insert into Users(FirstName,LastName,UserCategory,EmailID,Password,CreateDate,ModifiedDate)
	values (@FirstName, @LastName , @UserCategory , @EmailID , @Password, @CreateDate,@ModifiedDate);

	select * from Users where EmailID = @EmailID
end
else
begin
	raiserror('Email Already Exist', 11 , 1)	
end
end try
begin catch
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch