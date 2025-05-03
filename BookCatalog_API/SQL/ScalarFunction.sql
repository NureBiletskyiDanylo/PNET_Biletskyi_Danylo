/*CREATE FUNCTION dbo.GetAuthorBookCount(@AuthorId INT)
RETURNS INT
AS
BEGIN
    DECLARE @Count INT;
    SELECT @Count = COUNT(*) FROM Books WHERE AuthorId = @AuthorId;
    RETURN @Count;
END;*/
SELECT dbo.GetAuthorBookCount(4)
