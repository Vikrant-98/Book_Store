USE [Book_Store]
GO
/****** Object:  StoredProcedure [dbo].[spUpdateBookById]    Script Date: 8/10/2020 11:34:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[spUpdateBookById]
@BookID int,
@AdminID int,
@BookName varchar(255)='',
@Description varchar(255)='',
@AuthorName varchar(255)='',
@BooksAvailable int=0,
@Price int=0,
@Pages int=0,
@Images varchar(255)='',
@ModifiedDate datetime
as
begin try
	begin transaction
	if exists(select * from Books where BookID = @BookID and IsDeleted = 'false')
	begin
	declare @TempBookName varchar(255),
			@TempDescription varchar(255),
			@TempAuthorName varchar(255),
			@TempBooksAvailable int,
			@TempPrice int,
			@TempPages int,
			@TempImages varchar(255)

	select @TempBookName = BookName,
		   @TempDescription = Description,
		   @TempAuthorName = AuthorName,
		   @TempBooksAvailable = BookAvailable,
		   @TempImages = Images,
		   @TempPrice = Price,
		   @TempPages = Pages
	 from Books where BookID = @BookID

	 if(@BookName = '')
	 begin
		select @BookName = @TempBookName
	 end
	 if(@Description = '')
	 begin
		select @Description = @TempDescription
	 end
	 if(@AuthorName = '')
	 begin
		select @AuthorName = @AuthorName
	 end
	 if(@BooksAvailable < 0)
	 begin
		select @BooksAvailable = @TempBooksAvailable
	 end
	 if(@Images = '')
	 begin
		select @Images = @TempImages
	 end
	 if(@Pages = 0)
	 begin
		select @Pages = @TempPages
	 end
	 if(@Price = 0)
	 begin
		select @Price = @TempPrice
	 end

	 update Books
			set BookName=@BookName,
				AuthorName=	@AuthorName,
				Description= @Description,
				AdminID=@AdminID,
				Price=@Price,
				Pages=@Pages,
				Images=@Images,
				BookAvailable=@BooksAvailable,
				ModifiedDate=@ModifiedDate
			where BookID = @BookID 

	 select * from Books where BookID = @BookID;
	 commit transaction
	 end
	 else
	 begin
		rollback transaction
		raiserror('No Such Book Available',11,1)
	 end
end try
begin catch
		rollback transaction
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch