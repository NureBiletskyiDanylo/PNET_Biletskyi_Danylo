/*CREATE PROCEDURE GetTopFavouriteBooks
AS
BEGIN
    SELECT TOP 5 
        b.Id,
        b.Title,
        COUNT(*) AS FavouriteCount
    FROM Favourite f
    JOIN Books b ON f.BookId = b.Id
    GROUP BY b.Id, b.Title
    ORDER BY FavouriteCount DESC;
END;*/
EXEC GetTopFavouriteBooks
