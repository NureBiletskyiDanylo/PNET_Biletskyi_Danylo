-- Authors insert
INSERT INTO Authors (Name, AuthorInfo) VALUES 
(N'Mark Twain', N'American author and humorist best known for "Adventures of Huckleberry Finn".'),
(N'Agatha Christie', N'Mystery writer famous for Hercule Poirot and Miss Marple stories.'),
(N'J.K. Rowling', N'British author known globally for the Harry Potter series.'),
(N'Ernest Hemingway', N'American novelist and journalist, known for his terse prose style.'),
(N'Toni Morrison', N'Pulitzer and Nobel Prize-winning author focusing on African-American life.'),
(N'Leo Tolstoy', N'Russian author famous for epics like "War and Peace" and "Anna Karenina".'),
(N'Isabel Allende', N'Chilean-American writer known for magical realism and historical fiction.');

-- Genres insert
INSERT INTO Genres (Name) VALUES 
(N'mystery'),
(N'historical fiction'),
(N'science fiction'),
(N'romance'),
(N'thriller');


-- book insert
INSERT INTO Books (Title, Description, PublicationYear, AuthorId, CoverUrl)
VALUES 
(N'Book 101', 
 N'Desc', 
 '2002-05-14', 
 3, 
 N'some url'),
 (N'Book 201', 
 N'Desc', 
 '2002-05-14', 
 4, 
 N'some url'),
 (N'Book 105', 
 N'Desc', 
 '2002-05-14', 
 5, 
 N'some url'),
 (N'Book 101', 
 N'Desc', 
 '2002-05-14', 
 6, 
 N'some url');


 -- book genres insert
 INSERT INTO BookGenre (BookId, GenreId) VALUES
(9, 2),
(10, 2),
(11, 2),
(12, 2),
(9, 1),
(10, 1),
(4, 3),
(5, 4),
(6, 3),
(7, 4)

-- favourite insert
INSERT INTO Favourite (UserId, BookId) VALUES
(6, 1), -- 1984
(6, 5), -- book 2
(6, 7) -- book 52