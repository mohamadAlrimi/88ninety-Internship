DROP DATABASE University;
CREATE DATABASE University;
USE University;

CREATE TABLE Users (
    UserId INT PRIMARY KEY,
    UserName VARCHAR(64) NOT NULL,
    FirstName VARCHAR(64) NOT NULL,
    LastName VARCHAR(64) NOT NULL,
    EmailAddress VARCHAR(128) NOT NULL UNIQUE, 
    PhoneNumber VARCHAR(16) NOT NULL,
    Role VARCHAR(32) NOT NULL
);

CREATE TABLE Courses (
    CourseId INT PRIMARY KEY,
    CourseName VARCHAR(100) NOT NULL,
    TeacherId INT FOREIGN KEY REFERENCES Users(UserId),
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    SyllabusId INT FOREIGN KEY REFERENCES Syllabus(SyllabusId)
);

CREATE TABLE Assignments (
    AssignmentId INT PRIMARY KEY,
    CourseId INT NOT NULL FOREIGN KEY REFERENCES Courses(CourseId),
    AssignmentTitle VARCHAR(128) NOT NULL,
    Description TEXT NULL,
    Weight FLOAT NOT NULL,
    MaxGrade INT NOT NULL,
    DueDate DATE NOT NULL
);

CREATE TABLE Comments (
    CommentId INT PRIMARY KEY,
    AssignmentId INT NOT NULL FOREIGN KEY REFERENCES Assignments(AssignmentId),
    CreatedByUserId INT NOT NULL FOREIGN KEY REFERENCES Users(UserId),
    CreatedDate DATETIME NOT NULL,
    CommentContent TEXT NULL
);
CREATE TABLE Grades (
    GradeId INT PRIMARY KEY,
    AssignmentId INT NOT NULL FOREIGN KEY REFERENCES Assignments(AssignmentId),
    StudentId INT NOT NULL FOREIGN KEY REFERENCES Users(UserId),
    Grade INT NULL
);

CREATE TABLE Syllabus (
    SyllabusId INT PRIMARY KEY,
    Description TEXT NULL
);

-- Teachers
INSERT INTO Users (UserId,UserName,FirstName,LastName,EmailAddress,PhoneNumber,Role) VALUES
(1,'teacher.sami','Sami','Hijazi','sami.hijazi201@uni.com','+905307778899','Teacher'),
(2,'teacher.feryal','Feryal','Tulaimat','feryal.tulaimat202@uni.com','+905308889900','Teacher');

insert into Users (UserName,FirstName,LastName,EmailAddress,PhoneNumber,Role)
values 
('Ismaeel-Moussa','Ismaeel','Moussa','ismaeel.moussa1@gmail.com','+905346917747','Student'),
('Abdulsalam-Fateh','Abdulsalam','Fateh','fatehabdalsalam@gmail.com','+963958361088','Student'),
('Ahmad-Thaer-Ater','Ahmad','Ater','aeter520@gmail.com','+963946946565','Student'),
('Ihap-Abuwarda','Ihap','Abuwarda','ihababuwardah@gmail.com','+905355807082','Student'),
('Muhammed-Elrimi','Muhammed','Elrimi','mohamadrimi12345@gmail.com','+905343346036','Student'),
('Wasem-Alhariri','Wasem','Alhariri','wasemalhariri13@gmail.com','+963994801706','Student')


INSERT INTO Comments (CommentId, AssignmentId, CreatedByUserId, CreatedDate, CommentContent)
VALUES 
(1, 1, 3, '2026-02-16', 'First task done. Queries are working fine.'),
(2, 1, 3, '2026-02-17', 'Found a better way to join tables here.'),
(3, 3, 3, '2026-03-20', 'Loop logic implemented without built-in functions.'), --
(4, 4, 3, '2026-05-01', 'Database updated. Migrations look synced.'),
(5, 5, 3, '2026-07-15', 'State management is clear. Moving to next part.'),
(6, 4, 3, '2026-05-10', 'Endpoint tested. Returning JSON as expected.'),
(7, 1, 3, '2026-02-18', 'Aggregates added. Grouping logic is ready.'),
(8, 3, 3, '2026-03-25', 'Class structure follows the requirements.'),
(9, 5, 3, '2026-07-20', 'Component hierarchy is clean and organized.'),
(10, 2, 3, '2026-03-05', 'Solution uploaded. Ready for review.');


INSERT INTO Syllabus VALUES (101, 'SQL Fundamentals'), (102, 'C# Programming'), (103, 'Entity Framework'), (104, 'Web API'), (105, 'React JS');

INSERT INTO Courses (CourseName, TeacherId, StartDate, EndDate, SyllabusId) 
VALUES 
('SQL', 1, '2026-02-09', '2026-03-15', 101),
('C#', 1, '2026-03-16', '2026-04-20', 102),
('Entity Framework', 2, '2026-04-21', '2026-05-25', 103),
('Web API', 2, '2026-05-26', '2026-07-01', 104),
('React', 1, '2026-07-02', '2026-08-10', 105);
INSERT INTO Assignments (CourseId, AssignmentTitle, Weight, MaxGrade, DueDate)
VALUES 
(3, 'DB Context Setup', 20, 100, '2026-04-25'),
(3, 'Migrations', 20, 100, '2026-05-05'),
(3, 'LINQ Queries', 20, 100, '2026-05-15'),
(3, 'Relationships Mapping', 20, 100, '2026-05-25'),
(3, 'EF Performance', 20, 100, '2026-06-05');

SELECT * FROM Courses;

SELECT * FROM Assignments WHERE CourseId = 1;

SELECT * FROM Users WHERE Role = 'Student'; 

UPDATE Users SET Role = 'Graduated' WHERE UserId = 1;

DELETE FROM Comments WHERE CommentId = 10;
-- List of students and their grades in a specific course
SELECT U.FirstName, U.LastName, G.Grade, A.AssignmentTitle 
FROM Users U
JOIN Grades G ON U.UserId = G.StudentId
JOIN Assignments A ON G.AssignmentId = A.AssignmentId
WHERE A.CourseId = 2;

-- Average grades per course
SELECT C.CourseName, AVG(G.Grade) as AverageGrade
FROM Courses C
JOIN Assignments A ON C.CourseId = A.CourseId
JOIN Grades G ON A.AssignmentId = G.AssignmentId
GROUP BY C.CourseName;

CREATE PROCEDURE AddStudent 
    @Id INT, @User VARCHAR(64), @First VARCHAR(64), @Last VARCHAR(64), @Email VARCHAR(128), @Phone VARCHAR(16)
AS
BEGIN
    INSERT INTO Users VALUES (@Id, @User, @First, @Last, @Email, @Phone, 'Student');
END;


CREATE FUNCTION GetLetterGrade (@Grade INT)
RETURNS CHAR(1)
AS
BEGIN
    RETURN (CASE 
        WHEN @Grade >= 90 THEN 'A'
        WHEN @Grade >= 80 THEN 'B'
        WHEN @Grade >= 70 THEN 'C'
        WHEN @Grade >= 60 THEN 'D'
        ELSE 'F' END);
END;
EXEC AddStudent
  @Id = 300,
  @User = 'intern.muhammed',
  @First = 'Muhammed',
  @Last = 'Elrimi',
  @Email = 'muhammed400@uni.com',
  @Phone = '+905896988577';
