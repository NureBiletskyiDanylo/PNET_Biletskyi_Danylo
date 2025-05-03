DECLARE @AuthorId INT, @AuthorName NVARCHAR(MAX), @BookCount INT;

DECLARE author_cursor CURSOR FOR
SELECT Id, Name FROM Authors;

OPEN author_cursor;
FETCH NEXT FROM author_cursor INTO @AuthorId, @AuthorName;

WHILE @@FETCH_STATUS = 0
BEGIN
    SELECT @BookCount = COUNT(*) FROM Books WHERE AuthorId = @AuthorId;

    PRINT 'Author: ' + @AuthorName + ' | Books: ' + CAST(@BookCount AS NVARCHAR);

    FETCH NEXT FROM author_cursor INTO @AuthorId, @AuthorName;
END;

CLOSE author_cursor;
DEALLOCATE author_cursor;
