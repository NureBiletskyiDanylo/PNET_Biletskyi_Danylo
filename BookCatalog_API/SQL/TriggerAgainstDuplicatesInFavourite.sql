/*GO
CREATE TRIGGER trg_PreventDuplicateFavourite
ON Favourite
INSTEAD OF INSERT
AS
BEGIN
    INSERT INTO Favourite (UserId, BookId)
    SELECT i.UserId, i.BookId
    FROM inserted i
    WHERE NOT EXISTS (
        SELECT 1 FROM Favourite f
        WHERE f.UserId = i.UserId AND f.BookId = i.BookId
    );
END;*/

/*
INSERT INTO Favourite (UserId, BookId)
VALUES (1, 4);
*/

--SELECT * FROM Favourite;
