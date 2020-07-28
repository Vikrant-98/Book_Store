USE [Book_Store]
GO
/****** Object:  StoredProcedure [dbo].[spListOfBooks]    Script Date: 7/28/2020 7:56:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[spListOfBooks]
as
begin try
	begin
		select * from Books where IsDeleted = 'false'
	end
end try
begin catch
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch