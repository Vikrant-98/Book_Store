USE [Book_Store]
GO
/****** Object:  StoredProcedure [dbo].[spSortedBooksDetails]    Script Date: 7/28/2020 7:57:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spSortedBooksDetails]
@SortCol varchar(255),
@SortDir varchar(255)
AS
BEGIN

    With CTE_Books as
(Select ROW_NUMBER() over
(order by
 
case when (@SortCol = 1 and @SortDir='asc')
then BookName
end asc,
case when (@SortCol = 1 and @SortDir='desc')
then BookName
end desc,

case when (@SortCol = 4 and @SortDir='asc')
then Price
end asc,
case when (@SortCol = 4 and @SortDir='desc')
then Price
end desc,

case when (@SortCol = 5 and @SortDir='asc')
then Pages
end asc,
case when (@SortCol = 5 and @SortDir='desc')
then pages
end desc
)
as RowNum,
COUNT(*) over() as TotalCount,
BookID,
AdminID,
BookName,
Description,
AuthorName,
BookAvailable,
Price,
Pages,
CreateDate,
ModifiedDate,
IsDeleted,
Images
from Books
where IsDeleted = 'false'
 )
Select *
from CTE_Books
END