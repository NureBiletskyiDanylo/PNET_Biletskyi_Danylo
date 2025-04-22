/*CREATE FUNCTION dbo.GetBooksByGenre(@GenreId INT)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        b.Id,
        b.Title,
        a.Name AS AuthorName
    FROM BookGenre bg
    JOIN Books b ON bg.BookId = b.Id
    JOIN Authors a ON b.AuthorId = a.Id
    WHERE bg.GenreId = @GenreId
);*/
SELECT * FROM dbo.GetBooksByGenre(1);
