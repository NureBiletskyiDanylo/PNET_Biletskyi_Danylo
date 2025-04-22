GO
CREATE TRIGGER trg_LogBookInsert
ON Books
AFTER INSERT
AS
BEGIN
    INSERT INTO BookCreateLogs (BookId, Title, AuthorId, InsertedAt)
    SELECT
        i.Id,
        i.Title,
        i.AuthorId,
        GETDATE()
    FROM inserted i;
END;
