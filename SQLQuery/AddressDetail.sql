USE [Book_Store]
GO
/****** Object:  StoredProcedure [dbo].[spAddressDetail]    Script Date: 8/10/2020 11:27:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[spAddressDetail]
@UserID int,
@Name varchar(255),
@Locality varchar(255),
@City varchar(255),
@Address varchar(255),
@PhoneNumber varchar(255),
@PinCode varchar(255),
@LandMark varchar(255),
@CreateDate datetime2(7),
@ModifiedDate datetime2(7)
as
begin try
	begin transaction
	if not exists (select * from UserAddress where UserID = @UserID and Address = @Address and Locality = @Locality)
	begin
		
		insert into UserAddress(UserID,Name,Locality,City,Address,PhoneNumber,PinCode,LandMark,CreateDate,ModifiedDate)
		values(@UserID,@Name,@Locality,@City,@Address,@PhoneNumber,@PinCode,@LandMark,@CreateDate,@ModifiedDate)

		select * from UserAddress where UserID = @UserID and Address = @Address and Locality = @Locality

	end
	else if exists (select * from UserAddress where UserID = @UserID and Address = @Address and Locality = @Locality)
	begin
		select * from UserAddress where UserID = @UserID and Address = @Address and Locality = @Locality
	end
	else
	begin
		raiserror('No Address Added', 11, 1)
	end
	commit transaction
end try
begin catch
	rollback transaction
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch