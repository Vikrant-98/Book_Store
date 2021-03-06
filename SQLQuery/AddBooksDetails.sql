USE [Book_Store]
GO
/****** Object:  StoredProcedure [dbo].[spAddBooksDetail]    Script Date: 8/10/2020 11:25:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[spAddBooksDetail]

(

        @BookName varchar(255),
		@AdminID int,
        @AuthorName varchar(50),
		@Price int,
		@Pages int,
		@Description varchar(255),
		@BooksAvailable int,
		@IsDelete bit,
		@CreateDate datetime2(7),
		@ModifiedDate datetime2(7)
)

AS
BEGIN try
	begin transaction
	if not exists (select BookID from Books where BookName = @BookName)
	begin
		INSERT INTO Books(BookName,AdminID,AuthorName,Price,Pages,Description,BookAvailable,IsDeleted,CreateDate,ModifiedDate)
		VALUES(@BookName,
		@AdminID,
        @AuthorName,
		@Price,
		@Pages,
		@Description,
		@BooksAvailable,
		@IsDelete,
		@CreateDate,
		@ModifiedDate);

		select * from Books where BookName = @BookName;

	end 
	else if exists(select BookID from Books where BookName = @BookName and IsDeleted = 'true')
	begin
		update Books
			set BookName=@BookName,
				AuthorName=	@AuthorName,
				Description= @Description,
				IsDeleted=@IsDelete,
				AdminID=@AdminID,
				Price=@Price,
				Pages=@Pages,
				BookAvailable=@BooksAvailable,
				ModifiedDate=@ModifiedDate
			where BookName = @BookName 

			select * from Books where BookName = @BookName;

	end
	else
	begin
		raiserror('Book Already Exist',11,1);
	end
	commit transaction
END try
begin catch
	rollback transaction
	DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end	catch