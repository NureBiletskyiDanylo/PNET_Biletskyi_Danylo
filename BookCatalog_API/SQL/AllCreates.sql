-- Create Authors table
CREATE TABLE Authors (
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(MAX) NOT NULL,
    AuthorInfo NVARCHAR(MAX)
);

-- Create Users table
CREATE TABLE Users (
    Id INT IDENTITY PRIMARY KEY,
    Username NVARCHAR(MAX) NOT NULL,
    Email NVARCHAR(MAX) NOT NULL,
    PasswordHash VARBINARY(MAX) NOT NULL,
    PasswordSalt VARBINARY(MAX) NOT NULL
);

-- Create genres table
CREATE TABLE Genres (
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(MAX) NOT NULL
);

-- Create Books table
CREATE TABLE Books (
    Id INT IDENTITY PRIMARY KEY,
    Title NVARCHAR(MAX) NOT NULL,
    Description NVARCHAR(MAX),
    PublicationYear DATE NOT NULL,
    AuthorId INT NOT NULL,
    CoverUrl NVARCHAR(MAX),
    FOREIGN KEY (AuthorId) REFERENCES Authors(Id) ON DELETE CASCADE
);

-- Create book logs table
CREATE TABLE BookCreateLogs (
    Id INT IDENTITY PRIMARY KEY,
    BookId INT NOT NULL,
    Title NVARCHAR(MAX) NOT NULL,
    AuthorId INT NOT NULL,
    InsertedAt DATETIME2 NOT NULL,
    FOREIGN KEY (BookId) REFERENCES Books(Id) ON DELETE NO ACTION,
    FOREIGN KEY (AuthorId) REFERENCES Authors(Id) ON DELETE NO ACTION
);

-- Create book genre table
CREATE TABLE BookGenre (
    BookId INT NOT NULL,
    GenreId INT NOT NULL,
    PRIMARY KEY (BookId, GenreId),
    FOREIGN KEY (BookId) REFERENCES Books(Id) ON DELETE CASCADE,
    FOREIGN KEY (GenreId) REFERENCES Genres(Id) ON DELETE CASCADE
);

-- Create favourite table
CREATE TABLE Favourite (
    UserId INT NOT NULL,
    BookId INT NOT NULL,
    PRIMARY KEY (UserId, BookId),
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    FOREIGN KEY (BookId) REFERENCES Books(Id) ON DELETE CASCADE
);

