USE [Book_Store]
GO
/****** Object:  StoredProcedure [dbo].[spUpdateBookById]    Script Date: 7/28/2020 7:57:57 PM ******/
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
@BooksAvailable int='',
@Price int='',
@Pages int='',
@Images varchar(255)='',
@ModifiedDate datetime
as
begin try

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
	 if(@BooksAvailable = '')
	 begin
		select @BooksAvailable = @TempBooksAvailable
	 end
	 if(@Images = '')
	 begin
		select @Images = @TempImages
	 end
	 if(@Pages = '')
	 begin
		select @Pages = @TempPages
	 end
	 if(@Price = '')
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

	 select * from Books where BookID = @BookID
	 end
	 else
	 begin
		raiserror('No Such Book Available',11,1)
	 end
end try
begin catch
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch